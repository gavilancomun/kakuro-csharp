using Kakuro.Cell;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Kakuro.Kakuro;

namespace Kakuro {
  class TestKakuro2 {

    [Test]
    public void testDrawEmpty() {
      var result = e().draw();
      Assert.AreEqual("   -----  ", result);
    }

    [Test]
    public void testDrawAcross() {
      var result = a(5).draw();
      Assert.AreEqual("   --\\ 5  ", result);
    }

    [Test]
    public void testDrawDown() {
      var result = d(4).draw();
      Assert.AreEqual("    4\\--  ", result);
    }

    [Test]
    public void testDrawDownAcross() {
      var result = da(3, 4).draw();
      Assert.AreEqual("    3\\ 4  ", result);
    }

    [Test]
    public void testDrawValues() {
      var result = v().draw();
      Assert.AreEqual(" 123456789", result);
      var result12 = v(1, 2).draw();
      Assert.AreEqual(" 12.......", result12);
    }

    [Test]
    public void testDrawRow() {
      var line = AsList<ICell>(da(3, 4), v(), v(1, 2), d(4), e(), a(5), v(4), v(1));
      String result = DrawRow(line);
      Assert.AreEqual("    3\\ 4   123456789 12.......    4\\--     -----     --\\ 5       4         1    \n", result);
    }

    [Test]
    public void testProduct() {
      var data = AsList(AsSet(1, 2), AsSet(10), AsSet(100, 200, 300));
      var expected = AsList(
        AsList(1, 10, 100),
        AsList(1, 10, 200),
        AsList(1, 10, 300),
        AsList(2, 10, 100),
        AsList(2, 10, 200),
        AsList(2, 10, 300));
      Assert.AreEqual(expected, Product(data));
    }

    [Test]
    public void testPermute() {
      var vs = AsList(v(), v(), v());
      var results = PermuteAll(vs, 6);
      Assert.AreEqual(10, results.Count);
      var diff = results.Where(k => allDifferent(k)).ToList();
      Assert.AreEqual(6, diff.Count);
    }

    [Test]
    public void testTranspose() {
      var ints = Enumerable.Range(0, 3)
        .Select(i => Enumerable.Range(0, 4).ToList())
        .ToList();
      var tr = Transpose(ints);
      Assert.AreEqual(ints.Count, tr[0].Count);
      Assert.AreEqual(ints[0].Count, tr.Count);
    }

    [Test]
    public void testValueEquality() {
      Assert.AreEqual(v(), v());
      Assert.AreEqual(v(1, 2), v(1, 2));
    }

    [Test]
    public void testIsPoss() {
      var vc = v(1, 2, 3);
      Assert.AreEqual(true, IsPossible(vc, 2));
      Assert.AreEqual(false, IsPossible(vc, 4));
    }

    [Test]
    public void testTakeWhile() {
      var result = TakeWhile(n => n < 4, Enumerable.Range(0, 10).ToList()).ToList();
      Assert.AreEqual(4, result.Count);
    }

    [Test]
    public void testTakeWhile2() {
      var result = TakeWhile(n => (n < 4) || (n > 6), Enumerable.Range(0, 10).ToList()).ToList();
      Assert.AreEqual(4, result.Count);
    }

    [Test]
    public void testConcat() {
      var a = AsList(1, 2, 3);
      var b = AsList(4, 5, 6, 1, 2, 3);
      var result = concatLists(a, b);
      Assert.AreEqual(9, result.Count);
    }

    [Test]
    public void testDrop() {
      var a = AsList(1, 2, 3, 4, 5, 6);
      var result = Drop(4, a);
      Assert.AreEqual(2, result.Count);
    }

    [Test]
    public void testTake() {
      var a = AsList(1, 2, 3, 4, 5, 6);
      var result = Take(4, a);
      Assert.AreEqual(4, result.Count);
    }

    [Test]
    public void testPartBy() {
      var data = AsList(1, 2, 2, 2, 3, 4, 5, 5, 6, 7, 7, 8, 9);
      var result = PartitionBy(n => 0 == (n % 2), data);
      Assert.AreEqual(9, result.Count);
    }

    [Test]
    public void testPartAll() {
      var data = AsList(1, 2, 2, 2, 3, 4, 5, 5, 6, 7, 7, 8, 9);
      var result = PartitionAll(5, 3, data);
      Assert.AreEqual(5, result.Count);
    }

    [Test]
    public void testPartN() {
      var data = AsList(1, 2, 2, 2, 3, 4, 5, 5, 6, 7, 7, 8, 9);
      var result = PartitionN(5, data);
      Assert.AreEqual(3, result.Count);
    }

