using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public partial class MainMenuForm : Form
    {
        private Services.ATMService atmService;

        private Panel pnlHeader;
        private Panel pnlContent;
        private Panel pnlCard;

        private Label lblWelcome;
        private Label lblAccountInfo;

        private Button btnWithdraw;
        private Button btnDeposit;
        private Button btnBalance;
        private Button btnHistory;
        private Button btnChangePIN;
        private Button btnLogout;

        public MainMenuForm(Services.ATMService service)
        {
            InitializeComponent();
            atmService = service;
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            // ================= FORM =================
            Text = "ATM System - Main Menu";
            ClientSize = new Size(900, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            var user = atmService.GetCurrentUser();

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
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 100;
            pnlHeader.BackColor = Color.FromArgb(0, 90, 160);

            lblWelcome.Text = $"Welcome, {user.Username}";
            lblWelcome.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Height = 55;
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;

            lblAccountInfo.Text =
                $"Account: {user.AccountNumber}   |   Balance: ${atmService.CheckBalance():F2}";
            lblAccountInfo.Font = new Font("Segoe UI", 10);
            lblAccountInfo.ForeColor = Color.WhiteSmoke;
            lblAccountInfo.Dock = DockStyle.Bottom;
            lblAccountInfo.Height = 35;
            lblAccountInfo.TextAlign = ContentAlignment.MiddleCenter;

            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Controls.Add(lblAccountInfo);

            Controls.Add(pnlHeader);
            pnlHeader.BringToFront();

            // ================= CONTENT CARD =================
            pnlCard = new Panel
            {
                Size = new Size(760, 340),
                Location = new Point(70, 140),
                BackColor = Color.Transparent
            };
            bg.Controls.Add(pnlCard);


            // ================= GRID =================
            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(30),
                BackColor = Color.Transparent
            };


            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 3; i++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 33));

            StyleTile(btnWithdraw, "Withdraw Money");
            StyleTile(btnDeposit, "Deposit Money");
            StyleTile(btnBalance, "Check Balance");
            StyleTile(btnHistory, "Transaction History");
            StyleTile(btnChangePIN, "Change PIN");
            StyleLogout(btnLogout, "Logout");

            grid.Controls.Add(btnWithdraw, 0, 0);
            grid.Controls.Add(btnDeposit, 1, 0);
            grid.Controls.Add(btnBalance, 0, 1);
            grid.Controls.Add(btnHistory, 1, 1);
            grid.Controls.Add(btnChangePIN, 0, 2);
            grid.Controls.Add(btnLogout, 1, 2);

            pnlCard.Controls.Add(grid);
        }

        // ================= TILE STYLING =================
        private void StyleTile(Button btn, string text)
        {
            btn.Text = text;
            btn.Dock = DockStyle.Fill;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            btn.BackColor = Color.FromArgb(52, 152, 219);
            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.White;

            btn.UseVisualStyleBackColor = false; // 🔥 REQUIRED

            btn.Margin = new Padding(15);
            btn.Cursor = Cursors.Hand;
        }



        private void StyleLogout(Button btn, string text)
        {
            btn.Text = text;
            btn.Dock = DockStyle.Fill;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            btn.BackColor = Color.FromArgb(192, 57, 43);
            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.White;

            btn.UseVisualStyleBackColor = false; // 🔥 REQUIRED

            btn.Margin = new Padding(15);
            btn.Cursor = Cursors.Hand;
        }



        // ================= LOGIC =================
        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            new WithdrawForm(atmService).ShowDialog();
            RefreshBalance();
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            new DepositForm(atmService).ShowDialog();
            RefreshBalance();
        }

        private void BtnBalance_Click(object sender, EventArgs e)
        {
            new BalanceForm(atmService).ShowDialog();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            new TransactionHistoryForm(atmService).ShowDialog();
        }

        private void BtnChangePIN_Click(object sender, EventArgs e)
        {
            new ChangePINForm(atmService).ShowDialog();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            atmService.Logout();
            Close();
        }

        private void RefreshBalance()
        {
            var user = atmService.GetCurrentUser();
            lblAccountInfo.Text =
                $"Account: {user.AccountNumber}   |   Balance: ${atmService.CheckBalance():F2}";
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            pnlContent = new Panel();
            lblWelcome = new Label();
            lblAccountInfo = new Label();

            btnWithdraw = new Button();
            btnDeposit = new Button();
            btnBalance = new Button();
            btnHistory = new Button();
            btnChangePIN = new Button();
            btnLogout = new Button();

            Load += MainMenuForm_Load;

            btnWithdraw.Click += BtnWithdraw_Click;
            btnDeposit.Click += BtnDeposit_Click;
            btnBalance.Click += BtnBalance_Click;
            btnHistory.Click += BtnHistory_Click;
            btnChangePIN.Click += BtnChangePIN_Click;
            btnLogout.Click += BtnLogout_Click;
        }
    }
}
