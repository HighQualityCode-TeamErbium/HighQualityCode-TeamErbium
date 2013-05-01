namespace KingSurvival.Common
{
    using System;

    public class ConsoleEngine : IEngine
    {
        private const int BoardRows = 8;
        private const int BoardColumns = 8;

        private Board board;

        public ConsoleEngine()
        {
            this.board = new Board(BoardRows, BoardColumns);
        }

        private void DisplayCurrentEndOnConsole(int turn, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn pawnKing)
        {
            if (true) // isKingWinner
            {
                Console.Clear();
                Console.WriteLine(this.board.GetImage(pawnA, pawnB, pawnC, pawnD, pawnKing));
                Console.WriteLine("King wins in {0} turns.", turn / 2);
            }
            else
            {
                Console.Clear();
                this.board.GetImage(pawnA, pawnB, pawnC, pawnD, pawnKing);
                Console.WriteLine("King loses.");
            }
        }

        public void Run()
        {
            Pawn pawnA = new Pawn('A', 0, 0);
            Pawn pawnB = new Pawn('B', 0, 2);
            Pawn pawnC = new Pawn('C', 0, 4);
            Pawn pawnD = new Pawn('D', 0, 6);
            Pawn pawnKing = new Pawn('K', 7, 3);

            bool endOfGame = false;
            int currentMove = 1;
            do
            {
                bool isValidMove;
                do
                {
                    Console.Clear();
                    Console.WriteLine(this.board.GetImage(pawnA, pawnB, pawnC, pawnD, pawnKing));
                    isValidMove = GameManager.isMoveLeft(currentMove, pawnA, pawnB, pawnC, pawnD, pawnKing);
                } while (!isValidMove);

                endOfGame = GameManager.HasGameEnded(currentMove, pawnA, pawnB, pawnC, pawnD, pawnKing);
                currentMove++;
            } while (!endOfGame);
        }
    }
}
