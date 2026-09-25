namespace bankLIB.Entities;

// Customer : User means C inherits from U

public class Customer : User
{
    // a customer can have mult. accs, so a list will hold them
    // '= new()' just starts this as an empty list (never null) so .Add()/.Count are always safe -
    // EF Core overwrites it with real rows only when a query eager-loads it via .Include(c => c.Accounts)
    public List<Accounts> Accounts {get; set;} = new();
}