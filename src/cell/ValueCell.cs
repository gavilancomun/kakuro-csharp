using System;
using System.Linq;
using System.Collections.Generic;

namespace kakuro.cell {

	public class ValueCell : Cell {

	public SortedSet<int> values { get; set; }

	public ValueCell() {
	  values = new SortedSet<int>{1, 2, 3, 4, 5, 6, 7, 8, 9};
	}

	public virtual bool isPossible(int value) {
	  return values.Contains(value);
	}

  public virtual string draw() {
    if (1 == values.Count) {
      return values.Select(v => "     " + v + "    ").ToList()[0];
    }
    else {
      return Enumerable.Range(1, 9).Aggregate(" ", (acc, v) => acc + (isPossible(v) ? v.ToString() : "."));
    }
  }

	public virtual int size() {
	  return values.Count;
	}

	}

}
