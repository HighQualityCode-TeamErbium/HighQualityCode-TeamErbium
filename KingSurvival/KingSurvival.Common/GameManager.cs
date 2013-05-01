using System;

namespace KingSurvival.Common
{
    internal static class GameManager
    {
        private static bool isKingOnTurn;
        private static bool isKingWinner;
        
        private static bool HasGameEnded(int turn, MatrixCoordinates pawnA, MatrixCoordinates pawnB, MatrixCoordinates pawnC, MatrixCoordinates pawnD, MatrixCoordinates kingPawn)
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
                if (kingPawn.Row == 0)
                {
                    isKingWinner = true;
                    
                   DisplayCurrentEndOnConsole(turn, pawnA, pawnB, pawnC, pawnD, kingPawn);

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
                if (kingPawn.Row == 0)
                {
                    canKingGoUpLeft = false;
                    canKingGoUpRight = false;
                }
                else if (kingPawn.Row == 7)
                {
                    canKingGoDownLeft = false;
                    canKingGoDownRight = false;
                }

                if (kingPawn.Column == 0)
                {
                    canKingGoUpLeft = false;
                    canKingGoDownLeft = false;
                }
                else if (kingPawn.Column == 7)
                {
                    canKingGoUpRight = false;
                    canKingGoDownRight = false;
                }

                // check if king is near pawn
                canKingGoUpLeft = !(IsAvailableNextPosition(kingPawn.Row - 1, kingPawn.Column - 1, pawnA, pawnB, pawnC, pawnD));
                canKingGoUpRight = !(IsAvailableNextPosition(kingPawn.Row - 1, kingPawn.Column + 1, pawnA, pawnB, pawnC, pawnD));
                canKingGoDownLeft = !(IsAvailableNextPosition(kingPawn.Row + 1, kingPawn.Column - 1, pawnA, pawnB, pawnC, pawnD));
                canKingGoDownRight = !(IsAvailableNextPosition(kingPawn.Row + 1, kingPawn.Column + 1, pawnA, pawnB, pawnC, pawnD));
            

                bool isAnyOfKingMovesAvaiable = canKingGoDownRight || canKingGoDownLeft || canKingGoUpLeft || canKingGoUpRight;

                if (!isAnyOfKingMovesAvaiable)
                {
                    isKingWinner = false;

                    DisplayCurrentEndOnConsole(turn, pawnA, pawnB, pawnC, pawnD, kingPawn);
              
                    // game ends;
                    return true;
                }

                if (!proverka1(pawnA, pawnB, pawnC, pawnD, kingPawn) &&
                    !proverka1(pawnB, pawnA, pawnC, pawnD, kingPawn) &&
                    !proverka1(pawnC, pawnA, pawnB, pawnD, kingPawn) &&
                    !proverka1(pawnD, pawnA, pawnB, pawnC, kingPawn))
                {
                    
                    isKingWinner = true;
                
                    DisplayCurrentEndOnConsole(turn, pawnA, pawnB, pawnC, pawnD, kingPawn);
                    // game ends;
                    return true;
                }
                // game continues
                return false;
            }
        }

        private static void DisplayCurrentEndOnConsole(int turn, MatrixCoordinates pawnA, MatrixCoordinates pawnB, MatrixCoordinates pawnC, MatrixCoordinates pawnD, MatrixCoordinates kingPawn)
        {
            if (isKingWinner)
            {
                Console.Clear();
                PE4AT_DASKA(pawnA, pawnB, pawnC, pawnD, kingPawn);
                Console.WriteLine("King wins in {0} turns.", turn / 2);
            }
            else
            {
                Console.Clear();
                PE4AT_DASKA(pawnA, pawnB, pawnC, pawnD, kingPawn);
                Console.WriteLine("King loses.");
            }
        }

