using System.Linq;

using NUnit.Framework;

namespace Kakuro
{
    class Other
    {
        public static bool Even(int n)
        {
            return 0 == n % 2;
        }
        public bool MemberEven(int n)
        {
            return 0 == n % 2;
        }

    }
    class TestCS
    {

        public bool Even(int n)
        {
            return 0 == n % 2;
        }

        [Test]
        public void TestMemberDelegate()
        {
            var result = Enumerable.Range(0, 10).Where(Even).ToList();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void TestRemoteStaticDelegate()
        {
            var result = Enumerable.Range(0, 10).Where(Other.Even).ToList();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void TestRemoteMemberDelegate()
        {
            var other = new Other();
            var result = Enumerable.Range(0, 10).Where(other.MemberEven).ToList();
            Assert.AreEqual(5, result.Count);
        }
    }
}
