using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public partial class WelcomeForm : Form
    {
        private Panel pnlContent;

        public WelcomeForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.AutoScaleMode = AutoScaleMode.None;

            // ========= FORM =========
            Text = "ATM System";
            ClientSize = new Size(900, 500);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;

            // ========= BACKGROUND IMAGE =========
            PictureBox bg = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.Home_Image,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(bg);


            // ========= LEFT CONTENT PANEL =========
            Panel leftPanel = new Panel
            {
                Width = 420,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(200, 10, 30, 80) // deep glass blue
            };
            bg.Controls.Add(leftPanel);


            // ========= INNER WRAPPER (FOR SPACING) =========
            Panel inner = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 40, 25, 25)
            };
            leftPanel.Controls.Add(inner);


            // ========= LAYOUT =========
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 95));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            inner.Controls.Add(layout);


            // ========= TITLE =========
            Label lblWelcome = new Label
            {
                Text = "WELCOME TO ATM",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };


            // ========= SUB TEXT =========
            Label lblInstruction = new Label
            {
                Text = "Please insert your card to continue",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                ForeColor = Color.FromArgb(235, 235, 235),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };


            // ========= CARD IMAGE =========
            PictureBox card = new PictureBox
            {
                Image = RotateImage(Properties.Resources.ATM_CARD, 90),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };


            // ========= BUTTON =========
            Button btnInsert = new Button
            {
                Text = "INSERT CARD",
                Dock = DockStyle.Top,
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


            // ========= ADD CONTROLS =========
            layout.Controls.Add(lblWelcome, 0, 0);
            layout.Controls.Add(lblInstruction, 0, 1);
            layout.Controls.Add(card, 0, 2);
            layout.Controls.Add(btnInsert, 0, 3);
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
        private Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix
                {
                    Matrix33 = opacity // 0.0 = invisible, 1.0 = fully visible
                };

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                g.DrawImage(image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel,
                    attributes);
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

        private void InitializeComponent()
        {
            // intentionally empty
        }
    }
}
