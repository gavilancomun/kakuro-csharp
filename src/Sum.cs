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
private static bool areAllDifferent(List<int> candidates) {
  return (new SortedSet<int>(candidates).Count == candidates.Count);
}

private static List<int> copyAdd(IEnumerable<int> vs, int v) {
  var result = new List<int>(vs);
  result.Add(v);
  return result;
}

private static IEnumerable<List<int>> permute(List<ValueCell> cells, int pos, int target, IEnumerable<int> soFar) {
  if (target >= 1) {
    if (pos == (cells.Count - 1)) {
      return Enumerable.Repeat(copyAdd(soFar, target), 1);
    }
    else {
      return cells[pos].values.SelectMany(v => permute(cells, pos + 1, target - v, copyAdd(soFar, v)));
    }
  }
  else {
    return Enumerable.Empty<List<int>>();
  }
}

// Exhaustive search for possible solutions
private static IEnumerable<List<int>> permuteAll(List<ValueCell> cells, int total) {
  return permute(cells, 0, total, Enumerable.Empty<int>());
}

public int solveStep() {
  var possibles = cells.Select(c => new Possible(c)).ToList();
  int last = cells.Count - 1;
  var filtered = permuteAll(cells, total)
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