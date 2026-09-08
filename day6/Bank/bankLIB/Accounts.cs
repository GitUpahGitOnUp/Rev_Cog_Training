namespace bankLIB;

public class Accounts
{
    public int accNo {get; set;}

    public double accBalance {get; set;}

    public string accName {get; set;} = "";


    public double checkBalance()
    {
        return accBalance;
    }

    public double Withdraw(int amount)
    {
        // add input validations HERE

        // connect to DB and execute SQL query on table (using LINQ) 
        accBalance = accBalance - amount;
        return accBalance;
    }

    public double Deposit(int amount)
    {
        // add input validations HERE

        // connect to DB and execute SQL query on table (using LINQ) 
        accBalance = accBalance + amount;
        return accBalance;
    }
}
