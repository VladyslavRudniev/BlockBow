using System;
using System.Windows.Forms;

namespace BlockBow
{
    public partial class Controller_MainForm : Form
    {
        View view;
        Model model;

        public Controller_MainForm()
        {
            
            InitializeComponent();
            
            model = new Model();
            view = new View(model);
            this.Controls.Add(view);

            model.onHit += model.target.Message; 
        }
        
        private void Controller_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (view.modelPlay != null)
            {
                model.gameStatus = GameStatus.waiting;
                view.modelPlay.Abort();
            }
            if (view.shotThread != null)
            {
                model.gameStatus = GameStatus.waiting;
                view.shotThread.Abort();
            }
        }
        private void Controller_MainForm_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //изменяется высота лука и стрелы в соответствии с ростом лучника
            model.yB = 420 - ((int)numericUpDown1.Value - 160) / 2;
            model.yl1 = 425 - ((int)numericUpDown1.Value - 160) / 2;
            model.yl2 = 515 - ((int)numericUpDown1.Value - 160) / 2;
            model.ylc = 470 - ((int)numericUpDown1.Value - 160) / 2;
            model.yArrow = 470 - ((int)numericUpDown1.Value - 160) / 2;
            //меняет параметры лука влияющие на сложность и точность стрельбы
            model.geometryHand = comboBox1.Text;
            model.lenghtBow = (int)numericUpDown2.Value;
            model.baseSize = (int)numericUpDown4.Value;
            model.pullingForce = (int)numericUpDown3.Value;//дальность выстрела: min - 30, max - 80
            model.weight = (int)numericUpDown5.Value;
             
            model.bow = new Bow(model.geometryHand, model.lenghtBow, model.baseSize, model.pullingForce, model.weight, model.sight, model.stabilizer, model.yB, model.angleTurn, model.xl1, model.yl1, model.xl2, model.yl2, model.xlc, model.ylc);
            model.arrow = new Arrow(model.xArrow, model.yArrow);
            view.Invalidate();
        }
    }
}
