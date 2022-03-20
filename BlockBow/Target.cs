
using System.Drawing;
using System.Windows.Forms;

namespace BlockBow
{
    class Target
    {
        public Bitmap imgTarget = new Bitmap(Properties.Resources.Target);
        public int x, y;
        public Target(int x, int y)
        {
            
            this.x = x;
            this.y = y;
            imgTarget.MakeTransparent();
        }
        
        public void Message()
        {
            MessageBox.Show("Вы попали в цель!", "сообщение", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
