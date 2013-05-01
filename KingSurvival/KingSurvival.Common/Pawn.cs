namespace KingSurvival.Common
{
    using System;

    public class Pawn
    {
        private char symbol;
        private MatrixCoordinates coordinates;

        public Pawn(char symbol, int xCoordinate, int yCoordinate)
        {
            this.symbol = symbol;
            this.coordinates = new MatrixCoordinates(xCoordinate, yCoordinate); 
        }

        public char Symbol
        {
            get { return this.symbol; }
        }

        public int XCoordinate
        {
            get { return this.coordinates.Row; }
            set { this.coordinates.Row = value; }
        }

        public int YCoordinate
        {
            get { return this.coordinates.Column; }
            set { this.coordinates.Column = value; }
        }
    }
}
