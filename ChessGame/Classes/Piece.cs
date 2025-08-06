using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Classes
{
    public abstract class Piece
    {
        protected Point Position { get; set; }
        protected PlayerColor Color { get; set; }
        protected bool FirstMove = true;

        protected Piece(Point position, PlayerColor color)
        {
            Position = position;
            Color = color;
        }

        public abstract List<Point> GetValidMoves(Board board);

        public void MoveTo(Point newPosition)
        {
            Position = newPosition;
            FirstMove = false;
        }

        public PlayerColor GetColor()
        {
            return Color;
        }
    }
}
