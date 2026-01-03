namespace ATM_UI.Forms
{
    public partial class ChangePINForm : Form
    {
        private Services.ATMService atmService;
        private Panel pnlHeader;

        public ChangePINForm(Services.ATMService service)
        {
            InitializeComponent();
            atmService = service;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Utils.UIConstants.BackgroundColor;
        }

        private void ChangePINForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Utils.UIConstants.ChangePINFormWidth, Utils.UIConstants.ChangePINFormHeight);
            this.Text = "ATM - Change PIN";

            // Header
            pnlHeader.BackColor = Utils.UIConstants.PrimaryColor;
            pnlHeader.Bounds = new Rectangle(0, 0, this.ClientSize.Width, 70);

            lblTitle.Text = "CHANGE YOUR PIN";
            lblTitle.Font = Utils.UIConstants.HeadingFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Bounds = new Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);
            pnlHeader.Controls.Add(lblTitle);

            this.Controls.Add(pnlHeader);

            int margin = Utils.UIConstants.StandardMargin;
            int contentY = pnlHeader.Bottom + margin;

            lblOldPIN.Text = "Current PIN:";
            lblOldPIN.Font = Utils.UIConstants.LabelFont;
            lblOldPIN.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblOldPIN.AutoSize = true;
            lblOldPIN.Bounds = new Rectangle(margin, contentY, 150, Utils.UIConstants.LabelHeight);
            this.Controls.Add(lblOldPIN);

            txtOldPIN.Text = "";
            txtOldPIN.Font = Utils.UIConstants.NormalFont;
            txtOldPIN.BackColor = Color.White;
            txtOldPIN.PasswordChar = '*';
            txtOldPIN.BorderStyle = BorderStyle.FixedSingle;
            txtOldPIN.Bounds = new Rectangle(margin, lblOldPIN.Bottom + 5, this.ClientSize.Width - 2 * margin, Utils.UIConstants.InputHeight);
            this.Controls.Add(txtOldPIN);

            contentY = txtOldPIN.Bottom + Utils.UIConstants.StandardMargin;

            lblNewPIN.Text = "New PIN (4 digits):";
            lblNewPIN.Font = Utils.UIConstants.LabelFont;
            lblNewPIN.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblNewPIN.AutoSize = true;
            lblNewPIN.Bounds = new Rectangle(margin, contentY, 150, Utils.UIConstants.LabelHeight);
            this.Controls.Add(lblNewPIN);

            txtNewPIN.Text = "";
            txtNewPIN.Font = Utils.UIConstants.NormalFont;
            txtNewPIN.BackColor = Color.White;
            txtNewPIN.PasswordChar = '*';
            txtNewPIN.BorderStyle = BorderStyle.FixedSingle;
            txtNewPIN.Bounds = new Rectangle(margin, lblNewPIN.Bottom + 5, this.ClientSize.Width - 2 * margin, Utils.UIConstants.InputHeight);
            this.Controls.Add(txtNewPIN);

            contentY = txtNewPIN.Bottom + Utils.UIConstants.StandardMargin;

            lblConfirmPIN.Text = "Confirm New PIN:";
            lblConfirmPIN.Font = Utils.UIConstants.LabelFont;
            lblConfirmPIN.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lblConfirmPIN.AutoSize = true;
            lblConfirmPIN.Bounds = new Rectangle(margin, contentY, 150, Utils.UIConstants.LabelHeight);
            this.Controls.Add(lblConfirmPIN);

            txtConfirmPIN.Text = "";
            txtConfirmPIN.Font = Utils.UIConstants.NormalFont;
            txtConfirmPIN.BackColor = Color.White;
            txtConfirmPIN.PasswordChar = '*';
            txtConfirmPIN.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPIN.Bounds = new Rectangle(margin, lblConfirmPIN.Bottom + 5, this.ClientSize.Width - 2 * margin, Utils.UIConstants.InputHeight);
            this.Controls.Add(txtConfirmPIN);

            int buttonY = txtConfirmPIN.Bottom + Utils.UIConstants.StandardMargin + 10;
            int totalButtonWidth = 2 * Utils.UIConstants.StandardButtonWidth + Utils.UIConstants.StandardMargin;
            int startX = (this.ClientSize.Width - totalButtonWidth) / 2;

            btnChange.Text = "Change PIN";
            btnChange.Font = Utils.UIConstants.NormalFont;
            btnChange.BackColor = Utils.UIConstants.PrimaryColor;
            btnChange.ForeColor = Color.White;
            btnChange.FlatStyle = FlatStyle.Flat;
            btnChange.FlatAppearance.BorderSize = 0;
            btnChange.Bounds = new Rectangle(startX, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnChange.Cursor = Cursors.Hand;
            this.Controls.Add(btnChange);

            btnCancel.Text = "Cancel";
            btnCancel.Font = Utils.UIConstants.NormalFont;
            btnCancel.BackColor = Utils.UIConstants.ErrorColor;
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Bounds = new Rectangle(btnChange.Right + Utils.UIConstants.StandardMargin, buttonY, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnCancel.Cursor = Cursors.Hand;
            this.Controls.Add(btnCancel);

            lblMessage.Text = "";
            lblMessage.Font = Utils.UIConstants.NormalFont;
            lblMessage.AutoSize = false;
            lblMessage.TextAlign = ContentAlignment.TopCenter;
            lblMessage.Bounds = new Rectangle(margin, btnChange.Bottom + Utils.UIConstants.StandardMargin, this.ClientSize.Width - 2 * margin, 40);
            this.Controls.Add(lblMessage);
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            string oldPIN = txtOldPIN.Text;
            string newPIN = txtNewPIN.Text;
            string confirmPIN = txtConfirmPIN.Text;

            if (string.IsNullOrWhiteSpace(oldPIN))
            {
                lblMessage.Text = "Please enter your current PIN";
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
                return;
            }

            if (string.IsNullOrWhiteSpace(newPIN) || string.IsNullOrWhiteSpace(confirmPIN))
            {
                lblMessage.Text = "Please enter and confirm your new PIN";
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
                return;
            }

            if (newPIN != confirmPIN)
            {
                lblMessage.Text = "New PIN and confirmation do not match";
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
                txtNewPIN.Text = "";
                txtConfirmPIN.Text = "";
                return;
            }

            if (newPIN.Length != 4 || !newPIN.All(char.IsDigit))
            {
                lblMessage.Text = "New PIN must be exactly 4 digits";
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
                return;
            }

            var (success, message) = atmService.ChangePIN(oldPIN, newPIN);
            if (success)
            {
                lblMessage.Text = message;
                lblMessage.ForeColor = Utils.UIConstants.SuccessColor;
                txtOldPIN.Text = "";
                txtNewPIN.Text = "";
                txtConfirmPIN.Text = "";
            }
            else
            {
                lblMessage.Text = message;
                lblMessage.ForeColor = Utils.UIConstants.ErrorColor;
                txtOldPIN.Text = "";
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Label lblTitle;
        private Label lblOldPIN;
        private Label lblNewPIN;
        private Label lblConfirmPIN;
        private TextBox txtOldPIN;
        private TextBox txtNewPIN;
        private TextBox txtConfirmPIN;
        private Button btnChange;
        private Button btnCancel;
        private Label lblMessage;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblOldPIN = new Label();
            lblNewPIN = new Label();
            lblConfirmPIN = new Label();
            txtOldPIN = new TextBox();
            txtNewPIN = new TextBox();
            txtConfirmPIN = new TextBox();
            btnChange = new Button();
            btnCancel = new Button();
            lblMessage = new Label();

            SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(Utils.UIConstants.ChangePINFormWidth, Utils.UIConstants.ChangePINFormHeight);
            this.Name = "ChangePINForm";
            this.Text = "ATM - Change PIN";
            this.Load += ChangePINForm_Load;

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblOldPIN);
            this.Controls.Add(lblNewPIN);
            this.Controls.Add(lblConfirmPIN);
            this.Controls.Add(txtOldPIN);
            this.Controls.Add(txtNewPIN);
            this.Controls.Add(txtConfirmPIN);
            this.Controls.Add(btnChange);
            this.Controls.Add(btnCancel);
            this.Controls.Add(lblMessage);

            btnChange.Click += BtnChange_Click;
            btnCancel.Click += BtnCancel_Click;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
