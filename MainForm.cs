using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameSnake
{
    public partial class MainForm : Form
    {
        private Label labelMenu, labelBand;
        private Button buttonPlay, buttonAboutGame, buttonAboutAuthor, buttonFolding, buttonExit;
        private Thread threadAnimation;
        public MainForm()
        {
            InitializeComponent();
            threadAnimation = new Thread(PrintAnimation);
            threadAnimation.Start();
            labelBand.MouseDown += new MouseEventHandler(LabelBandMouseDown);
            labelBand.MouseMove += new MouseEventHandler(LabelBandMouseMove);
            buttonFolding.MouseClick += new MouseEventHandler(ButtonFoldingMouseClick);
            buttonFolding.MouseLeave += new EventHandler(ButtonFoldingMouseLeave);
            buttonFolding.MouseMove += new MouseEventHandler(ButtonFoldingMouseMove);
            buttonExit.MouseClick += new MouseEventHandler(ButtonExitMouseClick);
            buttonExit.MouseLeave += new EventHandler(ButtonExitMouseLeave);
            buttonExit.MouseMove += new MouseEventHandler(ButtonExitMouseMove);
            buttonPlay.MouseClick += new MouseEventHandler(ButtonPlayMouseClick);
            buttonAboutAuthor.MouseClick += new MouseEventHandler(ButtonAboutAuthorMouseClick);
            buttonAboutGame.MouseClick += new MouseEventHandler(ButtonAboutGameMouseClick);
        }

        private void InitializeComponent()
        {
            BackgroundImage = Image.FromFile(@"Resources\BackgroundImage.jpeg");
            BackgroundImageLayout = ImageLayout.None;
            Icon = Icon.ExtractAssociatedIcon(@"Resources\IconSnake.ico");
            ClientSize = new Size(855, 700);
            labelMenu = new Label
            {
                BackColor = Color.FromArgb(12, 12, 12),
                Font = new Font("Segoe Print", 22.2F, FontStyle.Italic),
                Location = new Point(313, 52),
                Size = new Size(242, 49),
                Text = "Меню",
                TextAlign = ContentAlignment.MiddleCenter               
            };
            Controls.Add(labelMenu);
            labelBand = new Label
            {
                BackColor = SystemColors.Highlight,
                Cursor = Cursors.Hand,
                Location = new Point(0, 0),
                Size = new Size(745, 25)
            };
            Controls.Add(labelBand);
            buttonPlay = new Button
            {
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe Print", 16.2F, FontStyle.Italic),
                ForeColor = SystemColors.ControlText,
                Location = new Point(325, 188),
                Size = new Size(221, 54),
                Text = "Играть",
                TabStop = false
            };
            buttonPlay.FlatAppearance.BorderSize = 0;
            buttonPlay.FlatAppearance.MouseDownBackColor = Color.Lime;
            Controls.Add(buttonPlay);
            buttonAboutGame = new Button 
            {
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe Print", 16.2F, FontStyle.Italic),
                ForeColor = SystemColors.ControlText,
                Location = new Point(325, 338),
                Size = new Size(221, 54),
                Text = "Об игре",
                TabStop = false
            };
            buttonAboutGame.FlatAppearance.BorderSize = 0;
            buttonAboutGame.FlatAppearance.MouseDownBackColor = Color.Lime;
            Controls.Add(buttonAboutGame);
            buttonAboutAuthor = new Button
            {
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe Print", 16.2F, FontStyle.Italic),
                ForeColor = SystemColors.ControlText,
                Location = new Point(316, 490),
                Size = new Size(230, 54),
                Text = "Об авторе",
                TabStop = false
            };
            buttonAboutAuthor.FlatAppearance.BorderSize = 0;
            buttonAboutAuthor.FlatAppearance.MouseDownBackColor = Color.Lime;
            Controls.Add(buttonAboutAuthor);
            buttonFolding = new Button
            {
                BackColor = Color.FromArgb(12, 12, 12),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular),
                ForeColor = Color.Lime,
                Location = new Point(755, 0),
                Size = new Size(45, 25),
                Text = "—",
                TabStop = false
            };
            buttonFolding.FlatAppearance.BorderSize = 0;
            buttonFolding.FlatAppearance.MouseOverBackColor = Color.Red;
            Controls.Add(buttonFolding);
            buttonExit = new Button
            {
                BackColor = Color.FromArgb(12, 12, 12),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular),
                ForeColor = Color.Lime,
                Location = new Point(805, 0),
                Size = new Size(45, 25),
                Text = "X",
                TabStop = false
            };
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatAppearance.MouseOverBackColor = Color.Red;
            Controls.Add(buttonExit);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void PrintAnimation()
        {
            float step = 0;
            Color currentColor = Color.DarkGreen;
            Color targetColor = Color.LightBlue;
            Random rnd = new Random();
            while (true)
            {
                if (step >= 1f)
                {
                    step = 0;

                    int R = rnd.Next(0, 255);
                    int G = rnd.Next(0, 255);
                    int B = rnd.Next(0, 255);
                    currentColor = targetColor;
                    targetColor = Color.FromArgb(R, G, B);
                }
                int mixR = (int)(currentColor.R * (1f - step) + targetColor.R * step);
                int mixG = (int)(currentColor.G * (1f - step) + targetColor.G * step);
                int mixB = (int)(currentColor.B * (1f - step) + targetColor.B * step);
                labelMenu.ForeColor = Color.FromArgb(mixR, mixG, mixB);
                step += 0.03f;
                Thread.Sleep(120);
            }
        }
        private void ButtonPlayMouseClick(object sender, MouseEventArgs e)
        {
            PlayForm playForm = new PlayForm();
            playForm.ShowDialog();
        }
        private void ButtonAboutGameMouseClick (object sender, MouseEventArgs e)
        {
            InformationForm informationForm = new InformationForm();
            informationForm.ShowDialog();
        }
        private void ButtonAboutAuthorMouseClick (object sender, MouseEventArgs e)
        {
            MessageBox.Show("Студент группы БО221ИСТ\nБезгачев Егор Дмитриевич", "Змейка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ButtonExitMouseMove(object sender, MouseEventArgs e)
        {
            buttonExit.ForeColor = Color.Black;
        }
        private void ButtonExitMouseLeave(object sender, EventArgs e)
        {
            buttonExit.ForeColor = Color.Lime;
        }
        private void ButtonFoldingMouseLeave(object sender, EventArgs e)
        {
            buttonFolding.ForeColor = Color.Lime;
        }
        private void ButtonFoldingMouseMove(object sender, MouseEventArgs e)
        {
            buttonFolding.ForeColor = Color.Black;
        }

        private void ButtonFoldingMouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ButtonExitMouseClick(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Змейка", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                threadAnimation.Abort();
                Close();
            }
        }

        Point lastPoint;
        private void LabelBandMouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void LabelBandMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }      
    }
}
