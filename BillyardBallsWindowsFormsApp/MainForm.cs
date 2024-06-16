using BallGame.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiffusionWindowsFormsApp
{
    public partial class MainForm : Form
    {
        private static Random random = new Random();
        private List<Molecule> firstMolecules;
        private Color firstMoleculeColor;
        private List<Molecule> secondMolecules;
        private Color secondMoleculeColor;
        private System.Timers.Timer сheckEndMixTimer;
        private bool IsMoved = false;
        public MainForm()
        {
            InitializeComponent();
            сheckEndMixTimer = new System.Timers.Timer();
            сheckEndMixTimer.Interval = 10;
            сheckEndMixTimer.Elapsed += CheckEndMix;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeCounterLabelsColor();

            var moleculesCount = random.Next(1, 15) * 4;

            firstMolecules = new List<Molecule>();
            secondMolecules = new List<Molecule>();

            for (int i = 0; i < moleculesCount; i++)
            {
                Molecule molecule;
                if (i % 2 == 0)
                {
                    molecule = new Molecule(this, firstMoleculeColor, MoleculSide.Left);
                    molecule.OnHited += Ball_OnHitedFirst;
                    firstMolecules.Add(molecule);
                }
                else
                {
                    molecule = new Molecule(this, secondMoleculeColor, MoleculSide.Right);
                    molecule.OnHited += Ball_OnHitedSecond;
                    secondMolecules.Add(molecule);
                }                
            }
            сheckEndMixTimer.Start();
        }
        private void Ball_OnHitedFirst(object? sender, HitEventArgs e)
        {
            switch (e.Side)
            {
                case Side.Left:
                    BeginInvoke(() => leftFirstCounterLabel.Text = (int.Parse(leftFirstCounterLabel.Text) + 1).ToString());
                    break;
                case Side.Right:
                    BeginInvoke(() => rightFirstCounterLabel.Text = (int.Parse(rightFirstCounterLabel.Text) + 1).ToString());
                    break;
                case Side.Top:
                    BeginInvoke(() => topFirstCounterLabel.Text = (int.Parse(topFirstCounterLabel.Text) + 1).ToString());
                    break;
                case Side.Down:
                    BeginInvoke(() => downFirstCounterLabel.Text = (int.Parse(downFirstCounterLabel.Text) + 1).ToString());
                    break;
            }
        }
        private void Ball_OnHitedSecond(object? sender, HitEventArgs e)
        {
            switch (e.Side)
            {
                case Side.Left:
                    BeginInvoke(() => leftSecondCounterLabel.Text = (int.Parse(leftSecondCounterLabel.Text) + 1).ToString());
                    break;
                case Side.Right:
                    BeginInvoke(() => rightSecondCounterLabel.Text = (int.Parse(rightSecondCounterLabel.Text) + 1).ToString());
                    break;
                case Side.Top:
                    BeginInvoke(() => topSecondCounterLabel.Text = (int.Parse(topSecondCounterLabel.Text) + 1).ToString());
                    break;
                case Side.Down:
                    BeginInvoke(() => downSecondCounterLabel.Text = (int.Parse(downSecondCounterLabel.Text) + 1).ToString());
                    break;
            }
        }
        private void InitializeCounterLabelsColor()
        {
            var colors = ColorsLibrary.GetAll();
            SynchronizeColors(colors, ref firstMoleculeColor, "first");
            SynchronizeColors(colors, ref secondMoleculeColor, "second");
        }
        private void SynchronizeColors(List<Color> colors, ref Color moleculeColor, string number)
        {
            var colorIndex = random.Next(0, colors.Count);
            moleculeColor = colors[colorIndex];
            foreach (var control in Controls)
            {
                if(control is Label)
                {
                    var label = control as Label;
                    if(label.Name.ToLower().Contains(number))
                        label.ForeColor = moleculeColor;
                }
            }
            colors.Remove(moleculeColor);
        }
        private void CheckEndMix(object? sender, System.Timers.ElapsedEventArgs e)
        {
            CountMoleculesOnSides(firstMolecules, out var firstMoleculesLeft, out var firstMoleculesRight);
            CountMoleculesOnSides(secondMolecules, out var secondMoleculesLeft, out var secondMoleculesRight);

            if (firstMoleculesLeft == secondMoleculesLeft && firstMoleculesLeft == firstMoleculesRight
                && firstMoleculesRight == secondMoleculesRight && secondMoleculesLeft == secondMoleculesRight)
            {
                сheckEndMixTimer.Stop();

                StopAll();
                MouseDown -= MainForm_MouseDown;
                Invoke(() => infoLabel.Text = "Произошло полное перемешивание газов");

                var colors = ColorsLibrary.GetAll();
                var colorIndex = random.Next(0, colors.Count);
                var mixColor = colors[colorIndex];

                ChangeColorAsync(firstMolecules, mixColor);
                ChangeColorAsync(secondMolecules, mixColor);
            }

            void CountMoleculesOnSides(IEnumerable<Molecule> molecules, out int moleculesLeft, out int moleculesRight)
            {
                moleculesLeft = 0;
                moleculesRight = 0;
                foreach (var molecule in molecules)
                {
                    if (molecule.GetCoordinates(out _) <= CenterX())
                    {
                        moleculesLeft++;
                        continue;
                    }
                    moleculesRight++;
                }
            }
            async Task ChangeColorAsync(IEnumerable<Molecule> molecules, Color newColor)
            {
                await Task.Run(() =>
                {
                    foreach (var molecule in molecules)
                    {
                        molecule.ChangeColor(newColor);
                        molecule.Show();
                    }
                });
            }
        }
        private int CenterX() => Width / 2;
        private void StopAll()
        {
            Task.Run(() =>
            {
                foreach (var molecule in firstMolecules) molecule.Stop();
            });
            Task.Run(() =>
            {
                foreach (var molecule in secondMolecules) molecule.Stop();
            });            
        }
        private void StartAll()
        {
            foreach (var molecule in firstMolecules) molecule.Start();
            foreach (var molecule in secondMolecules) molecule.Start();
        }
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SwitchInfoLabelText();
                if (IsMoved)
                {
                    StopAll();

                    var firstGasPressure = GetGasPressure(firstMolecules);
                    var secondGasPressure = GetGasPressure(secondMolecules);

                    MessageBox.Show($"Давление первого газа на стенки сосуда: {firstGasPressure:0.000} ед./пиксель^2\n" +
                        $"Давление второго газа на стенки сосуда: {secondGasPressure:0.000} ед./пиксель^2\n" +
                        $"Для получения физических величин умножьте полученные значения на массу молекулы соответсвующего газа\n" +
                        $"и на масштаб пиксель/м");
                }
                else StartAll();

                IsMoved = !IsMoved;
            }
        }
        private double GetGasPressure(IEnumerable<Ball> list)
        {
            var v = list.Average(molecule => molecule.GetTotalVelocity()); // pixels/second
            var volume = Width * Height; //pixels^3
            var n = list.Count() / (double)volume; //molecular density 1/pixels^3

            return (1.0 / 3.0) * n * 1 * v * v; //считаем массу молекулы неизвестной. Давление 1 у.е/pixels2 Выводить будем 1 кг/пк^2
        }
        private void SwitchInfoLabelText()
        {
            if (IsMoved) infoLabel.Text = "Для продолжения нажмите ЛКМ";
            else infoLabel.Text = "Для остановки нажмите ЛКМ";
        }
    }
}