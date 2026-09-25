using bankLIB.Requests;
using bankLIB.Entities;
using bankLIB.Data;
using Microsoft.EntityFrameworkCore;

namespace bankLIB.Services;

// this file checks login creds against known users
public class AuthService
{
    # region Fields
    
    // readonly means this field can only be assigned once, in the constructor, and never reassigned later
    // queries the DB directly now, instead of the hardcoded user list from the project's earlier skeleton phase
    private readonly BankDbContext _dbContext; 

    #endregion

        #region Constructor
    // constructor injection: AuthService receives an already-configured BankDbContext rather than building
    // its own, so it never needs to know about connection strings or how the DB is set up

        public AuthService(BankDbContext dbContext)
    {   
       // _dbContext is 'readonly', so it can only be assigned once, right here in the constructor -
      // it can never later be pointed at a different context, only queried through

        _dbContext = dbContext;
    }
        #endregion

        #region Authentication

        public User Login(LoginRequest request)
    {
        request.Validate();

        var matchedUser = _dbContext.Users
            .Include("Accounts.Transactions") // eager loading transactions here because this User obj. is only ever fetched once per session 
                                                // Program.cs reuses this same instance for everything afterword, including Last 5 Transacions
            .FirstOrDefault(u => u.Username == request.Username);

        if (matchedUser is null || !matchedUser.ValidateLogin(request.Password))
        {
            throw new InvalidOperationException(
                "Invalid username or password.");
        }

        return matchedUser;
    }
        #endregion
}
