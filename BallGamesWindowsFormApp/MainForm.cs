using BallGame.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BallGamesWindowsFormApp
{
    public partial class MainForm : Form
    {
        private List<RandomMoveBall> ballsList = new List<RandomMoveBall>();
        private int totalBallsCount;
        private int availableBallsCount;
        private int caughtBallsCount;

        public MainForm()
        {
            InitializeComponent();
            stopButton.Enabled = false;
            countBallsLabel.Text = string.Empty;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Refresh();

            SwitchButtonsEnabledStatus();

            ballsList.Clear();

            for (int i = 0; i < new Random().Next(5, 50); i++)
            {
                var moveBall = new RandomMoveBall(this);
                ballsList.Add(moveBall);

                moveBall.Start();
                moveBall.AddDissapearEvent((o, e) =>
                {
                    availableBallsCount--;
                    Invoke(ShowCurrentBallsStatus);
                    ballsList.Remove(moveBall);
                    Invoke(CheckEndGame);
                });
            }

            totalBallsCount = ballsList.Count;
            availableBallsCount = ballsList.Count;
            caughtBallsCount = 0;
            ShowCurrentBallsStatus();
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            foreach (var ball in ballsList)
            {
                if (!ball.PartiallyOutForm())
                    caughtBallsCount++;

                ball.Stop();
            }
            ballsList.Clear();
            CheckEndGame();
        }
        private void exitButton_Click(object sender, EventArgs e) => Application.Exit();
        private void SwitchButtonsEnabledStatus()
        {
            stopButton.Enabled = !stopButton.Enabled;
            createButton.Enabled = !createButton.Enabled;
        }
        private void ShowCurrentBallsStatus() => countBallsLabel.Text = $"Шарики: {availableBallsCount} из {totalBallsCount}";
        private void CheckEndGame()
        {
            if (ballsList.Count == 0)
            {
                MessageBox.Show($"Количество пойманных шариков: {caughtBallsCount}");
                SwitchButtonsEnabledStatus();
            }
        }

    }
}
