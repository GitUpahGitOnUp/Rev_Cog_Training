
namespace Banking
{
    public class Savings : Accounts
    {
        public Savings()
        {
            AccType = TypeOfAccount.Savings;
        }

        public override double Withdraw(int amount)
        {
            if(amount > 5000)
            {
                throw new Exception("The maximum amount for withdrawal is $5,000.");
            }
            else
            {
            return base.Withdraw(amount);
            }
        }
    }
}