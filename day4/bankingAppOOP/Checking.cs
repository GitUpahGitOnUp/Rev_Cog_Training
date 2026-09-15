namespace Banking
{
    public class Checking : Accounts
    {
        public Checking()
        {
            AccType = TypeOfAccount.Checking;
        }
        public bool AccIsODEnabled {get; set;}

        public override double Withdraw(int amount)
        {
            if(amount > 30000)
            {
                throw new Exception("The maximum withdrawal amount is $30,000 dollars.");
            }
            return base.Withdraw(amount);
        }

    }
}