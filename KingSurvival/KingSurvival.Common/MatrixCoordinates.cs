namespace KingSurvival.Common
{
    internal struct MatrixCoordinates
    {
        private int row;
        private int column;

        public MatrixCoordinates(int row, int column)
            : this()
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Column
        {
            get { return this.column; }
            set { this.column = value; }
        }
    }
}
