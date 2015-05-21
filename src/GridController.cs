namespace kakuro {

  using kakuro.cell;
  using System;
  using System.Collections.Generic;
  using System.Linq;

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
      return rows.Select(r => r.draw()).Aggregate("", (acc, v) => acc + v);
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
        foreach (var c in Enumerable.Range(0, rows[0].size())) {
          foreach (var cell in row[c].Where(cell => cell is Across)) {
            sums.Add(new Sum(((Across)cell).getAcrossTotal(),
              row.Skip(c + 1)
              .TakeWhile(v => v is ValueCell)
              .Cast<ValueCell>()
              .ToList()));
          }
        }
      }
    }

    public void createDownSums() {
      foreach (var r in Enumerable.Range(0, rows.Count)) {
        foreach (var c in Enumerable.Range(0, rows[0].size())) {
          foreach (var cell in rows[r][c].Where(cell => cell is Down)) {
            var vs = new List<ValueCell>();
            for (int pos = r + 1; pos < rows.Count; ++pos) {
              var optV = rows[pos][c];
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

    public void createSums() {
      createAcrossSums();
      createDownSums();
    }

    public int scan() {
      return sums.Sum(s => s.solveStep());
    }

    public string solve() {
      createSums();
      Console.WriteLine(draw());
      while (scan() > 0) {
        Console.WriteLine(draw());
      }
      return draw();
    }

    public static void Main() {
      new TestParse().testAPI();
    }

  }
}

