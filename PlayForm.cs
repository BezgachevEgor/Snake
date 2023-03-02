using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GameSnake
{
    public partial class PlayForm : Form
    {
        private Label labelLevel, labelBand, labelRecords;
        private ListBox listBoxEnterLevel;
        private Button buttonStart, buttonExit;
        private PictureBox pictureBoxLevel;
        private static string choosenLevel;
        private static int[] records;
        public PlayForm()
        {
            InitializeComponent();
            ReadRecords();
            labelBand.MouseDown += new MouseEventHandler(LabelBandMouseDown);
            labelBand.MouseMove += new MouseEventHandler(LabelBandMouseMove);
            buttonExit.MouseClick += new MouseEventHandler(ButtonExitMouseClick);
            buttonExit.MouseLeave += new EventHandler(ButtonExitMouseLeave);
            buttonExit.MouseMove += new MouseEventHandler(ButtonExitMouseMove);
            listBoxEnterLevel.SelectedIndexChanged += new EventHandler(ChangeListBoxEnterLevelAndLabelRecords);
            buttonStart.MouseClick += new MouseEventHandler(ButtonStartMouseClick);
        }
        public PlayForm(int count)
        {
            CheckRecords(count);
        }
        
        private void InitializeComponent()
        {
            BackColor = SystemColors.Desktop;
            Icon = Icon.ExtractAssociatedIcon(@"Resources\IconSnake.ico");
            ClientSize = new Size(705, 488);
            labelLevel = new Label
            {
                Font = new Font("Segoe Print", 16.2F, FontStyle.Regular),
                ForeColor = SystemColors.ControlLightLight,
                Location = new Point(82, 349),
                Size = new Size(296, 52),
                Text = "Выберите уровень",
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(labelLevel);
            labelBand = new Label
            {
                BackColor = SystemColors.Highlight,
                ForeColor = SystemColors.ButtonHighlight,
                Location = new Point(0, 0),
                Size = new Size(645, 25),        
            };
            Controls.Add(labelBand);
            labelRecords = new Label
            {
                Font = new Font("Microsoft YaHei UI", 12.2F, FontStyle.Regular),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(440, 420),
                Size = new Size(140, 40)
            };
            Controls.Add(labelRecords);
            listBoxEnterLevel = new ListBox
            {
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular),
                FormattingEnabled = true,
                Location = new Point(402, 360),
                Size = new Size(158, 29),
                TabStop = false
            };
            listBoxEnterLevel.Items.AddRange(new object[] { "Level1", "Level2", "Level3", "Level4" });
            Controls.Add(listBoxEnterLevel);
            buttonStart = new Button
            {
                BackColor = SystemColors.ButtonHighlight,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular),
                Location = new Point(290, 420),
                Size = new Size(109, 44),
                Text = "Старт",
                TabStop = false
            };
            buttonStart.FlatAppearance.MouseDownBackColor = Color.Lime;
            Controls.Add(buttonStart);
            buttonExit = new Button
            {
                BackColor = SystemColors.Desktop,
                Cursor = Cursors.Hand,             
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular),
                ForeColor = Color.Lime,
                Location = new Point(655, 1),
                Size = new Size(45, 25),
                Text = "X",
                TabStop = false
            };
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatAppearance.MouseOverBackColor = Color.Red;
            Controls.Add(buttonExit);
            pictureBoxLevel = new PictureBox
            {
                BackgroundImage = Image.FromFile(@"Image\DefaltImage.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                Location = new Point(105, 60),
                Size = new Size(450, 280),
                SizeMode = PictureBoxSizeMode.CenterImage,
                TabStop = false
            };
            Controls.Add(pictureBoxLevel);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void ReadRecords()
        {
            records = new int[4];
            using (StreamReader streamReader = new StreamReader(@"records.txt"))
            {
                for (int i = 0; i < 4; i++)
                {
                    records[i] = Convert.ToInt32(streamReader.ReadLine());
                }
            }          
        }

        private void WriteRecords()
        {
            using(StreamWriter streamWriter = new StreamWriter(@"records.txt", false))
            {
                for (int i = 0; i < 4; i++)
                {
                    streamWriter.WriteLine(records[i]);
                }
            }
        }

        private void ButtonStartMouseClick(object sender, MouseEventArgs e)
        {
            choosenLevel = listBoxEnterLevel.Text;
            if (choosenLevel != "")
            {
                Hide();
                Game gameSnake = new Game(choosenLevel);
                gameSnake.Run();
            }
            else
            {
                MessageBox.Show("Вы не выбрали уровень!", "Змейка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void ButtonExitMouseMove(object sender, MouseEventArgs e)
        {
            buttonExit.ForeColor = Color.Black;
        }
        private void ButtonExitMouseLeave(object sender, EventArgs e)
        {
            buttonExit.ForeColor = Color.Lime;
        }
        private void ButtonExitMouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }
        private void ChangeListBoxEnterLevelAndLabelRecords(object sender, EventArgs e)
        {
            switch (listBoxEnterLevel.Text)
            {
                case "Level1": pictureBoxLevel.BackgroundImage = Image.FromFile(@"Image\Level1.png"); labelRecords.Text = $"Рекорд: {records[0]}"; break;
                case "Level2": pictureBoxLevel.BackgroundImage = Image.FromFile(@"Image\Level2.png"); labelRecords.Text = $"Рекорд: {records[1]}"; break;
                case "Level3": pictureBoxLevel.BackgroundImage = Image.FromFile(@"Image\Level3.png"); labelRecords.Text = $"Рекорд: {records[2]}"; break;
                case "Level4": pictureBoxLevel.BackgroundImage = Image.FromFile(@"Image\Level4.png"); labelRecords.Text = $"Рекорд: {records[3]}"; break;
            }
        }
        private void CheckRecords(int count)
        {
            switch (choosenLevel)
            {
                case "Level1": if (count > records[0])
                    {
                        records[0] = count;
                        WriteRecords();
                    }
                    break;
                case "Level2": if (count > records[1]) 
                    {
                        records[1] = count;
                        WriteRecords();
                    }
                    break;
                case "Level3": if (count > records[2]) 
                    {
                        records[2] = count;
                        WriteRecords();
                    } 
                    break;
                case "Level4": if (count > records[3]) 
                    {
                        records[3] = count;
                        WriteRecords();
                    } 
                    break;
            }
        }
    }
}
