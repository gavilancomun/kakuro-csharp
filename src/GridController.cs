namespace kakuro {

  using System.Collections.Generic;
  using System;
  using System.Linq;

  using kakuro.cell;

  public class GridController {

    List<RowDef> rows = new List<RowDef>();
    List<Sum> sums = new List<Sum>();
    RowDef currentRowDef;

    public GridController() {
    }

    public RowDef createRow() {
      currentRowDef = new RowDef();
      rows.Add(currentRowDef);
      return currentRowDef;
    }

    public string draw() {
      List<string> results = rows.Select(r => r.draw()).ToList();
      string result = "";
      foreach (String s in results) {
        result += s;
      }
      return result;
    }

    public void addEmpty() {
      currentRowDef.addEmpty();
    }

    public void addValue(int n) {
      currentRowDef.addValue(n);
    }

    public void addDown(int n) {
      currentRowDef.addDown(n);
    }

    public void addAcross(int n) {
      currentRowDef.addAcross(n);
    }

    public void addDownAcross(int down, int across) {
      currentRowDef.addDownAcross(down, across);
    }

    public void createAcrossSums() {
      foreach (RowDef row in rows) {
        for (int c = 0; c < rows[0].size(); ++c) {
          Optional<Cell> optCell = row.get(c);
          if (optCell.HasValue) {
            Cell cell = optCell.get();
            if (cell is Across) {
              sums.Add(new Sum(((Across)cell).getAcrossTotal(),
                row.Skip(c + 1)
                .TakeWhile(v => v is ValueCell)
                .Cast<ValueCell>()
                .ToList()));
            }
          }
        }
      }
    }

    public void createDownSums() {
      for (int r = 0; r < rows.Count; ++r) {
        for (int c = 0; c < rows[0].size(); ++c) {
          Optional<Cell> optCell = rows[r].get(c);
          if (optCell.HasValue) {
            Cell cell = optCell.get();
            if (cell is Down) {
              List<ValueCell> vs = new List<ValueCell>();
              for (int pos = r + 1; pos < rows.Count; ++pos) {
                Optional<Cell> optV = rows[pos].get(c);
                if (optV.HasValue) {
                  Cell v = optV.get();
                  if (v is ValueCell) {
                    vs.Add((ValueCell)v);
                  }
                  else {
                    break;
                  }
                }
              }
              sums.Add(new Sum(((Down)cell).getDownTotal(), vs));
            }
          }
        }
      }
    }

    public void createSums() {
      createAcrossSums();
      createDownSums();
    }

    public int scan() {
      return sums.Sum(s => s.solveStep());
    }

    public void solve() {
      createSums();
      Console.WriteLine(draw());
      while (scan() > 0) {
        Console.WriteLine(draw());
      }
    }

  }
}

