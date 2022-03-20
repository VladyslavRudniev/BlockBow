using System;
using System.Timers;

namespace BlockBow
{
    class Model
    {
        public delegate void hitTarget(); 
        public event hitTarget onHit;     
        public GameStatus gameStatus;     
        System.Timers.Timer timer;       
        Random rand = new Random();       
        public Bow bow;                   
        public Arrow arrow;
        public Target target;
        
        //параметры Bow
        public double baseSize = 24.5, weight = 2.2;
        public int yB = 420; 
        public int lenghtBow = 48, pullingForce = 30;
        public string geometryHand = "defl";
        public bool sight = true, stabilizer = true;
        public double angleTurn;
        //параметры тетивы лука
        public int xl1 = 45, yl1 = 425, xl2 = 45, yl2 = 515, xlc = 45, ylc = 470;
        //координаты мыши в момент зажатия и отпускания
        public int x1, y1, x2, y2;

        //параметры Arrow
        public double yArrow = 470;
        public double xArrow = 45;
        double V;
        public int L;

        //параметры Target
        public int xTarget = 740;
        public int yTarget = 480;

        
        public Model()
        {
            gameStatus = GameStatus.waiting;

            bow = new Bow(geometryHand, lenghtBow, baseSize, pullingForce, weight, sight, stabilizer, yB, angleTurn, xl1, yl1, xl2, yl2, xlc, ylc);

            arrow = new Arrow(xArrow, yArrow);

            target = new Target(xTarget, yTarget);
            
            timer = new System.Timers.Timer();
        }
        
        //функция, вычисляющая силу натяжения тетивы
        public double l(int x1, int y1, int x2, int y2)
        {
            double l  = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            return l;
        }

        //вычисляет угол между координатами событий нажатия мыши и текущим положением курсора
        public double angle(int x1, int y1, int x2, int y2)
        {
            double angle = Math.Atan2(y2 - y1, x2 - x1) / Math.PI * 180;
            angle = (angle < 0) ? angle + 360 : angle;
            return angle;
        }

        //рандомное вычисление угла отклонения стрелы из за дрожания рук
        public double angleSlon()
        {
            double angleSlon;
            int dusp = (int)bow.duspers();
            angleSlon = rand.Next(0, dusp * 2) - dusp;
            return angleSlon;
        }

        //управление процессом полета стрели и попадания её в мишень
        public void play()
        {
            //вызывается конструктор лука, для того что бы вернуть его в исходное положение
            bow = new Bow(geometryHand, lenghtBow, baseSize, pullingForce, weight, sight, stabilizer, yB, angleTurn, xl1, yl1, xl2, yl2, xlc, ylc);
            //учитывая силу натяжения вычисляется скорость полета стрелы
            if (l(x1, y1, x2, y2) * 2 <= 100)
            {
                L = (int)l(x1, y1, x2, y2);
                V = L * 2;
            }
            else
            { 
                V = 100 + (pullingForce - 30)/2; 
                L = 45; 
            }
            //вызов конструктора стрелы которому передаются параметры для вычисления траектории  
            arrow = new Arrow(xArrow, yArrow, V, angleTurn - 270 - angleSlon());
            
            timer.Elapsed += new ElapsedEventHandler(arrow.Trajectory);
            timer.Interval = 1;
            
            while (arrow.Cy <= 580 & arrow.Cx >= -40 & arrow.Cx <= 840)
            {
                timer.Start(); 
                if (arrow.n1 >= 754 & arrow.n1 <= 770 & arrow.m1 >= 478 & arrow.m1 <= 544)
                {   
                    gameStatus = GameStatus.waiting;
                    onHit();
                    break;
                }
            }
            timer.Stop(); 
            gameStatus = GameStatus.waiting; 
        }
    }
}
