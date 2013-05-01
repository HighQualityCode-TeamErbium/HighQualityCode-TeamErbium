namespace KingSurvival.UI
{
    using System;
    using KingSurvival.Common;

    public class Game
    {
        public static void Main()
        {
            Console.Title = "King Survival v1.0 ---Team Erbium---";

            Pawn pawnA = new Pawn('A', 0, 0);
            Pawn pawnB = new Pawn('B', 0, 2);
            Pawn pawnC = new Pawn('C', 0, 4);
            Pawn pawnD = new Pawn('D', 0, 6);
            Pawn pawnKing = new Pawn('K', 7, 3);

            bool end = false;
            int onMove = 1;
            do
            {
                bool ok;
                do
                {
                    Console.Clear();
                    PE4AT_DASKA(pawnA, pawnB, pawnC, pawnD, pawnKing);
                    ok = isMoveLeft(kojE_naHod, ref pawnA, ref pawnB, ref pawnC, ref pawnD, ref pawnKing);
                } while (!ok);

                kraj = proverka2(kojE_naHod, pawnA, pawnB, pawnC, pawnD, pawnKing);
                kojE_naHod++;
            } while (!kraj);
        }
    }
}
