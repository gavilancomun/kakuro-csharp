﻿using kakuro.cell;
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


        public static String drawGrid(List<List<Cell>> grid)
        {
            return grid.Select(k => drawRow(k))
                    .Aggregate("", (acc, v) => acc + v);
        }

        public static bool allDifferent<T>(ICollection<T> nums)
        {
            return nums.Count == new HashSet<T>(nums).Count;
        }


        public static List<T> conj<T>(List<T> items, T item)
        {
            List<T> result = new List<T>(items);
            result.Add(item);
            return result;
        }

        public static List<T> concatLists<T>(List<T> a, List<T> b)
        {
            return a.Concat(b).ToList();
        }
        public static HashSet<T> asSet<T>(params T[] values)
        {
            return new HashSet<T>(values);
        }

        public static List<T> asList<T>(params T[] values)
        {
            return new List<T>(values);
        }

        public static List<List<T>> product<T>(List<HashSet<T>> colls)
        {
            switch (colls.Count)
            {
                case 0:
                    return new List<List<T>>();
                case 1:
                    return colls[0].Select(a => asList(a)).ToList();
                default:
                    ICollection<T> head = colls[0];
                    List<HashSet<T>> tail = colls.Skip(1).ToList();
                    List<List<T>> tailProd = product(tail);
                    return head.SelectMany(x => tailProd.Select(ys => concatLists(asList(x), ys)))
                            .ToList();
            }
        }

    }
}
