﻿namespace KingSurvival.Common
{
    public class King : Pawn
    {
        private const char KingSymbol = 'K';

        public King(int xCoordinate, int yCoordinate)
            : base(KingSymbol, xCoordinate, yCoordinate)
        {
        }
    }
}
