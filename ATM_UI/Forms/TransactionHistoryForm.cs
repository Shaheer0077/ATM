namespace ATM_UI.Forms
{
    public partial class TransactionHistoryForm : Form
    {
        private Services.ATMService atmService;
        private Panel pnlHeader;

        public TransactionHistoryForm(Services.ATMService service)
        {
            InitializeComponent();
            atmService = service;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Utils.UIConstants.BackgroundColor;
        }

        private void TransactionHistoryForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Utils.UIConstants.HistoryFormWidth, Utils.UIConstants.HistoryFormHeight);
            this.Text = "ATM - Transaction History";

            // Header
            pnlHeader.BackColor = Utils.UIConstants.PrimaryColor;
            pnlHeader.Bounds = new Rectangle(0, 0, this.ClientSize.Width, 70);

            lblTitle.Text = "TRANSACTION HISTORY";
            lblTitle.Font = Utils.UIConstants.HeadingFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Bounds = new Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);
            pnlHeader.Controls.Add(lblTitle);

            this.Controls.Add(pnlHeader);

            int margin = Utils.UIConstants.StandardMargin;
            int contentY = pnlHeader.Bottom + margin;

            lstTransactions.Font = new Font("Courier New", 10);
            lstTransactions.BackColor = Color.White;
            lstTransactions.ForeColor = Utils.UIConstants.TextPrimaryColor;
            lstTransactions.BorderStyle = BorderStyle.FixedSingle;
            lstTransactions.Items.Clear();

            var transactions = atmService.GetTransactionHistory();
            if (transactions.Count == 0)
            {
                lstTransactions.Items.Add("No transactions found");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    lstTransactions.Items.Add(transaction.ToString());
                }
            }

            lstTransactions.Bounds = new Rectangle(margin, contentY, this.ClientSize.Width - 2 * margin, this.ClientSize.Height - contentY - Utils.UIConstants.ButtonHeight - 50);
            this.Controls.Add(lstTransactions);

            btnOK.Text = "OK";
            btnOK.Font = Utils.UIConstants.NormalFont;
            btnOK.BackColor = Utils.UIConstants.PrimaryColor;
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Bounds = new Rectangle((this.ClientSize.Width - Utils.UIConstants.StandardButtonWidth) / 2, lstTransactions.Bottom + margin, Utils.UIConstants.StandardButtonWidth, Utils.UIConstants.ButtonHeight);
            btnOK.Cursor = Cursors.Hand;
            this.Controls.Add(btnOK);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Label lblTitle;
        private ListBox lstTransactions;
        private Button btnOK;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            lstTransactions = new ListBox();
            btnOK = new Button();

            SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(Utils.UIConstants.HistoryFormWidth, Utils.UIConstants.HistoryFormHeight);
            this.Name = "TransactionHistoryForm";
            this.Text = "ATM - Transaction History";
            this.Load += TransactionHistoryForm_Load;

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lstTransactions);
            this.Controls.Add(btnOK);

            btnOK.Click += BtnOK_Click;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
