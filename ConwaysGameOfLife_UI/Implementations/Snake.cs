using ConwaysGameOfLife_UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife_UI.Implementations
{
    public class Snake : IGameRule
    {
        public bool BirthRule(int liveNeighbours)
        {
            return (liveNeighbours == 1) ? true : false;
        }

        public bool DeathRule(int liveNeighbours)
        {
            return (liveNeighbours > 2) ? true : false;
        }
    }
}
