using System.Drawing;

namespace ChessGame.Classes
{
    public class Game
    {
        private Board Board { get; set; }
        private PlayerColor CurrentTurn {  get; set; }

        public Game()
        {
            Board = new Board();
            CurrentTurn  = PlayerColor.White;
        }

        public Piece GetPieceAtSquare(Point position)
        {
            return Board.GetPieceAtSquare(position);
        }

        public Board GetBoard()
        {
            return Board;
        }

        public void MovePiece(Point from, Point to)
        {
            Board.MovePiece(from, to);
        }

        public PlayerColor GetCurrentTurn()
        {
            return CurrentTurn;
        }

        public void SwitchTurn()
        {
            CurrentTurn = CurrentTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }
    }
}
