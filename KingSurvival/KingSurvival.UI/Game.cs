namespace KingSurvival.UI
{
    using System;
    using KingSurvival.Common;

    public class Game
    {
        public static void Main()
        {
            Console.Title = "King Survival v1.0 ---Team Erbium---";

            PawnCoordinates pawnA = new PawnCoordinates(0, 0);
            PawnCoordinates pawnB = new PawnCoordinates(0, 2);
            PawnCoordinates pawnC = new PawnCoordinates(0, 4);
            PawnCoordinates pawnD = new PawnCoordinates(0, 6);
            PawnCoordinates kingPawn = new PawnCoordinates(7, 3);

            bool kraj = false;
            int kojE_naHod = 1;
            do
            {
                bool ok;
                do
                {
                    Console.Clear();
                    PE4AT_DASKA(pawnA, pawnB, pawnC, pawnD, kingPawn);
                    ok = isMoveLeft(kojE_naHod, ref pawnA, ref pawnB, ref pawnC, ref pawnD, ref kingPawn);
                } while (!ok);

                kraj = proverka2(kojE_naHod, pawnA, pawnB, pawnC, pawnD, kingPawn);
                kojE_naHod++;
            } while (!kraj);
        }
    }
}
