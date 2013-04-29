namespace KingSurvival.Common
{
    using System;

    public class Board
    {
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
