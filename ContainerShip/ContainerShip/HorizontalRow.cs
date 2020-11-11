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
                if (VerticalRows[i].Placement == from && VerticalRows[i].CurrentWeight > currentWeightFrom)
                {
                    numberFrom = i;
                    currentWeightFrom = VerticalRows[i].CurrentWeight;
                }
                if (VerticalRows[i].Placement == to && VerticalRows[i].CurrentWeight < currentWeightTo)
                {
                    numberTo = i;
                    currentWeightTo = VerticalRows[i].CurrentWeight;
                }
            }
            weightShip = CurrentWeightAdjustments(weightShip, from, numberFrom, numberTo);

            switchRow = VerticalRows[numberFrom];
            VerticalRows[numberFrom] = VerticalRows[numberTo];
            VerticalRows[numberFrom].Placement = from;
            VerticalRows[numberTo] = switchRow;
            VerticalRows[numberTo].Placement = to;

            return weightShip;
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
                for (int i = (int)half; i < VerticalRows.Count; i++)
                {
                    VerticalRows[i].Placement = Placement.Right;
                }
            }
            else
            {
                for (int i = (int)Math.Floor(half); i < VerticalRows.Count; i++)
                {
                    VerticalRows[i].Placement = Placement.Right;
                }
                VerticalRows[(int)Math.Floor(half)].Placement = Placement.Middle;
            }

            if (VerticalRows.Count == 1)
            {
                VerticalRows[0].Placement = Placement.Middle;
            }
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
