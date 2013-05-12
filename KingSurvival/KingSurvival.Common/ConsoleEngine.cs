namespace KingSurvival.Common
{
    using System;

    public class ConsoleEngine : IEngine
    {
        private const int BoardRows = 8;
        private const int BoardColumns = 8;
        private bool isKingWinner = false;

        private static readonly int BoardMaxRow = BoardRows - 1;
        private static readonly int BoardMaxColumn = BoardColumns - 1;
        private static readonly int BoardMinRow = BoardRows - BoardRows;
        private static readonly int BoardMinColumn = BoardMaxColumn - BoardColumns;

        private readonly Board board;

        public ConsoleEngine()
        {
            this.board = new Board(BoardRows, BoardColumns);
        }

        public static bool HasGameEnded(int gameTurn, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isKingOnTurn = false;
            // on every odd game t king is on turn
            isKingOnTurn = (gameTurn % 2 == 1);
            // if king is not on top of board game continues
            if (isKingOnTurn && king.XCoordinate == 0)
            {
                return true;
            }
            else
            {
                // king is trapped by pawns -> game ends, king loses
                if (!CanKingMove(king, pawnA, pawnB, pawnC, pawnD) ||
                    (IsKingTrapped(king, pawnA, pawnB, pawnC, pawnD) && IsKingTrapped(king, pawnB, pawnA, pawnC, pawnD) &&
                    IsKingTrapped(king, pawnC, pawnA, pawnB, pawnD) && IsKingTrapped(king, pawnD, pawnB, pawnC, pawnD)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool HasKingWon(int gameTurn, bool gameCondition, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isGameEnded = gameCondition;
            bool isKingOnTurn = (gameTurn % 2 == 1);
            // we can only determine king as winner, after game ending
            if (isGameEnded)
            {
                // if king is on top is winner
                if (isKingOnTurn && king.XCoordinate == BoardMinRow)
                {
                    return true;
                }
                else
                {
                    // if king is trapped -> lost
                    if (!CanKingMove(king, pawnA, pawnB, pawnC, pawnD))
                    {
                        return false;
                    } // while king is not on top and not trapped
                    else if (IsKingTrapped(king, pawnA, pawnB, pawnC, pawnD) && IsKingTrapped(king, pawnB, pawnA, pawnC, pawnD) &&
                    IsKingTrapped(king, pawnC, pawnA, pawnB, pawnD) && IsKingTrapped(king, pawnD, pawnA, pawnB, pawnC))
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

        private static bool CanKingMove(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
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

        private static bool IsKingUpLeftMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoUpLeft = true;
            // check if king is near border
            if (king.XCoordinate == 0 || king.YCoordinate == 0)
            {
                canKingGoUpLeft = false;
            }
            // check if pawn is near king
            canKingGoUpLeft = IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD);

            return canKingGoUpLeft;
        }

        private static bool IsKingDownLeftMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoDownLeft = true;
            // check if king is near border
            if (king.XCoordinate == BoardMaxRow || king.YCoordinate == 0)
            {
                canKingGoDownLeft = false;
            }
            // check if pawn is near king
            canKingGoDownLeft = IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD);

            return canKingGoDownLeft;
        }

        private static bool IsKingUpRightMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoUpRight = true;
            // check if king is near border
            if (king.XCoordinate == 0 || king.YCoordinate == BoardMaxColumn)
            {
                canKingGoUpRight = false;
            }
            // check if pawn is near king
            canKingGoUpRight = IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD);

            return canKingGoUpRight;
        }

        private static bool IsKingDownRightMovementAvailable(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoDownRight = true;
            // check if king is near border
            if (king.XCoordinate == BoardMaxRow || king.YCoordinate == BoardMaxColumn)
            {
                canKingGoDownRight = false;
            }
            // check if pawn is near king
            canKingGoDownRight = IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD);

            return canKingGoDownRight;
        }

        private static bool IsKingTrapped(King king, Pawn pawnOne, Pawn pawnTwo, Pawn pawnThree, Pawn pawnFour)
        {
            if (king.XCoordinate == BoardMaxRow)
            {
                return false;
            }
            else if (king.YCoordinate > 0 && king.YCoordinate < BoardMaxColumn)
            {
                if (IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnOne, pawnTwo, pawnThree, pawnFour) &&
                    IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnOne, pawnTwo, pawnThree, pawnFour))
                {
                    return false;
                }
            }
            else if (king.YCoordinate == 0)
            {
                if (IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnOne, pawnTwo, pawnThree, pawnFour))
                {
                    return false;
                }
            }
            else if (king.YCoordinate == BoardMaxColumn)
            {
                if (IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnOne, pawnTwo, pawnThree, pawnFour))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidMove(int turn, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isValid;
            string command;
            if (turn % 2 == 1)
            {
                Console.Write("King’s turn: ");
                command = Console.ReadLine().ToLower();
                isValid = IsValidKingMove(command, king, pawnA, pawnB, pawnC, pawnD);
            }
            else
            {
                Console.Write("Pawns’ turn: ");
                command = Console.ReadLine().ToLower();
                isValid = IsValidPawnMove(command, king, pawnA, pawnB, pawnC, pawnD);
            }

            return isValid;
        }

        private static bool IsValidPawnMove(string command, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isValid;
            switch (command)
            {
                case "adl":
                    {
                        if (pawnA.XCoordinate < BoardMaxRow && pawnA.YCoordinate > 0 &&
                            IsAvailableNextPosition(pawnA.XCoordinate + 1, pawnA.YCoordinate - 1, king, pawnB, pawnC, pawnD))
                        {
                            pawnA.XCoordinate++;
                            pawnA.YCoordinate--;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "adr":
                    {
                        if (pawnA.XCoordinate < BoardMaxRow && pawnA.YCoordinate < BoardMaxColumn &&
                            IsAvailableNextPosition(pawnA.XCoordinate + 1, pawnA.YCoordinate + 1, king, pawnB, pawnC, pawnD))
                        {
                            pawnA.XCoordinate++;
                            pawnA.YCoordinate++;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "bdl":
                    {
                        if (pawnB.XCoordinate < BoardMaxRow && pawnB.YCoordinate > 0 &&
                            IsAvailableNextPosition(pawnB.XCoordinate + 1, pawnB.YCoordinate - 1, king, pawnA, pawnC, pawnD))
                        {
                            pawnB.XCoordinate++;
                            pawnB.YCoordinate--;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }
                        break;
                    }
                case "bdr":
                    {
                        if (pawnB.XCoordinate < BoardMaxRow && pawnB.YCoordinate < BoardMaxColumn &&
                            IsAvailableNextPosition(pawnB.XCoordinate + 1, pawnB.YCoordinate + 1, king, pawnA, pawnC, pawnD))
                        {
                            pawnB.XCoordinate++;
                            pawnB.YCoordinate++;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "cdl":
                    {
                        if (pawnC.XCoordinate < BoardMaxRow && pawnC.YCoordinate > 0 &&
                            IsAvailableNextPosition(pawnC.XCoordinate + 1, pawnC.YCoordinate + 1, king, pawnA, pawnB, pawnD))
                        {
                            pawnC.XCoordinate++;
                            pawnC.YCoordinate--;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }
                        break;
                    }
                case "cdr":
                    {
                        if (pawnC.XCoordinate < BoardMaxRow && pawnC.YCoordinate < BoardMaxColumn &&
                            IsAvailableNextPosition(pawnC.XCoordinate + 1, pawnC.YCoordinate + 1, king, pawnA, pawnB, pawnD))
                        {
                            pawnC.XCoordinate++;
                            pawnC.YCoordinate++;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "ddl":
                    {
                        if (pawnD.XCoordinate < BoardMaxRow && pawnD.YCoordinate > 0 &&
                            IsAvailableNextPosition(pawnD.XCoordinate + 1, pawnD.YCoordinate - 1, king, pawnA, pawnB, pawnC))
                        {
                            pawnD.XCoordinate++;
                            pawnD.YCoordinate--;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "ddr":
                    {
                        if (pawnD.XCoordinate < BoardMaxRow && pawnD.YCoordinate < BoardMaxColumn &&
                            IsAvailableNextPosition(pawnD.XCoordinate + 1, pawnD.YCoordinate + 1, king, pawnA, pawnB, pawnC))
                        {
                            pawnD.XCoordinate++;
                            pawnD.YCoordinate++;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Invalid move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                default:
                    {
                        Console.Write("Invalid move!");
                        Console.ReadKey();
                        isValid = false;
                    }

                    return isValid;
            }

            return isValid;
        }

        private static bool IsValidKingMove(string command, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isValid = false;
            switch (command)
            {
                case "kul":
                    {
                        if (king.XCoordinate > 0 && king.YCoordinate > 0 && 
                            IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.XCoordinate--;
                            king.YCoordinate--;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "kur":
                    {
                        if (king.XCoordinate > 0 && king.YCoordinate < BoardMaxColumn &&
                            IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.XCoordinate--;
                            king.YCoordinate++;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "kdl":
                    {
                        if (king.XCoordinate < BoardMaxRow && king.YCoordinate > 0 && 
                            IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.XCoordinate++;
                            king.YCoordinate--;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                case "kdr":
                    {
                        if (king.XCoordinate < BoardMaxRow && king.YCoordinate < BoardMaxColumn && 
                            IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.XCoordinate++;
                            king.YCoordinate++;
                            isValid = true;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            isValid = false;
                        }

                        break;
                    }
                default:
                    {
                        Console.Write("Illegal move!");
                        Console.ReadKey();
                        isValid = false;
                    }

                    return isValid;
            }

            return isValid;
        }

        private static bool IsAvailableNextPosition(
            int isAvaliableXCoordinate, int isAvaliableYCoordinate, Pawn pawnOne, Pawn pawnTwo, Pawn pawnThree, Pawn pawnFour)
        {
            bool isAvalable;

            if (isAvaliableXCoordinate == pawnOne.XCoordinate && isAvaliableYCoordinate == pawnOne.YCoordinate)
            {
                isAvalable = false;
            }
            else if (isAvaliableXCoordinate == pawnTwo.XCoordinate && isAvaliableYCoordinate == pawnTwo.YCoordinate)
            {
                isAvalable = false;
            }
            else if (isAvaliableXCoordinate == pawnThree.XCoordinate && isAvaliableYCoordinate == pawnThree.YCoordinate)
            {
                isAvalable = false;
            }
            else if (isAvaliableXCoordinate == pawnFour.XCoordinate && isAvaliableYCoordinate == pawnFour.YCoordinate)
            {
                isAvalable = false;
            }
            else
            {
                isAvalable = true;
            }

            return isAvalable;
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
                    isValidMove = IsValidMove(currentMove, king, pawnA, pawnB, pawnC, pawnD);
                } while (!isValidMove);

                endOfGame = HasGameEnded(currentMove, king, pawnA, pawnB, pawnC, pawnD);
                isKingWinner = HasKingWon(currentMove, endOfGame, king, pawnA, pawnB, pawnC, pawnD);
                currentMove++;
            } while (!endOfGame);

            if (endOfGame)
            {
                DisplayCurrentEndOnConsole(currentMove, king, pawnA, pawnB, pawnC, pawnD);
            }
        }
    }
}
