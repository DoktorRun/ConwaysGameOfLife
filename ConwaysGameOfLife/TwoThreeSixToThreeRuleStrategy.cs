using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    class TwoThreeSixToThreeRuleStrategy : IGameRuleStrategy

    {
        public void BirthRule(Cell cell)
        {
            if (!cell.Alive)
            {
                if(cell.NeighbourCount() == 3)
                {
                    cell.Birth();
                }
            }
        }

        public void DeathRule(Cell cell)
        {
            if (cell.Alive)
            {
                if(cell.NeighbourCount() != 2 ||
                    cell.NeighbourCount() != 3 ||
                    cell.NeighbourCount() != 6)
                {
                    cell.Die();
                }
            }
        }
    }
}
