using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public partial class BalanceForm : Form
    {
        private Services.ATMService atmService;

        private Panel pnlHeader;
        private RoundedPanel pnlCard;

        private Label lblTitle;
        private Label lblAccountLabel;
        private Label lblAccountNumber;
        private Label lblBalanceLabel;
        private Label lblBalance;
        private Button btnOK;

        public BalanceForm(Services.ATMService service)
        {
            atmService = service;

            if (atmService == null || atmService.GetCurrentUser() == null)
            {
                MessageBox.Show(
                    "No user is logged in.\nPlease login again.",
                    "Session Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Close();
                return;
            }

            BuildUI();

            Shown += (s, e) => RefreshBalance();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            RefreshBalance();
        }

        private void BuildUI()
        {
            Text = "ATM - Check Balance";
            ClientSize = new Size(920, 520);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            Controls.Clear();

            // ================= BACKGROUND =================
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
                Text = "ACCOUNT BALANCE",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 24),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);
            Controls.Add(pnlHeader);

            // ================= CARD =================
            pnlCard = new RoundedPanel
            {
                Size = new Size(420, 300),
                Location = new Point((ClientSize.Width - 420) / 2, 140),
                BackColor = Color.White,
                Radius = 22
            };
            Controls.Add(pnlCard);

            pnlHeader.BringToFront();
            pnlCard.BringToFront();

            int left = 30;
            int width = 360;

            lblAccountLabel = new Label
            {
                Text = "ACCOUNT NUMBER",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Gray,
                Top = 30,
                Left = left,
                Width = width,
                Height = 26,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblAccountNumber = new Label
            {
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 90, 160),
                Top = lblAccountLabel.Bottom + 6,
                Left = left,
                Width = width,
                Height = 32,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblBalanceLabel = new Label
            {
                Text = "AVAILABLE BALANCE",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Top = lblAccountNumber.Bottom + 16,
                Left = left,
                Width = width,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblBalance = new Label
            {
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Top = lblBalanceLabel.Bottom + 10,
                Left = left,
                Width = width,
                Height = 90,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Text = "$ 0.00"
            };

            btnOK = new Button
            {
                Text = "OK",
                Width = 160,
                Height = 60,
                Top = 230,
                Left = (pnlCard.Width - 160) / 2,
                BackColor = Color.FromArgb(0, 90, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 11)
            };

            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Click += (s, e) => Close();

            pnlCard.Controls.AddRange(new Control[]
            {
                lblAccountLabel,
                lblAccountNumber,
                lblBalanceLabel,
                lblBalance,
                btnOK
            });

            lblBalance.BringToFront();
        }

        private void RefreshBalance()
        {
            var user = atmService.GetCurrentUser();

            if (user == null)
            {
                lblAccountNumber.Text = "N/A";
                lblBalance.Text = "$ 0.00";
                return;
            }

            lblAccountNumber.Text = user.AccountNumber;
            lblBalance.Text = user.Balance.ToString("C2");
        }
    }
}
