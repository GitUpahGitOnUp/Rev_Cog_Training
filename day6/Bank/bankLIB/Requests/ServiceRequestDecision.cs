namespace bankLIB.Requests;

using bankLIB.Entities; // needed for AccountType

// validated whether a pending service request on an account
// is eligible to be approved or denied

public class ServiceRequestDecision 
{
    public AccountType AccType {get; set;}

    public decimal AccBalance {get; set;}

    public void Validate()
    {
        // Excludes Loan Accounts as only non - Loan type accounts are eligible for check books
        // and accounts that are overdrawn

        if (AccType != AccountType.Loan && AccBalance < 0)
        {
            throw new InvalidOperationException($"Cannot approve or deny: the account is overdrawn. Balance:    {AccBalance:C}");
        }
    }
}