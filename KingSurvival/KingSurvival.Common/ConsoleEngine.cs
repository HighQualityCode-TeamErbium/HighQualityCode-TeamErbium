namespace KingSurvival.Common
{
    using System;

    public class ConsoleEngine : IEngine
    {
        private const int BoardRows = 8;
        private const int BoardColumns = 8;
        private bool isKingWinner = false;

        private readonly Board board;

        public ConsoleEngine()
        {
            this.board = new Board(BoardRows, BoardColumns);
        }

        private void DisplayCurrentEndOnConsole(int turn, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            if (isKingWinner)
            {
                Console.Clear();
                Console.WriteLine(this.board.GetImage(king, pawnA, pawnB, pawnC, pawnD));
                Console.WriteLine("King wins in {0} turns.", turn / 2);
            }
            else
            {
                Console.Clear();
                this.board.GetImage(pawnA, pawnB, pawnC, pawnD, king);
                Console.WriteLine("King loses.");
            }
        }

        public void Run()
        {
            Pawn pawnA = new Pawn('A', 0, 0);
            Pawn pawnB = new Pawn('B', 0, 2);
            Pawn pawnC = new Pawn('C', 0, 4);
            Pawn pawnD = new Pawn('D', 0, 6);
            King king = new King(7, 3);

            bool endOfGame = false;
            int currentMove = 1;
            do
            {
                bool isValidMove;
                do
                {
                    Console.Clear();
                    Console.WriteLine(this.board.GetImage(king, pawnA, pawnB, pawnC, pawnD));
                    isValidMove = GameManager.IsValidMove(currentMove, king, pawnA, pawnB, pawnC, pawnD);
                } while (!isValidMove);

                endOfGame = GameManager.HasGameEnded(currentMove, king, pawnA, pawnB, pawnC, pawnD);
                isKingWinner = GameManager.HasKingWon(currentMove, endOfGame, king, pawnA, pawnB, pawnC, pawnD);
                currentMove++;
            } while (!endOfGame);

            if (endOfGame)
            {
                DisplayCurrentEndOnConsole(currentMove, king, pawnA, pawnB, pawnC, pawnD);
            }
        }
    }
}
