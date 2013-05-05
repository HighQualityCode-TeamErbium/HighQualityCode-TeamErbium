using System;

namespace KingSurvival.Common
{
    internal static class GameManager
    {
        public static bool HasGameEnded(int gameTurn, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn pawnKing)
        {
            bool isKingOnTurn = false;            
            // on every odd game t king is on turn
            isKingOnTurn = (gameTurn % 2 == 1);
            // if king is not on top of board game continues
            if (isKingOnTurn && pawnKing.XCoordinate == 0)
            {
                return true;
            }
            else
            {
                // king is trapped by pawns -> game ends, king loses
                if (!CanKingMove(pawnA, pawnB, pawnC, pawnD, pawnKing) ||
                    (IsKingTrapped(pawnA, pawnB, pawnC, pawnD, pawnKing) && IsKingTrapped(pawnB, pawnA, pawnC, pawnD, pawnKing) &&
                    IsKingTrapped(pawnC, pawnA, pawnB, pawnD, pawnKing) && IsKingTrapped(pawnD, pawnA, pawnB, pawnC, pawnKing)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal static bool HasKingWon(int gameTurn, bool gameCondition, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn pawnKing)
        {
            bool isGameEnded = gameCondition;
            bool isKingOnTurn = (gameTurn % 2 == 1);
            // we can only determine king as winner, after game ending
            if (isGameEnded)
            {
                // if king is on top is winner
                if (isKingOnTurn && pawnKing.XCoordinate == 7)
                {
                    return true;
                }
                else
                {
                    // if king is trapped -> lost
                    if (!CanKingMove(pawnA, pawnB, pawnC, pawnD, pawnKing))
                    {
                        return false;
                    }//while king is not on top and not trapped
                    else if (IsKingTrapped(pawnA, pawnB, pawnC, pawnD, pawnKing) && IsKingTrapped(pawnB, pawnA, pawnC, pawnD, pawnKing) &&
                    IsKingTrapped(pawnC, pawnA, pawnB, pawnD, pawnKing) && IsKingTrapped(pawnD, pawnA, pawnB, pawnC, pawnKing))
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

        private static bool CanKingMove(Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn pawnKing)
        {
            // determine king restrictions
            bool canKingGoUpLeft = IsKingUpLeftMovementAvailable(pawnKing, pawnA, pawnB, pawnC, pawnD);
            bool canKingGoDownLeft = IsKingDownLeftMovementAvailable(pawnKing, pawnA, pawnB, pawnC, pawnD);
            bool canKingGoUpRight = IsKingUpRightMovementAvailable(pawnKing, pawnA, pawnB, pawnC, pawnD);
            bool canKingGoDownRight = IsKingDownRightMovementAvailable(pawnKing, pawnA, pawnB, pawnC, pawnD);
            // check if all 
            bool isAnyOfKingMovesAvaiable = canKingGoDownRight || canKingGoDownLeft || canKingGoUpLeft || canKingGoUpRight;
            
            return isAnyOfKingMovesAvaiable;
        }

        private static bool IsKingUpLeftMovementAvailable(Pawn pawnKing, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoUpLeft = true;
            // check if king is near border
            if (pawnKing.XCoordinate == 0 || pawnKing.YCoordinate == 0)
            {
                canKingGoUpLeft = false;
            }
            // check if pawn is near king
            canKingGoUpLeft = IsAvailableNextPosition(pawnKing.XCoordinate - 1, pawnKing.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD);
     
            return canKingGoUpLeft;
        }

        private static bool IsKingDownLeftMovementAvailable(Pawn pawnKing, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoDownLeft = true;
            // check if king is near border
            if (pawnKing.XCoordinate == 7 || pawnKing.YCoordinate == 0)
            {
                canKingGoDownLeft = false;
            }
            // check if pawn is near king
            canKingGoDownLeft = (IsAvailableNextPosition(pawnKing.XCoordinate + 1, pawnKing.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD));

            return canKingGoDownLeft;
        }

        private static bool IsKingUpRightMovementAvailable(Pawn pawnKing, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoUpRight = true;
            // check if king is near border
            if (pawnKing.XCoordinate == 0 || pawnKing.YCoordinate == 7)
            {
                canKingGoUpRight = false;
            }
            // check if pawn is near king
            canKingGoUpRight = IsAvailableNextPosition(pawnKing.XCoordinate - 1, pawnKing.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD);

            return canKingGoUpRight;
        }

        private static bool IsKingDownRightMovementAvailable(Pawn pawnKing, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool canKingGoDownRight = true;
            // check if king is near border
            if (pawnKing.XCoordinate == 7 || pawnKing.YCoordinate == 7)
            {
                canKingGoDownRight = false;
            }
            // check if pawn is near king
            canKingGoDownRight = (IsAvailableNextPosition(pawnKing.XCoordinate + 1, pawnKing.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD));

            return canKingGoDownRight;
        }
        // TODO : Immediately change name !!!
        private static bool IsKingTrapped(Pawn kingPawn, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            if (kingPawn.XCoordinate == 7)
            {
                return false;
            }
            else if (kingPawn.YCoordinate > 0 && kingPawn.YCoordinate < 7)
            {
                if (IsAvailableNextPosition(kingPawn.XCoordinate + 1, kingPawn.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD) &&
                    IsAvailableNextPosition(kingPawn.XCoordinate + 1, kingPawn.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            else if (kingPawn.YCoordinate == 0)
            {
                if (IsAvailableNextPosition(kingPawn.XCoordinate + 1, kingPawn.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            else if (kingPawn.YCoordinate == 7)
            {
                if (IsAvailableNextPosition(kingPawn.XCoordinate + 1, kingPawn.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidMove(int turn, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn king)
        {
            bool isValid;
            string command;
            if (turn % 2 == 1)
            {
                Console.Write("King’s turn: ");
                command = Console.ReadLine().ToLower();
                isValid = IsValidKingMove(command, pawnA, pawnB, pawnC, pawnD, king);
            }
            else
            {
                Console.Write("Pawns’ turn: ");
                command = Console.ReadLine().ToLower();
                isValid = IsValidPawnMove(command, pawnA, pawnB, pawnC, pawnD, king);
            }

            return isValid;
        }

        private static bool IsValidPawnMove(string command, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn king)
        {
            bool isValid;
            switch (command)
            {
                case "adl":
                    {
                        if (pawnA.YCoordinate > 0 && pawnA.XCoordinate < 7 && IsAvailableNextPosition(pawnA.XCoordinate + 1, pawnA.YCoordinate - 1, king, pawnB, pawnC, pawnD))
                        {
                            pawnA.YCoordinate--;
                            pawnA.XCoordinate++;
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
                        if (pawnA.YCoordinate < 7 && pawnA.XCoordinate < 7 && IsAvailableNextPosition(pawnA.XCoordinate + 1, pawnA.YCoordinate + 1, king, pawnB, pawnC, pawnD))
                        {
                            pawnA.YCoordinate++;
                            pawnA.XCoordinate++;
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
                        if (pawnB.YCoordinate > 0 && pawnB.XCoordinate < 7 && IsAvailableNextPosition(pawnB.XCoordinate + 1, pawnB.YCoordinate - 1, pawnA, king, pawnC, pawnD))
                        {
                            pawnB.YCoordinate--;
                            pawnB.XCoordinate++;
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
                        if (pawnB.YCoordinate < 7 && pawnB.XCoordinate < 7 && IsAvailableNextPosition(pawnB.XCoordinate + 1, pawnB.YCoordinate + 1, pawnA, king, pawnC, pawnD))
                        {
                            pawnB.YCoordinate++;
                            pawnB.XCoordinate++;
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
                        if (pawnC.YCoordinate > 0 && pawnC.XCoordinate < 7 && IsAvailableNextPosition(pawnC.XCoordinate + 1, pawnC.YCoordinate + 1, pawnA, pawnB, king, pawnD))
                        {
                            pawnC.YCoordinate--;
                            pawnC.XCoordinate++;
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
                        if (pawnC.YCoordinate < 7 && pawnC.XCoordinate < 7 && IsAvailableNextPosition(pawnC.XCoordinate + 1, pawnC.YCoordinate + 1, pawnA, pawnB, king, pawnD))
                        {
                            pawnC.YCoordinate++;
                            pawnC.XCoordinate++;
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
                        if (pawnD.YCoordinate > 0 && pawnD.XCoordinate < 7 && IsAvailableNextPosition(pawnD.XCoordinate + 1, pawnD.YCoordinate - 1, pawnA, pawnB, pawnC, king))
                        {
                            pawnD.YCoordinate--;
                            pawnD.XCoordinate++;
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
                        if (pawnD.YCoordinate < 7 && pawnD.XCoordinate < 7 && IsAvailableNextPosition(pawnD.XCoordinate + 1, pawnD.YCoordinate + 1, pawnA, pawnB, pawnC, king))
                        {
                            pawnD.YCoordinate++;
                            pawnD.XCoordinate++;
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

        private static bool IsValidKingMove(string command, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn king)
        {
            bool isValid;
            switch (command)
            {
                case "kul":
                    {
                        if (king.YCoordinate > 0 && king.XCoordinate > 0 && IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.YCoordinate--;
                            king.XCoordinate--;
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
                        if (king.YCoordinate < 7 && king.XCoordinate > 0 && IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.YCoordinate++;
                            king.XCoordinate--;
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
                        if (king.YCoordinate > 0 && king.XCoordinate < 7 && IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.YCoordinate--;
                            king.XCoordinate++;
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
                        if (king.YCoordinate < 7 && king.XCoordinate < 7 && IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
                        {
                            king.YCoordinate++;
                            king.XCoordinate++;
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

        private static bool IsAvailableNextPosition(int isAvaliableXCoordinate, int isAvaliableYCoordinate, Pawn figureOne, Pawn figureTwo, Pawn figureThree, Pawn figureFour)
        {
            bool isAvalable;

            if (isAvaliableXCoordinate == figureOne.XCoordinate && isAvaliableYCoordinate == figureOne.YCoordinate)
            {
                isAvalable = false;
            }
            else if (isAvaliableXCoordinate == figureTwo.XCoordinate && isAvaliableYCoordinate == figureTwo.YCoordinate)
            {
                isAvalable = false;
            }
            else if (isAvaliableXCoordinate == figureThree.XCoordinate && isAvaliableYCoordinate == figureThree.YCoordinate)
            {
                isAvalable = false;
            }
            else if (isAvaliableXCoordinate == figureFour.XCoordinate && isAvaliableYCoordinate == figureFour.YCoordinate)
            {
                isAvalable = false;
            }
            else
            {
                isAvalable = true;
            }

            return isAvalable;
        }
    }
}
