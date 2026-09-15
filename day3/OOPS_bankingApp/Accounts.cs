namespace OOPS_bankingApp
{
    public class Accounts
    {
        
        public int AccNo {get; set;}

        public string AccName {get; set;} = "";

        public double AccountBalance {get; set;}

        public bool isActive {get; set;}

        public string email {get; set;}

    

    public double Withdraw(int amount)
        {
            AccountBalance = AccountBalance - amount;
            return AccountBalance;
        }

    public double Deposit(int amount)
        {
            AccountBalance = AccountBalance + amount;
            return AccountBalance;
        }
    }
}