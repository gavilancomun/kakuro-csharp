namespace kakuro {

  using kakuro.cell;
  using System.Collections.Generic;
  using System.Linq;

public class RowDef {

List<Cell> cells = new List<Cell>();

public RowDef() {
}

public int size() {
  return cells.Count;
}

public string draw() {
  return cells.Aggregate("", (acc, v) => acc + v.draw()) + "\n";
}

public RowDef addEmpty() {
  cells.Add(new EmptyCell());
  return this;
}

public RowDef addValue(int n) {
  cells.AddRange(Enumerable.Range(1, n).Select(i => new ValueCell()).ToList());
  return this;
}

public RowDef addDown(int n) {
  cells.Add(new DownCell(n));
  return this;
}

public RowDef addAcross(int n) {
  cells.Add(new AcrossCell(n));
  return this;
}

public RowDef addDownAcross(int down, int across) {
  cells.Add(new DownAcrossCell(down, across));
  return this;
}

public Optional<Cell> get(int i) {
  return (i >= cells.Count) ? Optional<Cell>.empty() : Optional<Cell>.ofNullable(cells[i]);
}

public IEnumerable<Cell> Skip(int i) {
  return cells.Skip(i);
}
}
}


