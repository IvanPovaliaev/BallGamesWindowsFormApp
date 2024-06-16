using BallGame.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BallGame2WindowsFormApp
{
    public partial class MainForm : Form
    {
        private List<Ball> ballsList = new List<Ball>();
        private int totalBallsCount;
        private int caughtBallsCount;

        public MainForm()
        {
            InitializeComponent();
            countBallsLabel.Text = string.Empty;
            breakGameButton.Enabled = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Refresh();
            caughtBallsCount = 0;

            MouseDown += MainForm_MouseDown;

            SwitchButtonsEnabledStatus();

            for (int i = 0; i < new Random().Next(5, 50); i++)
            {
                var moveBall = new RandomMoveBall(this);
                ballsList.Add(moveBall);

                moveBall.Start();
                moveBall.AddDissapearEvent((o, e) =>
                {
                    ballsList.Remove(moveBall);
                    Invoke(CheckEndGame);
                });
            }

            totalBallsCount = ballsList.Count;
            ShowCurrentBallsStatus();
        }
        private void breakGameButton_Click(object sender, EventArgs e)
        {
            ballsList.ForEach(ball => ball.Stop());
            ballsList.Clear();
            MessageBox.Show($"Игра была прервана. Вы успели поймать следующее количество шаров: {caughtBallsCount}");
            caughtBallsCount = 0;
            SwitchButtonsEnabledStatus();            
        }
        private void exitButton_Click(object sender, EventArgs e) => Application.Exit();
        private void ShowCurrentBallsStatus() => countBallsLabel.Text = $"Шарики: {caughtBallsCount} из {totalBallsCount}";
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            var removeBalls = new List<Ball>();

            foreach (var ball in ballsList)
            {
                var x0 = ball.GetCoordinates(out var y0);

                if (Math.Pow(e.X - x0, 2) + Math.Pow(e.Y - y0, 2) > Math.Pow(ball.GetSize(), 2))
                    continue;

                if (ball.PartiallyOutForm())
                    continue;

                ball.Stop();
                ball.Clear();

                caughtBallsCount++;
                ShowCurrentBallsStatus();
                removeBalls.Add(ball);
            }

            removeBalls.ForEach(ball => ballsList.Remove(ball));

            CheckEndGame();
        }
        private void CheckEndGame()
        {
            if (ballsList.Count == 0)
            {
                MouseDown -= MainForm_MouseDown;
                MessageBox.Show($"Количество пойманных шариков: {caughtBallsCount}");
                SwitchButtonsEnabledStatus();                
            }
        }
        private void SwitchButtonsEnabledStatus()
        {
            createButton.Enabled = !createButton.Enabled;
            breakGameButton.Enabled = !breakGameButton.Enabled;
        }
    }
}
