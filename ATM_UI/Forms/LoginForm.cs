using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public class LoginForm : Form
    {
        private Services.ATMService atmService;

        private Panel pnlHeader;
        private Panel pnlLogin;
        private Panel pnlKeypad;

        private TextBox txtAccount;
        private TextBox txtPin;
        private TextBox activeTextBox;

        private Label lblError;
        private Label lblAttempts;

        private Button btnLogin;
        private Button btnExit;

        private int attemptCount = 0;
        private const int MaxAttempts = 3;

        public LoginForm()
        {
            atmService = new Services.ATMService();
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            // ================= FORM =================
            Text = "ATM Login";
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
                Text = "ATM LOGIN",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 24),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);
            Controls.Add(pnlHeader);
            pnlHeader.BringToFront();

            // ================= LOGIN CONTAINER =================
            pnlLogin = new Panel
            {
                Size = new Size(360, 330),
                Location = new Point(60, 130),
                BackColor = Color.Transparent
            };
            bg.Controls.Add(pnlLogin);

            // ================= ACCOUNT =================
            pnlLogin.Controls.Add(CreateLabel("ENTER YOUR ID :", 23));

            Panel accBg = CreateFieldBackground(50);
            pnlLogin.Controls.Add(accBg);

            txtAccount = CreateInnerTextBox();
            txtAccount.Enter += (s, e) => activeTextBox = txtAccount;
            accBg.Controls.Add(txtAccount);

            // ================= PIN =================
            pnlLogin.Controls.Add(CreateLabel("ENTER YOUR PIN :", 95));

            Panel pinBg = CreateFieldBackground(125);
            pnlLogin.Controls.Add(pinBg);

            txtPin = CreateInnerTextBox();
            txtPin.PasswordChar = '●';
            txtPin.Enter += (s, e) => activeTextBox = txtPin;
            pinBg.Controls.Add(txtPin);

            // ================= ERROR =================
            lblError = new Label
            {
                ForeColor = Color.FromArgb(220, 80, 80),
                Width = 300,
                Height = 22,
                Top = 170,
                Left = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            // ================= ATTEMPTS =================
            Panel attemptsBg = CreateFieldBackground(195);
            attemptsBg.Height = 32;

            lblAttempts = new Label
            {
                Dock = DockStyle.Fill,
                Text = $"Attempts remaining: {MaxAttempts}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(52, 152, 219),
            };

            attemptsBg.Controls.Add(lblAttempts);

            // ================= BUTTONS =================
            btnLogin = CreateButton("LOGIN", 245);
            btnExit = CreateButton("EXIT", 245);
            btnExit.Left = 190;

            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += (s, e) => Close();

            pnlLogin.Controls.AddRange(new Control[]
            {
                lblError,
                attemptsBg,
                btnLogin,
                btnExit
            });

            // ================= KEYPAD =================
            pnlKeypad = BuildKeypad();
            pnlKeypad.Location = new Point(470, 130);
            bg.Controls.Add(pnlKeypad);

            activeTextBox = txtAccount;
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
                activeTextBox.Text = "";
            else if (btn.Text == "←" && activeTextBox.Text.Length > 0)
                activeTextBox.Text = activeTextBox.Text[..^1];
            else if (btn.Text != "←")
                activeTextBox.Text += btn.Text;
        }

        // ================= LOGIN =================
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var (success, message) = atmService.Authenticate(
                txtAccount.Text.Trim(),
                txtPin.Text.Trim());

            if (success)
            {
                Hide();
                new MainMenuForm(atmService).ShowDialog();
                Close();
            }
            else
            {
                attemptCount++;
                lblError.Text = message;
                lblAttempts.Text = $"Attempts remaining: {MaxAttempts - attemptCount}";

                if (attemptCount >= MaxAttempts)
                {
                    MessageBox.Show("ATM Locked");
                    Close();
                }
            }
        }

        // ================= HELPERS =================
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
                BackColor = Color.FromArgb(245, 247, 250)
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
