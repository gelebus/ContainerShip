using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip
{
    public class Ship
    {
        public Ship(List<Container> containers,int maxWeight, int length, int width)
        {
            Containers = containers;
            MaxWeight = maxWeight;
            Length = length;
            Width = width;
        }
        public int MaxWeight { get; private set; }
        
        public WeightShip WeightShip { get; set; }
        
        private readonly int Length;
        private readonly int Width;

        private List<Container> Containers;
        private List<HorizontalRow> HorizontalRows;

        public List<Container> ContainersLeft { get; private set; }

        private bool ValuablePrio;

        public void Sort()
        {
            Init();
            SortContainers();
            AddWeightToPlacements();
            if (WeightCorrectionNeeded())
            {
                WeightDistributionCorrection();
            }
            ValuableAccesibilityCorrection();
            if (WeightCorrectionNeeded())
            {
                SecondWeightCorrection();
            }
        }
        private void Init()
        {
            HorizontalRows = new List<HorizontalRow>();
            ContainersLeft = new List<Container>();

            WeightShip = new WeightShip();

            ValuablePrio = true;

            for (int i = 0; i < CalculateNumberOfHorizontalRowsPossible(); i++)
            {
                HorizontalRows.Add(new HorizontalRow(CalculateNumberOfVerticalRowsInAHorizontalRow()));
            }
        }
        private int CalculateNumberOfVerticalRowsInAHorizontalRow()
        {
            double widthContainer = 2.3;
            return (int)Math.Floor(Width / widthContainer);
        }
        private int CalculateNumberOfHorizontalRowsPossible()
        {
            double lengthContainer = 12;
            return (int)Math.Floor(Length / lengthContainer);
        }
        private void SortContainers()
        {
            for (int HorizontalRowCounter = 0; HorizontalRowCounter < HorizontalRows.Count; HorizontalRowCounter++)
            {
                HorizontalRowCounter = RunSortContainerChecks(HorizontalRowCounter);
            }
        }
        private int RunSortContainerChecks(int HorizontalRowCounter)
        {
            for (int VerticalRowCounter = 0; VerticalRowCounter < HorizontalRows[HorizontalRowCounter].VerticalRows.Count; VerticalRowCounter++)
            {
                var values = SortContainerChecks(HorizontalRowCounter, VerticalRowCounter);
                HorizontalRowCounter = values.Item1;
                VerticalRowCounter = values.Item2;
                if (HorizontalRowCounter == HorizontalRows.Count - 1 && VerticalRowCounter == HorizontalRows[HorizontalRowCounter].VerticalRows.Count - 1 && Containers.Count > 0 && VerticalRowPossibleAdition())
                {
                    ValuablePrio = false;
                    return -1;
                }
            }
            
            return HorizontalRowCounter;
        }
        private (int, int) SortContainerChecks(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (ValuablePrio)
            {
                VerticalRowCounter = SortValuableContainers(HorizontalRowCounter, VerticalRowCounter);
            }
            else
            {
                SortRegularAndCooledContainers(HorizontalRowCounter, VerticalRowCounter);
            }
            
            if (!VerticalRowPossibleAdition())
            {
                RemoveAllContainersWithTheseFeatures(false, false);
            }
            if (!CooledAdditionPossible())
            {
                RemoveAllContainersWithTheseFeatures(false, true);
            }
            return (HorizontalRowCounter, VerticalRowCounter);
        }
        private int SortValuableContainers(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (ContainerPresent(false, true) || ContainerPresent(true, true))
            {
                VerticalRowCounter = ValuableContainerChecks(HorizontalRowCounter, VerticalRowCounter);
            }
            else
            {
                ValuablePrio = false;
                VerticalRowCounter--;
            }
            if (HorizontalRowCounter == HorizontalRows.Count - 1 && VerticalRowCounter == HorizontalRows[HorizontalRowCounter].VerticalRows.Count - 1 && ContainerPresent(false, true))
            {
                RemoveAllContainersWithTheseFeatures(true, false);
            }
            return VerticalRowCounter;
        }
        private int ValuableContainerChecks(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (HorizontalRowCounter != 0 && ContainerPresent(true, true))
            {
                RemoveAllContainersWithTheseFeatures(true, true);
                VerticalRowCounter--;
            }
            else if (HorizontalRowCounter == 0)
            {
                AdditionOfOneOfTheseTwoContainers(GetContainer(true, true), GetContainer(false, true), HorizontalRowCounter, VerticalRowCounter);
            }
            else
            {
                HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(false, true));
                Containers.Remove(GetContainer(false, true));
            }
            return VerticalRowCounter;
        }
        private void SortRegularAndCooledContainers(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (HorizontalRowCounter == 0)
            {
                AdditionOfOneOfTheseTwoContainers(GetContainer(true, false), GetContainer(false, false), HorizontalRowCounter, VerticalRowCounter);
            }
            else
            {
                HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(false, false));
                Containers.Remove(GetContainer(false, false));
            }
        }
        private void AdditionOfOneOfTheseTwoContainers(Container FirstContainerToCheck, Container SecondContainer, int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(FirstContainerToCheck) == false)
            {
                HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(SecondContainer);
                Containers.Remove(SecondContainer);
            }
            else
            {
                Containers.Remove(FirstContainerToCheck);
            }
        }
        private void WeightDistributionCorrection()
        {
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                if (WeightShip.WeightRight > WeightShip.WeightLeft)
                {
                    WeightShip = HorizontalRows[i].SwitchPlacementVerticalRow(WeightShip, Placement.Right, Placement.Left);
                }
                else
                {
                    WeightShip = HorizontalRows[i].SwitchPlacementVerticalRow(WeightShip, Placement.Left, Placement.Right);
                }
            }
        }
        private void RemoveAllContainersWithTheseFeatures(bool valuable, bool cooled)
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                if (Containers[i].Cooled == cooled && Containers[i].Valuable == valuable)
                {
                    ContainersLeft.Add(Containers[i]);
                    Containers.Remove(Containers[i]);
                    i = -1;
                }
            }
        }
        private void SecondWeightCorrection()
        {
            Container highestWeightContainer = new Container(0, false, false);
            int containerNumber = 0;
            int verticalRowNumber = 0;
            Placement placement;
            if (WeightShip.WeightLeft > WeightShip.WeightRight)
            {
                placement = Placement.Left;
            }
            else
            {
                placement = Placement.Right;
            }
            for (int a = 0; a < HorizontalRows[0].VerticalRows.Count; a++)
            {
                for (int b = 0; b < HorizontalRows[0].VerticalRows[a].Containers.Count; b++)
                {
                    if (HorizontalRows[0].VerticalRows[a].Containers[b].Weight > highestWeightContainer.Weight && HorizontalRows[0].VerticalRows[a].Placement == placement)
                    {
                        containerNumber = b;
                        verticalRowNumber = a;
                        highestWeightContainer = HorizontalRows[0].VerticalRows[a].Containers[b];
                    }
                }
            }
            ContainersLeft.Add(highestWeightContainer);
            HorizontalRows[0].VerticalRows[verticalRowNumber].CurrentWeight -= highestWeightContainer.Weight;
            WeightShip.WeightTotal -= highestWeightContainer.Weight;
            if (placement == Placement.Left)
            {
                WeightShip.WeightLeft -= highestWeightContainer.Weight;
            }
            else
            {
                WeightShip.WeightRight -= highestWeightContainer.Weight;
            }
            HorizontalRows[0].VerticalRows[verticalRowNumber].Containers.Remove(HorizontalRows[0].VerticalRows[verticalRowNumber].Containers[containerNumber]);
        }
        private void ValuableAccesibilityCorrection()
        {
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                RunValuableAccesibilityChecks(i);
            }
        }
        private void RunValuableAccesibilityChecks(int HorizontalRowCounter)
        {
            for (int VerticalRowCounter = 0; VerticalRowCounter < HorizontalRows[HorizontalRowCounter].VerticalRows.Count; VerticalRowCounter++)
            {
                VerticalRowCounter = ValuableAccesibilityChecks(VerticalRowCounter, HorizontalRowCounter);
            }
        }
        private int ValuableAccesibilityChecks(int VerticalRowCounter, int HorizontalRowCounter)
        {
            if (!AccesibleFromFrontOrBack(VerticalRowCounter, HorizontalRowCounter) && HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].Containers.Count > 0)
            {
                int OtherHorizontalRow = HorizontalRowCounter + 1;
                int ContainerNumber = HorizontalRows[OtherHorizontalRow].VerticalRows[VerticalRowCounter].Containers.Count - 1;
                Container Othercontainer = HorizontalRows[OtherHorizontalRow].VerticalRows[VerticalRowCounter].Containers[ContainerNumber];

                ContainersLeft.Add(Othercontainer);
                HorizontalRows[OtherHorizontalRow].VerticalRows[VerticalRowCounter].CurrentWeight -= Othercontainer.Weight;
                removeContainerWeightFromShip(Othercontainer,OtherHorizontalRow, VerticalRowCounter);
                HorizontalRows[OtherHorizontalRow].VerticalRows[VerticalRowCounter].Containers.Remove(Othercontainer);
                VerticalRowCounter--;
            }
            return VerticalRowCounter;
        }
        private void removeContainerWeightFromShip(Container container, int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].Placement == Placement.Right)
            {
                WeightShip.WeightRight -= container.Weight;
            }
            else if (HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].Placement == Placement.Left)
            {
                WeightShip.WeightLeft -= container.Weight;
            }
            WeightShip.WeightTotal -= container.Weight;
        }
        private bool AccesibleFromFrontOrBack(int verticalRowNumber, int horizontalRowNumber)
        {
            if (horizontalRowNumber == 0 || horizontalRowNumber == HorizontalRows.Count - 1)
            {
                return true;
            }
            else if (HorizontalRows[horizontalRowNumber].VerticalRows[verticalRowNumber].Containers.Count > HorizontalRows[horizontalRowNumber - 1].VerticalRows[verticalRowNumber].Containers.Count || HorizontalRows[horizontalRowNumber].VerticalRows[verticalRowNumber].Containers.Count > HorizontalRows[horizontalRowNumber + 1].VerticalRows[verticalRowNumber].Containers.Count)
            {
                return true;
            }
            return false;
        }
        private bool VerticalRowPossibleAdition()
        {
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                for (int a = 0; a < HorizontalRows[i].VerticalRows.Count; a++)
                {
                    if (HorizontalRows[i].VerticalRows[a].ContainerPossible)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CooledAdditionPossible()
        {
            for (int a = 0; a < HorizontalRows[0].VerticalRows.Count; a++)
            {
                if (HorizontalRows[0].VerticalRows[a].ContainerPossible)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ContainerPresent(bool cooled, bool valuable)
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                if (Containers[i].Cooled == cooled && Containers[i].Valuable == valuable)
                {
                    return true;
                }
            }
            return false;
        }
        private Container GetContainer(bool cooled, bool valuable)
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                if(Containers[i].Cooled == cooled && Containers[i].Valuable == valuable)
                {
                    return Containers[i];
                }
            }
            return null;
        }
        private bool WeightCorrectionNeeded()
        {
            int difference;
            if (WeightShip.WeightLeft > WeightShip.WeightRight)
            {
                difference = WeightShip.WeightLeft - WeightShip.WeightRight;
            }
            else
            {
                difference = WeightShip.WeightRight - WeightShip.WeightLeft;
            }
            if (difference > 0.2f * WeightShip.WeightTotal)
            {
                return true;
            }
            return false;
        }
        private void AddWeightToPlacements()
        {
            for (int HorizontalRowCounter = 0; HorizontalRowCounter < HorizontalRows.Count; HorizontalRowCounter++)
            {
                AddWeight(HorizontalRowCounter);
            }
        }
        private void AddWeight(int HorizontalRowCounter)
        {
            for (int VerticalRowCounter = 0; VerticalRowCounter < HorizontalRows[HorizontalRowCounter].VerticalRows.Count; VerticalRowCounter++)
            {
                AddWeightChecks(HorizontalRowCounter, VerticalRowCounter);
            }
        }
        private void AddWeightChecks(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].Placement == Placement.Left)
            {
                WeightShip.WeightLeft += HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].CurrentWeight;
                WeightShip.WeightTotal += HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].CurrentWeight;
            }
            else if (HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].Placement == Placement.Right)
            {
                WeightShip.WeightRight += HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].CurrentWeight;
                WeightShip.WeightTotal += HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].CurrentWeight;
            }
            else
            {
                WeightShip.WeightTotal += HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].CurrentWeight;
            }
        }

        public override string ToString()
        {
            string shipString = $"Ship (WeightLeft:{WeightShip.WeightLeft}) (WeightRight: {WeightShip.WeightRight}) (TotalWeight: {WeightShip.WeightTotal})" + Environment.NewLine;
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                int Number = i + 1;
                shipString += $"HorizontalRow {Number}" + Environment.NewLine + HorizontalRows[i] + Environment.NewLine + Environment.NewLine;
            }
            return shipString;
        }
    }
}
