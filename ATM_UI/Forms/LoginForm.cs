namespace ATM_UI.Forms
{
    public partial class LoginForm : Form
    {
        private Services.ATMService atmService;
        private int attemptCount = 0;
        private const int MaxAttempts = 3;
        private Panel pnlHeader;
        private Panel pnlContent;

        public LoginForm()
        {
            InitializeComponent();
            atmService = new Services.ATMService();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Utils.UIConstants.BackgroundColor;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Utils.UIConstants.LoginFormWidth, Utils.UIConstants.LoginFormHeight);
            this.Text = "ATM System - Login";

            // Header Panel
            pnlHeader.BackColor = Utils.UIConstants.PrimaryColor;
            pnlHeader.Bounds = new Rectangle(0, 0, this.ClientSize.Width, 80);

            lblTitle.Text = "ATM LOGIN";
            lblTitle.Font = Utils.UIConstants.TitleFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Bounds = new Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);
            pnlHeader.Controls.Add(lblTitle);

            // Content Panel
            pnlContent.BackColor = Utils.UIConstants.BackgroundColor;
            pnlContent.Bounds = new Rectangle(0, pnlHeader.Bottom, this.ClientSize.Width, this.ClientSize.Height - pnlHeader.Height);

            int contentMargin = Utils.UIConstants.StandardMargin;
            int contentY = contentMargin;

            lblAccountNumber.Text = "Account Number";
            lblAccountNumber.Font = Utils.UIConstants.LabelFont;
            lblAccountNumber.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblAccountNumber.AutoSize = true;
            lblAccountNumber.Bounds = new Rectangle(contentMargin, contentY, 150, Utils.UIConstants.LabelHeight);
            pnlContent.Controls.Add(lblAccountNumber);

            txtAccountNumber.Text = "";
            txtAccountNumber.Font = Utils.UIConstants.NormalFont;
            txtAccountNumber.BackColor = Color.White;
            txtAccountNumber.ForeColor = Utils.UIConstants.TextPrimaryColor;
            txtAccountNumber.BorderStyle = BorderStyle.FixedSingle;
            txtAccountNumber.Bounds = new Rectangle(contentMargin, lblAccountNumber.Bottom + 5, pnlContent.Width - 2 * contentMargin, Utils.UIConstants.InputHeight);
            pnlContent.Controls.Add(txtAccountNumber);

            contentY = txtAccountNumber.Bottom + Utils.UIConstants.StandardMargin;

            lblPIN.Text = "PIN";
            lblPIN.Font = Utils.UIConstants.LabelFont;
            lblPIN.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblPIN.AutoSize = true;
            lblPIN.Bounds = new Rectangle(contentMargin, contentY, 100, Utils.UIConstants.LabelHeight);
            pnlContent.Controls.Add(lblPIN);

            txtPIN.Text = "";
            txtPIN.Font = Utils.UIConstants.NormalFont;
            txtPIN.BackColor = Color.White;
            txtPIN.ForeColor = Utils.UIConstants.TextPrimaryColor;
            txtPIN.PasswordChar = '*';
            txtPIN.BorderStyle = BorderStyle.FixedSingle;
            txtPIN.Bounds = new Rectangle(contentMargin, lblPIN.Bottom + 5, pnlContent.Width - 2 * contentMargin, Utils.UIConstants.InputHeight);
            pnlContent.Controls.Add(txtPIN);

            contentY = txtPIN.Bottom + Utils.UIConstants.StandardMargin;

            // Buttons
            int buttonY = contentY;
            int totalButtonWidth = 2 * Utils.UIConstants.StandardButtonWidth + Utils.UIConstants.StandardMargin;
            int startX = (pnlContent.Width - totalButtonWidth) / 2;

            btnLogin.Text = "Login";
            btnLogin.Font = Utils.UIConstants.NormalFont;
            btnLogin.BackColor = Utils.UIConstants.PrimaryColor;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Bounds = new Rectangle(startX, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnLogin.Cursor = Cursors.Hand;
            pnlContent.Controls.Add(btnLogin);

            btnExit.Text = "Exit";
            btnExit.Font = Utils.UIConstants.NormalFont;
            btnExit.BackColor = Utils.UIConstants.ErrorColor;
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Bounds = new Rectangle(btnLogin.Right + Utils.UIConstants.StandardMargin, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnExit.Cursor = Cursors.Hand;
            pnlContent.Controls.Add(btnExit);

            contentY = btnLogin.Bottom + Utils.UIConstants.StandardMargin;

            lblAttempts.Text = $"Attempts remaining: {MaxAttempts - attemptCount}";
            lblAttempts.Font = Utils.UIConstants.SmallFont;
            lblAttempts.ForeColor = Utils.UIConstants.TextSecondaryColor;
            lblAttempts.AutoSize = false;
            lblAttempts.TextAlign = ContentAlignment.MiddleCenter;
            lblAttempts.Bounds = new Rectangle(contentMargin, contentY, pnlContent.Width - 2 * contentMargin, 20);
            pnlContent.Controls.Add(lblAttempts);

            lblError.Text = "";
            lblError.Font = Utils.UIConstants.NormalFont;
            lblError.ForeColor = Utils.UIConstants.ErrorColor;
            lblError.AutoSize = false;
            lblError.TextAlign = ContentAlignment.TopCenter;
            lblError.Bounds = new Rectangle(contentMargin, lblAttempts.Bottom + 5, pnlContent.Width - 2 * contentMargin, 40);
            pnlContent.Controls.Add(lblError);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string accountNumber = txtAccountNumber.Text.Trim();
            string pin = txtPIN.Text.Trim();

            if (string.IsNullOrWhiteSpace(accountNumber) || string.IsNullOrWhiteSpace(pin))
            {
                lblError.Text = "Please enter both account number and PIN";
                lblError.ForeColor = Utils.UIConstants.ErrorColor;
                return;
            }

            var (success, message) = atmService.Authenticate(accountNumber, pin);

            if (success)
            {
                lblError.Text = "";
                var user = atmService.GetCurrentUser();
                MessageBox.Show($"Welcome, {user.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainMenuForm mainForm = new MainMenuForm(atmService);
                this.Hide();
                mainForm.ShowDialog();
                this.Show();
                ResetLogin();
            }
            else
            {
                attemptCount++;
                lblError.Text = message;
                lblError.ForeColor = Utils.UIConstants.ErrorColor;
                lblAttempts.Text = $"Attempts remaining: {MaxAttempts - attemptCount}";

                txtPIN.Text = "";
                txtAccountNumber.Focus();

                if (attemptCount >= MaxAttempts)
                {
                    MessageBox.Show("Maximum login attempts exceeded. The ATM has been locked.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetLogin()
        {
            txtAccountNumber.Text = "";
            txtPIN.Text = "";
            lblError.Text = "";
            attemptCount = 0;
            lblAttempts.Text = $"Attempts remaining: {MaxAttempts - attemptCount}";
        }

        private Label lblTitle;
        private Label lblAccountNumber;
        private Label lblPIN;
        private TextBox txtAccountNumber;
        private TextBox txtPIN;
        private Button btnLogin;
        private Button btnExit;
        private Label lblAttempts;
        private Label lblError;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            pnlContent = new Panel();
            lblTitle = new Label();
            lblAccountNumber = new Label();
            lblPIN = new Label();
            txtAccountNumber = new TextBox();
            txtPIN = new TextBox();
            btnLogin = new Button();
            btnExit = new Button();
            lblAttempts = new Label();
            lblError = new Label();

            SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(Utils.UIConstants.LoginFormWidth, Utils.UIConstants.LoginFormHeight);
            this.Name = "LoginForm";
            this.Text = "ATM System - Login";
            this.Load += LoginForm_Load;

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);

            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += BtnExit_Click;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
