using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kakuro {
  class Pair<T, U> {

    public T left;
    public U right;

    public Pair(T left, U right) {
      this.left = left;
      this.right = right;
    }

    override public int GetHashCode() {
      int hash = 7;
      hash = 79 * hash + this.left.GetHashCode();
      hash = 79 * hash + this.right.GetHashCode();
      return hash;
    }

    override public bool Equals(System.Object obj) {
      if (obj == null) {
        return false;
      }
      Pair<T, U> that = obj as Pair<T, U>;
      if (null == that) {
        return false;
      }
      return this.left.Equals(that.left) && this.right.Equals(that.right);
    }

    public T getLeft() {
      return left;
    }

    public U getRight() {
      return right;
    }

    override public String ToString() {
      return "Pair[left=" + left.ToString() + ", right=" + right.ToString() + "]";
    }

  }
}
