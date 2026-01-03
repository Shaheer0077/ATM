namespace ATM_UI.Forms
{
    public partial class WithdrawForm : Form
    {
        private Services.ATMService atmService;
        private Panel pnlHeader;

        public WithdrawForm(Services.ATMService service)
        {
            InitializeComponent();
            atmService = service;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Utils.UIConstants.BackgroundColor;
        }

        private void WithdrawForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Utils.UIConstants.OperationFormWidth, Utils.UIConstants.OperationFormHeight);
            this.Text = "ATM - Withdraw Money";

            // Header
            pnlHeader.BackColor = Utils.UIConstants.PrimaryColor;
            pnlHeader.Bounds = new Rectangle(0, 0, this.ClientSize.Width, 70);

            lblTitle.Text = "WITHDRAW MONEY";
            lblTitle.Font = Utils.UIConstants.HeadingFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Bounds = new Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);
            pnlHeader.Controls.Add(lblTitle);

            this.Controls.Add(pnlHeader);

            int margin = Utils.UIConstants.StandardMargin;
            int contentY = pnlHeader.Bottom + margin;

            lblCurrentBalance.Text = $"Current Balance: ${atmService.CheckBalance():F2}";
            lblCurrentBalance.Font = Utils.UIConstants.LabelFont;
            lblCurrentBalance.ForeColor = Utils.UIConstants.PrimaryColor;
            lblCurrentBalance.AutoSize = false;
            lblCurrentBalance.Bounds = new Rectangle(margin, contentY, this.ClientSize.Width - 2 * margin, 25);
            this.Controls.Add(lblCurrentBalance);

            contentY += 35;

            lblAmount.Text = "Withdrawal Amount:";
            lblAmount.Font = Utils.UIConstants.LabelFont;
            lblAmount.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblAmount.AutoSize = true;
            lblAmount.Bounds = new Rectangle(margin, contentY, 150, Utils.UIConstants.LabelHeight);
            this.Controls.Add(lblAmount);

            txtAmount.Text = "";
            txtAmount.Font = Utils.UIConstants.NormalFont;
            txtAmount.BackColor = Color.White;
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Bounds = new Rectangle(margin, lblAmount.Bottom + 5, this.ClientSize.Width - 2 * margin, Utils.UIConstants.InputHeight);
            this.Controls.Add(txtAmount);

            int buttonY = txtAmount.Bottom + Utils.UIConstants.StandardMargin;
            int totalButtonWidth = 2 * Utils.UIConstants.StandardButtonWidth + Utils.UIConstants.StandardMargin;
            int startX = (this.ClientSize.Width - totalButtonWidth) / 2;

            btnWithdraw.Text = "Withdraw";
            btnWithdraw.Font = Utils.UIConstants.NormalFont;
            btnWithdraw.BackColor = Utils.UIConstants.PrimaryColor;
            btnWithdraw.ForeColor = Color.White;
            btnWithdraw.FlatStyle = FlatStyle.Flat;
            btnWithdraw.FlatAppearance.BorderSize = 0;
            btnWithdraw.Bounds = new Rectangle(startX, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnWithdraw.Cursor = Cursors.Hand;
            this.Controls.Add(btnWithdraw);

            btnCancel.Text = "Cancel";
            btnCancel.Font = Utils.UIConstants.NormalFont;
            btnCancel.BackColor = Utils.UIConstants.ErrorColor;
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Bounds = new Rectangle(btnWithdraw.Right + Utils.UIConstants.StandardMargin, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnCancel.Cursor = Cursors.Hand;
            this.Controls.Add(btnCancel);

            lblMessage.Text = "";
            lblMessage.Font = Utils.UIConstants.NormalFont;
            lblMessage.AutoSize = false;
            lblMessage.TextAlign = ContentAlignment.TopCenter;
            lblMessage.Bounds = new Rectangle(margin, btnCancel.Bottom + Utils.UIConstants.StandardMargin, this.ClientSize.Width - 2 * margin, 40);
            this.Controls.Add(lblMessage);
        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                lblMessage.Text = "Please enter a valid amount";
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
                return;
            }

            var (success, message) = atmService.Withdraw(amount);
            if (success)
            {
                lblMessage.Text = message;
                lblMessage.ForeColor = Utils.UIConstants.SuccessColor;
                lblCurrentBalance.Text = $"Current Balance: ${atmService.CheckBalance():F2}";
                txtAmount.Text = "";
            }
            else
            {
                lblMessage.Text = message;
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Label lblTitle;
        private Label lblCurrentBalance;
        private Label lblAmount;
        private TextBox txtAmount;
        private Button btnWithdraw;
        private Button btnCancel;
        private Label lblMessage;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblCurrentBalance = new Label();
            lblAmount = new Label();
            txtAmount = new TextBox();
            btnWithdraw = new Button();
            btnCancel = new Button();
            lblMessage = new Label();

            SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(Utils.UIConstants.OperationFormWidth, Utils.UIConstants.OperationFormHeight);
            this.Name = "WithdrawForm";
            this.Text = "ATM - Withdraw";
            this.Load += WithdrawForm_Load;

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblCurrentBalance);
            this.Controls.Add(lblAmount);
            this.Controls.Add(txtAmount);
            this.Controls.Add(btnWithdraw);
            this.Controls.Add(btnCancel);
            this.Controls.Add(lblMessage);

            btnWithdraw.Click += BtnWithdraw_Click;
            btnCancel.Click += BtnCancel_Click;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
