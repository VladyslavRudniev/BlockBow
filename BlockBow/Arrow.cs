using System;
using System.Drawing;
using System.Timers;

namespace BlockBow
{
    internal class Arrow
    {
        //первоначальные координаты
        public double Cx, Cy, y0;

        private double yArrow, xArrow, M, N, A, B;
        private double V, t = 0;
        private double angle;
        private const double m = 0.026;
        private const double g = 9.8;
        double turn;

        public double x, y, n1, m1, a, b, L;
        
        public Bitmap arrowImg = new Bitmap(Properties.Resources.Arrow);

        
        public Arrow(double x, double y)
        {
            
            this.xArrow = x; 
            this.yArrow = y;

            this.x = xArrow; 
            this.y = yArrow; 
            n1 = this.xArrow + 79; 
            m1 = this.yArrow; 
            a = this.xArrow; 
            b = this.yArrow + 5;
            arrowImg.MakeTransparent();
        }
        public Arrow(double x, double y, double V, double angle)
        {
            this.Cx = x;
            this.y0 = y;
            this.V = V;
            this.angle = angle;
            arrowImg.MakeTransparent();
        }

        //вычисление траектории полета
        public void Trajectory(object source, ElapsedEventArgs e)
        {
            Turn();
            //движение стрелы по траектории
            Cx = 50 + (V * t * Math.Cos(angle * Math.PI / 180));
            Cy = y0 - (V * t * Math.Sin(angle * Math.PI / 180) - ((g * Math.Pow(t, 2)) / 2));
            t += 0.2;//время полета
        }
        
        //Поворачивает картинку на угол "angle" от горизонта и меняет положение в соответствии с силой натяжения тетивы
        public void KoopA(double angle, double CBx, double CBy)
        {
            Cx = xArrow + 79 / 2;
            Cy = yArrow + 5 / 2;
            N = Cx + 79 / 2;
            M = Cy - 5 / 2;
            A = Cx - 79 / 2;
            B = Cy + 5 / 2;

            xArrow -= (int)L; N -= (int)L; A -= (int)L;

            x = CBx + (-xArrow + CBx) * Math.Sin(angle * Math.PI / 180) + (-CBy + yArrow) * Math.Cos(angle * Math.PI / 180);
            y = CBy + (-xArrow + CBx) * Math.Cos(angle * Math.PI / 180) - (-Cy + yArrow) * Math.Sin(angle * Math.PI / 180);

            n1 = CBx + (-N + CBx) * Math.Sin(angle * Math.PI / 180) + (-CBy + M) * Math.Cos(angle * Math.PI / 180);
            m1 = CBy + (-N + CBx) * Math.Cos(angle * Math.PI / 180) - (-CBy + M) * Math.Sin(angle * Math.PI / 180);

            a = CBx + (-A + CBx) * Math.Sin(angle * Math.PI / 180) + (-CBy + B) * Math.Cos(angle * Math.PI / 180);
            b = CBy + (-A + CBx) * Math.Cos(angle * Math.PI / 180) - (-CBy + B) * Math.Sin(angle * Math.PI / 180);

            xArrow = 45; N = 45; A = 124;
        }
        //Поворачивает картинку на угол "turn" от горизонта
        public void Turn() 
        { 
            xArrow = Cx - 79 / 2;
            yArrow = Cy - 5 / 2;
            N = Cx + 79 / 2;
            M = Cy - 5 / 2;
            A = Cx - 79 / 2;
            B = Cy + 5 / 2;

            turn = 270 + Math.Atan((Math.Tan(angle * Math.PI / 180) - ((g * Cx) / (V * V * Math.Pow(Math.Cos(angle * Math.PI / 180), 2))))) * 180 / Math.PI;
            
            x = Cx + (-xArrow + Cx) * Math.Sin(turn * Math.PI / 180) + (Cy - yArrow) * Math.Cos(turn * Math.PI / 180);
            y = Cy + (-xArrow + Cx) * Math.Cos(turn * Math.PI / 180) - (Cy - yArrow) * Math.Sin(turn * Math.PI / 180);

            n1 = Cx + (-N + Cx) * Math.Sin(turn * Math.PI / 180) + (Cy - M) * Math.Cos(turn * Math.PI / 180);
            m1 = Cy + (-N + Cx) * Math.Cos(turn * Math.PI / 180) - (Cy - M) * Math.Sin(turn * Math.PI / 180);

            a = Cx + (-A + Cx) * Math.Sin(turn * Math.PI / 180) + (Cy - B) * Math.Cos(turn * Math.PI / 180);
            b = Cy + (-A + Cx) * Math.Cos(turn * Math.PI / 180) - (Cy - B) * Math.Sin(turn * Math.PI / 180);

        }
    }
}
