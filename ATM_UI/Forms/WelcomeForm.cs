using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            AutoScaleMode = AutoScaleMode.None;

            Text = "ATM System";
            ClientSize = new Size(900, 500);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;

            PictureBox bg = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.Home_Image,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(bg);

            Panel leftPanel = new Panel
            {
                Width = 420,
                Dock = DockStyle.Left,
                BackColor = Color.Transparent
            };
            bg.Controls.Add(leftPanel);

            Panel inner = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 40, 25, 25)
            };
            leftPanel.Controls.Add(inner);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                AutoSize = true
            };

            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Welcome
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180)); // Card
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Instruction
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Button

            inner.Controls.Add(layout);

            Panel welcomeBg = new Panel
            {
                Dock = DockStyle.Top,
                //BackColor = Color.FromArgb(200, 10, 30, 80),
                Padding = new Padding(10),
                AutoSize = true
            };

            Label lblWelcome = new Label
            {
                Text = "WELCOME TO ATM",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            welcomeBg.Controls.Add(lblWelcome);

            PictureBox card = new PictureBox
            {
                Image = RotateImage(Properties.Resources.ATM_CARD, 90),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            Label lblInstruction = new Label
            {
                Text = "Please insert your card to continue",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(200, 10, 30, 80),
                ForeColor = Color.White,
                AutoSize = true,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnInsert = new Button
            {
                Text = "INSERT CARD",
                Width = 220,
                Height = 52,
                BackColor = Color.FromArgb(0, 130, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            btnInsert.FlatAppearance.BorderSize = 2;
            btnInsert.FlatAppearance.BorderColor = Color.White;
            btnInsert.Cursor = Cursors.Hand;
            btnInsert.Click += BtnInsert_Click;

            Panel btnWrapper = new Panel { Dock = DockStyle.Fill };
            btnWrapper.Controls.Add(btnInsert);
            btnWrapper.Resize += (s, e) =>
            {
                btnInsert.Left = (btnWrapper.Width - btnInsert.Width) / 2;
                btnInsert.Top = 10;
            };

            layout.Controls.Add(welcomeBg, 0, 0);
            layout.Controls.Add(card, 0, 1);
            layout.Controls.Add(lblInstruction, 0, 2);
            layout.Controls.Add(btnWrapper, 0, 3);
        }

        private Image RotateImage(Image img, float angle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TranslateTransform(img.Width / 2f, img.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-img.Width / 2f, -img.Height / 2f);
                g.DrawImage(img, new Point(0, 0));
            }

            return bmp;
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            Hide();
            login.ShowDialog();
            Close();
        }

        private void InitializeComponent() { }
    }
}
