namespace bankLIB.Requests;
using System;
// transports the details of a transfer between 2 accs
public class TransferRequest
{
    #region TR Properties
    public int FromAccNo {get; set;}

    public int ToAccNo {get; set;}

    public decimal Amount {get; set;}

    #endregion

    #region Validation 

    public void Validate()
    {
        if (FromAccNo <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(FromAccNo), "The source account number is invalid."); // using nameof will prevent me from changing the property name and 
        }                                                                    // not reflecting it here. The compiler would give me guff about it.   

        if (ToAccNo <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(ToAccNo), "The destination account number is invalid.");
        }

        if (FromAccNo == ToAccNo)
        {
            throw new InvalidOperationException( //this exception is thrown instead of out of range because the individ. acc. nums are 
                    "You cannot make a transfer to the same Account. ");  // valid, but the logic of the operation is not
        }

        if (Amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                    nameof(Amount), "The transfer amount must be greater than 0.");
        }
    }
    #endregion
}