using System.Drawing;

namespace ChessGame.Classes
{
    public class Board
    {
        private Piece[,] Grid = new Piece[8, 8];

        public Board()
        {
            IntializeBoard();
        }

        public void IntializeBoard()
        {
            InitializePawns();
            InitializeRooks();
            InitializeKnights();
            InitializeBishops();
            InitializeQueens();
            InitializeKings();
        }

        private void InitializeKings()
        {
            Grid[4, 0] = new King(new Point(4, 0), PlayerColor.Black);
            Grid[4, 7] = new King(new Point(4, 7), PlayerColor.White);
        }

        private void InitializeQueens()
        {
            Grid[3, 0] = new Queen(new Point(3, 0), PlayerColor.Black);
            Grid[3, 7] = new Queen(new Point(3, 7), PlayerColor.White);
        }

        private void InitializeBishops()
        {
            Grid[2, 0] = new Bishop(new Point(2, 0), PlayerColor.Black);
            Grid[5, 0] = new Bishop(new Point(5, 0), PlayerColor.Black);
            Grid[2, 7] = new Bishop(new Point(2, 7), PlayerColor.White);
            Grid[5, 7] = new Bishop(new Point(5, 7), PlayerColor.White);
        }

        private void InitializeKnights()
        {
            Grid[1, 0] = new Knight(new Point(1, 0), PlayerColor.Black);
            Grid[6, 0] = new Knight(new Point(6, 0), PlayerColor.Black);
            Grid[1, 7] = new Knight(new Point(1, 7), PlayerColor.White);
            Grid[6, 7] = new Knight(new Point(6, 7), PlayerColor.White);
        }

        private void InitializeRooks()
        {
            Grid[0, 0] = new Rook(new Point(0, 0), PlayerColor.Black);
            Grid[7, 0] = new Rook(new Point(7, 0), PlayerColor.Black);
            Grid[0, 7] = new Rook(new Point(0, 7), PlayerColor.White);
            Grid[7, 7] = new Rook(new Point(7, 7), PlayerColor.White);
        }

        private void InitializePawns()
        {
            for (int i = 0; i < 8; i++)
            {
                Grid[i, 1] = new Pawn(new Point(i, 1), PlayerColor.Black);
                Grid[i, 6] = new Pawn(new Point(i, 6), PlayerColor.White);
            }
        }

        public Piece GetPieceAtSquare(Point position)
        {
            return Grid[position.X, position.Y];
        }

        public bool IsSquareEmpty(Point position)
        {
            return GetPieceAtSquare(position) == null;
        }

        public bool IsOpponentSquare(Point position, PlayerColor currentPlayerColor)
        {
            Piece piece = GetPieceAtSquare(position);
            return piece != null && piece.GetColor() != currentPlayerColor;
        }

        public bool IsInBounds(Point position)
        {
            return position.X >= 0 && position.X < 8 && position.Y >= 0 && position.Y < 8;
        }

        public void MovePiece(Point from, Point to)
        {
            Piece piece = GetPieceAtSquare(from);
            Grid[to.X, to.Y] = piece;
            Grid[from.X, from.Y] = null;
            piece.MoveTo(to);
        }
    }
}
