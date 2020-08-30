using System;

namespace Kakuro
{
    public class Pair<T, U>
    {
        public T left;
        public U right;

        public Pair(T left, U right)
        {
            this.left = left;
            this.right = right;
        }

        override public int GetHashCode()
        {
            int hash = 7;
            hash = (79 * hash) + left.GetHashCode();
            return (79 * hash) + right.GetHashCode();
        }

        override public bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var that = obj as Pair<T, U>;
            if (that == null)
            {
                return false;
            }
            else
            {
                return left.Equals(that.left) && right.Equals(that.right);
            }
        }

        public T GetLeft()
        {
            return left;
        }

        public U GetRight()
        {
            return right;
        }

        override public String ToString()
        {
            return "Pair[left=" + left.ToString() + ", right=" + right.ToString() + "]";
        }
    }
}
