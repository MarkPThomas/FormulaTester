using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Formula2 : IFormula
    {
        public double calculate(double[] args)
        {
            return (1D / 2) * (args[0] + args[1]);
        }
    }
}
