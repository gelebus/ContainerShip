using System;
using System.Collections.Generic;
using ContainerShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerShipTests
{
    [TestClass]
    public class ShipTests
    {
        private Ship ship;
        private List<Container> containers;
        [TestInitialize]
        public void setup()
        {
            containers = new List<Container>();
        }

        [TestMethod]
        public void ValuableContainersOnTopTest()
        {
            containers.Add(new Container(30000, true, false));
            containers.Add(new Container(30000, true, false));
            containers.Add(new Container(30000, true, false));
            containers.Add(new Container(30000, true, false));
            containers.Add(new Container(30000, true, false));
            ship = new Ship(containers,500000, 25, 5);
            int expected = 1;

            ship.Sort();

            Assert.AreEqual(expected, ship.ContainersLeft.Count);
        }
        [TestMethod]
        public void CooledContainersInTheFrontRowTest()
        {
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            containers.Add(new Container(30000, false, true));
            ship = new Ship(containers, 500000, 25, 5);
            int expected = 1;

            ship.Sort();

            Assert.AreEqual(expected, ship.ContainersLeft.Count);
        }
        [TestMethod]
        public void ToMuchContainersTest()
        {
            containers.Add(new Container(30000, true, true));
            containers.Add(new Container(30000, true, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            ship = new Ship(containers,500000, 13, 3);
            int expected = 2;

            ship.Sort();

            Assert.AreEqual(expected, ship.ContainersLeft.Count);
        }
        [TestMethod]
        public void BothWeightChecksTest()
        {
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(10000, false, false));
            containers.Add(new Container(1, false, false));
            ship = new Ship(containers,500000, 15, 5);
            int expected = 3;

            ship.Sort();

            Assert.AreEqual(expected, ship.WeightShip.WeightTotal);
        }
        [TestMethod]
        public void ValuableAccesibilityTest()
        {
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            ship = new Ship(containers,500000, 49, 3);
            int expected = 12000;

            ship.Sort();

            Assert.AreEqual(expected, ship.WeightShip.WeightTotal);
        }
    }
}
