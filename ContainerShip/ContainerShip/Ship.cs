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
        public int WeightLeft { get; set; }
        public int WeightRight { get; set; }
        public int WeightTotal { get; set; }
        
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

            WeightLeft = 0;
            WeightRight = 0;
            WeightTotal = 0;

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
                for (int VerticalRowCounter = 0; VerticalRowCounter < HorizontalRows[HorizontalRowCounter].VerticalRows.Count; VerticalRowCounter++)
                {
                    if (ValuablePrio)
                    {
                        VerticalRowCounter = SortValuableContainers(HorizontalRowCounter, VerticalRowCounter);
                    }
                    else
                    {
                        if (HorizontalRowCounter == 0)
                        {
                            HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(Containers, true, ContainerPresent(true, false));
                        }
                        else
                        {
                            HorizontalRows[HorizontalRowCounter].VerticalRows[VerticalRowCounter].AddContainer(Containers, false, ContainerPresent(true, false));
                        }
                    }
                    if (HorizontalRowCounter == HorizontalRows.Count - 1 && VerticalRowCounter == HorizontalRows[HorizontalRowCounter].VerticalRows.Count - 1 && Containers.Count > 0 && VerticalRowPossibleAdition())
                    {
                        ValuablePrio = false;
                        HorizontalRowCounter = 0;
                        VerticalRowCounter = -1;
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
            }
        }

        private int SortValuableContainers(int HorizontalrowCounter, int VerticalrowCounter)
        {
            if (ContainerPresent(false, true) || ContainerPresent(true, true))
            {
                if (HorizontalrowCounter != 0 && ContainerPresent(true, true))
                {
                    RemoveAllContainersWithTheseFeatures(true, true);
                    VerticalrowCounter--;
                }
                else if (HorizontalrowCounter == 0)
                {
                    HorizontalRows[HorizontalrowCounter].VerticalRows[VerticalrowCounter].AddValuableContainer(Containers, true, ContainerPresent(true, true));
                }
                else
                {
                    HorizontalRows[HorizontalrowCounter].VerticalRows[VerticalrowCounter].AddValuableContainer(Containers, false, ContainerPresent(true, true));
                }
            }
            else
            {
                ValuablePrio = false;
                VerticalrowCounter--;
            }
            if (HorizontalrowCounter == HorizontalRows.Count - 1 && VerticalrowCounter == HorizontalRows[HorizontalrowCounter].VerticalRows.Count - 1 && ContainerPresent(false, true))
            {
                RemoveAllContainersWithTheseFeatures(true, false);
            }
            return VerticalrowCounter;
        }

        private void WeightDistributionCorrection()
        {
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                if (!WeightCorrectionNeeded())
                {
                    return;
                }
                if (WeightRight > WeightLeft)
                {
                    HorizontalRows[i].SwitchPlacementVerticalRow(this, Placement.Right, Placement.Left);
                }
                else
                {
                    HorizontalRows[i].SwitchPlacementVerticalRow(this, Placement.Left, Placement.Right);
                }
            }
        }
        private void RemoveAllContainersWithTheseFeatures(bool valuable, bool cooled)
        {
            for (int b = 0; b < Containers.Count; b++)
            {
                if (Containers[b].Cooled == cooled && Containers[b].Valuable == valuable)
                {
                    ContainersLeft.Add(Containers[b]);
                    Containers.Remove(Containers[b]);
                    b = -1;
                }
            }
        }
        private void SecondWeightCorrection()
        {
            Container highestWeightContainer = new Container(0, false, false);
            int containerNumber = 0;
            int verticalRowNumber = 0;
            Placement placement;
            if (WeightLeft > WeightRight)
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
            WeightTotal -= highestWeightContainer.Weight;
            if (placement == Placement.Left)
            {
                WeightLeft -= highestWeightContainer.Weight;
            }
            else
            {
                WeightRight -= highestWeightContainer.Weight;
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
                            WeightRight -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
                        }
                        else if (HorizontalRows[i + 1].VerticalRows[a].Placement == Placement.Left)
                        {
                            WeightLeft -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
                        }
                        WeightTotal -= HorizontalRows[i + 1].VerticalRows[a].Containers[HorizontalRows[i + 1].VerticalRows[a].Containers.Count - 1].Weight;
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
        private bool WeightCorrectionNeeded()
        {
            int difference;
            if (WeightLeft > WeightRight)
            {
                difference = WeightLeft - WeightRight;

            }
            else
            {
                difference = WeightRight - WeightLeft;
            }
            if (difference > 0.2f * WeightTotal)
            {
                return true;
            }
            return false;
        }
        private void AddWeightToPlacements()
        {
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                for (int a = 0; a < HorizontalRows[i].VerticalRows.Count; a++)
                {
                    if (HorizontalRows[i].VerticalRows[a].Placement == Placement.Left)
                    {
                        WeightLeft += HorizontalRows[i].VerticalRows[a].CurrentWeight;
                        WeightTotal += HorizontalRows[i].VerticalRows[a].CurrentWeight;
                    }
                    else if (HorizontalRows[i].VerticalRows[a].Placement == Placement.Right)
                    {
                        WeightRight += HorizontalRows[i].VerticalRows[a].CurrentWeight;
                        WeightTotal += HorizontalRows[i].VerticalRows[a].CurrentWeight;
                    }
                    else
                    {
                        WeightTotal += HorizontalRows[i].VerticalRows[a].CurrentWeight;
                    }
                }
            }
        }

        public override string ToString()
        {
            string shipString = $"Ship (WeightLeft:{WeightLeft}) (WeightRight: {WeightRight}) (TotalWeight: {WeightTotal})" + Environment.NewLine;
            for (int i = 0; i < HorizontalRows.Count; i++)
            {
                int Number = i + 1;
                shipString += $"HorizontalRow {Number}" + Environment.NewLine + HorizontalRows[i] + Environment.NewLine + Environment.NewLine;
            }
            return shipString;
        }
    }
}
