namespace kakuro {

  using System.Collections.Generic;

  using kakuro.cell;

public class RowDef {

List<Cell> cells = new List<Cell>();

public RowDef() {
}

public int size() {
  return cells.Count;
}

public string draw() {
  string result = "";
  foreach (Cell cell in cells) {
    result += cell.draw();
  }
  result += "\n";
  return result;
}

public RowDef addEmpty() {
  cells.Add(new EmptyCell());
  return this;
}

public RowDef addValue(int n) {
  for (int i = 1; i <= n; ++i) {
    cells.Add(new ValueCell());
  }
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
}
}


