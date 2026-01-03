namespace ATM_UI.Models
{
    public class ATMTransaction
    {
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public DateTime Timestamp { get; set; }

        public ATMTransaction()
        {
            Timestamp = DateTime.Now;
        }

        public ATMTransaction(string type, decimal amount, decimal balanceAfter)
        {
            TransactionType = type;
            Amount = amount;
            BalanceAfter = balanceAfter;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp:yyyy-MM-dd HH:mm:ss} | {TransactionType,-12} | ${Amount:F2} | Balance: ${BalanceAfter:F2}";
        }
    }
}
