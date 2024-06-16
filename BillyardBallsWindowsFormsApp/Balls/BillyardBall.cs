using BallGame.Common;
using System;
using System.Windows.Forms;

namespace DiffusionWindowsFormsApp
{
    public class BillyardBall : MoveBall
    {
        public event EventHandler<HitEventArgs> OnHited;
        public BillyardBall(Form form) : base(form)
        {
        }
        protected override void Go()
        {
            base.Go();
            if (centerX <= LeftSide())
            {
                vx = -vx;
                OnHited.Invoke(this, new HitEventArgs(Side.Left));
            }
            if (centerX >= RightSide())
            {
                vx = -vx;
                OnHited.Invoke(this, new HitEventArgs(Side.Right));
            }
            if (centerY <= TopSide())
            {
                vy = -vy;
                OnHited.Invoke(this, new HitEventArgs(Side.Top));
            }
            if (centerY >= DownSide())
            {
                vy = -vy;
                OnHited.Invoke(this, new HitEventArgs(Side.Down));
            }
        }
    }
}
