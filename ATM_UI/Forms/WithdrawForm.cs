using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public class WithdrawForm : Form
    {
        private Services.ATMService atmService;

        private Panel pnlHeader;
        private Panel pnlWithdraw;
        private Panel pnlKeypad;

        private Label lblTitle;
        private Label lblBalance;
        private Label lblMessage;

        private TextBox txtAmount;

        private Button btnWithdraw;
        private Button btnCancel;

        public WithdrawForm(Services.ATMService service)
        {
            atmService = service;
            BuildUI();
        }

        private void BuildUI()
        {
            // ================= FORM =================
            Text = "ATM Withdraw";
            ClientSize = new Size(920, 520);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            // ================= BACKGROUND IMAGE =================
            PictureBox bg = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.BG_Image,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(bg);
            bg.SendToBack();

            // ================= HEADER =================
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(0, 90, 160)
            };

            lblTitle = new Label
            {
                Text = "WITHDRAW CASH",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);
            Controls.Add(pnlHeader);
            pnlHeader.BringToFront();

            // ================= MAIN CONTAINER (NO BG) =================
            pnlWithdraw = new Panel
            {
                Size = new Size(370, 340),
                Location = new Point(60, 120),
                BackColor = Color.Transparent
            };
            bg.Controls.Add(pnlWithdraw);

            // ================= BALANCE =================
            lblBalance = new Label
            {
                Text = $"Current Balance: ${atmService.CheckBalance():F2}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Top = 20,
                Left = 25,
                Width = 320,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(52, 152, 219)
            };
            pnlWithdraw.Controls.Add(lblBalance);

            // ================= AMOUNT FIELD =================
            Panel amountBg = CreateFieldBackground(70);
            pnlWithdraw.Controls.Add(amountBg);

            txtAmount = CreateInnerTextBox();
            txtAmount.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtAmount.TextAlign = HorizontalAlignment.Center;
            amountBg.Controls.Add(txtAmount);

            // ================= PRESET BUTTONS =================
            FlowLayoutPanel presetPanel = new FlowLayoutPanel
            {
                Top = 130,
                Left = 20,
                Width = 330,
                Height = 90
            };

            AddPreset(presetPanel, "500");
            AddPreset(presetPanel, "1000");
            AddPreset(presetPanel, "5000");
            AddPreset(presetPanel, "10000");
            AddPreset(presetPanel, "20000");
            AddPreset(presetPanel, "50000");

            pnlWithdraw.Controls.Add(presetPanel);

            // ================= MESSAGE =================
            lblMessage = new Label
            {
                Top = 230,
                Left = 25,
                Width = 320,
                Height = 22,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };
            pnlWithdraw.Controls.Add(lblMessage);

            // ================= BUTTONS =================
            btnWithdraw = CreateButton("WITHDRAW", 260, Color.FromArgb(46, 204, 113));
            btnCancel = CreateButton("CANCEL", 260, Color.FromArgb(231, 76, 60));
            btnCancel.Left = 200;

            btnWithdraw.Click += BtnWithdraw_Click;
            btnCancel.Click += (s, e) => Close();

            pnlWithdraw.Controls.Add(btnWithdraw);
            pnlWithdraw.Controls.Add(btnCancel);

            // ================= KEYPAD =================
            pnlKeypad = BuildKeypad();
            pnlKeypad.Location = new Point(480, 120);
            bg.Controls.Add(pnlKeypad);
        }

        // ================= PRESET BUTTON =================
        private void AddPreset(FlowLayoutPanel panel, string amount)
        {
            Button btn = new Button
            {
                Text = amount,
                Width = 95,
                Height = 36,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.White;
            btn.Click += (s, e) => txtAmount.Text = amount;

            panel.Controls.Add(btn);
        }

        // ================= KEYPAD =================
        private Panel BuildKeypad()
        {
            Panel panel = new Panel
            {
                Size = new Size(320, 340),
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
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand
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
            Button btn = sender as Button;

            if (btn.Text == "CLR")
                txtAmount.Text = "";
            else if (btn.Text == "←" && txtAmount.Text.Length > 0)
                txtAmount.Text = txtAmount.Text[..^1];
            else
                txtAmount.Text += btn.Text;
        }

        // ================= LOGIC =================
        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                lblMessage.Text = "Enter a valid amount";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            var (success, message) = atmService.Withdraw(amount);

            lblMessage.Text = message;
            lblMessage.ForeColor = success ? Color.LightGreen : Color.Red;
            lblBalance.Text = $"Current Balance: ${atmService.CheckBalance():F2}";

            if (success) txtAmount.Clear();
        }

        // ================= HELPERS =================
        private Panel CreateFieldBackground(int top) =>
            new Panel
            {
                Left = 25,
                Top = top,
                Width = 320,
                Height = 40,
                BackColor = Color.FromArgb(245, 247, 250)
            };

        private TextBox CreateInnerTextBox() =>
            new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 12),
                Left = 10,
                Top = 10,
                Width = 300,
                BackColor = Color.FromArgb(245, 247, 250)
            };

        private Button CreateButton(string text, int top, Color color) =>
            new Button
            {
                Text = text,
                Top = top,
                Left = 25,
                Width = 150,
                Height = 42,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance =
                {
                    BorderSize = 2,
                    BorderColor = Color.White
                }
            };
    }
}
