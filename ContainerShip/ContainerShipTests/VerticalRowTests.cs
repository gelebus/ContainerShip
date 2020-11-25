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
    public class VerticalRowTests
    {
        private VerticalRow verticalRow;

        [TestInitialize]
        public void Setup()
        {
            verticalRow = new VerticalRow();
        }
        
        [TestMethod]
        public void IfNoContainerIsPassedItCantBeAdded()
        {
            int expected = 0;

            verticalRow.AddContainer(null);

            Assert.AreEqual(expected, verticalRow.Containers.Count);
        }

        [TestMethod]
        public void IfAContainerIsPassedItCanBeAdded()
        {
            int expected = 1;

            verticalRow.AddContainer(new Container(4000, true, true));
            Assert.AreEqual(expected, verticalRow.Containers.Count);
        }

        [TestMethod]
        public void DisableContainerPossibleAfterLimit()
        {
            verticalRow.AddContainer(new Container(30000, false, false));
            verticalRow.AddContainer(new Container(30000, false, false));
            verticalRow.AddContainer(new Container(30000, false, false));
            verticalRow.AddContainer(new Container(30000, false, false));
            verticalRow.AddContainer(new Container(30000, false, false));


            Assert.IsFalse(verticalRow.ContainerPossible);
        }
    }
}
