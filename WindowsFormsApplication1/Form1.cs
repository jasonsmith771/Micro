using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private readonly double pi = 3.14159265359;
        private readonly double dToR = 1.74532925199432E-02;
       private Robot r;
        public Form1()
        {
            InitializeComponent();
            r = new Robot(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = r.GetImage();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            r = new Robot(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = r.GetImage();
        }
        private void pictureBox1_SizeChanged(object sender, System.EventArgs e)
        {
            r.Resize(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = r.GetImage();
            pictureBox1.Refresh();
            pictureBox1.Show();
        }
        private double round(double x, int n)
        {
            //Was 
            double v;
            v = Math.Pow(10, n);
            return Math.Round(x * v) / v;

        }
        private void RefreshLabels()
        {
            tuple p = r.GetPosition();
            lX.Text = round((p.t1), 4).ToString();
            lY.Text = round(p.t2, 4).ToString();
            lZ.Text = round(p.t3, 4).ToString();
            lA.Text = round(p.t4 / dToR, 4).ToString();
            lB.Text = round(p.t5 / dToR, 4).ToString();
            lC.Text = round(p.t6 / dToR, 4).ToString();
          //  MessageBox.Show("CLR MessageBox", round((p.t1), 4).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
        private void RefreshAngles()
        {
            r.MoveToMotorAngles((double)nA1.Value * dToR, (double)nA2.Value * dToR, (double)nA3.Value * dToR, (double)nA4.Value * dToR, (double)nA5.Value * dToR, (double)nA6.Value * dToR);
            this.pictureBox1.Image = r.GetImage();
            RefreshLabels();
        }
        private void nA1_ValueChanged(object sender, System.EventArgs e)
        {
            if (rAngles.Checked)
            {
                RefreshAngles();
            }
        }

        private void nA2_ValueChanged(object sender, System.EventArgs e)
        {
            if (rAngles.Checked)
            {
                RefreshAngles();
            }
        }
        private void nA3_ValueChanged(object sender, System.EventArgs e)
        {
            if (rAngles.Checked)
            {
                RefreshAngles();
            }
        }
        private void nA4_ValueChanged(object sender, System.EventArgs e)
        {
            if (rAngles.Checked)
            {
                RefreshAngles();
            }
        }
        private void nA5_ValueChanged(object sender, System.EventArgs e)
        {
            if (rAngles.Checked)
            {
                RefreshAngles();
            }
        }
        private void nA6_ValueChanged(object sender, System.EventArgs e)
        {
            if (rAngles.Checked)
            {
                RefreshAngles();
            }
        }
        private void rAngles_CheckedChanged(object sender, System.EventArgs e)
        {
            this.groupBox1.Enabled = rAngles.Checked;
            this.groupBox2.Enabled = !rAngles.Checked;
        }
        private void RefreshPositions()
        {
            tuple p;
            p = r.MoveToPositions((double)nX.Value, (double)nY.Value, (double)nZ.Value, (double)nA.Value * dToR, (double)nB.Value * dToR, (double)nC.Value * dToR);
            Console.WriteLine(p);
            //char buffer[32];
            //snprintf(buffer, sizeof(buffer), "%g", (double)nX->Value);
            this.pictureBox1.Image = r.GetImage();
            try
            {

                nA1.Value = decimal.Round((decimal)(p.t1 / dToR), 2);
                nA2.Value = decimal.Round((decimal)(p.t2 / dToR), 2);
                nA3.Value = decimal.Round((decimal)(p.t3 / dToR), 2);
                nA4.Value = decimal.Round((decimal)(p.t4 / dToR), 2);
                nA5.Value = decimal.Round((decimal)(p.t5 / dToR), 2);
                nA6.Value = decimal.Round((decimal)(p.t6 / dToR), 2);
                Console.WriteLine(nA1.Value);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            RefreshLabels();
        }
        private void nX_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshPositions();

        }
        private void nY_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshPositions();
        }
        private void nZ_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshPositions();
        }
        private void nA_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshPositions();
            Console.WriteLine("A1 RP");
        }
        private void nB_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshPositions();
        }
        private void nC_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshPositions();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Update();
        }
    }

}

