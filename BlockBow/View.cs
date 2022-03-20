using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BlockBow
{
    partial class View : UserControl
    {
        Model model;
        System.Timers.Timer timer;

        public Thread modelPlay, shotThread; //два потока, один для прорисовки выстрела, другой для прорисовки полета стрелы
        public int shotStartX, shotStartY, shotActiveX, shotActiveY; 
        public View(Model model)
        {

            InitializeComponent();
            this.model = model;
            timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(shot);
            timer.Interval = 50;
        }

        protected override void OnPaint(PaintEventArgs e)  
        {
            Draw(e);
        }

        public void Draw(PaintEventArgs e)
        {
            DrawTarget(e);
            DrawBow(e);

            if (model.arrow != null)
                DrawArrow(e);
            
            if (model.gameStatus == GameStatus.waiting)
                return;

            Invalidate();
        }

        private void DrawBow(PaintEventArgs e)
        {
            Point a = new Point((int)model.bow.x, (int)model.bow.y);
            Point b = new Point((int)model.bow.N, (int)model.bow.M);
            Point c = new Point((int)model.bow.A, (int)model.bow.B);
            Point[] bO = {a,b,c};//рисуем по трем точкам что бы можно было повернуть картинку на любой угол
            e.Graphics.DrawImage(model.bow.bowImg, bO);
            //рисует две линии которые представляют собой тетиву лука
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black)), new Point((int)model.bow.xL1, (int)model.bow.yL1), new Point((int)model.bow.xLC, (int)model.bow.yLC));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black)), new Point((int)model.bow.xLC, (int)model.bow.yLC), new Point((int)model.bow.xL2, (int)model.bow.yL2));

        }

        private void DrawTarget(PaintEventArgs e)
        {
             e.Graphics.DrawImage(model.target.imgTarget, new Point(model.target.x, model.target.y));
        }
 
        private void DrawArrow(PaintEventArgs e)
        {
             Point a = new Point((int)model.arrow.x, (int)model.arrow.y);
             Point b = new Point((int)model.arrow.n1, (int)model.arrow.m1);
             Point c = new Point((int)model.arrow.a, (int)model.arrow.b);
             Point[] bO = { a, b, c };//рисуем по трем точкам что бы можно было повернуть картинку на любой угол
             e.Graphics.DrawImage(model.arrow.arrowImg, bO);
        }


void start() 
{
    while (model.gameStatus == GameStatus.shot)//прекращает работу когда кнопку мыши отпустят
    {
        timer.Start();//таймер каждый промежуток времени меняет и перерисовывает угол лука и тетивы
    }
    timer.Stop();
}
void shot(object source, System.Timers.ElapsedEventArgs e)
{
            
            shotActiveX = Control.MousePosition.X;
            shotActiveY = Control.MousePosition.Y;
            //сила натяжения тетивы
            if (model.l(shotStartX, shotStartY, shotActiveX, shotActiveY) <= 45) model.L = (int)model.l(shotStartX, shotStartY, shotActiveX, shotActiveY); else model.L = 45;
            model.bow.L = model.L;
            model.arrow.L = model.L;
            //угол между двумя точками
            model.angleTurn = 270 + (90 - (model.angle(shotStartX, shotStartY, shotActiveX, shotActiveY) - 90));
            //вычисление координат лука, тетивы и стрелы
            model.bow.Koop(model.angleTurn);
            model.bow.KoopL(model.angleTurn);
            model.arrow.KoopA(model.angleTurn, model.bow.Cx, model.bow.Cy);
        }

private void View_MouseDown(object sender, MouseEventArgs e)
{
    shotStartX = Control.MousePosition.X;
    shotStartY = Control.MousePosition.Y;
    model.arrow = new Arrow(model.xArrow, model.yArrow);//создается новый обьект стрелы и вкладывается в лук
    shotThread = new Thread(start);
    model.gameStatus = GameStatus.shot;
    
    shotThread.Start();
    Invalidate();
}

private void View_MouseUp(object sender, MouseEventArgs e)
{
            model.gameStatus = GameStatus.playing;
            model.x1 = shotStartX; model.y1 = shotStartY;//в модель передается начальные координаты зажатия мыши
            model.x2 = shotActiveX; model.y2 = shotActiveY;//и конечные координаты

            modelPlay = new Thread(model.play);
            modelPlay.Start();
            Invalidate();
}
}
}
