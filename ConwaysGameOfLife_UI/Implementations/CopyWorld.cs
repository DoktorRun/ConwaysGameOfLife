using ConwaysGameOfLife_UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife_UI.Implementations
{
    public class CopyWorld : IGameRule
    {
        public bool BirthRule(int liveNeighbours)
        {
            if (liveNeighbours == 1 || liveNeighbours == 3 || liveNeighbours == 5 || liveNeighbours == 7) return true;
            return false;
        }

        public bool DeathRule(int liveNeighbours)
        {
            if (liveNeighbours == 0 || liveNeighbours == 2 || liveNeighbours == 4 || liveNeighbours == 6 || liveNeighbours == 8) return false;
            return true;
        }
    }
}
