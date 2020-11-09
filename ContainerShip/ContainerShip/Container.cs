using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip
{
    public class Container
    {
        public Container(int weight, bool valuable, bool cooled)
        {
            Weight = weight;
            Valuable = valuable;
            Cooled = cooled;
        }

        public int ContainerId;
        public int Weight { get; private set; }
        public bool Valuable { get; private set; }
        public bool Cooled { get; private set; }

        public override string ToString()
        {
            return $"Id: {ContainerId}  ||  Weight: {Weight}  ||  Valuable: {Valuable}  ||  Cooled: {Cooled}";
        }
    }
}
