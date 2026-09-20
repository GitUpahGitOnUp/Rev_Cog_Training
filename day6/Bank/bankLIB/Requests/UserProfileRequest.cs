namespace bankLIB.Requests;

// transports the details for an Admin editing a customer's acc.
public class UserProfileRequest
{
    public int AccNo {get; set;}

    public string NewAccHolderName {get; set;} = "";
    
    public void Validate()
    {
        if (AccNo >= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(AccNo), "This account number is invalid");
        }

        if (string.IsNullOrWhiteSpace(NewAccHolderName))
        {
            throw new ArgumentException(
                "The account holder name cannot be empty.",
                    nameof(NewAccHolderName));
        }

    }
}