using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip
{
    public class WeightShip
    {
        public WeightShip()
        {
            WeightLeft = 0;
            WeightRight = 0;
            WeightTotal = 0;
        }
        public int WeightLeft { get; set; }
        public int WeightRight { get; set; }
        public int WeightTotal { get; set; }
    }
}
