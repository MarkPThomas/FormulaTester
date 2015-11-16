using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Formula4 : IFormula
    {
        public double calculate(double[] args)
        {
            return 0.5D * (args[0] + args[1]);
        }
    }
}
