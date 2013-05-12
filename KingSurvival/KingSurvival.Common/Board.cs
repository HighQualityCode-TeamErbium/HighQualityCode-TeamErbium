﻿namespace KingSurvival.Common
{
    using System.Text;

    public class Board
    {
        private const char HorizontalBorderSymbol = '-';
        private const char VerticalBorderSymbol = '|';
        private const char BlackCellSymbol = '-';
        private const char WhiteCellSymbol = '+';
        private const char WhiteSpaceSymbol = ' ';

        private readonly StringBuilder image;
        private readonly int boardRows;
        private readonly int boardColumns;

        public Board(int rows, int columns)
        {
            this.boardRows = rows;
            this.boardColumns = columns;
            this.image = new StringBuilder();
        }

        public int BoardRows
        {
            get { return this.boardRows; }
        }

        public int BoardColumns
        {
            get { return this.boardColumns; }
        }

        public string GetImage(params Pawn[] pawns)
        {
            this.image.Clear();

            this.AppendRowsLine();

            this.AppendBorder();

            this.AppendRowAndColumnIndicators(pawns);

            this.AppendBorder();

            return this.image.ToString();
        }

        private void AppendRowsLine()
        {
            // Append white space in the beginning
            this.image.Append(new string(WhiteSpaceSymbol, 4));

            for (int row = 0; row < this.BoardRows; row++)
            {
                this.image.AppendFormat("{0} ", row);
            }

            this.image.AppendLine();
        }

        private void AppendRowAndColumnIndicators(params Pawn[] pawns)
        {
            for (int row = 0; row < this.BoardRows; row++)
            {
                this.image.AppendFormat("{0} {1} ", row, VerticalBorderSymbol);

                for (int col = 0; col < this.BoardColumns; col++)
                {
                    char symbol = this.GetSymbol(row, col, pawns);
                    this.image.AppendFormat("{0} ", symbol);
                }

                this.image.AppendLine(VerticalBorderSymbol.ToString());
            }
        }

        private char GetSymbol(int row, int column, params Pawn[] pawns)
        {
            char symbol = new char();
            bool isPawnSymbol = false;

            for (int i = 0; i < pawns.Length; i++)
            {
                if (pawns[i].XCoordinate == row && pawns[i].YCoordinate == column)
                {
                    isPawnSymbol = true;
                    symbol = pawns[i].Symbol;
                    break;
                }
            }

            if (!isPawnSymbol)
            {
                if ((row + column) % 2 == 0)
                {
                    symbol = WhiteCellSymbol;
                }
                else
                {
                    symbol = BlackCellSymbol;
                }
            }

            return symbol;
        }

        private void AppendBorder()
        {
            // Append white space in the beginning
            this.image.Append(new string(WhiteSpaceSymbol, 3));

            // Append border of '-' symbols
            this.image.AppendLine(new string(HorizontalBorderSymbol, (2 * this.BoardRows) + 1));
        }
    }
}
