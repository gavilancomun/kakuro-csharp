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


    }
}
