namespace KingSurvival.Common
{
    using System;

    internal static class GameManager
    {
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
                    (IsKingTrapped(king, pawnA, pawnB, pawnC, pawnD) && IsKingTrapped(king, pawnA, pawnB, pawnC, pawnD) &&
                    IsKingTrapped(king, pawnA, pawnB, pawnC, pawnD) && IsKingTrapped(king, pawnA, pawnB, pawnC, pawnD)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal static bool HasKingWon(int gameTurn, bool gameCondition, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isGameEnded = gameCondition;
            bool isKingOnTurn = (gameTurn % 2 == 1);
            // we can only determine king as winner, after game ending
            if (isGameEnded)
            {
                // if king is on top is winner
                if (isKingOnTurn && king.XCoordinate == 7)
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
            if (king.XCoordinate == 7 || king.YCoordinate == 0)
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
            if (king.XCoordinate == 0 || king.YCoordinate == 7)
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
            if (king.XCoordinate == 7 || king.YCoordinate == 7)
            {
                canKingGoDownRight = false;
            }
            // check if pawn is near king
            canKingGoDownRight = IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD);

            return canKingGoDownRight;
        }
        // TODO : Immediately change name !!!
        private static bool IsKingTrapped(King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            if (king.XCoordinate == 7)
            {
                return false;
            }
            else if (king.YCoordinate > 0 && king.YCoordinate < 7)
            {
                if (IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD) &&
                    IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            else if (king.YCoordinate == 0)
            {
                if (IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            else if (king.YCoordinate == 7)
            {
                if (IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
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
                        if (pawnA.YCoordinate > 0 && pawnA.XCoordinate < 7 && 
                            IsAvailableNextPosition(pawnA.XCoordinate + 1, pawnA.YCoordinate - 1, king, pawnB, pawnC, pawnD))
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
                        if (pawnA.YCoordinate < 7 && pawnA.XCoordinate < 7 && 
                            IsAvailableNextPosition(pawnA.XCoordinate + 1, pawnA.YCoordinate + 1, king, pawnB, pawnC, pawnD))
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
                        if (pawnB.YCoordinate > 0 && pawnB.XCoordinate < 7 && 
                            IsAvailableNextPosition(pawnB.XCoordinate + 1, pawnB.YCoordinate - 1, king, pawnA, pawnC, pawnD))
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
                        if (pawnB.YCoordinate < 7 && pawnB.XCoordinate < 7 &&
                            IsAvailableNextPosition(pawnB.XCoordinate + 1, pawnB.YCoordinate + 1, king, pawnA, pawnC, pawnD))
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
                        if (pawnC.YCoordinate > 0 && pawnC.XCoordinate < 7 &&
                            IsAvailableNextPosition(pawnC.XCoordinate + 1, pawnC.YCoordinate + 1, king, pawnA, pawnB, pawnD))
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
                        if (pawnC.YCoordinate < 7 && pawnC.XCoordinate < 7 &&
                            IsAvailableNextPosition(pawnC.XCoordinate + 1, pawnC.YCoordinate + 1, king, pawnA, pawnB, pawnD))
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
                        if (pawnD.YCoordinate > 0 && pawnD.XCoordinate < 7 &&
                            IsAvailableNextPosition(pawnD.XCoordinate + 1, pawnD.YCoordinate - 1, king, pawnA, pawnB, pawnC))
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
                        if (pawnD.YCoordinate < 7 && pawnD.XCoordinate < 7 &&
                            IsAvailableNextPosition(pawnD.XCoordinate + 1, pawnD.YCoordinate + 1, king, pawnA, pawnB, pawnC))
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

        private static bool IsValidKingMove(string command, King king, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
        {
            bool isValid = false;
            switch (command)
            {
                case "kul":
                    {
                        if (king.YCoordinate > 0 && king.XCoordinate > 0 && 
                            IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
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
                        if (king.YCoordinate < 7 && king.XCoordinate > 0 &&
                            IsAvailableNextPosition(king.XCoordinate - 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
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
                        if (king.YCoordinate > 0 && king.XCoordinate < 7 && 
                            IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD))
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
                        if (king.YCoordinate < 7 && king.XCoordinate < 7 && 
                            IsAvailableNextPosition(king.XCoordinate + 1, king.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD))
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
    }
}
