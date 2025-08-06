using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Classes
{
    public class Rook : Piece
    {
        public Rook(Point position, PlayerColor color) : base(position, color) { }
        private Point[] Directions =
            {
                new Point(-1, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, -1)
            };

        public override List<Point> GetValidMoves(Board board)
        {
            var validMoves = new List<Point>();

            foreach ( var direction in Directions)
            {
                Point currentPosition = Position;

                while(true)
                {
                    currentPosition = new Point(currentPosition.X + direction.X, currentPosition.Y + direction.Y);
                    if (!board.IsInBounds(currentPosition))
                    {
                        break;
                    }

                    if (!board.IsSquareEmpty(currentPosition))
                    {
                        if (board.IsOpponentSquare(currentPosition, Color))
                        {
                            validMoves.Add(currentPosition);
                        }

                        break;
                    }

                    validMoves.Add(currentPosition);
                }
            }

            return validMoves;
        }
    }
}
