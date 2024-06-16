using System;
using System.Windows.Forms;

namespace SaluteWinFormsApp
{
    public class VerticalSaluteBall : SaluteBall
    {
        public VerticalSaluteBall(Form form, int centerX) : base(form, centerX, 0)
        {
            radius = 25;
            vx = 0;

            //Находим предельную скорость такую, чтобы шарик подлетал максимум на 80% текущей высоты окна
            var vyLowerLimit = -Convert.ToInt32(Math.Sqrt(1.6 * form.ClientSize.Height * g));

            vy = random.Next(vyLowerLimit, vyLowerLimit / 2);

            this.centerX = centerX;
            centerY = form.ClientSize.Height;
        }

        protected override void Move_Tick(object? sender, EventArgs e)
        {
            base.Move_Tick(sender, e);
            if (vy >= 0) timer.Dispose();
        }
    }
}
