namespace Banking
{  
    public enum TypeOfAccount
    {
        Checking,
        Savings,
        Loan
    }

    public abstract class Accounts
    {
        #region Account Properties

        public int AccNo {get; set;}

        public string AccHolderName {get; set;} = "";
        
        public TypeOfAccount AccType {get; set;}
        
        public double AccBalance {get; set;}
        
        public bool AccIsActive {get; set;} = true;

     #endregion

        #region Methods
        public virtual double Withdraw(int amount)
        {
            if(amount < 100)
            {
                throw new Exception("Withdrawals must be greater than $100.");
            }
            else
            {
                AccBalance = AccBalance - amount;
                return AccBalance;
            }
        }

        public double Deposit(int amount)
        {
            if(amount < 0)
            {
                throw new Exception("Deposits cannot be a negative amount.");
            }
            else
            {
                AccBalance = AccBalance + amount;
                return AccBalance;
            }
 
        }

        public double CheckBalance()
        {
            return AccBalance;
        }
    #endregion
    }
}