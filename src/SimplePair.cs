using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kakuro {
  class SimplePair<T> : Pair<T, T> {

    public SimplePair(T x, T y) : base (x, y) {
    }
  }
}

