using kakuro.cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kakuro
{
    class Kakuro
    {
        public static ValueCell v(List<int> values)
        {
            return new ValueCell(values);
        }

        public static ValueCell v()
        {
            System.Diagnostics.Debug.WriteLine("v");
            return v(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        public static ValueCell v(params int[] values)
        {
            return v(values.ToList());
        }


        public static EmptyCell e()
        {
            return new EmptyCell();
        }

        public static DownCell d(int d)
        {
            return new DownCell(d);
        }

        public static AcrossCell a(int a)
        {
            return new AcrossCell(a);
        }

        public static DownAcrossCell da(int d, int a)
        {
            return new DownAcrossCell(d, a);
        }

        public static String drawRow(List<Cell> row)
        {
            return row.Select(c => c.draw())
                .Aggregate("", (acc, v) => acc + v) + "\n";
        }

    }
}
