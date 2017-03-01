using kakuro.cell;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static kakuro.Kakuro;

namespace kakuro
{
    class TestKakuro2
    {

        [Test]
        public void testDrawEmpty()
        {
            String result = e().draw();
            Assert.AreEqual("   -----  ", result);
        }

        [Test]
        public void testDrawAcross()
        {
            String result = a(5).draw();
            Assert.AreEqual("   --\\ 5  ", result);
        }

        [Test]
        public void testDrawDown()
        {
            String result = d(4).draw();
            Assert.AreEqual("    4\\--  ", result);
        }

        [Test]
        public void testDrawDownAcross()
        {
            String result = da(3, 4).draw();
            Assert.AreEqual("    3\\ 4  ", result);
        }

        [Test]
        public void testDrawValues()
        {
            String result = v().draw();
            Assert.AreEqual(" 123456789", result);
            String result12 = v(1, 2).draw();
            Assert.AreEqual(" 12.......", result12);
        }

        [Test]
        public void testDrawRow()
        {
            List<Cell> line = asList<Cell>(da(3, 4), v(), v(1, 2), d(4), e(), a(5), v(4), v(1));
            String result = drawRow(line);
            Assert.AreEqual("    3\\ 4   123456789 12.......    4\\--     -----     --\\ 5       4         1    \n", result);
        }

        [Test]
        public void testProduct()
        {
            List<SortedSet<int>> data = asList(asSet(1, 2), asSet(10), asSet(100, 200, 300));
            List<List<int>> expected = asList(
              asList(1, 10, 100),
              asList(1, 10, 200),
              asList(1, 10, 300),
              asList(2, 10, 100),
              asList(2, 10, 200),
              asList(2, 10, 300));
            Assert.AreEqual(expected, product(data));
        }

        [Test]
        public void testPermute()
        {
            List<ValueCell> vs = asList(v(), v(), v());
            List<List<int>> results = permuteAll(vs, 6);
            Assert.AreEqual(10, results.Count);
            List<List<int>> diff = results.Where(k => allDifferent(k))
              .ToList();
            Assert.AreEqual(6, diff.Count);
        }

        [Test]
        public void testTranspose()
        {
            List<List<int>> ints = Enumerable.Range(0, 3)
              .Select(i => Enumerable.Range(0, 4).ToList())
              .ToList();
            List<List<int>> tr = transpose(ints);
            Assert.AreEqual(ints.Count, tr[0].Count);
            Assert.AreEqual(ints[0].Count, tr.Count);
        }

        [Test]
        public void testValueEquality()
        {
            Assert.AreEqual(v(), v());
            Assert.AreEqual(v(1, 2), v(1, 2));
        }

       [Test]
public void testIsPoss()
        {
            ValueCell vc = v(1, 2, 3);
            Assert.AreEqual(true, isPossible(vc, 2));
            Assert.AreEqual(false, isPossible(vc, 4));
        }

    }
}
