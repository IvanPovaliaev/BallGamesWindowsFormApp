using System.Drawing;
using System.Windows.Forms;

namespace DiffusionWindowsFormsApp
{
    public class Molecule : BillyardBall
    {
        public MoleculSide Side { get; }
        public Molecule(Form form, Color color, MoleculSide side) : base(form)
        {
            this.color = color;
            radius = 20;
            Side = side;
            InitializePositionBySide();
        }

        public override void Show()
        {
            base.Show();
            DrawLine();
        }
        public void ChangeColor(Color color) => this.color = color;
        private void DrawLine()
        {
            var graphics = form.CreateGraphics();
            var linePen = new Pen(Color.Black, 4);
            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            var x = form.Width / 2;
            var y0 = 0;
            var y1 = form.Height;
            graphics.DrawLine(linePen, x, y0, x, y1);
        }
        private void InitializePositionBySide()
        {
            if (Side == MoleculSide.Left)
            {
                centerX = random.Next(LeftSide(), form.Width / 2 - radius);
                centerY = random.Next(TopSide(), DownSide());
                return;
            }
            if (Side == MoleculSide.Right)
            {
                centerX = random.Next(form.Width / 2 + radius, RightSide());
                centerY = random.Next(TopSide(), DownSide());
                return;
            }
        }
    }
}
