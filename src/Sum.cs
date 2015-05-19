namespace kakuro {
  using kakuro.cell;
  using System.Collections.Generic;
  using System.Linq;

public class Sum {

private int total;
private List<ValueCell> cells = new List<ValueCell>();

public Sum(int total, List<ValueCell> valueCells) {
  this.total = total;
  cells.AddRange(valueCells);
}

// All different is part of the definition of a kakuro puzzle
private bool areAllDifferent(List<int> candidates) {
  return (new SortedSet<int>(candidates).Count == candidates.Count);
}

private List<int> copyAdd(List<int> vs, int v) {
  var result = new List<int>(vs);
  result.Add(v);
  return result;
}

private List<List<int>> permute(int pos, int target, List<int> soFar) {
  if (target >= 1) {
    if (pos == (cells.Count - 1)) {
      var result = new List<List<int>>();
      result.Add(copyAdd(soFar, target));
      return result;
    }
    else {
      var result = new List<List<int>>();
      foreach (var v in cells[pos].values) {
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
  var possibles = cells.Select(c => new Possible(c)).ToList();
  int last = cells.Count - 1;
  var filtered = permuteAll()
          .Where(p => cells[last].isPossible(p[last]))
          .Where(p => areAllDifferent(p));
  foreach (var p in filtered) {
    foreach (var i in Enumerable.Range(0, last + 1)) {
      possibles[i].Add(p[i]);
    }
  }
  return possibles.Sum(v => v.update());
}

}

}