using BallGame.Common;
using System.Windows.Forms;

namespace SaluteWinFormsApp
{
    public class SaluteBall : MoveBall
    {
        protected float g = 4.0f;
        public SaluteBall(Form form, float centerX, float centerY) : base(form)
        {
            radius = 15;
            vy = random.Next(-20, 0);
            this.centerX = centerX;
            this.centerY = centerY;
        }
        protected override void Go()
        {
            base.Go();
            vy += g;
        }
    }
}
