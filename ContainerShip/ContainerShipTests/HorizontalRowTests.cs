using ContainerShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShipTests
{
    [TestClass]
    public class HorizontalRowTests
    {
        private HorizontalRow horizontalRow;
        [TestInitialize]
        public void Setup()
        {
            horizontalRow = new HorizontalRow(2);
        }
        [TestMethod]
        public void CorrectWeightTransfer()
        {
            WeightShip weightShip = new WeightShip();
            weightShip.WeightLeft = 4000;
            weightShip.WeightRight = 30000;
            weightShip.WeightTotal = 34000;
            WeightShip expected = new WeightShip();
            expected.WeightLeft = 30000;
            expected.WeightRight = 4000;
            expected.WeightTotal = 34000;
            horizontalRow.VerticalRows[0].AddContainer(new Container(4000, false, false));
            horizontalRow.VerticalRows[1].AddContainer(new Container(30000, false, false));

            weightShip = horizontalRow.SwitchPlacementVerticalRow(weightShip, Placement.Left, Placement.Right);

            Assert.AreEqual(expected.WeightLeft, weightShip.WeightLeft);
            Assert.AreEqual(expected.WeightRight, weightShip.WeightRight);
            Assert.AreEqual(expected.WeightTotal, weightShip.WeightTotal);
        }
    }
}
