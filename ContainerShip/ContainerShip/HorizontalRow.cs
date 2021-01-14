using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip
{
    public class HorizontalRow
    {
        public HorizontalRow(int NumberOfVerticalRows)
        {
            VerticalRows = new List<VerticalRow>();
            for (int i = 0; i < NumberOfVerticalRows; i++)
            {
                VerticalRows.Add(new VerticalRow());
            }
            SetVerticalRowPlacements();
        }

        public List<VerticalRow> VerticalRows { get; private set; }

        private int Number = 1;
        public WeightShip SwitchPlacementVerticalRow(WeightShip weightShip, Placement from, Placement to)
        {
            VerticalRow switchRow;
            int currentWeightFrom = 0;
            int currentWeightTo = 30001;
            int numberFrom = 0;
            int numberTo = 0;

            for (int i = 0; i < VerticalRows.Count; i++)
            {
                var valuesFrom = SwitchChecksFrom(i, numberFrom, currentWeightFrom, from);
                numberFrom = valuesFrom.Item1;
                currentWeightFrom = valuesFrom.Item2;

                var valuesTo = SwitchChecksTo(i, numberTo, currentWeightTo, to);
                numberTo = valuesTo.Item1;
                currentWeightTo = valuesTo.Item2;
            }
            weightShip = CurrentWeightAdjustments(weightShip, from, numberFrom, numberTo);

            switchRow = VerticalRows[numberFrom];
            VerticalRows[numberFrom] = VerticalRows[numberTo];
            VerticalRows[numberFrom].Placement = from;
            VerticalRows[numberTo] = switchRow;
            VerticalRows[numberTo].Placement = to;

            return weightShip;
        }

        private (int, int) SwitchChecksFrom(int VerticalRowCounter,int currentNumber, int currentWeight, Placement placement)
        {
            if (VerticalRows[VerticalRowCounter].Placement == placement && VerticalRows[VerticalRowCounter].CurrentWeight > currentWeight)
            {
                currentNumber = VerticalRowCounter;
                currentWeight = VerticalRows[VerticalRowCounter].CurrentWeight;
            }
            return (currentNumber, currentWeight);
        }
        private (int, int) SwitchChecksTo(int VerticalRowCounter, int currentNumber, int currentWeight, Placement placement)
        {
            if (VerticalRows[VerticalRowCounter].Placement == placement && VerticalRows[VerticalRowCounter].CurrentWeight < currentWeight)
            {
                currentNumber = VerticalRowCounter;
                currentWeight = VerticalRows[VerticalRowCounter].CurrentWeight;
            }
            return (currentNumber, currentWeight);
        }

        private WeightShip CurrentWeightAdjustments(WeightShip weightShip, Placement from, int numberFrom, int numberTo)
        {
            if(from == Placement.Left)
            {
                int switchNumber = numberFrom;
                numberFrom = numberTo;
                numberTo = switchNumber;
            }
            weightShip.WeightRight -= VerticalRows[numberFrom].CurrentWeight;
            weightShip.WeightLeft += VerticalRows[numberFrom].CurrentWeight;

            weightShip.WeightRight += VerticalRows[numberTo].CurrentWeight;
            weightShip.WeightLeft -= VerticalRows[numberTo].CurrentWeight;

            return weightShip;
        }
        private void SetVerticalRowPlacements()
        {
            double half = (double)VerticalRows.Count / 2;
            if (Math.Ceiling(half) == half)
            {
                SetPositionsOfVerticalRows(half, true);
            }
            else
            {
                SetPositionsOfVerticalRows(half, false);
            }

            if (VerticalRows.Count == 1)
            {
                VerticalRows[0].Placement = Placement.Middle;
            }
        }

        private void SetPositionsOfVerticalRows(double half ,bool even)
        {
            for (int i = (int)Math.Floor(half); i < VerticalRows.Count; i++)
            {
                VerticalRows[i].Placement = Placement.Right;
            }
            if(!even)
            {
                VerticalRows[(int)Math.Floor(half)].Placement = Placement.Middle;
            }
        }
        public Container MaxWeightCorrectionRemoval(int maxWeight, WeightShip weightShip)
        {
            Container removedContainer = null;
            foreach (var verticalRow in VerticalRows)
            {
                if (maxWeight < weightShip.WeightTotal)
                {
                    removedContainer = verticalRow.RemoveLastContainer();
                    if (verticalRow.Placement == Placement.Left)
                    {
                        weightShip.WeightLeft -= removedContainer.Weight;
                    }
                    else
                    {
                        weightShip.WeightRight -= removedContainer.Weight;
                    }
                    weightShip.WeightTotal -= removedContainer.Weight;
                    return removedContainer;
                }
            }
            return removedContainer;
        }

        public override string ToString()
        {
            string horizontalRowString = "";
            for (int i = 0; i < VerticalRows.Count; i++)
            {
                horizontalRowString += Environment.NewLine + $"{Number}:" + VerticalRows[i] + Environment.NewLine;
                Number++;
            }
            return horizontalRowString;
        }
    }
}
