namespace kakuro {

  using System.Collections.Generic;

  using kakuro.cell;

public class Possible {

internal ValueCell cell;
internal SortedSet<int> values = new SortedSet<int>();

public Possible(ValueCell cell) {
  this.cell = cell;
}

/**
 * @return the values
 */
public SortedSet<int> getValues() {
  return values;
}

/**
 * @param values the values to set
 */
public void setValues(SortedSet<int> values) {
  this.values = values;
}

/**
 * @return the cell
 */
public ValueCell getCell() {
  return cell;
}

public void Add(int n) {
  values.Add(n);
}

public int update() {
  int previousSize = cell.size();
  cell.setValues(values);
  return previousSize - cell.size();
}

}

}

