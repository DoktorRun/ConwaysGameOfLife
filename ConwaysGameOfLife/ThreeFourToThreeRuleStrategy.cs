using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class ThreeFourToThreeRuleStrategy : IGameRuleStrategy
    {
        public void BirthRule(Cell cell)
        {
            if (!cell.Alive)
            {
                if (cell.NeighbourCount() == 3)
                {
                    cell.Birth();
                }
            }
        }

        public void DeathRule(Cell cell)
        {
            if (cell.Alive)
            {
                if(cell.NeighbourCount() < 3 || cell.NeighbourCount() > 4){
                    cell.Die();
                }
            }
        }
    }
}
