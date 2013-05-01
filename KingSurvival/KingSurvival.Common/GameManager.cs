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

        public static bool isMoveLeft(int turn, Pawn A, Pawn B, Pawn C, Pawn D, Pawn K)
        {
            if (turn % 2 == 1)
            {
                Console.Write("King’s turn: ");
                string move = Console.ReadLine();
                switch (move)
                {
                    case "KUL":
                        if (K.YCoordinate > 0 && K.XCoordinate > 0 && !IsAvailableNextPosition(K.XCoordinate - 1, K.YCoordinate - 1, A, B, C, D))
                        {
                            K.YCoordinate--;
                            K.XCoordinate--;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KUR": // if KUR... gotta love these moments
                        if (K.YCoordinate < 7 && K.XCoordinate > 0 && !IsAvailableNextPosition(K.XCoordinate - 1, K.YCoordinate + 1, A, B, C, D))
                        {
                            K.YCoordinate++;
                            K.XCoordinate--;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KDL":
                        if (K.YCoordinate > 0 && K.XCoordinate < 7 && !IsAvailableNextPosition(K.XCoordinate + 1, K.YCoordinate - 1, A, B, C, D))
                        {
                            K.YCoordinate--;
                            K.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KDR":
                        if (K.YCoordinate < 7 && K.XCoordinate < 7 && !IsAvailableNextPosition(K.XCoordinate + 1, K.YCoordinate + 1, A, B, C, D))
                        {
                            K.YCoordinate++;
                            K.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    default:
                        Console.Write("Illegal move!");
                        Console.ReadKey();
                        return false;
                }
            }
            else
            {
                Console.Write("Pawns’ turn: ");
                string move = Console.ReadLine();
                switch (move)
                {
                    case "ADL":
                        if (A.YCoordinate > 0 && A.XCoordinate < 7 && !IsAvailableNextPosition(A.XCoordinate + 1, A.YCoordinate - 1, K, B, C, D))
                        {
                            A.YCoordinate--;
                            A.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "ADR":
                        if (A.YCoordinate < 7 && A.XCoordinate < 7 && !IsAvailableNextPosition(A.XCoordinate + 1, A.YCoordinate + 1, K, B, C, D))
                        {
                            A.YCoordinate++;
                            A.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "BDL":
                        if (B.YCoordinate > 0 && B.XCoordinate < 7 &&
                            !IsAvailableNextPosition(B.XCoordinate + 1, B.YCoordinate - 1, A, K, C, D))
                        {
                            B.YCoordinate--;
                            B.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "BDR":
                        if (B.YCoordinate < 7 && B.XCoordinate < 7 && !IsAvailableNextPosition(B.XCoordinate + 1, B.YCoordinate + 1, A, K, C, D))
                        {
                            B.YCoordinate++;
                            B.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "CDL":
                        if (C.YCoordinate > 0 && C.XCoordinate < 7 && !IsAvailableNextPosition(C.XCoordinate + 1, C.YCoordinate + 1, A, B, K, D))
                        {
                            C.YCoordinate--;
                            C.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "CDR":
                        if (C.YCoordinate < 7 && C.XCoordinate < 7 && !IsAvailableNextPosition(C.XCoordinate + 1, C.YCoordinate + 1, A, B, K, D))
                        {
                            C.YCoordinate++;
                            C.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "DDL":
                        if (D.YCoordinate > 0 && D.XCoordinate < 7 && !IsAvailableNextPosition(D.XCoordinate + 1, D.YCoordinate - 1, A, B, C, K))
                        {
                            D.YCoordinate--;
                            D.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "DDR":
                        if (D.YCoordinate < 7 && D.XCoordinate < 7 && !IsAvailableNextPosition(D.XCoordinate + 1, D.YCoordinate + 1, A, B, C, K))
                        {
                            D.YCoordinate++;
                            D.XCoordinate++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    default:
                        Console.Write("Illegal move!");
                        Console.ReadKey();
                        return false;
                }
            }

            return true;
        }

        private static bool IsAvailableNextPosition(int notOverlapedXCoordinate, int notOverlapedYCoordinate, Pawn overlap1, Pawn overlap2, Pawn overlap3, Pawn overlap4)
        {
            if (notOverlapedXCoordinate == overlap1.XCoordinate && notOverlapedYCoordinate == overlap1.YCoordinate)
            {
                return false;
            }
            else if (notOverlapedXCoordinate == overlap2.XCoordinate && notOverlapedYCoordinate == overlap2.YCoordinate)
            {
                return false;
            }
            else if (notOverlapedXCoordinate == overlap3.XCoordinate && notOverlapedYCoordinate == overlap3.YCoordinate)
            {
                return false;
            }
            else if (notOverlapedXCoordinate == overlap4.XCoordinate && notOverlapedYCoordinate == overlap4.YCoordinate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
