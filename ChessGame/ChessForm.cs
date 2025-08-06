using ChessGame.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessForm : Form
    {
        private Game Game;
        private Panel PanelBoard;
        private PictureBox[,] PiecePictureBoxes = new PictureBox[8, 8];

        private TcpListener Server;
        private TcpClient Client;
        private NetworkStream Stream;

        public ChessForm(bool isHost, string serverIp)
        {
            InitializeComponent();

            if (isHost)
            {
                StarServer();
            }
            else
            {
                ConnectToServer(serverIp);
            }
        }

        #region Server&Client
        private void StarServer()
        {
            Server = new TcpListener(IPAddress.Any, 5000);
            Server.Start();
            MessageBox.Show("Waiting for opponent");

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Client = Server.AcceptTcpClient();
                Stream = Client.GetStream();
                MessageBox.Show("Opponent connected");

                Invoke(new Action(() => InitializeGame(true)));
            });
        }

        private void ConnectToServer(string serverIp)
        {
            try
            {
                Client = new TcpClient(serverIp, 5000);
                Stream = Client.GetStream();
                MessageBox.Show("Connected to the server");

                InitializeGame(false);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }
        }

        private void ListenForMoves()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[1_024];
                        int bytesRead = Stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string move = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            string[] moveParts = move.Split(':');
                            string[] fromParts = moveParts[0].Split(',');
                            string[] toParts = moveParts[1].Split(',');

                            Point from = new Point(int.Parse(fromParts[0]), int.Parse(fromParts[1]));
                            Point to = new Point(int.Parse(toParts[0]), int.Parse(toParts[1]));

                            Invoke(new Action(() =>
                            {
                                Piece piece = Game.GetPieceAtSquare(to);

                                Game.MovePiece(from, to);

                                PictureBox fromSquare = PiecePictureBoxes[from.X, from.Y];
                                PictureBox toSquare = PiecePictureBoxes[to.X, to.Y];

                                toSquare.Image = fromSquare.Image;
                                fromSquare.Image = null;

                                if (piece is King)
                                {
                                    MessageBox.Show("You lost...");
                                    CloseGame();
                                }

                                Game.SwitchTurn();
                                EnableSquares();
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
        #endregion Server&Client

        #region Initialization
        private void InitializeGame(bool isHost)
        {
            Game = new Game();
            InitializeBoardPanel();
            InitializePieceImages();

            if (isHost)
            {
                EnableSquares();
            }
            else
            {
                ListenForMoves();
            }
        }

        private void InitializeBoardPanel()
        {
            PanelBoard = new Panel
            {
                BackgroundImage = Properties.Resources.BoardImage,
                BackgroundImageLayout = ImageLayout.Stretch,
                Anchor = AnchorStyles.None
            };

            AdjustBoardSize();

            this.Controls.Add(PanelBoard);

            int squareSize = PanelBoard.Width / 8;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox piecePictureBox = new PictureBox
                    {
                        Size = new Size(squareSize, squareSize),
                        Location = new Point(i * squareSize, j * squareSize),
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = new Point(i, j),
                        Enabled = false
                    };

                    piecePictureBox.MouseClick += Piece_MouseClick;

                    PanelBoard.Controls.Add(piecePictureBox);
                    PiecePictureBoxes[i, j] = piecePictureBox;
                }
            }
        }

        private Image GetPieceImage(Piece piece)
        {
            PlayerColor color = piece.GetColor();

            if (piece is Pawn)
                return color == PlayerColor.White ? Properties.Resources.Pawn_White : Properties.Resources.Pawn_Black;
            if (piece is Rook)
                return color == PlayerColor.White ? Properties.Resources.Rook_White : Properties.Resources.Rook_Black;
            if (piece is Knight)
                return color == PlayerColor.White ? Properties.Resources.Knight_White : Properties.Resources.Knight_Black;
            if (piece is Bishop)
                return color == PlayerColor.White ? Properties.Resources.Bishop_White : Properties.Resources.Bishop_Black;
            if (piece is Queen)
                return color == PlayerColor.White ? Properties.Resources.Queen_White : Properties.Resources.Queen_Black;
            if (piece is King)
                return color == PlayerColor.White ? Properties.Resources.King_White : Properties.Resources.King_Black;

            return null;
        }

        private void InitializePieceImages()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = Game.GetPieceAtSquare(new Point(i, j));
                    PictureBox piecePictureBox = PiecePictureBoxes[i, j];

                    if (piece != null)
                    {
                        piecePictureBox.Image = GetPieceImage(piece);
                    }
                    else
                    {
                        piecePictureBox.Image = null;
                    }
                }
            }
        }
        #endregion Initialization

        #region DynamicWindow
        private void AdjustBoardSize()
        {
            if (PanelBoard == null)
            {
                return;
            }

            int size = Math.Min(this.ClientSize.Width, this.ClientSize.Height);
            PanelBoard.Size = new Size(size, size);
            PanelBoard.Location = new Point((this.ClientSize.Width - size) / 2, (this.ClientSize.Height - size) / 2);

            AdjustPieceSize();
        }

        private void AdjustPieceSize()
        {
            int squareSize = PanelBoard.Size.Width / 8;
            foreach (Control control in PanelBoard.Controls)
            {
                if (control is PictureBox piece)
                {
                    piece.Size = new Size(squareSize, squareSize);

                    if (piece.Tag is Point position)
                    {
                        piece.Location = new Point(position.X * squareSize, position.Y * squareSize);
                    }
                }
            }
        }
        #endregion DynamicWindow

        #region SquareSettings
        private void EnableSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox square = PiecePictureBoxes[i, j];
                    square.BackColor = Color.Transparent;

                    if (square.Image != null)
                    {
                        Piece piece = Game.GetPieceAtSquare((Point)square.Tag);
                        List<Point> validMoves = piece.GetValidMoves(Game.GetBoard());
                        PlayerColor currentPlayer = Game.GetCurrentTurn();
                        
                        if (piece.GetColor() == currentPlayer && validMoves.Count > 0)
                        {
                            square.Enabled = true;
                            square.MouseEnter += Square_MouseEnter;
                            square.MouseLeave += Square_MouseLeave;
                        }
                    }
                }
            }
        }

        private void DisableSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox square = PiecePictureBoxes[i, j];
                    square.BackColor = Color.Transparent;

                    square.Enabled = false;

                    if (square.Image != null)
                    {
                        Piece piece = Game.GetPieceAtSquare((Point)square.Tag);
                        PlayerColor currentPlayer = Game.GetCurrentTurn();

                        if (piece.GetColor() != currentPlayer)
                        {
                            square.MouseEnter -= Square_MouseEnter;
                            square.MouseLeave -= Square_MouseLeave;
                        }
                    }
                }
            }
        }

        private void HighlightValidMoves(Piece piece)
        {
            List<Point> validMoves = piece.GetValidMoves(Game.GetBoard());

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox square = PiecePictureBoxes[i, j];
                    Point squareLocation = new Point(i, j);

                    if (validMoves.Contains(squareLocation))
                    {
                        square.Enabled = true;
                        square.BackColor = Color.FromArgb(128, Color.LightBlue);
                    }
                }
            }
        }
        #endregion SquareSettings

        #region GameState
        private void CloseGame()
        {
            Stream?.Close();
            Client?.Close();
            Server?.Stop();
            Application.Exit();
        }
        #endregion GameState

        #region Events
        private void Square_MouseEnter(object sender, EventArgs e)
        {
            PictureBox square = sender as PictureBox;
            square.BackColor = Color.FromArgb(128, Color.LightBlue);
        }

        private void Square_MouseLeave(object sender, EventArgs e)
        {
            PictureBox square = sender as PictureBox;
            square.BackColor = Color.Transparent;
        }

        private void ChessForm_Resize(object sender, EventArgs e)
        {
            AdjustBoardSize();
        }

        private PictureBox SelectedPiece = null;
        private void Piece_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox clickedSquare)
            {
                if (SelectedPiece == null)
                {
                    if (clickedSquare.Image != null)
                    {
                        SelectedPiece = clickedSquare;
                        clickedSquare.BackColor = Color.FromArgb(128, Color.LightBlue);
                        
                        Piece piece = Game.GetPieceAtSquare((Point)clickedSquare.Tag);
                        DisableSquares();
                        HighlightValidMoves(piece);
                    }
                }
                else
                {
                    Point from = (Point)SelectedPiece.Tag;
                    Point to = (Point)clickedSquare.Tag;

                    Piece piece = Game.GetPieceAtSquare(to);

                    Game.MovePiece(from, to);

                    clickedSquare.Image = SelectedPiece.Image;                    

                    SelectedPiece.Image = null;
                    SelectedPiece.MouseEnter -= Square_MouseEnter;
                    SelectedPiece.MouseLeave -= Square_MouseLeave;
                    SelectedPiece = null;

                    string move = $"{from.X},{from.Y}:{to.X},{to.Y}";
                    byte[] data = Encoding.UTF8.GetBytes(move);
                    Stream.Write(data, 0, data.Length);

                    if (piece is King)
                    {
                        MessageBox.Show("You won!");
                        CloseGame();
                    }

                    Game.SwitchTurn();
                    DisableSquares();
                    ListenForMoves();
                }
            }
        }

        private void ChessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseGame();
        }
        #endregion Events
    }
}
