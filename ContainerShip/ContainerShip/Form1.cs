using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerShip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btAddContainer_Click(object sender, EventArgs e)
        {
            int weight = Convert.ToInt32(tbContainerWeight.Text);
            bool valuable = false;
            bool cooled = false;
            if (btCooled.Checked)
            {
                cooled = true;
            }
            if (btValuable.Checked)
            {
                valuable = true;
            }
            Container container = new Container(weight, valuable, cooled);
            container.ContainerId = 1 + LbContainers.Items.Count;
            LbContainers.Items.Add(container);
        }

        private void btSort_Click(object sender, EventArgs e)
        {
            int weightShip = Convert.ToInt32(tbWeightShip.Text);
            int lengthShip = Convert.ToInt32(tbLengthShip.Text);
            int widthShip = Convert.ToInt32(tbWidthShip.Text);
            List<Container> containers = new List<Container>();
            for (int i = 0; i < LbContainers.Items.Count; i++)
            {
                containers.Add((Container)LbContainers.Items[i]);
            }
            Ship ship = new Ship(containers,weightShip, lengthShip, widthShip);
            ship.Sort();
            if(ship.MaxWeight/2 > ship.WeightShip.WeightTotal)
            {
                MessageBox.Show("De boot kapseist, omdat het totaalgewicht minder dan 50% van het maximale gewicht is.");
            }
            else
            {
                MessageBox.Show(Convert.ToString(ship));
                if (ship.ContainersLeft.Count >= 1)
                {
                    string containersLeft = "";
                    for (int i = 0; i < ship.ContainersLeft.Count; i++)
                    {
                        containersLeft += Convert.ToString(ship.ContainersLeft[i]) + Environment.NewLine;
                    }
                    MessageBox.Show("Containers left:" + Environment.NewLine + containersLeft);
                }
            }
            LbContainers.Items.Clear();
        }
    }
}
