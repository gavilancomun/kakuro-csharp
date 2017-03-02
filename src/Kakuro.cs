using kakuro.cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kakuro {
  class Kakuro {
    public static ValueCell v(List<int> values) {
      return new ValueCell(values);
    }

    public static ValueCell v() {
      System.Diagnostics.Debug.WriteLine("v");
      return v(1, 2, 3, 4, 5, 6, 7, 8, 9);
    }

    public static ValueCell v(params int[] values) {
      return v(values.ToList());
    }

    public static EmptyCell e() {
      return new EmptyCell();
    }

    public static DownCell d(int d) {
      return new DownCell(d);
    }

    public static AcrossCell a(int a) {
      return new AcrossCell(a);
    }

    public static DownAcrossCell da(int d, int a) {
      return new DownAcrossCell(d, a);
    }

    public static String drawRow(List<ICell> row) {
      return row.Select(c => c.draw())
          .Aggregate("", (acc, v) => acc + v) + "\n";
    }


    public static String drawGrid(List<List<ICell>> grid) {
      return grid.Select(k => drawRow(k))
              .Aggregate("", (acc, v) => acc + v);
    }

    public static bool allDifferent<T>(ICollection<T> nums) {
      return nums.Count == new HashSet<T>(nums).Count;
    }


    public static List<T> conj<T>(List<T> items, T item) {
      List<T> result = new List<T>(items);
      result.Add(item);
      return result;
    }

    public static List<T> concatLists<T>(List<T> a, List<T> b) {
      return a.Concat(b).ToList();
    }
    public static SortedSet<T> asSet<T>(params T[] values) {
      return new SortedSet<T>(values);
    }

    public static List<T> asList<T>(params T[] values) {
      return new List<T>(values);
    }

    public static List<List<T>> product<T>(List<SortedSet<T>> colls) {
      switch (colls.Count) {
        case 0:
          return new List<List<T>>();
        case 1:
          return colls[0].Select(a => asList(a)).ToList();
        default:
          ICollection<T> head = colls[0];
          List<SortedSet<T>> tail = colls.Skip(1).ToList();
          List<List<T>> tailProd = product(tail);
          return head.SelectMany(x => tailProd.Select(ys => concatLists(asList(x), ys)))
                  .ToList();
      }
    }
    public static List<List<int>> permuteAll(List<ValueCell> vs, int target) {
      List<SortedSet<int>> values = vs.Select(v => v.values).ToList();
      return product(values).Where(x => target == x.Sum())
              .ToList();
    }

    public static List<List<T>> transpose<T>(List<List<T>> m) {
      if (0 == m.Count) {
        return new List<List<T>>();
      }
      else {
        return Enumerable.Range(0, m[0].Count)
                .Select(i => m.Select(col => col[i]).ToList())
                .ToList();
      }
    }

    public static bool isPossible(ValueCell v, int n) {
      return v.Contains(n);
    }

    public static IEnumerable<T> takeWhile<T>(Predicate<T> f, List<T> coll) {
      foreach (var item in coll) {
        if (f.Invoke(item)) {
          yield return item;
        }
        else {
          yield break;
        }
      }
    }

    public static List<T> drop<T>(int n, List<T> coll) {
      return coll.Skip(n).ToList();
    }

    public static List<T> take<T>(int n, List<T> coll) {
      return coll.Take(n).ToList();
    }

    public static List<List<T>> partitionBy<T>(Predicate<T> f, List<T> coll) {
      if (0 == coll.Count) {
        return Enumerable.Empty<List<T>>().ToList();
      }
      else {
        T head = coll[0];
        bool fx = f.Invoke(head);
        List<T> group = takeWhile(y => fx == f.Invoke(y), coll).ToList();
        return concatLists(asList(group), partitionBy(f, drop(group.Count, coll)));
      }
    }

    public static List<List<T>> partitionAll<T>(int n, int step, List<T> coll) {
      if (0 == coll.Count) {
        return Enumerable.Empty<List<T>>().ToList();
      }
      else {
        return concatLists(asList(take(n, coll)), partitionAll(n, step, drop(step, coll)));
      }
    }

    public static List<List<T>> partitionN<T>(int n, List<T> coll) {
      return partitionAll(n, n, coll);
    }

    public static List<ValueCell> solveStep(List<ValueCell> cells, int total) {
      int finalIndex = cells.Count - 1;
      var perms = permuteAll(cells, total)
              .Where(v => isPossible(cells.Last(), v[finalIndex]))
              .Where(v => allDifferent(v))
              .ToList();
      return transpose(perms)
              .Select(item => v(item))
              .ToList();
    }

    // returns (non-vals, vals)*
    public static List<List<ICell>> gatherValues(List<ICell> line) {
      return partitionBy(v => (v is ValueCell), line);
    }

    public static List<SimplePair<List<ICell>>> pairTargetsWithValues(List<ICell> line) {
      return partitionN(2, gatherValues(line))
              .Select(part=> new SimplePair<List<ICell>>(part[0], (1 == part.Count) ? new List<ICell>() : part[1]))
              .ToList();
    }

  }
}
