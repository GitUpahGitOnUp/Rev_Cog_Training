using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankLIB.Entities;

public class Accounts
{
    #region Properties
    // explicit primarky key as AccNo doesn't match Ef's auto-detect. naming pattern
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccNo {get; set;} // this should be PK ID

    public decimal AccBalance {get; set;}

    public string AccHolderName {get; set;} = "";

    public AccountType Type {get; set;}
    
    // Ea. account has its own transaction history. Last 5 Transactions option displays 1 acc. only.
    public List<Transaction> Transactions {get; set;} = new();

    // Ea. account holds its own service request  history.
    public List<ServiceRequest> ServiceRequests {get; set;} = new();

    #endregion
    public decimal CheckBalance()
    {
        return AccBalance;
    }

    public decimal Withdraw(decimal amount)
    {
        // add input validations HERE
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Withdrawal amount must be greater than 0.");
            
        }
        if (amount > AccBalance)
        {
            throw new InvalidOperationException(
                $"Insufficient funds. The Current balance is {AccBalance:C}, request withdrawal amount is {amount:C} ");
        }
        // connect to DB and execute SQL query on table (using LINQ) 
        AccBalance = AccBalance - amount;
        return AccBalance;
    }

    public decimal Deposit(decimal amount)
    {
        // add input validations HERE
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                    nameof(amount),                     // nameof(amount) the compiler checks for matching parameter, would give you guff if the amount var changed
                    "The deposit amount must be greater than zero.");
        }
        // connect to DB and execute SQL query on table (using LINQ) 
        AccBalance = AccBalance + amount;
        return AccBalance;
    }
}
