using System;
using System.Linq;
using System.Collections.Generic;

namespace Kakuro.Cell {

  public class ValueCell : ICell {

    public readonly ISet<int> values;

    public ValueCell() {
      values = new SortedSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    public ValueCell(ICollection<int> coll) {
      values = new SortedSet<int>(coll);
    }

    public virtual bool IsPossible(int value) {
      return values.Contains(value);
    }

    public virtual string Draw() {
      System.Diagnostics.Debug.WriteLine("values " + values);
      if (1 == values.Count) {
        return values.Select(v => "     " + v + "    ").First();
      }
      else {
        return Enumerable.Range(1, 9).Aggregate(" ", (acc, v) => acc + (IsPossible(v) ? v.ToString() : "."));
      }
    }

    public override bool Equals(object obj) {
      ValueCell that = obj as ValueCell;
      return this.values.SetEquals(that.values);
    }

    public override int GetHashCode() {
      return values.Sum();
    }

    public bool Contains(int n) {
      return values.Contains(n);
    }

  }

}
