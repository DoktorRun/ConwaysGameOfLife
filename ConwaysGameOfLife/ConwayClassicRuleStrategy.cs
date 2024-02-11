using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    //This rule is equal to the description of 23/3 strategy.
    public class ConwayClassicRuleStrategy : IGameRuleStrategy
    {
        public void BirthRule(Cell cell)
        {
            if (cell.Alive == false)
            {
                if (cell.NeighbourCount() == 3)
                {
                    cell.Birth();
                }
            }
        }

        public void DeathRule(Cell cell)
        {
            if (cell.Alive == true)
            {
                if (cell.NeighbourCount() < 2)
                {
                    cell.Die();
                }
                else if (cell.NeighbourCount() > 3)
                {
                    cell.Die();
                }
            }
        }
    }
}
