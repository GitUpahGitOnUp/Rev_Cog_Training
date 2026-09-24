namespace bankLIB.Requests;

public class ChangePasswordRequest
{
    public string NewPassword {get; set;} = "";
    public void Validate()
    {
        if (NewPassword.Length < 8)
        {
            throw new ArgumentException("The password must be at least 8 characters");
        }

        if (!NewPassword.Any(char.IsUpper))
        {
            throw new ArgumentException("The password must contain at least one uppercase letter.");
        }

        if (!NewPassword.Any(char.IsLower))
        {
            throw new ArgumentException("The password must contain at least one lowercase letter");
        }

        if (!NewPassword.Any(char.IsDigit))
        {
            throw new ArgumentException("The password must contain at least one number.");
        }

        if (NewPassword.All(char.IsLetterOrDigit))
        {
            throw new ArgumentException("The password must contain at least 1 special character.");
        }
    }
}