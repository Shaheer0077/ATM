using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public partial class WelcomeForm : Form
    {
        private Panel pnlHeader;
        private Panel pnlContent;

        public WelcomeForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            // ================= FORM =================
            Text = "ATM System";
            ClientSize = new Size(900, 500);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;

            // ================= BACKGROUND IMAGE =================
            PictureBox bgImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.BG_Image,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(bgImage);
            bgImage.SendToBack();

            // ================= HEADER =================
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(0, 90, 160)
            };

            Label lblBank = new Label
            {
                Text = "GLOBAL BANK",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblBank);
            Controls.Add(pnlHeader);
            pnlHeader.BringToFront();

            // ================= CONTENT PANEL (NO WHITE BG) =================
            pnlContent = new Panel
            {
                Size = new Size(520, 260),
                BackColor = Color.Transparent,
                Location = new Point(
                    (ClientSize.Width - 520) / 2,
                    (ClientSize.Height - 260) / 2 + 40
                )
            };

            // 🔥 MUST be added to background for transparency
            bgImage.Controls.Add(pnlContent);
            pnlContent.BringToFront();

            // ================= CONTENT LAYOUT =================
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 35));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 30));

            Label lblWelcome = new Label
            {
                Text = "WELCOME TO ATM",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.DarkCyan,
            };

            Label lblInstruction = new Label
            {
                Text = "Please insert your card to continue",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.WhiteSmoke,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.MediumPurple
            };

            // ================= BUTTON =================
            Button btnInsert = new Button
            {
                Text = "INSERT CARD",
                Width = 220,
                Height = 50,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.None,

                UseVisualStyleBackColor = false // 🔥 REQUIRED
            };

            btnInsert.FlatAppearance.BorderSize = 2;
            btnInsert.FlatAppearance.BorderColor = Color.White;
            btnInsert.Cursor = Cursors.Hand;

            btnInsert.Click += BtnInsert_Click;

            layout.Controls.Add(lblWelcome, 0, 0);
            layout.Controls.Add(lblInstruction, 0, 1);
            layout.Controls.Add(btnInsert, 0, 2);

            pnlContent.Controls.Add(layout);
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
