using System.Drawing;
using System.Windows.Forms;

namespace GameSnake
{
    public partial class InformationForm : Form
    {
        private Label labelHeader, labelAboutGame, labelHeader2, labelAboutDescription, labelHeader3, labelAboutNote;
        public InformationForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            labelHeader = new Label
            {
                AutoSize = true,
                Font = new Font("Times New Roman", 16.2F, FontStyle.Regular),
                Location = new Point(283, 20),
                Size = new Size(266, 33),
                Text = "Классическая змейка",
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(labelHeader);
            labelHeader2 = new Label
            {
                AutoSize = true,
                Font = new Font("Times New Roman", 16.2F, FontStyle.Regular),
                Location = new Point(342, 237),
                Size = new Size(131, 33),
                Text = "Описание",
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(labelHeader2);
            labelHeader3 = new Label
            {
                AutoSize = true,
                Font = new Font("Times New Roman", 16.2F, FontStyle.Regular),
                Location = new Point(334, 429),
                Size = new Size(160, 33),
                Text = "Примечание",
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(labelHeader3);
            labelAboutGame = new Label
            {
                AutoEllipsis = true,
                Font = new Font("Times New Roman", 13.8F, FontStyle.Regular),
                Location = new Point(12, 77),
                Size = new Size(816, 140),
                Text = "Многоуровневая игра, в которой вы берете под контроль небольшую змею. " +
                "Чем больше  фруктов она съест, тем больше вырастет и увеличиться её скорость передвижения. " +
                "Цель состоит в том, чтобы как можно больше съесть фруктов, не ударяясь при этом об стены и блоки.  " +
                "С увелечением уровней растет количество блоков."
            };
            Controls.Add(labelAboutGame);
            labelAboutDescription = new Label
            {
                AutoEllipsis = true,
                Font = new Font("Times New Roman", 13.8F, FontStyle.Regular),
                Location = new Point(12, 291),
                Size = new Size(816, 120),
                Text = "В самом начале игры змейка появляется в верхнем левом углу.\nДля управления змейкой используйте клавиши ↑  ↓  ←  →.\n" +
                "Для начала игры нажмите любую клавишу управления и змейка начнет движение\n.Чтобы снять или поставить игру на паузу, используйте пробел."
            };
            Controls.Add(labelAboutDescription);
            labelAboutNote = new Label
            {
                AutoEllipsis = true,
                Font = new Font("Times New Roman", 13.8F, FontStyle.Regular),
                Location = new Point(16, 484),
                Size = new Size(816, 65),
                Text = "Рекорды поставленные на каждом уровне можно посмотреть при выборе карты."
            };
            Controls.Add(labelAboutNote);
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(837, 635);
            StartPosition = FormStartPosition.CenterScreen;
            Icon = Icon.ExtractAssociatedIcon(@"Resources\IconSnake.ico");
            Text = "Змейка";
        }
    }
}
