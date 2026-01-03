namespace ATM_UI.Services
{
    public class ATMService
    {
        private Dictionary<string, Models.ATMUser> users;
        private Models.ATMUser currentUser;

        public ATMService()
        {
            users = new Dictionary<string, Models.ATMUser>();
            InitializeUsers();
        }

        private void InitializeUsers()
        {
            users.Add("1001", new Models.ATMUser("1001", "Alishba Ahmed", "1234", 5000m));
            users.Add("1002", new Models.ATMUser("1002", "Fatimah", "5678", 3500m));
            users.Add("1003", new Models.ATMUser("1003", "Maryam", "9999", 2000m));
        }

        public (bool success, string message) Authenticate(string accountNumber, string pin)
        {
            if (!users.ContainsKey(accountNumber))
            {
                return (false, "Account not found");
            }

            var user = users[accountNumber];
            if (user.PIN != pin)
            {
                return (false, "Invalid PIN");
            }

            currentUser = user;
            return (true, "Authentication successful");
        }

        public Models.ATMUser GetCurrentUser()
        {
            return currentUser;
        }

        public void Logout()
        {
            currentUser = null;
        }

        public (bool success, string message) Withdraw(decimal amount)
        {
            if (currentUser == null)
                return (false, "No user logged in");

            if (amount <= 0)
                return (false, "Amount must be greater than 0");

            if (amount > currentUser.Balance)
                return (false, "Insufficient funds");

            currentUser.Balance -= amount;
            var transaction = new Models.ATMTransaction("Withdrawal", amount, currentUser.Balance);
            currentUser.TransactionHistory.Add(transaction);

            return (true, $"Withdrawal successful. New balance: ${currentUser.Balance:F2}");
        }

        public (bool success, string message) Deposit(decimal amount)
        {
            if (currentUser == null)
                return (false, "No user logged in");

            if (amount <= 0)
                return (false, "Amount must be greater than 0");

            currentUser.Balance += amount;
            var transaction = new Models.ATMTransaction("Deposit", amount, currentUser.Balance);
            currentUser.TransactionHistory.Add(transaction);

            return (true, $"Deposit successful. New balance: ${currentUser.Balance:F2}");
        }

        public decimal CheckBalance()
        {
            if (currentUser == null)
                return 0;

            return currentUser.Balance;
        }

        public List<Models.ATMTransaction> GetTransactionHistory()
        {
            if (currentUser == null)
                return new List<Models.ATMTransaction>();

            return currentUser.TransactionHistory;
        }

        public (bool success, string message) ChangePIN(string oldPIN, string newPIN)
        {
            if (currentUser == null)
                return (false, "No user logged in");

            if (currentUser.PIN != oldPIN)
                return (false, "Current PIN is incorrect");

            if (string.IsNullOrWhiteSpace(newPIN) || newPIN.Length != 4)
                return (false, "New PIN must be 4 digits");

            currentUser.PIN = newPIN;
            return (true, "PIN changed successfully");
        }
    }
}
