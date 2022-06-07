using System;
using System.Drawing;

namespace BlockBow
{
    class Bow
    {       
        public double x, y, n, m, a, b; //координаты полученные после поворота картинки
        public int xl1, yl1, xl2, yl2, xlc, ylc;  //координаты тетивы
        public double xL1, yL1, xL2, yL2, xLC, yLC, L;//координаты тетивы
        //параметры лука
        string geometryHand;
        int lenghtBow;
        double baseSize;
        int pullingForce;
        double weight;
        bool sight, stabilizer;
        
        public double angleTurn;
        public double x0, y0, N, M, A, B, Cx, Cy;//первоначальные координаты
        
        public Bitmap bowImg = new Bitmap(Properties.Resources.Bow);

        public Bow(string geometryHand, int lenghtBow, double baseSize, int pullingForce, double weight, bool sight, bool stabilizer, int yBow, double angleTurn, int xl1, int yl1, int xl2, int yl2, int xlc, int ylc)
        {
            bowImg.MakeTransparent();
            //присваивание первоначальных координат
            this.y0 = yBow;
            x0 = 0;
            y = y0; x = x0; N = 125; M = y0; A = 0; B = y0 + 100;
            this.xl1 = xl1; this.yl1 = yl1; this.xl2 = xl2; this.yl2 = yl2; this.xlc = xlc; this.ylc = ylc;
            xL1 = xl1; yL1 = yl1; xL2 = xl2; yL2 = yl2; xLC = xlc; yLC = ylc;
            //присваивание параметров лука
            this.angleTurn = angleTurn;
            this.geometryHand = geometryHand;
            this.lenghtBow = lenghtBow;
            this.baseSize = baseSize;
            this.pullingForce = pullingForce;
            this.weight = weight;
            this.sight = sight;
            this.stabilizer = stabilizer;
        }
        //поворачивает лук на угол angleTurn относительно горизонта
        public void Koop(double angle)
        {
            Cx = x0 + 125/2;
            Cy = y0 + 100/2;
            n = x0 + 125;
            m = y0;
            a = x0;
            b = y0 + 100;

            x = Cx + (-x0 + Cx) * Math.Sin(angle * Math.PI / 180) + (-Cy + y0) * Math.Cos(angle * Math.PI / 180);
            y = Cy + (-x0 + Cx) * Math.Cos(angle * Math.PI / 180) - (-Cy + y0) * Math.Sin(angle * Math.PI / 180);

            N = Cx + (-n + Cx) * Math.Sin(angle * Math.PI / 180) + (-Cy + m) * Math.Cos(angle * Math.PI / 180);
            M = Cy + (-n + Cx) * Math.Cos(angle * Math.PI / 180) - (-Cy + m) * Math.Sin(angle * Math.PI / 180);

            A = Cx + (-a + Cx) * Math.Sin(angle * Math.PI / 180) + (-Cy + b) * Math.Cos(angle * Math.PI / 180);
            B = Cy + (-a + Cx) * Math.Cos(angle * Math.PI / 180) - (-Cy + b) * Math.Sin(angle * Math.PI / 180);
        }

        //поворачивает тетиву на угол angleTurn относительно горизонта
        public void KoopL(double angle)
        {
            Cx = x0 + 125 / 2;
            Cy = y0 + 100 / 2;
            xlc -= (int)L;

            xL1 = Cx + (-xl1 + Cx) * Math.Sin(angle * Math.PI / 180) + (-Cy + yl1) * Math.Cos(angle * Math.PI / 180);
            yL1 = Cy + (-xl1 + Cx) * Math.Cos(angle * Math.PI / 180) - (-Cy + yl1) * Math.Sin(angle * Math.PI / 180);

            xL2 = Cx + (-xl2 + Cx) * Math.Sin(angle * Math.PI / 180) + (-Cy + yl2) * Math.Cos(angle * Math.PI / 180);
            yL2 = Cy + (-xl2 + Cx) * Math.Cos(angle * Math.PI / 180) - (-Cy + yl2) * Math.Sin(angle * Math.PI / 180);

            xLC = Cx + (-xlc + Cx) * Math.Sin(angle * Math.PI / 180) + (-Cy + ylc) * Math.Cos(angle * Math.PI / 180);
            yLC = Cy + (-xlc + Cx) * Math.Cos(angle * Math.PI / 180) - (-Cy + ylc) * Math.Sin(angle * Math.PI / 180);

            xlc = 45;
        }
        //вычисление дисперсии
        public double Duspers()
        {
            double dusper = 0;
            if (sight == true & stabilizer == true)
            {
                dusper += 0.1;
            }
            if (sight == true | stabilizer == true)
            {
                dusper += 0.3;
            }
            if (sight == false & stabilizer == false)
            {
                if (geometryHand == "defl") dusper += 0.3; else dusper += 0.8;
                dusper += (48 - lenghtBow) / 10;
                dusper += (24.5 - baseSize) / 4;
                dusper += (2.2 - weight);
            }

            return dusper;
        }
    }
}