        private static bool proverka1(MatrixCoordinates kingPawn, MatrixCoordinates pawnA, MatrixCoordinates pawnB, MatrixCoordinates pawnC, MatrixCoordinates pawnD)
        {
            if (kingPawn.Row == 7)
            {
                return false;
            }
            else if (kingPawn.Column > 0 && kingPawn.Column < 7)
            {
                if (IsAvailableNextPosition(kingPawn.Row + 1, kingPawn.Column + 1, pawnA, pawnB, pawnC, pawnD) &&
                    IsAvailableNextPosition(kingPawn.Row + 1, kingPawn.Column - 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            else if (kingPawn.Column == 0)
            {
                if (IsAvailableNextPosition(kingPawn.Row + 1, kingPawn.Column + 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            else if (kingPawn.Column == 4 + 3)
            {
                if (IsAvailableNextPosition(kingPawn.Row + 1, kingPawn.Column - 1, pawnA, pawnB, pawnC, pawnD))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool isMoveLeft(int turn, ref MatrixCoordinates A, ref MatrixCoordinates B, ref MatrixCoordinates C, ref MatrixCoordinates D, ref MatrixCoordinates K)
        {
            if (turn % 2 == 1)
            {
                Console.Write("King’s turn: ");
                string move = Console.ReadLine();
                switch (move)
                {
                    case "KUL":
                        if (K.Column > 0 && K.Row > 0 && !IsAvailableNextPosition(K.Row - 1, K.Column - 1, A, B, C, D))
                        {
                            K.Column--;
                            K.Row--;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KUR": // if KUR... gotta love these moments
                        if (K.Column < 7 && K.Row > 0 && !IsAvailableNextPosition(K.Row - 1, K.Column + 1, A, B, C, D))
                        {
                            K.Column++;
                            K.Row--;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KDL":
                        if (K.Column > 0 && K.Row < 7 && !IsAvailableNextPosition(K.Row + 1, K.Column - 1, A, B, C, D))
                        {
                            K.Column--;
                            K.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KDR":
                        if (K.Column < 7 && K.Row < 7 && !IsAvailableNextPosition(K.Row + 1, K.Column + 1, A, B, C, D))
                        {
                            K.Column++;
                            K.Row++;
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
                        if (A.Column > 0 && A.Row < 7 && !IsAvailableNextPosition(A.Row + 1, A.Column - 1, K, B, C, D))
                        {
                            A.Column--;
                            A.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "ADR":
                        if (A.Column < 7 && A.Row < 7 && !IsAvailableNextPosition(A.Row + 1, A.Column + 1, K, B, C, D))
                        {
                            A.Column++;
                            A.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "BDL":
                        if (B.Column > 0 && B.Row < 7 &&
                            !IsAvailableNextPosition(B.Row + 1, B.Column - 1, A, K, C, D))
                        {
                            B.Column--;
                            B.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "BDR":
                        if (B.Column < 7 && B.Row < 7 && !IsAvailableNextPosition(B.Row + 1, B.Column + 1, A, K, C, D))
                        {
                            B.Column++;
                            B.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "CDL":
                        if (C.Column > 0 && C.Row < 7 && !IsAvailableNextPosition(C.Row + 1, C.Column + 1, A, B, K, D))
                        {
                            C.Column--;
                            C.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "CDR":
                        if (C.Column < 7 && C.Row < 7 && !IsAvailableNextPosition(C.Row + 1, C.Column + 1, A, B, K, D))
                        {
                            C.Column++;
                            C.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "DDL":
                        if (D.Column > 0 && D.Row < 7 && !IsAvailableNextPosition(D.Row + 1, D.Column - 1, A, B, C, K))
                        {
                            D.Column--;
                            D.Row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "DDR":
                        if (D.Column < 7 && D.Row < 7 && !IsAvailableNextPosition(D.Row + 1, D.Column + 1, A, B, C, K))
                        {
                            D.Column++;
                            D.Row++;
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

        private static bool IsAvailableNextPosition(int notOverlapedRow, int notOverlapedColumn, MatrixCoordinates overlap1, MatrixCoordinates overlap2, MatrixCoordinates overlap3, MatrixCoordinates overlap4)
        {
            if (notOverlapedRow == overlap1.Row && notOverlapedColumn == overlap1.Column)
            {
                return false;
            }
            else if (notOverlapedRow == overlap2.Row && notOverlapedColumn == overlap2.Column)
            {
                return false;
            }
            else if (notOverlapedRow == overlap3.Row && notOverlapedColumn == overlap3.Column)
            {
                return false;
            }
            else if (notOverlapedRow == overlap4.Row && notOverlapedColumn == overlap4.Column)
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
