namespace EmployeeManagement
{
    public class SavingsAccount
    {
        #region Properties
        public int AccNo {get; set;}
        
        public string AccHolderName {get; set;} = "";

        public double AccBalance {get; set;}
        
        public bool AccIsActive {get; set;}
        
        public int AccBranchNo {get; set;}

        #endregion

        #region Methods
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

        public double Withdraw(int amount)
        {
            if(amount < 100)
            {
                throw new Exception("Withdrawals must be at least $100");
            }
            else
            {
                AccBalance = AccBalance - amount;
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