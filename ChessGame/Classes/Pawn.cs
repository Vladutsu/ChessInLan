using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Classes
{
    public class Pawn : Piece
    {
        public Pawn(Point position, PlayerColor color) : base(position, color) { }
        private int Direction => Color == PlayerColor.White ? -1 : 1;

        public override List<Point> GetValidMoves(Board board)
        {
            var validMoves = new List<Point>();

            AddForwardMoves(board, validMoves);
            AddCaptureMoves(board, validMoves);

            return validMoves;
        }

        private void AddCaptureMoves(Board board, List<Point> validMoves)
        {
            Point captureLeft = new Point(Position.X - 1, Position.Y + Direction);
            Point captureRight = new Point(Position.X + 1, Position.Y + Direction);

            if (board.IsInBounds(captureLeft) && board.IsOpponentSquare(captureLeft, Color))
            {
                validMoves.Add(captureLeft);
            }
            if (board.IsInBounds(captureRight) && board.IsOpponentSquare(captureRight, Color))
            {
                validMoves.Add(captureRight);
            }
        }

        private void AddForwardMoves(Board board, List<Point> validMoves)
        {
            Point forward = new Point(Position.X, Position.Y + Direction);
            if (board.IsInBounds(forward) && board.IsSquareEmpty(forward))
            {
                validMoves.Add(forward);

                if (FirstMove)
                {
                    Point doubleForward = new Point(Position.X, Position.Y + (2 * Direction));
                    if (board.IsSquareEmpty(doubleForward))
                    {
                        validMoves.Add(doubleForward);
                    }
                }
            }
        }
    }
}
