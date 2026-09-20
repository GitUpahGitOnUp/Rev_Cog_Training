namespace bankLIB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

// this abstract class serves as a template for different kinds of users
// so User objs cannot be made i.e. new User(), but its subclasses *can*

public abstract class User
{
    [DatabaseGenerated(
    DatabaseGeneratedOption.None)]
    public int UserId {get; set;} // both admins & customers get a UId
    public string Username {get; set;} = "";

    public string Password {get; set;} = "";

    // gotta figure out how do incorporate BCrypt or something here! ***
    public string PasswordHash { get; set; } = "";

    // each sub-class gets access to this validation logic
    public bool ValidateLogin(string enteredPassword)
    {
        return Password == enteredPassword;
    }
}