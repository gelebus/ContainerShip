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

        public void UpdateCurrentWeight()
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
        public void AddValuableContainer(List<Container> containers, bool cooledAllowed, bool cooledAndValuablePresent)
        {
            if (cooledAllowed && cooledAndValuablePresent)
            {
                for (int i = 0; i < containers.Count; i++)
                {
                    if (containers[i].Valuable && containers[i].Cooled)
                    {
                        AddContainerToList(containers, i);
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < containers.Count; i++)
                {
                    if (containers[i].Valuable)
                    {
                        AddContainerToList(containers, i);
                        return;
                    }
                }
            }
        }
        public void AddContainer(List<Container> containers, bool cooledAllowed, bool cooledPresent)
        {
            if (!ContainerPossible)
            {
                return;
            }
            if (cooledAllowed && cooledPresent)
            {
                for (int i = 0; i < containers.Count; i++)
                {
                    if (containers[i].Cooled)
                    {
                        AddContainerToList(containers, i);
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < containers.Count; i++)
                {
                    if (!containers[i].Cooled)
                    {
                        AddContainerToList(containers, i);
                        return;
                    }
                }
            }
        }
        private void AddContainerToList(List<Container> containers, int i)
        {
            Containers.Add(containers[i]);
            UpdateCurrentWeight();
            ContainerPossible = IsContainerPossible();
            containers.Remove(containers[i]);
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
