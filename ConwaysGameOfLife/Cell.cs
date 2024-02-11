using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class Cell
    {
        int row, col;
        public List<Cell> Neighbours { get; set; } = new();
        public bool Alive { get; set; } = false;

        public Cell(int row, int col) : this(row, col, false) { }

        public Cell(int row, int col, bool state)
        {
            this.row = row;
            this.col = col;
            this.Alive = state;
        }

        public int NeighbourCount()
        {
            return Neighbours.Count;
        }

        public void Die()
        {
            Alive = false;
        }

        public void Birth()
        {
            Alive = true;
        }

        public override string ToString()
        {
            return Alive == true ? " O " : " . ";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        public bool Equals(Cell other)
        {
            return other != null &&
                other.row == this.row &&
                other.col == this.col &&
                other.Alive == this.Alive;
            //TODO: maybe check the Neighbours as well, have to find a way to not recursively check all Neighbours to reduce time complexity.
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(row, col, Neighbours, Alive);
        }
    }
}
