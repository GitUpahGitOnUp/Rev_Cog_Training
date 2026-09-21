namespace bankLIB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

// this abstract class serves as a template for different kinds of users
// so User objs cannot be made i.e. new User(), but its subclasses *can*

public abstract class User
{

    public int UserId {get; set;}
    public string FirstName {get; set;} = "";

    public string MiddleInitial {get; set;} = "";

    public string LastName {get; set;} = "";
    public string Username {get; set;} = "";

    public string PasswordHash { get; set; } = "";

    // each sub-class gets access to this validation logic

    // BCrypt.Verify hashes enteredPassword using the same
    // salt embedded in PasswordHash, compares the 2 hashes
    // but doesn't actually decrypt them as hashing is 1-way
    public bool ValidateLogin(string enteredPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, PasswordHash);
    }
}