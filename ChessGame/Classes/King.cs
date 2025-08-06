using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Classes
{
    public class King : Piece
    {
        public King(Point position, PlayerColor color) : base(position, color) { }
        private Point[] Directions =
            {
                new Point(-1, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, -1),
                new Point(-1, 1),
                new Point(1, 1),
                new Point(1, -1),
                new Point(-1, -1)
            };

        public override List<Point> GetValidMoves(Board board)
        {
            var validMoves = new List<Point>();

            foreach (var direction in Directions)
            {
                Point move = new Point(Position.X + direction.X, Position.Y + direction.Y);
                if (board.IsInBounds(move) && (board.IsSquareEmpty(move) || board.IsOpponentSquare(move, Color)))
                {
                    validMoves.Add(move);
                }
            }

            return validMoves;
        }
    }
}