    [Test]
    public void testSolveStep() {
      List<ValueCell> result = SolveStep(AsList(v(1, 2), v()), 5);
      Assert.AreEqual(v(1, 2), result[0]);
      Assert.AreEqual(v(3, 4), result[1]);
    }

    [Test]
    public void testGatherValues() {
      var line = AsList<ICell>(da(3, 4), v(), v(), d(4), e(), a(4), v(), v());
      var result = GatherValues(line);
      Assert.AreEqual(4, result.Count);
      Assert.AreEqual(da(3, 4), result[0][0]);
      Assert.AreEqual(d(4), result[2][0]);
      Assert.AreEqual(e(), result[2][1]);
      Assert.AreEqual(a(4), result[2][2]);
    }

    [Test]
    public void testPairTargets() {
      var line = AsList<ICell>(da(3, 4), v(), v(), d(4), e(), a(4), v(), v());
      var result = PairTargetsWithValues(line);
      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(da(3, 4), result[0].left[0]);
      Assert.AreEqual(d(4), result[1].left[0]);
      Assert.AreEqual(e(), result[1].left[1]);
      Assert.AreEqual(a(4), result[1].left[2]);
    }

    [Test]
    public void testSolvePair() {
      var line = AsList<ICell>(da(3, 4), v(), v(), d(4), e(), a(4), v(), v());
      var pairs = PairTargetsWithValues(line);
      var pair = pairs[0];
      var result = SolvePair(cell => ((IDown)cell).getDown(), pair);
      Assert.AreEqual(3, result.Count);
      Assert.AreEqual(v(1, 2), result[1]);
      Assert.AreEqual(v(1, 2), result[2]);
    }

    [Test]
    public void testSolveLine() {
      var line = AsList<ICell>(da(3, 4), v(), v(), d(4), e(), a(5), v(), v());
      var result = SolveLine(line, x => ((IAcross)x).getAcross());
      Assert.AreEqual(8, result.Count);
      Assert.AreEqual(v(1, 3), result[1]);
      Assert.AreEqual(v(1, 3), result[2]);
      Assert.AreEqual(v(1, 2, 3, 4), result[6]);
      Assert.AreEqual(v(1, 2, 3, 4), result[7]);
    }

    [Test]
    public void testSolveRow() {
      var result = SolveRow(AsList<ICell>(a(3), v(1, 2, 3), v(1)));
      Assert.AreEqual(v(2), result[1]);
      Assert.AreEqual(v(1), result[2]);
    }

    [Test]
    public void testSolveCol() {
      var result = SolveColumn(AsList<ICell>(da(3, 12), v(1, 2, 3), v(1)));
      Assert.AreEqual(v(2), result[1]);
      Assert.AreEqual(v(1), result[2]);
    }

    [Test]
    public void testGridEquals() {
      var grid1 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      var grid2 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      Assert.AreEqual(true, GridEquals(grid1, grid2));
    }

    [Test]
    public void testGridEquals2() {
      var grid1 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      var grid2 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      Assert.AreEqual(false, GridEquals(grid1, grid2));
    }

    [Test]
    public void testGridEquals3() {
      var grid1 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      var grid2 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      Assert.AreEqual(false, GridEquals(grid1, grid2));
    }

    [Test]
    public void testSolver() {
      var grid1 = AsList(
              AsList<ICell>(e(), d(4), d(22), e(), d(16), d(3)),
              AsList<ICell>(a(3), v(), v(), da(16, 6), v(), v()),
              AsList<ICell>(a(18), v(), v(), v(), v(), v()),
              AsList<ICell>(e(), da(17, 23), v(), v(), v(), d(14)),
              AsList<ICell>(a(9), v(), v(), a(6), v(), v()),
              AsList<ICell>(a(15), v(), v(), a(12), v(), v()));
      var result = Solver(grid1);
      Assert.AreEqual("   --\\ 3       1         2       16\\ 6       4         2    \n", DrawRow(result[1]));
      Assert.AreEqual("   --\\18       3         5         7         2         1    \n", DrawRow(result[2]));
      Assert.AreEqual("   -----     17\\23       8         9         6       14\\--  \n", DrawRow(result[3]));
      Assert.AreEqual("   --\\ 9       8         1       --\\ 6       1         5    \n", DrawRow(result[4]));
      Assert.AreEqual("   --\\15       9         6       --\\12       3         9    \n", DrawRow(result[5]));
    }

  }
}
