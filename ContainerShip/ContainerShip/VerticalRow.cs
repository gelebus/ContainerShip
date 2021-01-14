using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip
{
    public class VerticalRow
    {
        public VerticalRow()
        {
            CurrentWeight = 0;
            ContainerPossible = true;
            Placement = Placement.Left;
            Containers = new List<Container>();
        }

        public int CurrentWeight { get; set; }
        public bool ContainerPossible { get; set; }
        public Placement Placement { get; set; }
        public List<Container> Containers { get; private set; }

        private void UpdateCurrentWeight()
        {
            CurrentWeight = 0;
            for (int i = 0; i < Containers.Count; i++)
            {
                CurrentWeight += Containers[i].Weight;
            }
        }
        private bool IsContainerPossible()
        {
            if(CurrentWeight > 120000)
            {
                return false;
            }
            return true;
        }
        public bool AddContainer(Container container)
        {
            if(container == null)
            {
                return false;
            }
            Containers.Add(container);
            UpdateCurrentWeight();
            ContainerPossible = IsContainerPossible();
            return true;
        }
        public Container RemoveLastContainer()
        {
            Container container = Containers[Containers.Count - 1];
            Containers.Remove(container);
            UpdateCurrentWeight();
            return container;
        }
        public override string ToString()
        {
            string VerticalRowString = $"VerticalRow ({Placement}) (Weight: {CurrentWeight})";
            for (int i = 0; i < Containers.Count; i++)
            {
                VerticalRowString += Environment.NewLine + Containers[i];
            }
            return VerticalRowString;
        }

    }
}
