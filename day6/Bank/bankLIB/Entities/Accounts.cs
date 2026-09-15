namespace bankLIB;

public class Accounts
{
    public int AccNo {get; set;} // this should be PK ID

    public double AccBalance {get; set;}

    public string AccHolderName {get; set;} = "";


    public double checkBalance()
    {
        return AccBalance;
    }

    public double Withdraw(int amount)
    {
        // add input validations HERE

        // connect to DB and execute SQL query on table (using LINQ) 
        AccBalance = AccBalance - amount;
        return AccBalance;
    }

    public double Deposit(int amount)
    {
        // add input validations HERE

        // connect to DB and execute SQL query on table (using LINQ) 
        AccBalance = AccBalance + amount;
        return AccBalance;
    }
}
