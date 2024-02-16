using ConwaysGameOfLife_UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife_UI.Implementations
{
    public class ConwayClassicRuleset : IGameRule
    {
        public bool BirthRule(int liveNeighbours)
        {
            if(liveNeighbours == 3)
            {
                return true;
            }
            return false;
        }

        public bool DeathRule(int liveNeighbours)
        {
            if (liveNeighbours < 2 || liveNeighbours > 3)
            {
                return false;
            }
            else return true;
        }
    }
}
