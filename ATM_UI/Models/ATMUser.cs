namespace ATM_UI.Models
{
    public class ATMUser
    {
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string PIN { get; set; }
        public decimal Balance { get; set; }
        public List<ATMTransaction> TransactionHistory { get; set; }

        public ATMUser()
        {
            TransactionHistory = new List<ATMTransaction>();
        }

        public ATMUser(string accountNumber, string username, string pin, decimal initialBalance = 1000m)
        {
            AccountNumber = accountNumber;
            Username = username;
            PIN = pin;
            Balance = initialBalance;
            TransactionHistory = new List<ATMTransaction>();
        }
    }
}
