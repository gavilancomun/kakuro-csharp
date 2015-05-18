namespace kakuro {
  using System.Collections.Generic;
  using System.Linq;
  using kakuro.cell;

public class Sum {

internal int total;
internal List<ValueCell> cells = new List<ValueCell>();

public Sum(int total, List<ValueCell> valueCells) {
  this.total = total;
  cells.AddRange(valueCells);
}

// All different is part of the definition of a kakuro puzzle
private bool areAllDifferent(List<int> candidates) {
  return (new SortedSet<int>(candidates).Count == candidates.Count);
}

private List<int> copyAdd(List<int> vs, int v) {
  List<int> result = new List<int>(vs);
  result.Add(v);
  return result;
}

private List<List<int>> permute(int pos, int target, List<int> soFar) {
  if (target >= 1) {
    if (pos == (cells.Count - 1)) {
      List<List<int>> result = new List<List<int>>();
      result.Add(copyAdd(soFar, target));
      return result;
    }
    else {
      List<List<int>> result = new List<List<int>>();
      foreach (int v in cells[pos].getValues()) {
        result.AddRange(permute(pos + 1, target - v, copyAdd(soFar, v)));
      }
      return result;
    }
  }
  else {
    return new List<List<int>>();
  }
}

// Exhaustive search for possible solutions
private List<List<int>> permuteAll() {
  return permute(0, total, new List<int>());
}

public int solveStep() {
  List<Possible> possibles = new List<Possible>();
  foreach (ValueCell cell in cells) {
    possibles.Add(new Possible(cell));
  }
  int last = cells.Count - 1;
  List<List<int>> filtered = permuteAll()
          .Where(p => cells[last].isPossible(p[last]))
          .Where(p => areAllDifferent(p))
          .ToList();
  foreach (List<int> p in filtered) {
    for (int i = 0; i <= last; ++i) {
      possibles[i].Add(p[i]);
    }
  }
  int sum = 0;
  foreach (Possible p in possibles) {
    sum += p.update();
  }
  return sum;
}

}

}