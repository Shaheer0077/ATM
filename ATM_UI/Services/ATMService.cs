using System.Collections.Generic;

namespace ATM_UI.Services
{
    public class ATMService
    {
        // 🔥 SHARED across entire app
        private static Dictionary<string, Models.ATMUser> users;

        private Models.ATMUser currentUser;

        public ATMService()
        {
            if (users == null)
            {
                users = new Dictionary<string, Models.ATMUser>();
                InitializeUsers();
            }
        }

        private void InitializeUsers()
        {
            users["1001"] = new Models.ATMUser("1001", "Alishba Ahmed", "1234", 5000m);
            users["1002"] = new Models.ATMUser("1002", "Fatimah", "5678", 3500m);
            users["1003"] = new Models.ATMUser("1003", "Maryam", "9999", 2000m);
        }

        // ================= AUTH =================
        public (bool success, string message) Authenticate(string accountNumber, string pin)
        {
            if (!users.ContainsKey(accountNumber))
                return (false, "Account not found");

            var user = users[accountNumber];

            if (user.PIN != pin)
                return (false, "Invalid PIN");

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

        // ================= BALANCE =================
        public decimal CheckBalance()
        {
            return currentUser?.Balance ?? 0;
        }

        // ================= WITHDRAW =================
        public (bool success, string message) Withdraw(decimal amount)
        {
            if (currentUser == null)
                return (false, "No user logged in");

            if (amount <= 0)
                return (false, "Amount must be greater than 0");

            if (amount > currentUser.Balance)
                return (false, "Insufficient funds");

            currentUser.Balance -= amount;
            currentUser.TransactionHistory.Add(
                new Models.ATMTransaction("Withdrawal", amount, currentUser.Balance)
            );

            return (true, "Withdrawal successful");
        }

        // ================= DEPOSIT =================
        public (bool success, string message) Deposit(decimal amount)
        {
            if (currentUser == null)
                return (false, "No user logged in");

            if (amount <= 0)
                return (false, "Amount must be greater than 0");

            currentUser.Balance += amount;
            currentUser.TransactionHistory.Add(
                new Models.ATMTransaction("Deposit", amount, currentUser.Balance)
            );

            return (true, "Deposit successful");
        }

        // ================= TRANSACTIONS =================
        public List<Models.ATMTransaction> GetTransactionHistory()
        {
            return currentUser?.TransactionHistory ?? new List<Models.ATMTransaction>();
        }

        // ================= CHANGE PIN =================
        public (bool success, string message) ChangePIN(string oldPIN, string newPIN)
        {
            if (currentUser == null)
                return (false, "No user logged in");

            if (currentUser.PIN != oldPIN)
                return (false, "Current PIN is incorrect");

            if (string.IsNullOrWhiteSpace(newPIN) || newPIN.Length != 4)
                return (false, "New PIN must be 4 digits");

            // 🔥 updates shared user object
            currentUser.PIN = newPIN;

            return (true, "PIN changed successfully");
        }
    }
}
