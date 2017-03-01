using System;
using System.Linq;
using System.Collections.Generic;

namespace kakuro.cell
{

    public class ValueCell : Cell
    {

        public SortedSet<int> values { get; set; }

        public ValueCell()
        {
            values = new SortedSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public ValueCell(ICollection<int> coll)
        {
            values = new SortedSet<int>(coll);
        }

        public virtual bool isPossible(int value)
        {
            return values.Contains(value);
        }

        public virtual string draw()
        {
            System.Diagnostics.Debug.WriteLine("values " + values);
            if (1 == values.Count)
            {
                return values.Select(v => "     " + v + "    ").ToList()[0];
            }
            else
            {
                return Enumerable.Range(1, 9).Aggregate(" ", (acc, v) => acc + (isPossible(v) ? v.ToString() : "."));
            }
        }

        public virtual int size()
        {
            return values.Count;
        }

        public override bool Equals(object obj)
        {
            ValueCell that = obj as ValueCell;
            return this.values.SetEquals(that.values);
        }

        public override int GetHashCode()
        {
            return values.Sum();
        }
    }

}
