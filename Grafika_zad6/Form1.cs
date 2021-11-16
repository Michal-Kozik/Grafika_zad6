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
        private List<Point> pointList = new List<Point>();

        SolidBrush px = new SolidBrush(Color.Black);
        Pen newpx = new Pen(Brushes.Red);
        Graphics g;

        // Ilosc punktow bedzie 2 razy mniejsza.
        const int POINTS_PER_CURVE = 1000;

        public Form1()
        {
            InitializeComponent();
        }

        private void Draw()
        {
            g.Clear(Form1.ActiveForm.BackColor);
            // Rysowanie punktow.
            for (int i = 0; i < pointList.Count; i++)
            {
                Point point = pointList[i];
                Rectangle rectangle = new Rectangle(point.X - 2, point.Y - 2, 4, 4);
                g.FillRectangle(px, rectangle);
            }

            // Rysowanie krzywej z POINTS_PER_CURVE / 2 punktow.
            Bezier bezier = new Bezier();
            List<Point> pointListCopy = new List<Point>(pointList);
            double[] p = new double[POINTS_PER_CURVE];

            p = bezier.BezierCalculate(pointListCopy, POINTS_PER_CURVE / 2, p);
            for (int i = 1; i < POINTS_PER_CURVE - 1; i += 2)
            {
                g.DrawRectangle(newpx, new Rectangle((int)p[i + 1], (int)p[i], 1, 1));
                g.Flush();
                Application.DoEvents();
            }
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            pointList.Add(point);
            numericUpDownPoint.Maximum++;
            numericUpDownPoint.Value = pointList.Count;
            numericUpDownPoint.Minimum = 1;
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(pictureBoxBezier.Handle);
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (numericUpDownPoint.Value <= pointList.Count && numericUpDownPoint.Value != 0)
            {
                pointList[(int)numericUpDownPoint.Value - 1] = new Point(Int32.Parse(numericUpDownX.Value.ToString()), Int32.Parse(numericUpDownY.Value.ToString()));
                Draw();
            }
        }

        private void NumericUpDownPoint_ValueChanged(object sender, EventArgs e)
        {
            Point point = pointList[(int)numericUpDownPoint.Value - 1];
            numericUpDownX.Value = point.X;
            numericUpDownY.Value = point.Y;
        }
    }
}
