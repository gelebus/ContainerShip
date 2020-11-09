using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShip
{
    public class HorizontalRow
    {
        public HorizontalRow(int availableSpots)
        {
            VerticalRows = new List<VerticalRow>();
            for (int i = 0; i < availableSpots; i++)
            {
                VerticalRows.Add(new VerticalRow());
            }
            CooledRow = false;
            SetVerticalRowPlacements();
        }

        public List<VerticalRow> VerticalRows { get; private set; }
        public bool CooledRow { get; set; }

        private int Number = 1;
        public void SwitchPlacementVerticalRow(Ship ship, Placement from, Placement to)
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
            if (from == Placement.Right)
            {
                ship.WeightRight -= VerticalRows[numberFrom].CurrentWeight;
                ship.WeightLeft += VerticalRows[numberFrom].CurrentWeight;

                ship.WeightRight += VerticalRows[numberTo].CurrentWeight;
                ship.WeightLeft -= VerticalRows[numberTo].CurrentWeight;
            }
            else
            {
                ship.WeightRight += VerticalRows[numberFrom].CurrentWeight;
                ship.WeightLeft -= VerticalRows[numberFrom].CurrentWeight;

                ship.WeightRight -= VerticalRows[numberTo].CurrentWeight;
                ship.WeightLeft += VerticalRows[numberTo].CurrentWeight;
            }

            switchRow = VerticalRows[numberFrom];
            VerticalRows[numberFrom] = VerticalRows[numberTo];
            VerticalRows[numberFrom].Placement = from;
            VerticalRows[numberTo] = switchRow;
            VerticalRows[numberTo].Placement = to;
        }
        private void SetVerticalRowPlacements()
        {
            double half = VerticalRows.Count / 2;
            if (Math.Floor(half) == half)
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
