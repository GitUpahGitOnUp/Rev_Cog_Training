using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankLIB.Entities;

public class Accounts
{
    #region     Properties
    // Data Annotation attributes telling EF that this is the PK, but don't auto-gen the value
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccNo {get; set;} // this should be PK ID

    public decimal AccBalance {get; private set;}

    public string AccHolderName {get; set;} = "";

    public AccountType Type {get; set;}

    // only applicable to Loan accounts, null for Checking + Savings acc.s
    public decimal? InterestRate {get; set;}

    public int? LoanTermYears {get; set;}
    
    // Ea. account has its own transaction history. Last 5 Transactions option displays 1 acc. only.
    public List<Transaction> Transactions {get; set;} = new();

    // Ea. account holds its own service request  history.
    public List<ServiceRequest> ServiceRequests {get; set;} = new();

    #endregion
    
    # region Accounts Constructor
    
    // parameterless allows EF Core a way to materialize an Acc. obj. when it reads from the DB
    public Accounts() {}

    // the only way to set an initial AccBalance from outside this class, used at creation timeacc
    public Accounts(int accNo, string accHoldername, AccountType type, decimal startingBalance = 0)
    {
        AccNo = accNo;
        AccHolderName = accHoldername;
        Type = type;
        AccBalance = startingBalance;
    }


    #endregion

#region     Account Methods

    public decimal CheckBalance()
    {
        return AccBalance;
    }

    public decimal Withdraw(decimal amount)
    {
        // won't allow withdrawals of no money or negative amount
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Withdrawal amount must be greater than 0.");
            
        }

        // won't allow withdrawal greater than the account balance
        if (amount > AccBalance)
        {
            throw new InvalidOperationException(
                $"Insufficient funds. The Current balance is {AccBalance:C}, request withdrawal amount is {amount:C} ");
        }
        // in-memory arithmetic only. Balance changes will hit the DB via dbContext.SaveChanges()
        AccBalance = AccBalance - amount;
        return AccBalance;
    }

    public decimal Deposit(decimal amount)
    {
        // cannot deposit no money or a negative amount
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                    nameof(amount),             // nameof(amount) the compiler checks for matching parameter, would give you guff if the amount var changed
                    "The deposit amount must be greater than zero.");
        }

        AccBalance = AccBalance + amount;
        return AccBalance;
    }
#endregion

}
