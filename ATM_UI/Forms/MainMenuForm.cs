namespace ATM_UI.Forms
{
    public partial class MainMenuForm : Form
    {
        private Services.ATMService atmService;
        private Panel pnlHeader;
        private Panel pnlContent;

        public MainMenuForm(Services.ATMService service)
        {
            InitializeComponent();
            atmService = service;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Utils.UIConstants.BackgroundColor;
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Utils.UIConstants.MainMenuFormWidth, Utils.UIConstants.MainMenuFormHeight);
            this.Text = "ATM System - Main Menu";

            var user = atmService.GetCurrentUser();

            // Header Panel
            pnlHeader.BackColor = Utils.UIConstants.PrimaryColor;
            pnlHeader.Bounds = new Rectangle(0, 0, this.ClientSize.Width, 100);

            lblWelcome.Text = $"Welcome, {user.Username}!";
            lblWelcome.Font = Utils.UIConstants.HeadingFont;
            lblWelcome.ForeColor = Color.White;
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Bounds = new Rectangle(0, 15, pnlHeader.Width, 30);
            pnlHeader.Controls.Add(lblWelcome);

            lblAccountInfo.Text = $"Account: {user.AccountNumber} | Balance: ${atmService.CheckBalance():F2}";
            lblAccountInfo.Font = Utils.UIConstants.SmallFont;
            lblAccountInfo.ForeColor = Color.White;
            lblAccountInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblAccountInfo.Bounds = new Rectangle(0, lblWelcome.Bottom + 5, pnlHeader.Width, 30);
            pnlHeader.Controls.Add(lblAccountInfo);

            // Content Panel
            pnlContent.BackColor = Utils.UIConstants.BackgroundColor;
            pnlContent.Bounds = new Rectangle(0, pnlHeader.Bottom, this.ClientSize.Width, this.ClientSize.Height - pnlHeader.Height);

            lblMenuTitle.Text = "MAIN MENU";
            lblMenuTitle.Font = Utils.UIConstants.HeadingFont;
            lblMenuTitle.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblMenuTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblMenuTitle.Bounds = new Rectangle(Utils.UIConstants.StandardMargin, Utils.UIConstants.StandardMargin, pnlContent.Width - 2 * Utils.UIConstants.StandardMargin, 30);
            pnlContent.Controls.Add(lblMenuTitle);

            int buttonWidth = pnlContent.Width - 2 * Utils.UIConstants.StandardMargin;
            int buttonY = lblMenuTitle.Bottom + Utils.UIConstants.StandardMargin;

            btnWithdraw.Text = "Withdraw Money";
            StyleButton(btnWithdraw, Utils.UIConstants.StandardMargin, buttonY, buttonWidth);
            pnlContent.Controls.Add(btnWithdraw);

            buttonY += Utils.UIConstants.ButtonHeight + Utils.UIConstants.SmallMargin;
            btnDeposit.Text = "Deposit Money";
            StyleButton(btnDeposit, Utils.UIConstants.StandardMargin, buttonY, buttonWidth);
            pnlContent.Controls.Add(btnDeposit);

            buttonY += Utils.UIConstants.ButtonHeight + Utils.UIConstants.SmallMargin;
            btnBalance.Text = "Check Balance";
            StyleButton(btnBalance, Utils.UIConstants.StandardMargin, buttonY, buttonWidth);
            pnlContent.Controls.Add(btnBalance);

            buttonY += Utils.UIConstants.ButtonHeight + Utils.UIConstants.SmallMargin;
            btnHistory.Text = "Transaction History";
            StyleButton(btnHistory, Utils.UIConstants.StandardMargin, buttonY, buttonWidth);
            pnlContent.Controls.Add(btnHistory);

            buttonY += Utils.UIConstants.ButtonHeight + Utils.UIConstants.SmallMargin;
            btnChangePIN.Text = "Change PIN";
            StyleButton(btnChangePIN, Utils.UIConstants.StandardMargin, buttonY, buttonWidth);
            pnlContent.Controls.Add(btnChangePIN);

            buttonY += Utils.UIConstants.ButtonHeight + Utils.UIConstants.SmallMargin;
            btnLogout.Text = "Logout";
            btnLogout.Font = Utils.UIConstants.NormalFont;
            btnLogout.BackColor = Utils.UIConstants.ErrorColor;
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Bounds = new Rectangle(Utils.UIConstants.StandardMargin, buttonY, buttonWidth, Utils.UIConstants.ButtonHeight);
            btnLogout.Cursor = Cursors.Hand;
            pnlContent.Controls.Add(btnLogout);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);
        }

        private void StyleButton(Button btn, int x, int y, int width)
        {
            btn.Font = Utils.UIConstants.NormalFont;
            btn.BackColor = Utils.UIConstants.PrimaryColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Bounds = new Rectangle(x, y, width, Utils.UIConstants.ButtonHeight);
            btn.Cursor = Cursors.Hand;
        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawForm form = new WithdrawForm(atmService);
            form.ShowDialog();
            RefreshBalance();
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            DepositForm form = new DepositForm(atmService);
            form.ShowDialog();
            RefreshBalance();
        }

        private void BtnBalance_Click(object sender, EventArgs e)
        {
            BalanceForm form = new BalanceForm(atmService);
            form.ShowDialog();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            TransactionHistoryForm form = new TransactionHistoryForm(atmService);
            form.ShowDialog();
        }

        private void BtnChangePIN_Click(object sender, EventArgs e)
        {
            ChangePINForm form = new ChangePINForm(atmService);
            form.ShowDialog();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            atmService.Logout();
            this.Close();
        }

        private void RefreshBalance()
        {
            var user = atmService.GetCurrentUser();
            lblAccountInfo.Text = $"Account: {user.AccountNumber} | Balance: ${atmService.CheckBalance():F2}";
        }

        private Label lblWelcome;
        private Label lblAccountInfo;
        private Label lblMenuTitle;
        private Button btnWithdraw;
        private Button btnDeposit;
        private Button btnBalance;
        private Button btnHistory;
        private Button btnChangePIN;
        private Button btnLogout;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            pnlContent = new Panel();
            lblWelcome = new Label();
            lblAccountInfo = new Label();
            lblMenuTitle = new Label();
            btnWithdraw = new Button();
            btnDeposit = new Button();
            btnBalance = new Button();
            btnHistory = new Button();
            btnChangePIN = new Button();
            btnLogout = new Button();

            SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(Utils.UIConstants.MainMenuFormWidth, Utils.UIConstants.MainMenuFormHeight);
            this.Name = "MainMenuForm";
            this.Text = "ATM System - Main Menu";
            this.Load += MainMenuForm_Load;

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);

            btnWithdraw.Click += BtnWithdraw_Click;
            btnDeposit.Click += BtnDeposit_Click;
            btnBalance.Click += BtnBalance_Click;
            btnHistory.Click += BtnHistory_Click;
            btnChangePIN.Click += BtnChangePIN_Click;
            btnLogout.Click += BtnLogout_Click;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
