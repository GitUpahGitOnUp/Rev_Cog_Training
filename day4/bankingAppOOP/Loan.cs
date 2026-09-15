namespace Banking
{
    public class Loan : Accounts
    {
        public Loan()
        {
            AccType = TypeOfAccount.Loan;
        }
        public override double Withdraw(int amount)
        {
            throw new Exception("Withdrawals are not allowed for this account type, please contact the bank.");
        }
    }
}