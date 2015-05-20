namespace kakuro {

  using kakuro.cell;
  using System.Collections.Generic;

public class Possible {

private ValueCell cell;
private SortedSet<int> values = new SortedSet<int>();

public Possible(ValueCell cell) {
  this.cell = cell;
}

public void Add(int n) {
  values.Add(n);
}

public int update() {
  int previousSize = cell.size();
  cell.values = values;
  return previousSize - cell.size();
}

}

}

