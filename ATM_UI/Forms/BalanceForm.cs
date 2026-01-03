namespace ATM_UI.Forms
{
    public partial class BalanceForm : Form
    {
        private Services.ATMService atmService;
        private Panel pnlHeader;

        public BalanceForm(Services.ATMService service)
        {
            InitializeComponent();
            atmService = service;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Utils.UIConstants.BackgroundColor;
        }

        private void BalanceForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Utils.UIConstants.BalanceFormWidth, Utils.UIConstants.BalanceFormHeight);
            this.Text = "ATM - Check Balance";

            var user = atmService.GetCurrentUser();
            decimal balance = atmService.CheckBalance();

            // Header
            pnlHeader.BackColor = Utils.UIConstants.PrimaryColor;
            pnlHeader.Bounds = new Rectangle(0, 0, this.ClientSize.Width, 70);

            lblTitle.Text = "YOUR ACCOUNT BALANCE";
            lblTitle.Font = Utils.UIConstants.HeadingFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Bounds = new Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);
            pnlHeader.Controls.Add(lblTitle);

            this.Controls.Add(pnlHeader);

            int margin = Utils.UIConstants.StandardMargin;
            int contentY = pnlHeader.Bottom + Utils.UIConstants.LargeMargin;

            lblAccountInfo.Text = $"Account Number: {user.AccountNumber}";
            lblAccountInfo.Font = Utils.UIConstants.LabelFont;
            lblAccountInfo.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblAccountInfo.AutoSize = false;
            lblAccountInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblAccountInfo.Bounds = new Rectangle(margin, contentY, this.ClientSize.Width - 2 * margin, 30);
            this.Controls.Add(lblAccountInfo);

            contentY += 50;

            lblBalance.Text = $"${balance:F2}";
            lblBalance.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            lblBalance.ForeColor = Utils.UIConstants.SuccessColor;
            lblBalance.TextAlign = ContentAlignment.MiddleCenter;
            lblBalance.AutoSize = false;
            lblBalance.Bounds = new Rectangle(margin, contentY, this.ClientSize.Width - 2 * margin, 70);
            this.Controls.Add(lblBalance);

            int buttonY = lblBalance.Bottom + Utils.UIConstants.StandardMargin;
            btnOK.Text = "OK";
            btnOK.Font = Utils.UIConstants.NormalFont;
            btnOK.BackColor = Utils.UIConstants.PrimaryColor;
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Bounds = new Rectangle((this.ClientSize.Width - Utils.UIConstants.StandardButtonWidth) / 2, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnOK.Cursor = Cursors.Hand;
            this.Controls.Add(btnOK);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Label lblTitle;
        private Label lblAccountInfo;
        private Label lblBalance;
        private Button btnOK;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblAccountInfo = new Label();
            lblBalance = new Label();
            btnOK = new Button();

            SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(Utils.UIConstants.BalanceFormWidth, Utils.UIConstants.BalanceFormHeight);
            this.Name = "BalanceForm";
            this.Text = "ATM - Balance";
            this.Load += BalanceForm_Load;

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblAccountInfo);
            this.Controls.Add(lblBalance);
            this.Controls.Add(btnOK);

            btnOK.Click += BtnOK_Click;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
