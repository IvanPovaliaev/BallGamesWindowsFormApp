using System;
using System.Windows.Forms;

namespace SaluteWinFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var verticalSaluteBall = new VerticalSaluteBall(this, e.X);
                verticalSaluteBall.Start();
                verticalSaluteBall.AddDissapearEvent((s, e) =>
                {
                    var x = verticalSaluteBall.GetCoordinates(out var y);
                    verticalSaluteBall.Clear();
                    for (int i = 0; i < new Random().Next(5, 100); i++)
                    {                                     
                        var saluteBall = new SaluteBall(this, x, y);
                        saluteBall.Start();
                    }
                });
            }
        }
    }
}
