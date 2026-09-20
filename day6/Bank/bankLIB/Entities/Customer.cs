namespace bankLIB.Entities;

// Customer : User means C inherits from U

public class Customer : User
{
    // a customer can have mult. accs, so a list will hold them
    public List<Accounts> Accounts {get; set;} = new();
}