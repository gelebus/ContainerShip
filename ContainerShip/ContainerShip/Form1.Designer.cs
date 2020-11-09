namespace ContainerShip
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LbContainers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbWidthShip = new System.Windows.Forms.TextBox();
            this.tbLengthShip = new System.Windows.Forms.TextBox();
            this.tbWeightShip = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btCooled = new System.Windows.Forms.CheckBox();
            this.btValuable = new System.Windows.Forms.CheckBox();
            this.tbContainerWeight = new System.Windows.Forms.TextBox();
            this.btAddContainer = new System.Windows.Forms.Button();
            this.btSort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LbContainers
            // 
            this.LbContainers.FormattingEnabled = true;
            this.LbContainers.Location = new System.Drawing.Point(12, 12);
            this.LbContainers.Name = "LbContainers";
            this.LbContainers.Size = new System.Drawing.Size(453, 433);
            this.LbContainers.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(455, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "---------------------------------------------------------------------------------" +
    "------------------------------------------";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "---------------------------------------------------------------------------------" +
    "------------------------------------------";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(485, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Standard size containers: l = 12m, w = 2.3m, h = 2.4m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(488, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Width Ship:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(488, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Length Ship:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(491, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Weight Ship:";
            // 
            // tbWidthShip
            // 
            this.tbWidthShip.Location = new System.Drawing.Point(556, 49);
            this.tbWidthShip.Name = "tbWidthShip";
            this.tbWidthShip.Size = new System.Drawing.Size(50, 20);
            this.tbWidthShip.TabIndex = 8;
            this.tbWidthShip.Text = "5";
            // 
            // tbLengthShip
            // 
            this.tbLengthShip.Location = new System.Drawing.Point(556, 77);
            this.tbLengthShip.Name = "tbLengthShip";
            this.tbLengthShip.Size = new System.Drawing.Size(50, 20);
            this.tbLengthShip.TabIndex = 9;
            this.tbLengthShip.Text = "26";
            // 
            // tbWeightShip
            // 
            this.tbWeightShip.Location = new System.Drawing.Point(556, 103);
            this.tbWeightShip.Name = "tbWeightShip";
            this.tbWeightShip.Size = new System.Drawing.Size(50, 20);
            this.tbWeightShip.TabIndex = 10;
            this.tbWeightShip.Text = "500000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(613, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(613, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "m";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(613, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "kg";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(488, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(284, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "The Weight of Containers can be within 4000 to 30000 kg.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(491, 202);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Weight:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(491, 234);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Cooled:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(491, 264);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Valuable:";
            // 
            // btCooled
            // 
            this.btCooled.AutoSize = true;
            this.btCooled.Location = new System.Drawing.Point(544, 234);
            this.btCooled.Name = "btCooled";
            this.btCooled.Size = new System.Drawing.Size(15, 14);
            this.btCooled.TabIndex = 18;
            this.btCooled.UseVisualStyleBackColor = true;
            // 
            // btValuable
            // 
            this.btValuable.AutoSize = true;
            this.btValuable.Location = new System.Drawing.Point(544, 264);
            this.btValuable.Name = "btValuable";
            this.btValuable.Size = new System.Drawing.Size(15, 14);
            this.btValuable.TabIndex = 19;
            this.btValuable.UseVisualStyleBackColor = true;
            // 
            // tbContainerWeight
            // 
            this.tbContainerWeight.Location = new System.Drawing.Point(544, 202);
            this.tbContainerWeight.Name = "tbContainerWeight";
            this.tbContainerWeight.Size = new System.Drawing.Size(62, 20);
            this.tbContainerWeight.TabIndex = 20;
            this.tbContainerWeight.Text = "30000";
            // 
            // btAddContainer
            // 
            this.btAddContainer.Location = new System.Drawing.Point(631, 195);
            this.btAddContainer.Name = "btAddContainer";
            this.btAddContainer.Size = new System.Drawing.Size(141, 90);
            this.btAddContainer.TabIndex = 21;
            this.btAddContainer.Text = "Add Container";
            this.btAddContainer.UseVisualStyleBackColor = true;
            this.btAddContainer.Click += new System.EventHandler(this.btAddContainer_Click);
            // 
            // btSort
            // 
            this.btSort.Location = new System.Drawing.Point(556, 329);
            this.btSort.Name = "btSort";
            this.btSort.Size = new System.Drawing.Size(141, 90);
            this.btSort.TabIndex = 22;
            this.btSort.Text = "Sort Containers";
            this.btSort.UseVisualStyleBackColor = true;
            this.btSort.Click += new System.EventHandler(this.btSort_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btSort);
            this.Controls.Add(this.btAddContainer);
            this.Controls.Add(this.tbContainerWeight);
            this.Controls.Add(this.btValuable);
            this.Controls.Add(this.btCooled);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbWeightShip);
            this.Controls.Add(this.tbLengthShip);
            this.Controls.Add(this.tbWidthShip);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LbContainers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LbContainers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbWidthShip;
        private System.Windows.Forms.TextBox tbLengthShip;
        private System.Windows.Forms.TextBox tbWeightShip;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox btCooled;
        private System.Windows.Forms.CheckBox btValuable;
        private System.Windows.Forms.TextBox tbContainerWeight;
        private System.Windows.Forms.Button btAddContainer;
        private System.Windows.Forms.Button btSort;
    }
}

