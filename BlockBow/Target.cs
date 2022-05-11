using System.Drawing;
using System.Windows.Forms;

namespace BlockBow
{
    internal class Target
    {
        public Bitmap ImgTarget { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Target() : this(0, 0) { }
        public Target(int x, int y)
        {
            this.X = x;
            this.Y = y;
            ImgTarget = new Bitmap(Properties.Resources.Target);
            ImgTarget.MakeTransparent();
        }
        
        public void Message()
        {
            MessageBox.Show("Вы попали в цель!", "сообщение", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
