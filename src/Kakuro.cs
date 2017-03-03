using kakuro.cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kakuro {
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

    public static String drawRow(IList<ICell> row) {
      return row.Select(c => c.draw())
          .Aggregate("", (acc, v) => acc + v) + "\n";
    }


    public static String drawGrid(IList<List<ICell>> grid) {
      return grid.Select(k => drawRow(k))
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
    public static ISet<T> asSet<T>(params T[] values) {
      return new SortedSet<T>(values);
    }

    public static List<T> asList<T>(params T[] values) {
      return new List<T>(values);
    }

    public static IList<List<T>> product<T>(List<ISet<T>> colls) {
      switch (colls.Count) {
        case 0:
          return new List<List<T>>();
        case 1:
          return colls[0].Select(a => asList(a)).ToList();
        default:
          var head = colls[0];
          var tail = colls.Skip(1).ToList();
          var tailProd = product(tail);
          return head.SelectMany(x => tailProd.Select(ys => concatLists(asList(x), ys)))
                  .ToList();
      }
    }
    public static IList<List<int>> permuteAll(IList<ValueCell> vs, int target) {
      var values = vs.Select(v => v.values).ToList();
      return product(values).Where(x => target == x.Sum())
              .ToList();
    }

    public static IList<List<T>> transpose<T>(IList<List<T>> m) {
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

    public static IEnumerable<T> takeWhile<T>(Predicate<T> f, IList<T> coll) {
      foreach (var item in coll) {
        if (f.Invoke(item)) {
          yield return item;
        }
        else {
          yield break;
        }
      }
    }

    public static List<T> drop<T>(int n, IList<T> coll) {
      return coll.Skip(n).ToList();
    }

    public static List<T> take<T>(int n, IList<T> coll) {
      return coll.Take(n).ToList();
    }

    public static List<List<T>> partitionBy<T>(Predicate<T> f, IList<T> coll) {
      if (0 == coll.Count) {
        return Enumerable.Empty<List<T>>().ToList();
      }
      else {
        T head = coll[0];
        bool fx = f.Invoke(head);
        var group = takeWhile(y => fx == f.Invoke(y), coll).ToList();
        return concatLists(asList(group), partitionBy(f, drop(group.Count, coll)));
      }
    }

    public static List<List<T>> partitionAll<T>(int n, int step, IList<T> coll) {
      if (0 == coll.Count) {
        return Enumerable.Empty<List<T>>().ToList();
      }
      else {
        return concatLists(asList(take(n, coll)), partitionAll(n, step, drop(step, coll)));
      }
    }

    public static List<List<T>> partitionN<T>(int n, IList<T> coll) {
      return partitionAll(n, n, coll);
    }

    public static List<ValueCell> solveStep(IList<ValueCell> cells, int total) {
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
    public static List<List<ICell>> gatherValues(IList<ICell> line) {
      return partitionBy(v => (v is ValueCell), line);
    }

    public static List<SimplePair<List<ICell>>> pairTargetsWithValues(IList<ICell> line) {
      return partitionN(2, gatherValues(line))
              .Select(part => new SimplePair<List<ICell>>(part[0], (1 == part.Count) ? new List<ICell>() : part[1]))
              .ToList();
    }

    public static List<ICell> solvePair(Func<ICell, int> f, SimplePair<List<ICell>> pair) {
      var notValueCells = pair.left;
      if (0 == pair.right.Count) {
        return notValueCells;
      }
      else {
        var valueCells = pair.right.Select(cell => (ValueCell)cell).ToList();
        var newValueCells = solveStep(valueCells, f.Invoke(notValueCells.Last()));
        return notValueCells.Concat(newValueCells).ToList();
      }
    }

    public static List<ICell> solveLine(IList<ICell> line, Func<ICell, int> f) {
      return pairTargetsWithValues(line)
              .SelectMany(pair => solvePair(f, pair))
              .ToList();
    }

    public static List<ICell> solveRow(IList<ICell> row) {
      return solveLine(row, x => ((IAcross)x).getAcross());
    }

    public static List<ICell> solveColumn(IList<ICell> column) {
      return solveLine(column, x => ((IDown)x).getDown());
    }

    public static IList<List<ICell>> solveGrid(IList<List<ICell>> grid) {
      var rowsDone = grid.Select(r => solveRow(r)).ToList();
      var colsDone = transpose(rowsDone).Select(c => solveColumn(c)).ToList();
      return transpose(colsDone);
    }

    public static bool gridEquals(IList<List<ICell>> g1, IList<List<ICell>> g2) {
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

    public static IList<List<ICell>> solver(IList<List<ICell>> grid) {
      Console.WriteLine(drawGrid(grid));
      var g = solveGrid(grid);
      if (gridEquals(g, grid)) {
        return g;
      }
      else {
        return solver(g);
      }
    }

    public static void Main() {
      var grid1 = asList(
           asList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
           asList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
           asList<ICell>(a(18), v(), v(), v(), v(), v()),
           asList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
           asList<ICell>(a(9), v(), v(), a(6), v(), v()),
           asList<ICell>(a(15), v(), v(), a(12), v(), v()));
      var result = solver(grid1);
    }

  }
}
