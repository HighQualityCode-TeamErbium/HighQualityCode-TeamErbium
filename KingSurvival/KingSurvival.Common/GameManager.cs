using System;

namespace KingSurvival.Common
{
    internal static class GameManager
    {
        private static bool isKingOnTurn;
        private static bool isKingWinner;

        public static bool HasGameEnded(int turn, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD, Pawn pawnKing)
        {
            // TODO : Refactor this code (yoan)
            #region /*Change turn input*/
            if (turn % 2 == 1)
            {
                isKingOnTurn = true;
            }
            else
            {
                isKingOnTurn = false;
            }
            #endregion

            if (isKingOnTurn)
            {
                if (pawnKing.XCoordinate == 0)
                {
                    isKingWinner = true;

                    //DisplayCurrentEndOnConsole(turn, pawnA, pawnB, pawnC, pawnD, pawnKing);

                    // game ends
                    return true;
                }
                else
                {
                    //game continues
                    return false;
                }
            }
            else
            {
                // Kings movements
                bool canKingGoUpLeft = true;
                bool canKingGoUpRight = true;
                bool canKingGoDownLeft = true;
                bool canKingGoDownRight = true;
                // checks is king over some border
                if (pawnKing.XCoordinate == 0)
                {
                    canKingGoUpLeft = false;
                    canKingGoUpRight = false;
                }
                else if (pawnKing.XCoordinate == 7)
                {
                    canKingGoDownLeft = false;
                    canKingGoDownRight = false;
                }

                if (pawnKing.YCoordinate == 0)
                {
                    canKingGoUpLeft = false;
                    canKingGoDownLeft = false;
                }
                else if (pawnKing.YCoordinate == 7)
                {
                    canKingGoUpRight = false;
                    canKingGoDownRight = false;
                }

                // check if king is near pawn
                canKingGoUpLeft = !(IsAvailableNextPosition(pawnKing.XCoordinate - 1, pawnKing.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD));
                canKingGoUpRight = !(IsAvailableNextPosition(pawnKing.XCoordinate - 1, pawnKing.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD));
                canKingGoDownLeft = !(IsAvailableNextPosition(pawnKing.XCoordinate + 1, pawnKing.YCoordinate - 1, pawnA, pawnB, pawnC, pawnD));
                canKingGoDownRight = !(IsAvailableNextPosition(pawnKing.XCoordinate + 1, pawnKing.YCoordinate + 1, pawnA, pawnB, pawnC, pawnD));


                bool isAnyOfKingMovesAvaiable = canKingGoDownRight || canKingGoDownLeft || canKingGoUpLeft || canKingGoUpRight;

                if (!isAnyOfKingMovesAvaiable)
                {
                    isKingWinner = false;

                    //DisplayCurrentEndOnConsole(turn, pawnA, pawnB, pawnC, pawnD, pawnKing);

                    // game ends;
                    return true;
                }

                if (!proverka1(pawnA, pawnB, pawnC, pawnD, pawnKing) &&
                    !proverka1(pawnB, pawnA, pawnC, pawnD, pawnKing) &&
                    !proverka1(pawnC, pawnA, pawnB, pawnD, pawnKing) &&
                    !proverka1(pawnD, pawnA, pawnB, pawnC, pawnKing))
                {

                    isKingWinner = true;

                    //DisplayCurrentEndOnConsole(turn, pawnA, pawnB, pawnC, pawnD, pawnKing);
                    // game ends;
                    return true;
                }
                // game continues
                return false;
            }
        }

        private static bool proverka1(Pawn kingPawn, Pawn pawnA, Pawn pawnB, Pawn pawnC, Pawn pawnD)
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
            else if (kingPawn.YCoordinate == 4 + 3)
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
