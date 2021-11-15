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

        const int POINTS_PER_CURVE = 1000;

        public Form1()
        {
            InitializeComponent();
        }

        private void Draw()
        {
            g.Clear(Form1.ActiveForm.BackColor);
            // Rysowanie punktow.
            for (int i = 0; i < pointList.Count; i += 2)
            {
                Rectangle rectangle = new Rectangle((int)pointList[i] - 2, (int)pointList[i + 1] - 2, 4, 4);
                g.FillRectangle(px, rectangle);
            }

            // Rysowanie krzywej z POINTS_PER_CURVE punktow.
            Bezier bezier = new Bezier();
            double[] pointArray = new double[pointList.Count];
            pointList.CopyTo(pointArray, 0);
            double[] p = new double[POINTS_PER_CURVE];

            p = bezier.BezierCalculate(pointArray, POINTS_PER_CURVE / 2, p);
            for (int i = 1; i < POINTS_PER_CURVE - 1; i += 2)
            {
                g.DrawRectangle(newpx, new Rectangle((int)p[i + 1], (int)p[i], 1, 1));
                g.Flush();
                Application.DoEvents();
            }

        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            pointList.Add(e.X);
            pointList.Add(e.Y);
            //numericUpDownPoint.Maximum = numericUpDownPoint.Value + 1;
            numericUpDownPoint.Value++;
            //numericUpDownPoint.Minimum = 1;
            Draw();
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
                Draw();
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
                numericUpDownY.Value = 0;
            }
        }

    }
}
