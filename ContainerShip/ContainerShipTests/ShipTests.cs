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
        public void IsValuableOnTop()
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
        public void AreCooledContainersInFront()
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
        public void CantTakeToMuchContainers()
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
        public void CanWeightBeDistributedCorrectly()
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
        public void CanWeightBeDistributedCorrectlyWithMultipleRows()
        {
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(1, false, false));
            containers.Add(new Container(10000, false, false));
            ship = new Ship(containers, 500000, 15, 7);
            int expected = 5;

            ship.Sort();

            Assert.AreEqual(expected, ship.WeightShip.WeightTotal);
        }
        [TestMethod]
        public void CanAValuableBeAccesibleFromFrontOrBack()
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
        [TestMethod]
        public void CanACooledAndValuableAndValuableBeAccesibleFromFrontOrBack()
        {
            containers.Add(new Container(4000, true, true));
            containers.Add(new Container(4000, true, true));
            containers.Add(new Container(4000, true, true));
            containers.Add(new Container(4000, true, true));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            containers.Add(new Container(4000, true, false));
            ship = new Ship(containers, 500000, 37, 7);
            int expected = 4;

            ship.Sort();

            Assert.AreEqual(expected, ship.ContainersLeft.Count);
        }
        [TestMethod]
        public void MaxWeightGetsCorrected()
        {
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            containers.Add(new Container(30000, false, false));
            ship = new Ship(containers, 300000, 25, 5);
            int expectedWeightTotal = 300000;

            ship.Sort();

            Assert.AreEqual(expectedWeightTotal, ship.WeightShip.WeightTotal);
        }
    }
}
