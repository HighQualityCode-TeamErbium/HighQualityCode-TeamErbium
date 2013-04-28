namespace KingSurvival.UI
{
    using System;
    using KingSurvival.Common;

    public class Game
    {
        static void Main(string[] args)
        {
            PawnCoordinates A = new PawnCoordinates(0, 0);
            PawnCoordinates B = new PawnCoordinates(0, 2);
            PawnCoordinates C = new PawnCoordinates(0, 4);
            PawnCoordinates D = new PawnCoordinates(0, 6);


            PawnCoordinates K = new PawnCoordinates(7, 3);
            bool kraj = false;
            int kojE_naHod = 1;
            do
            {
                bool ok;
                do
                {
                    System.Console.Clear();
                    PE4AT_DASKA(A, B, C, D, K);


                    ok = isMoveLeft(kojE_naHod, ref A, ref B, ref C, ref D, ref K);
                } while (!ok);

                kraj = proverka2(kojE_naHod, A, B, C, D, K);
                kojE_naHod++;


            } while (!kraj);
        }

        private static bool proverka2(int turn, PawnCoordinates A, PawnCoordinates B, PawnCoordinates C, PawnCoordinates D, PawnCoordinates K)
        {
            if (turn % 2 == 1)
            {
                if (K.Row == 0)
                {
                    System.Console.Clear();
                    PE4AT_DASKA(A, B, C, D, K);
                    Console.WriteLine("King wins in {0} turns.", turn / 2 + 1);
                    return true;
                }
                else return false;
            }
            else
            {
                bool KUL = true;
                bool KUR = true; // yup, it's a boy
                bool KDL = true;
                bool KDR = true;

                if (K.Row == 0)
                {
                    // tuka carya e na hod
                    KUL = false;
                    KUR = false;
                }
                else if (K.Row == 7)
                {
                    KDL = false;
                    KDR = false;
                }

                if (K.Column == 0)
                {
                    KUL = false;
                    KDL = false;
                }
                else if (K.Column == 7)
                {
                    KUR = false; // kur v gyzaaaaa, oh boli!
                    KDR = false;
                }

                if (proverka(K.Row - 1, K.Column - 1, A, B, C, D))
                {
                    KUL = false;
                }
                if (proverka(K.Row - 1, K.Column + 1, A, B, C, D))
                {
                    KUR = false; // castration... nasty
                }
                if (proverka(K.Row + 1, K.Column - 1, A, B, C, D))
                {
                    KDL = false;
                }
                if (proverka(K.Row + 1, K.Column + 1, A, B, C, D))
                {
                    KDR = false;
                }

                if (!KDR && !KDL && !KUL && !KUR)
                {
                    System.Console.Clear();
                    PE4AT_DASKA(A, B, C, D, K);
                    Console.WriteLine("King loses.");
                    return true;
                }

                if (!proverka1(A, B, C, D, K) && !proverka1(B, A, C, D, K) && !proverka1(C, A, B, D, K) && !proverka1(D, A, B, C, K))
                {
                    System.Console.Clear();
                    PE4AT_DASKA(A, B, C, D, K);
                    Console.WriteLine("King wins in {0} turns.", turn / 2);
                    return true;
                }

                return false;
            }
        }

        private static bool proverka1(PawnCoordinates pawn, PawnCoordinates obstacle1, PawnCoordinates obstacle2, PawnCoordinates obstacle3, PawnCoordinates obstacle4)
        {
            if (pawn.Row == 7)
            {
                return false;
            }
            else if (pawn.Column > 0 && pawn.Column < 7)
            {
                if (proverka(pawn.Row + 1, pawn.Column + 1, obstacle1, obstacle2, obstacle3, obstacle4) &&

                    proverka(
                    pawn.Row + 1,
                    pawn.Column - 1,
                    obstacle1,
                    obstacle2,
                    obstacle3,
                    obstacle4)) return false;


            }
            else if (pawn.Column == 0)
            {
                if (proverka(pawn.Row + 1, pawn.Column + 1, obstacle1, obstacle2, obstacle3, obstacle4))
                {
                    return false;
                }
            }
            else if (pawn.Column == 4 + 3)
            {
                if (proverka(pawn.Row + 1, pawn.Column - 1, obstacle1, obstacle2, obstacle3, obstacle4))
                {
                    return false;


                }
            }
            return true;
        }

        private static bool isMoveLeft(int turn, ref PawnCoordinates A, ref PawnCoordinates B, ref PawnCoordinates C, ref PawnCoordinates D, ref PawnCoordinates K)
        {
            if (turn % 2 == 1)
            {
                Console.Write("King’s turn: ");
                string move = Console.ReadLine();
                switch (move)
                {
                    case "KUL":
                        if (K.Column > 0 && K.Row > 0 && !proverka(K.Row - 1, K.Column - 1, A, B, C, D))
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
                        if (K.Column < 7 && K.Row > 0 && !proverka(K.Row - 1, K.Column + 1, A, B, C, D))
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
                        if (K.Column > 0 && K.Row < 7 && !proverka(K.Row + 1, K.Column - 1, A, B, C, D))
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


                        if (K.Column < 7 && K.Row < 7 && !proverka(K.Row + 1, K.Column + 1, A, B, C, D))
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
                        if (A.Column > 0 && A.Row < 7 && !proverka(A.Row + 1, A.Column - 1, K, B, C, D))
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
                        if (A.Column < 7 && A.Row < 7 && !proverka(A.Row + 1, A.Column + 1, K, B, C, D))
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

                            !proverka(B.Row + 1, B.Column - 1, A, K, C, D))
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
                        if (B.Column < 7 && B.Row < 7 && !proverka(B.Row + 1, B.Column + 1, A, K, C, D))
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
                        if (C.Column > 0 && C.Row < 7 && !proverka(C.Row + 1, C.Column + 1, A, B, K, D))
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
                        if (C.Column < 7 && C.Row < 7 && !proverka(C.Row + 1, C.Column + 1, A, B, K, D))
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
                        if (D.Column > 0 && D.Row < 7 && !proverka(D.Row + 1, D.Column - 1, A, B, C, K))
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
                        if (D.Column < 7 && D.Row < 7 && !proverka(D.Row + 1, D.Column + 1, A, B, C, K))
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

        private static bool proverka(int notOverlapedRow, int notOverlapedColumn, PawnCoordinates overlap1, PawnCoordinates overlap2, PawnCoordinates overlap3, PawnCoordinates overlap4)
        {
            if (notOverlapedRow == overlap1.Row && notOverlapedColumn == overlap1.Column) return true;
            else if (notOverlapedRow == overlap2.Row && notOverlapedColumn == overlap2.Column) return true;
            else if (notOverlapedRow == overlap3.Row && notOverlapedColumn == overlap3.Column) return true;
            else if (notOverlapedRow == overlap4.Row && notOverlapedColumn == overlap4.Column) return true;
            else
                return false;


        }

        private static void PE4AT_DASKA(PawnCoordinates A, PawnCoordinates B, PawnCoordinates C, PawnCoordinates D, PawnCoordinates K)
        {
            int row = 0;
            for (int i = 0; i < 19; i++)
            {
                if (i > 3)
                {
                    if (i % 2 == 0)
                    {
                        // ostaviame interval sled chisloto
                        Console.Write("{0} ", row++);
                    }
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            Console.Write("   ");
            for (int i = 0; i < 17; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.Write("{0} | ", i);

                for (int j = 0; j < 8; j++)
                {
                    char symbol;


                    find(A, B, C, D, K, i, j, out symbol);

                    Console.Write(symbol + " ");
                }
                Console.WriteLine('|');
            }

            Console.Write("   ");
            for (int i = 0; i < 17; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }

        private static void find(PawnCoordinates A, PawnCoordinates B, PawnCoordinates C, PawnCoordinates D, PawnCoordinates K, int i, int j, out char symbol)
        {
            if (A.Row == i && A.Column == j)
            {
                symbol = 'A';
            }
            else if (B.Row == i && B.Column == j)
            {
                symbol = 'B';
            }
            else if (C.Row == i && C.Column == j)
            {
                symbol = 'C';
            }
            else if (D.Row == i && D.Column == j)
            {
                symbol = 'D';
            }
            else if (K.Row == i && K.Column == j)
            {
                symbol = 'K';
            }
            else if ((i + j) % 2 == 0)
            {
                symbol = '+';
            }
            else
            {
                symbol = '-';
            }
        }
    }
}
