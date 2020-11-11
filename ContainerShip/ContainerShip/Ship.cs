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
        
        private int Length;
        private int Width;

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
                HorizontalRowCounter = SortContainerChecks(HorizontalRowCounter);
            }
        }
        private int SortContainerChecks(int HorizontalRowCounter)
        {
            for (int VerticalRowCounter = 0; VerticalRowCounter < HorizontalRows[HorizontalRowCounter].VerticalRows.Count; VerticalRowCounter++)
            {
                if (ValuablePrio)
                {
                    VerticalRowCounter = SortValuableContainers(HorizontalRowCounter, VerticalRowCounter);
                }
                else
                {
                    SortRegularAndCooledContainers(HorizontalRowCounter, VerticalRowCounter);
                }
                if (HorizontalRowCounter == HorizontalRows.Count - 1 && VerticalRowCounter == HorizontalRows[HorizontalRowCounter].VerticalRows.Count - 1 && Containers.Count > 0 && VerticalRowPossibleAdition())
                {
                    ValuablePrio = false;
                    return -1;
                }
                if (!VerticalRowPossibleAdition())
                {
                    RemoveAllContainersWithTheseFeatures(false, false);
                }
                if (!CooledAdditionPossible())
                {
                    RemoveAllContainersWithTheseFeatures(false, true);
                }
            }
            return HorizontalRowCounter;
        }
        private int SortValuableContainers(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (ContainerPresent(false, true) || ContainerPresent(true, true))
            {
                if (HorizontalRowCounter != 0 && ContainerPresent(true, true))
                {
                    RemoveAllContainersWithTheseFeatures(true, true);
                    VerticalRowCounter--;
                }
                else if (HorizontalRowCounter == 0)
                {
                    if(HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(true,true)) == false)
                    {
                        HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(false, true));
                        Containers.Remove(GetContainer(false, true));
                    }
                    else
                    {
                        Containers.Remove(GetContainer(true, true));
                    }
                }
                else
                {
                    HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(false,true));
                    Containers.Remove(GetContainer(false, true));
                }
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
        private void SortRegularAndCooledContainers(int HorizontalRowCounter, int VerticalRowCounter)
        {
            if (HorizontalRowCounter == 0)
            {
                if (HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(true, false)) == false)
                {
                    HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(false, false));
                    Containers.Remove(GetContainer(false, false));
                }
                else
                {
                    Containers.Remove(GetContainer(true, false));
                }
            }
            else
            {
                HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(GetContainer(false, false));
                Containers.Remove(GetContainer(false, false));
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
                for (int a = 0; a < HorizontalRows[i].VerticalRows.Count; a++)
                {
                    if (!AccesibleFromFrontOrBack(a, i) && HorizontalRows[i].VerticalRows[a].Containers.Count > 0)
                    {
                        ContainersLeft.Add(HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1]);
                        HorizontalRows[i + 1].VerticalRows[a].CurrentWeight -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
                        if (HorizontalRows[i + 1].VerticalRows[a].Placement == Placement.Right)
                        {
                            WeightShip.WeightRight -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
                        }
                        else if (HorizontalRows[i + 1].VerticalRows[a].Placement == Placement.Left)
                        {
                            WeightShip.WeightLeft -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
                        }
                        WeightShip.WeightTotal -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
                        HorizontalRows[i + 1].VerticalRows[a].Containers.Remove(HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1]);
                        a--;
                    }
                }
            }
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
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                AddWeight(i);
            }
        }
        private void AddWeight(int HorizontalRowCounter)
        {
            for (int i = 0; i < HorizontalRows[HorizontalRowCounter].VerticalRows.Count; i++)
            {
                if (HorizontalRows[HorizontalRowCounter].VerticalRows[i].Placement == Placement.Left)
                {
                    WeightShip.WeightLeft += HorizontalRows[HorizontalRowCounter].VerticalRows[i].CurrentWeight;
                    WeightShip.WeightTotal += HorizontalRows[HorizontalRowCounter].VerticalRows[i].CurrentWeight;
                }
                else if (HorizontalRows[HorizontalRowCounter].VerticalRows[i].Placement == Placement.Right)
                {
                    WeightShip.WeightRight += HorizontalRows[HorizontalRowCounter].VerticalRows[i].CurrentWeight;
                    WeightShip.WeightTotal += HorizontalRows[HorizontalRowCounter].VerticalRows[i].CurrentWeight;
                }
                else
                {
                    WeightShip.WeightTotal += HorizontalRows[HorizontalRowCounter].VerticalRows[i].CurrentWeight;
                }
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
