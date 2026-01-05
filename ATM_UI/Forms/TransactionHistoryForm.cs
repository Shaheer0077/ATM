using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATM_UI.Forms
{
    public class TransactionHistoryForm : Form
    {
        private Services.ATMService atmService;

        private Panel pnlHeader;
        private RoundedPanel pnlCard;

        private Label lblTitle;
        private ListView lvHistory;
        private Button btnOK;

        public TransactionHistoryForm(Services.ATMService service)
        {
            atmService = service;
            BuildUI();
        }

        private void BuildUI()
        {
            // ================= FORM =================
            Text = "ATM - Transaction History";
            ClientSize = new Size(920, 520);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            // ================= HEADER =================
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(39, 119, 225)
            };

            lblTitle = new Label
            {
                Text = "TRANSACTION HISTORY",
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
                Size = new Size(820, 350),
                Location = new Point(50, 120),
                BackColor = Color.White,
                Radius = 22
            };
            Controls.Add(pnlCard);

            // ================= LIST VIEW =================
            lvHistory = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11),
                Dock = DockStyle.Top,
                Height = 260,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable
            };

            lvHistory.Columns.Add("Date & Time", 240);
            lvHistory.Columns.Add("Type", 160);
            lvHistory.Columns.Add("Amount", 160);
            lvHistory.Columns.Add("Balance", 180);

            pnlCard.Controls.Add(lvHistory);

            PopulateHistory();

            // ================= OK BUTTON =================
            btnOK = new Button
            {
                Text = "OK",
                Width = 160,
                Height = 44,
                Top = 285,
                Left = (pnlCard.Width - 160) / 2,
                BackColor = Color.FromArgb(39, 119, 225),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 11),
                Cursor = Cursors.Hand
            };
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Click += (s, e) => Close();

            pnlCard.Controls.Add(btnOK);
        }

        // ================= DATA =================
        private void PopulateHistory()
        {
            lvHistory.Items.Clear();

            var transactions = atmService.GetTransactionHistory();

            if (transactions.Count == 0)
            {
                var empty = new ListViewItem("No transactions found")
                {
                    ForeColor = Color.Gray
                };
                lvHistory.Items.Add(empty);
                return;
            }

            foreach (var t in transactions)
            {
                // Expected format:
                // Date | Type | Amount | Balance

                string raw = t.ToString();
                string[] parts = raw.Split('|');

                if (parts.Length < 4) continue;

                ListViewItem item = new ListViewItem(parts[0].Trim());
                item.SubItems.Add(parts[1].Trim());
                item.SubItems.Add(parts[2].Trim());
                item.SubItems.Add(parts[3].Replace("Balance:", "").Trim());

                lvHistory.Items.Add(item);
            }
        }

        // ================= BACKGROUND =================
        protected override void OnPaint(PaintEventArgs e)
        {
            using LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(248, 250, 252),
                Color.FromArgb(236, 240, 245),
                90f);

            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }
}
