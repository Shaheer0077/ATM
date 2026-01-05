using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public class ChangePINForm : Form
    {
        private Services.ATMService atmService;

        private Panel pnlHeader;
        private Panel pnlChange;
        private Panel pnlKeypad;

        private TextBox txtOldPIN;
        private TextBox txtNewPIN;
        private TextBox txtConfirmPIN;
        private TextBox activeTextBox;

        private Label lblMessage;

        private Button btnChange;
        private Button btnCancel;

        public ChangePINForm(Services.ATMService service)
        {
            atmService = service;
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            // ================= FORM =================
            Text = "ATM - Change PIN";
            ClientSize = new Size(920, 520);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            // ================= BACKGROUND =================
            PictureBox bg = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.BG_Image,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(bg);

            // ================= HEADER =================
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(0, 90, 160)
            };

            Label lblTitle = new Label
            {
                Text = "CHANGE PIN",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 24),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);
            Controls.Add(pnlHeader);
            pnlHeader.BringToFront();

            // ================= MAIN CONTAINER =================
            pnlChange = new Panel
            {
                Size = new Size(360, 360),
                Location = new Point(60, 120),
                BackColor = Color.Transparent
            };
            bg.Controls.Add(pnlChange);

            // ================= OLD PIN =================
            pnlChange.Controls.Add(CreateLabel("CURRENT PIN :", 20));
            Panel oldBg = CreateFieldBackground(50);
            pnlChange.Controls.Add(oldBg);

            txtOldPIN = CreateInnerTextBox();
            txtOldPIN.PasswordChar = '●';
            txtOldPIN.MaxLength = 4;
            txtOldPIN.Enter += (s, e) => activeTextBox = txtOldPIN;
            oldBg.Controls.Add(txtOldPIN);

            // ================= NEW PIN =================
            pnlChange.Controls.Add(CreateLabel("NEW PIN :", 100));
            Panel newBg = CreateFieldBackground(130);
            pnlChange.Controls.Add(newBg);

            txtNewPIN = CreateInnerTextBox();
            txtNewPIN.PasswordChar = '●';
            txtNewPIN.MaxLength = 4;
            txtNewPIN.Enter += (s, e) => activeTextBox = txtNewPIN;
            newBg.Controls.Add(txtNewPIN);

            // ================= CONFIRM PIN =================
            pnlChange.Controls.Add(CreateLabel("CONFIRM PIN :", 180));
            Panel confirmBg = CreateFieldBackground(210);
            pnlChange.Controls.Add(confirmBg);

            txtConfirmPIN = CreateInnerTextBox();
            txtConfirmPIN.PasswordChar = '●';
            txtConfirmPIN.MaxLength = 4;
            txtConfirmPIN.Enter += (s, e) => activeTextBox = txtConfirmPIN;
            confirmBg.Controls.Add(txtConfirmPIN);

            // ================= MESSAGE =================
            lblMessage = new Label
            {
                Top = 255,
                Left = 30,
                Width = 300,
                Height = 22,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            pnlChange.Controls.Add(lblMessage);

            // ================= BUTTONS =================
            btnChange = CreateButton("CHANGE", 285);
            btnCancel = CreateButton("CANCEL", 285);
            btnCancel.Left = 190;

            btnChange.Click += BtnChange_Click;
            btnCancel.Click += (s, e) => Close();

            pnlChange.Controls.Add(btnChange);
            pnlChange.Controls.Add(btnCancel);

            // ================= KEYPAD =================
            pnlKeypad = BuildKeypad();
            pnlKeypad.Location = new Point(470, 130);
            bg.Controls.Add(pnlKeypad);

            activeTextBox = txtOldPIN;
        }

        // ================= KEYPAD =================
        private Panel BuildKeypad()
        {
            Panel panel = new Panel
            {
                Size = new Size(320, 330),
                BackColor = Color.Transparent
            };

            string[] keys =
            {
                "1","2","3",
                "4","5","6",
                "7","8","9",
                "CLR","0","←"
            };

            int index = 0;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Button btn = new Button
                    {
                        Text = keys[index++],
                        Size = new Size(85, 60),
                        Location = new Point(25 + c * 95, 25 + r * 75),
                        Font = new Font("Segoe UI Semibold", 14),
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.White;
                    btn.Click += Keypad_Click;
                    panel.Controls.Add(btn);
                }
            }
            return panel;
        }

        private void Keypad_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) return;

            Button btn = sender as Button;

            if (btn.Text == "CLR")
                activeTextBox.Clear();
            else if (btn.Text == "←" && activeTextBox.Text.Length > 0)
                activeTextBox.Text = activeTextBox.Text[..^1];
            else if (activeTextBox.Text.Length < 4)
                activeTextBox.Text += btn.Text;
        }

        // ================= LOGIC =================
        private void BtnChange_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Text != txtConfirmPIN.Text)
            {
                lblMessage.Text = "PINs do not match";
                lblMessage.ForeColor = Color.FromArgb(231, 76, 60);
                return;
            }

            var (success, message) =
                atmService.ChangePIN(txtOldPIN.Text, txtNewPIN.Text);

            lblMessage.Text = message;
            lblMessage.ForeColor = success
                ? Color.FromArgb(46, 204, 113)
                : Color.FromArgb(231, 76, 60);

            if (success)
            {
                txtOldPIN.Clear();
                txtNewPIN.Clear();
                txtConfirmPIN.Clear();
            }
        }

        // ================= HELPERS (MATCH LOGIN) =================
        private Label CreateLabel(string text, int top) =>
            new Label
            {
                Text = text,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Top = top,
                Left = 30,
                Width = 280,
                BackColor = Color.Transparent
            };

        private Panel CreateFieldBackground(int top)
        {
            Panel panel = new Panel
            {
                Left = 25,
                Top = top,
                Width = 310,
                Height = 40,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            panel.Paint += (s, e) =>
            {
                using Pen pen = new Pen(Color.White, 2);
                Rectangle rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            };

            return panel;
        }

        private TextBox CreateInnerTextBox() =>
            new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 12),
                Left = 10,
                Top = 10,
                Width = 290,
                BackColor = Color.FromArgb(245, 247, 250),
                TextAlign = HorizontalAlignment.Center
            };

        private Button CreateButton(string text, int top) =>
            new Button
            {
                Text = text,
                Top = top,
                Left = 30,
                Width = 140,
                Height = 42,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 11)
            };

        private void InitializeComponent() { }
    }
}
