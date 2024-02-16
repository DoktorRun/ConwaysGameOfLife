using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife_UI.Abstractions
{
    public interface IGameRule
    {
        public bool BirthRule(int liveNeighbours);
        public bool DeathRule(int liveNeighbours);
    }
}
