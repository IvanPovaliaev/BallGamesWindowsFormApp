using System.Drawing;
using System.Windows.Forms;

namespace BallGame2WindowsFormApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            createButton = new Button();
            exitButton = new Button();
            countBallsLabel = new Label();
            breakGameButton = new Button();
            SuspendLayout();
            // 
            // createButton
            // 
            createButton.FlatStyle = FlatStyle.Flat;
            createButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            createButton.ForeColor = Color.Black;
            createButton.Location = new Point(12, 12);
            createButton.Name = "createButton";
            createButton.Size = new Size(150, 35);
            createButton.TabIndex = 0;
            createButton.Text = "Создать";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += startButton_Click;
            // 
            // exitButton
            // 
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            exitButton.Location = new Point(12, 94);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(150, 35);
            exitButton.TabIndex = 0;
            exitButton.Text = "Выход";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // countBallsLabel
            // 
            countBallsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            countBallsLabel.AutoSize = true;
            countBallsLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            countBallsLabel.Location = new Point(13, 520);
            countBallsLabel.Name = "countBallsLabel";
            countBallsLabel.Size = new Size(136, 20);
            countBallsLabel.TabIndex = 1;
            countBallsLabel.Text = "Шарики 0 из 10";
            // 
            // breakGameButton
            // 
            breakGameButton.FlatStyle = FlatStyle.Flat;
            breakGameButton.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            breakGameButton.ForeColor = Color.Black;
            breakGameButton.Location = new Point(12, 53);
            breakGameButton.Name = "breakGameButton";
            breakGameButton.Size = new Size(150, 35);
            breakGameButton.TabIndex = 0;
            breakGameButton.Text = "Прервать игру";
            breakGameButton.UseVisualStyleBackColor = true;
            breakGameButton.Click += breakGameButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(784, 561);
            Controls.Add(countBallsLabel);
            Controls.Add(exitButton);
            Controls.Add(breakGameButton);
            Controls.Add(createButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(800, 600);
            MinimizeBox = false;
            MinimumSize = new Size(800, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поймай меня";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button createButton;
        private Button exitButton;
        private Label countBallsLabel;
        private Button breakGameButton;
    }
}
