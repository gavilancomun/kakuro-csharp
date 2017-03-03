using Kakuro.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kakuro {
  class Kakuro {
    public static ValueCell v(ICollection<int> values) {
      return new ValueCell(values);
    }

    public static ValueCell v() {
      return new ValueCell();
    }

    public static ValueCell v(params int[] values) {
      return new ValueCell(values);
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

    public static string DrawRow(IList<ICell> row) {
      return row.Select(c => c.draw())
          .Aggregate("", (acc, v) => acc + v) + "\n";
    }


    public static string DrawGrid(IList<List<ICell>> grid) {
      return grid.Select(k => DrawRow(k))
              .Aggregate("", (acc, v) => acc + v);
    }

    public static bool allDifferent<T>(ICollection<T> nums) {
      return nums.Count == new HashSet<T>(nums).Count;
    }


    public static IList<T> conj<T>(IList<T> items, T item) {
      List<T> result = new List<T>(items);
      result.Add(item);
      return result;
    }

    public static List<T> concatLists<T>(IList<T> a, IList<T> b) {
      return a.Concat(b).ToList();
    }
    public static ISet<T> AsSet<T>(params T[] values) {
      return new SortedSet<T>(values);
    }

    public static List<T> AsList<T>(params T[] values) {
      return new List<T>(values);
    }

    public static IList<List<T>> Product<T>(List<ISet<T>> colls) {
      switch (colls.Count) {
        case 0:
          return new List<List<T>>();
        case 1:
          return colls[0].Select(a => AsList(a)).ToList();
        default:
          var head = colls[0];
          var tail = colls.Skip(1).ToList();
          var tailProd = Product(tail);
          return head.SelectMany(x => tailProd.Select(ys => concatLists(AsList(x), ys)))
                  .ToList();
      }
    }
    public static IList<List<int>> PermuteAll(IList<ValueCell> vs, int target) {
      var values = vs.Select(v => v.values).ToList();
      return Product(values).Where(x => target == x.Sum())
              .ToList();
    }

    public static IList<List<T>> Transpose<T>(IList<List<T>> m) {
      if (0 == m.Count) {
        return new List<List<T>>();
      }
      else {
        return Enumerable.Range(0, m[0].Count)
                .Select(i => m.Select(col => col[i]).ToList())
                .ToList();
      }
    }

    public static bool IsPossible(ValueCell v, int n) {
      return v.Contains(n);
    }

    public static IEnumerable<T> TakeWhile<T>(Predicate<T> f, IList<T> coll) {
      foreach (var item in coll) {
        if (f.Invoke(item)) {
          yield return item;
        }
        else {
          yield break;
        }
      }
    }

    public static List<T> Drop<T>(int n, IList<T> coll) {
      return coll.Skip(n).ToList();
    }

    public static List<T> Take<T>(int n, IList<T> coll) {
      return coll.Take(n).ToList();
    }

    public static List<List<T>> PartitionBy<T>(Predicate<T> f, IList<T> coll) {
      if (0 == coll.Count) {
        return Enumerable.Empty<List<T>>().ToList();
      }
      else {
        T head = coll[0];
        bool fx = f.Invoke(head);
        var group = TakeWhile(y => fx == f.Invoke(y), coll).ToList();
        return concatLists(AsList(group), PartitionBy(f, Drop(group.Count, coll)));
      }
    }

    public static List<List<T>> PartitionAll<T>(int n, int step, IList<T> coll) {
      if (0 == coll.Count) {
        return Enumerable.Empty<List<T>>().ToList();
      }
      else {
        return concatLists(AsList(Take(n, coll)), PartitionAll(n, step, Drop(step, coll)));
      }
    }

    public static List<List<T>> PartitionN<T>(int n, IList<T> coll) {
      return PartitionAll(n, n, coll);
    }

    public static List<ValueCell> SolveStep(IList<ValueCell> cells, int total) {
      int finalIndex = cells.Count - 1;
      var perms = PermuteAll(cells, total)
              .Where(v => IsPossible(cells.Last(), v[finalIndex]))
              .Where(v => allDifferent(v))
              .ToList();
      return Transpose(perms)
              .Select(item => v(item))
              .ToList();
    }

    // returns (non-vals, vals)*
    public static List<List<ICell>> GatherValues(IList<ICell> line) {
      return PartitionBy(v => (v is ValueCell), line);
    }

    public static List<SimplePair<List<ICell>>> PairTargetsWithValues(IList<ICell> line) {
      return PartitionN(2, GatherValues(line))
              .Select(part => new SimplePair<List<ICell>>(part[0], (1 == part.Count) ? new List<ICell>() : part[1]))
              .ToList();
    }

    public static List<ICell> SolvePair(Func<ICell, int> f, SimplePair<List<ICell>> pair) {
      var notValueCells = pair.left;
      if (0 == pair.right.Count) {
        return notValueCells;
      }
      else {
        var valueCells = pair.right.Select(cell => (ValueCell)cell).ToList();
        var newValueCells = SolveStep(valueCells, f.Invoke(notValueCells.Last()));
        return notValueCells.Concat(newValueCells).ToList();
      }
    }

    public static List<ICell> SolveLine(IList<ICell> line, Func<ICell, int> f) {
      return PairTargetsWithValues(line)
              .SelectMany(pair => SolvePair(f, pair))
              .ToList();
    }

    public static List<ICell> SolveRow(IList<ICell> row) {
      return SolveLine(row, x => ((IAcross)x).getAcross());
    }

    public static List<ICell> SolveColumn(IList<ICell> column) {
      return SolveLine(column, x => ((IDown)x).getDown());
    }

    public static IList<List<ICell>> SolveGrid(IList<List<ICell>> grid) {
      var rowsDone = grid.Select(r => SolveRow(r)).ToList();
      var colsDone = Transpose(rowsDone).Select(c => SolveColumn(c)).ToList();
      return Transpose(colsDone);
    }

    public static bool GridEquals(IList<List<ICell>> g1, IList<List<ICell>> g2) {
      if (g1.Count == g2.Count) {
        return Enumerable.Range(0, g1.Count).All(i => {
          var xi = g1[i];
          var yi = g2[i];
          return Enumerable.Range(0, xi.Count).All(j => (xi.Count == yi.Count) && xi[j].Equals(yi[j]));
        });
      }
      else {
        return false;
      }
    }

    public static IList<List<ICell>> Solver(IList<List<ICell>> grid) {
      Console.WriteLine(DrawGrid(grid));
      var g = SolveGrid(grid);
      if (GridEquals(g, grid)) {
        return g;
      }
      else {
        return Solver(g);
      }
    }

    public static void Main() {
      var grid1 = AsList(
           AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
           AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
           AsList<ICell>(a(18), v(), v(), v(), v(), v()),
           AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
           AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
           AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      var result = Solver(grid1);
    }

  }
}
