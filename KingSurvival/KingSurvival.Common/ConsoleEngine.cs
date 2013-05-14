namespace KingSurvival.Common
{
    using System;

    public class ConsoleEngine : IEngine
    {
        private const int KingInitialRow = 7;
        private const int KingInitialColumn = 3;
        private const int PawnAInitialRow = 0;
        private const int PawnAInitialColumn = 0;
        private const int PawnBInitialRow = 0;
        private const int PawnBInitialColumn = 2;
        private const int PawnCInitialRow = 0;
        private const int PawnCInitialColumn = 4;
        private const int PawnDInitialRow = 0;
        private const int PawnDInitialColumn = 6;        
        private const int BoardRows = 8;
        private const int BoardColumns = 8;
        private bool isKingWinner = false;

        private static readonly int BoardMaxRow = BoardRows - 1;
        private static readonly int BoardMaxColumn = BoardColumns - 1;
        private static readonly MatrixCoordinates UpLeftDirection = new MatrixCoordinates(-1, -1);
        private static readonly MatrixCoordinates UpRightDirection = new MatrixCoordinates(-1, 1);
        private static readonly MatrixCoordinates DownLeftDirection = new MatrixCoordinates(1, -1);
        private static readonly MatrixCoordinates DownRightDirection = new MatrixCoordinates(1, 1);
        
        private readonly Board board;

        public ConsoleEngine()
        {
            this.board = new Board(BoardRows, BoardColumns);
        }

        private bool HasGameEnded(int gameTurn, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isKingOnTurn = false;
            
            isKingOnTurn = (gameTurn % 2 == 1);
           
            if (isKingOnTurn && king.Coordinates.Row == 0)
            {
                return true;
            }
            else
            {
                if (!CanKingMove(king, pawnA, pawnB, pawnC, pawnD) ||
                    !CanAtLeastOnePawnMove(king, pawnA, pawnB, pawnC, pawnD))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool HasKingWon(int gameTurn, bool gameCondition, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isGameEnded = gameCondition;
            bool isKingOnTurn = (gameTurn % 2 == 1);
            
            if (isGameEnded)
            {
                if (isKingOnTurn && king.Coordinates.Row == 0)
                {
                    return true;
                }
                else
                {
                    if (!CanKingMove(king, pawnA, pawnB, pawnC, pawnD))
                    {
                        return false;
                    }
                    else if (!CanAtLeastOnePawnMove(king, pawnA, pawnB, pawnC, pawnD))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        private bool CanKingMove(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            // determine king restrictions
            bool canKingGoUpLeft = IsKingUpLeftMovementAvailable(king, pawnA, pawnB, pawnC, pawnD);
            bool canKingGoDownLeft = IsKingDownLeftMovementAvailable(king, pawnA, pawnB, pawnC, pawnD);
            bool canKingGoUpRight = IsKingUpRightMovementAvailable(king, pawnA, pawnB, pawnC, pawnD);
            bool canKingGoDownRight = IsKingDownRightMovementAvailable(king, pawnA, pawnB, pawnC, pawnD);
            // check if all 
            bool isAnyOfKingMovesAvaiable = canKingGoDownRight || canKingGoDownLeft || canKingGoUpLeft || canKingGoUpRight;

            return isAnyOfKingMovesAvaiable;
        }

        private bool IsKingUpLeftMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            // check if king is near border
            if (king.Coordinates.Row == 0 || king.Coordinates.Column == 0)
            {
                return false;
            }
            // check if pawn is near king
            MatrixCoordinates newKingCoordinates = king.Coordinates + UpLeftDirection;
            bool canKingGoUpLeft = 
                IsAvailableNextPosition(newKingCoordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
            return canKingGoUpLeft;
        }

        private bool IsKingDownLeftMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            // check if king is near border
            if (king.Coordinates.Row == BoardMaxRow || king.Coordinates.Column == 0)
            {
                return false;
            }
            // check if pawn is near king
            MatrixCoordinates newKingCoordinates = king.Coordinates + DownLeftDirection;
            bool canKingGoDownLeft = 
                IsAvailableNextPosition(newKingCoordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
            return canKingGoDownLeft;
        }

        private bool IsKingUpRightMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            // check if king is near border
            if (king.Coordinates.Row == 0 || king.Coordinates.Column == BoardMaxColumn)
            {
                return false;
            }
            // check if pawn is near king
            MatrixCoordinates newKingCoordinates = king.Coordinates + UpRightDirection;
            bool canKingGoUpRight =
                IsAvailableNextPosition(newKingCoordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
            return canKingGoUpRight;
        }

        private bool IsKingDownRightMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            // check if king is near border
            if (king.Coordinates.Row == BoardMaxRow || king.Coordinates.Column == BoardMaxColumn)
            {
                return false;
            }
            // check if pawn is near king
            MatrixCoordinates newKingCoordinates = king.Coordinates + DownRightDirection;
            bool canKingGoDownRight =
                IsAvailableNextPosition(newKingCoordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);

            return canKingGoDownRight;
        }

        private bool CanAtLeastOnePawnMove(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canPawnAMove = 
                CanCurrentPawnMove(pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates, king.Coordinates);
            bool canPawnBMove =
                CanCurrentPawnMove(pawnB.Coordinates, pawnA.Coordinates, pawnC.Coordinates, pawnD.Coordinates, king.Coordinates);
            bool canPawnCMove = 
                CanCurrentPawnMove(pawnC.Coordinates, pawnA.Coordinates, pawnB.Coordinates, pawnD.Coordinates, king.Coordinates);
            bool canPawnDMove = 
                CanCurrentPawnMove(pawnD.Coordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, king.Coordinates);

            bool canAtLeastOnePawnMove = canPawnAMove || canPawnBMove || canPawnCMove || canPawnDMove;

            return canAtLeastOnePawnMove;
        }

        private bool CanCurrentPawnMove(MatrixCoordinates currentPawnCoordinates, params MatrixCoordinates[] obstaclesCoordinates)
        {
            if (currentPawnCoordinates.Row == BoardMaxRow)
            {
                return false;
            }
            else if (currentPawnCoordinates.Column > 0 && currentPawnCoordinates.Column < BoardMaxColumn)
            {
                MatrixCoordinates newCoordinatesDownRight = currentPawnCoordinates + DownRightDirection;
                MatrixCoordinates newCoordinatesDownLeft = currentPawnCoordinates + DownLeftDirection;

                if (!IsAvailableNextPosition(newCoordinatesDownRight, obstaclesCoordinates) &&
                    !IsAvailableNextPosition(newCoordinatesDownLeft, obstaclesCoordinates))
                {
                    return false;
                }
            }
            else if (currentPawnCoordinates.Row == 0)
            {
                MatrixCoordinates newCoordinates = currentPawnCoordinates + DownRightDirection;

                if (!IsAvailableNextPosition(newCoordinates, obstaclesCoordinates))
                {
                    return false;
                }
            }
            else if (currentPawnCoordinates.Column == BoardMaxColumn)
            {
                MatrixCoordinates newCoordinates = currentPawnCoordinates + DownLeftDirection;
                
                if (!IsAvailableNextPosition(newCoordinates, obstaclesCoordinates))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidMove(int turn, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isValid;
            string command;
            if (turn % 2 == 1)
            {
                Console.Write("King's turn: ");
                command = Console.ReadLine().ToLower();
                isValid = IsValidKingMove(command, king, pawnA, pawnB, pawnC, pawnD);
            }
            else
            {
                Console.Write("Pawn's turn: ");
                command = Console.ReadLine().ToLower();
                isValid = IsValidPawnMove(command, king, pawnA, pawnB, pawnC, pawnD);
            }

            return isValid;
        }

        private bool HandleDownLeftPawnMove(Pawn pawn, params MatrixCoordinates[] otherPawnsCoordinates)
        {
            MatrixCoordinates newCoordinates = pawn.Coordinates + DownLeftDirection;

            if (pawn.Coordinates.Row < BoardMaxRow && pawn.Coordinates.Column > 0 &&
                IsAvailableNextPosition(newCoordinates, otherPawnsCoordinates))
            {
                pawn.Coordinates = newCoordinates;
                return true;
            }
            else
            {
                Console.Write("Invalid move!");
                Console.ReadKey();
                return false;
            }
        }

        private bool HandleDownRightPawnMove(Pawn pawn, params MatrixCoordinates[] otherPawnsCoordinates)
        {
            MatrixCoordinates newCoordinates = pawn.Coordinates + DownRightDirection;

            if (pawn.Coordinates.Row < BoardMaxRow && pawn.Coordinates.Column < BoardMaxColumn && 
                IsAvailableNextPosition(newCoordinates, otherPawnsCoordinates))
            {
                pawn.Coordinates = newCoordinates;
                return true;
            }
            else
            {
                Console.Write("Invalid move!");
                Console.ReadKey();
                return false;
            }
        }

        private bool IsValidPawnMove(string command, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            switch (command)
            {
                case "adl":
                    return HandleDownLeftPawnMove(pawnA, king.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "adr":
                    return HandleDownRightPawnMove(pawnA, king.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "bdl":
                    return HandleDownLeftPawnMove(pawnB, king.Coordinates, pawnA.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "bdr":
                    return HandleDownRightPawnMove(pawnB, king.Coordinates, pawnA.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "cdl":
                    return HandleDownLeftPawnMove(pawnC, king.Coordinates, pawnA.Coordinates, pawnB.Coordinates, pawnD.Coordinates);
                case "cdr":
                    return HandleDownRightPawnMove(pawnC, king.Coordinates, pawnA.Coordinates, pawnB.Coordinates, pawnD.Coordinates);
                case "ddl":
                    return HandleDownLeftPawnMove(pawnD, king.Coordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates);
                case "ddr":
                    return HandleDownRightPawnMove(pawnD, king.Coordinates, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates);
                default:
                    {
                        Console.Write("Invalid move!");
                        Console.ReadKey();
                        return false;
                    }
            }
        }

        private bool HandleUpLeftKingMove(King king, params MatrixCoordinates[] otherPawnsCoordinates)
        {
            MatrixCoordinates newCoordinates = king.Coordinates + UpLeftDirection;

            if (king.Coordinates.Row > 0 && king.Coordinates.Column > 0 &&
                IsAvailableNextPosition(newCoordinates, otherPawnsCoordinates))
            {
                king.Coordinates = newCoordinates;
                return true;
            }
            else
            {
                Console.Write("Invalid move!");
                Console.ReadKey();
                return false;
            }
        }

        private bool HandleUpRightKingMove(King king, params MatrixCoordinates[] otherPawnsCoordinates)
        {
            MatrixCoordinates newCoordinates = king.Coordinates + UpRightDirection;

            if (king.Coordinates.Row > 0 && king.Coordinates.Column < BoardMaxColumn &&
                IsAvailableNextPosition(newCoordinates, otherPawnsCoordinates))
            {
                king.Coordinates = newCoordinates;
                return true;
            }
            else
            {
                Console.Write("Invalid move!");
                Console.ReadKey();
                return false;
            }
        }

        private bool IsValidKingMove(string command, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            switch (command)
            {
                case "kul":
                    return HandleUpLeftKingMove(king, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "kur":
                    return HandleUpRightKingMove(king, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "kdl":
                    return HandleDownLeftPawnMove(king, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                case "kdr":
                    return HandleDownRightPawnMove(king, pawnA.Coordinates, pawnB.Coordinates, pawnC.Coordinates, pawnD.Coordinates);
                default:
                    {
                        Console.Write("Invalid move!");
                        Console.ReadKey();
                        return false;
                    }
            }
        }

        private bool IsAvailableNextPosition(MatrixCoordinates newCoordinates, params MatrixCoordinates[] pawnsCoordinates)
        {
            foreach (MatrixCoordinates coordinates in pawnsCoordinates)
            {
                if (newCoordinates == coordinates)
                {
                    return false;
                }
            }

            return true;
        }

        private void DisplayCurrentEndOnConsole(int turn, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            if (isKingWinner)
            {
                Console.Clear();
                Console.WriteLine(this.board.GetImage(king, pawnA, pawnB, pawnC, pawnD));
                Console.WriteLine("King wins in {0} turns!", turn / 2);
            }
            else
            {
                Console.Clear();
                Console.WriteLine(this.board.GetImage(king, pawnA, pawnB, pawnC, pawnD));
                Console.WriteLine("King loses in {0} turns...", turn / 2);
            }
        }

        public void Run()
        {
            MatrixCoordinates kingCoordinates = new MatrixCoordinates(KingInitialRow, KingInitialColumn);
            King king = new King(kingCoordinates);

            MatrixCoordinates pawnACoordinates = new MatrixCoordinates(PawnAInitialRow, PawnAInitialColumn);
            Pawn pawnA = new Pawn('A', pawnACoordinates);

            MatrixCoordinates pawnBCoordinates = new MatrixCoordinates(PawnBInitialRow, PawnBInitialColumn);
            Pawn pawnB = new Pawn('B', pawnBCoordinates);

            MatrixCoordinates pawnCCoordinates = new MatrixCoordinates(PawnCInitialRow, PawnCInitialColumn);
            Pawn pawnC = new Pawn('C', pawnCCoordinates);

            MatrixCoordinates pawnDCoordinates = new MatrixCoordinates(PawnDInitialRow, PawnDInitialColumn);
            Pawn pawnD = new Pawn('D', pawnDCoordinates);
            
            bool endOfGame = false;
            int currentMove = 1;
            do
            {
                bool isValidMove;
                do
                {
                    Console.Clear();
                    Console.WriteLine(this.board.GetImage(king, pawnA, pawnB, pawnC, pawnD));
                    isValidMove = IsValidMove(currentMove, king, pawnA, pawnB, pawnC, pawnD);
                } 
                while (!isValidMove);

                endOfGame = HasGameEnded(currentMove, king, pawnA, pawnB, pawnC, pawnD);
                isKingWinner = HasKingWon(currentMove, endOfGame, king, pawnA, pawnB, pawnC, pawnD);
                currentMove++;
            } 
            while (!endOfGame);

            if (endOfGame)
            {
                DisplayCurrentEndOnConsole(currentMove, king, pawnA, pawnB, pawnC, pawnD);
            }
        }
    }
}
