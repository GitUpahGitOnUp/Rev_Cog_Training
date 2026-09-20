namespace bankLIB.Requests;

// used to carry raw username/password entered @ login
// does not check for correctness but that the input format is valid

public class LoginRequest
{
    public string Username {get; set;} = "";

    public string Password {get; set;} = "";

    #region checks for empty field or whitespace
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            throw new ArgumentException(
                "Username cannot be empty." , nameof(Username));             
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            throw new ArgumentException(
                "Password cannot be empty.", nameof(Password));
        }
    }
    #endregion
}