using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Formula3 : IFormula
    {
        public double calculate(double[] args)
        {
            return (args[0] + args[1]) / 2;
        }
    }
}
