using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_zad6
{
    public partial class Form1 : Form
    {
        private List<double> pointList = new List<double>();

        SolidBrush px = new SolidBrush(Color.Black);
        Pen newpx = new Pen(Brushes.Red);
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            pointList.Add(e.X);
            pointList.Add(e.Y);
            numericUpDownPoint.Value++;
            // rysowanie;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(pictureBoxBezier.Handle);
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (numericUpDownPoint.Value <= pointList.Count / 2 && numericUpDownPoint.Value != 0)
            {
                pointList[(int)numericUpDownPoint.Value * 2 - 2] = Int32.Parse(numericUpDownX.Value.ToString());
                pointList[(int)numericUpDownPoint.Value * 2 - 1] = Int32.Parse(numericUpDownY.Value.ToString());
                // rysowanie;
            }
        }

        private void NumericUpDownPoint_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPoint.Value <= pointList.Count / 2 && numericUpDownPoint.Value != 0)
            {
                numericUpDownX.Value = (decimal)pointList[(int)numericUpDownPoint.Value * 2 - 2];
                numericUpDownY.Value = (decimal)pointList[(int)numericUpDownPoint.Value * 2 - 1];
            }
            else
            {
                numericUpDownX.Value = 0;
                numericUpDownX.Value = 0;
            }
        }

    }
}
