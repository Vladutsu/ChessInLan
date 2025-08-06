using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Classes
{
    public class Knight : Piece
    {
        public Knight(Point position, PlayerColor color) : base(position, color) { }
        private Point[] Directions =
            {
                new Point(2, 1),
                new Point(2, -1),
                new Point(-2, 1),
                new Point(-2, -1),
                new Point(1, 2),
                new Point(1, -2),
                new Point(-1, 2),
                new Point(-1, -2)
            };

        public override List<Point> GetValidMoves(Board board)
        {
            var validMoves = new List<Point>();

            foreach (var direction in Directions)
            {
                Point jump = new Point(Position.X + direction.X, Position.Y + direction.Y);
                if (board.IsInBounds(jump) && (board.IsSquareEmpty(jump) || board.IsOpponentSquare(jump, Color)))
                {
                    validMoves.Add(jump);
                }
            }

            return validMoves;
        }
    }
}
