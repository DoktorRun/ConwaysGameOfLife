using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public interface IGameRuleStrategy
    {
        void BirthRule(Cell cell);
        void DeathRule(Cell cell);

    }
}
