using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_zad6
{
    class Bezier
    {
        private double[] factorialArray;

        public Bezier()
        {
            CreateFactorialArray();
        }

        private void CreateFactorialArray()
        {
            double[] factorialArray = new double[33];
            factorialArray[0] = 1.0;
            factorialArray[1] = 1.0;
            factorialArray[2] = 2.0;
            factorialArray[3] = 6.0;
            factorialArray[4] = 24.0;
            factorialArray[5] = 120.0;
            factorialArray[6] = 720.0;
            factorialArray[7] = 5040.0;
            factorialArray[8] = 40320.0;
            factorialArray[9] = 362880.0;
            factorialArray[10] = 3628800.0;
            factorialArray[11] = 39916800.0;
            factorialArray[12] = 479001600.0;
            factorialArray[13] = 6227020800.0;
            factorialArray[14] = 87178291200.0;
            factorialArray[15] = 1307674368000.0;
            factorialArray[16] = 20922789888000.0;
            factorialArray[17] = 355687428096000.0;
            factorialArray[18] = 6402373705728000.0;
            factorialArray[19] = 121645100408832000.0;
            factorialArray[20] = 2432902008176640000.0;
            factorialArray[21] = 51090942171709440000.0;
            factorialArray[22] = 1124000727777607680000.0;
            factorialArray[23] = 25852016738884976640000.0;
            factorialArray[24] = 620448401733239439360000.0;
            factorialArray[25] = 15511210043330985984000000.0;
            factorialArray[26] = 403291461126605635584000000.0;
            factorialArray[27] = 10888869450418352160768000000.0;
            factorialArray[28] = 304888344611713860501504000000.0;
            factorialArray[29] = 8841761993739701954543616000000.0;
            factorialArray[30] = 265252859812191058636308480000000.0;
            factorialArray[31] = 8222838654177922817725562880000000.0;
            factorialArray[32] = 263130836933693530167218012160000000.0;
            this.factorialArray = factorialArray;
        }

        // Silnia
        private double Factorial(int n)
        {
            if (n < 0 || n > 32)
            {
                throw new Exception("n musi byc z przedzialu od 0 do 32");
            }
            return factorialArray[n];
        }

        // Symbol Newtona: n! / (i! * (n-i)!)
        private double Ni(int n, int i)
        {
            return Factorial(n) / (Factorial(i) * Factorial(n - i));
        }

        // Wielomian Bernsteina: (n nad i) * t^i * (1-t)^(n-i)
        private double BernsteinPolynomial(int n, int i, double t)
        {
            double result;
            double ti;      // t^i
            double tni;     // (1-n)^(n-i)

            ti = Math.Pow(t, i);
            tni = Math.Pow(1 - t, n - i);
            result = Ni(n, i) * ti * tni;
            return result;
        }

        // Koncowy wzor
        public double[] BezierCalculate(List<Point> pointList, int POINTS_PER_CURVE, double[] p)
        {
            int numberOfPoints = pointList.Count;
            int iCount = 0;
            int jCount;
            double step;
            double t = 0;

            step = 1.0 / (POINTS_PER_CURVE - 1);
            for (int k = 0; k < POINTS_PER_CURVE; k++)
            {
                jCount = 0;
                p[iCount] = 0.0;
                p[iCount + 1] = 0.0;
                for (int i = 0; i < numberOfPoints; i++)
                {
                    double bernsteinResult = BernsteinPolynomial(numberOfPoints - 1, i, t);
                    Point point = pointList[jCount];
                    p[iCount] += bernsteinResult * point.X;
                    p[iCount + 1] += bernsteinResult * point.Y;
                    jCount++;
                }
                iCount += 2;
                t += step;
            }
            return p;
        }
    }
}
