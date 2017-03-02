using kakuro.cell;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static kakuro.Kakuro;

namespace kakuro {
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
      var line = asList<Cell>(da(3, 4), v(), v(1, 2), d(4), e(), a(5), v(4), v(1));
      String result = drawRow(line);
      Assert.AreEqual("    3\\ 4   123456789 12.......    4\\--     -----     --\\ 5       4         1    \n", result);
    }

    [Test]
    public void testProduct() {
      var data = asList(asSet(1, 2), asSet(10), asSet(100, 200, 300));
      var expected = asList(
        asList(1, 10, 100),
        asList(1, 10, 200),
        asList(1, 10, 300),
        asList(2, 10, 100),
        asList(2, 10, 200),
        asList(2, 10, 300));
      Assert.AreEqual(expected, product(data));
    }

    [Test]
    public void testPermute() {
      var vs = asList(v(), v(), v());
      var results = permuteAll(vs, 6);
      Assert.AreEqual(10, results.Count);
      var diff = results.Where(k => allDifferent(k)).ToList();
      Assert.AreEqual(6, diff.Count);
    }

    [Test]
    public void testTranspose() {
      var ints = Enumerable.Range(0, 3)
        .Select(i => Enumerable.Range(0, 4).ToList())
        .ToList();
      var tr = transpose(ints);
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
      Assert.AreEqual(true, isPossible(vc, 2));
      Assert.AreEqual(false, isPossible(vc, 4));
    }

    [Test]
    public void testTakeWhile() {
      var result = takeWhile(n => n < 4, Enumerable.Range(0, 10).ToList()).ToList();
      Assert.AreEqual(4, result.Count);
    }

    [Test]
    public void testTakeWhile2() {
      var result = takeWhile(n => (n < 4) || (n > 6), Enumerable.Range(0, 10).ToList()).ToList();
      Assert.AreEqual(4, result.Count);
    }

  }
}
