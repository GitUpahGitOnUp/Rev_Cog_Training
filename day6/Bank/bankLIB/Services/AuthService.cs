using bankLIB.Requests;
using bankLIB.Entities;
using bankLIB.Data;

namespace bankLIB.Services;

// this file checks login creds against known users
public class AuthService
{
    # region Fields
    
    // readonly means this field can only be assigned once in the contructor, and never reassigned after
    // now queries the DB directly (instead of the hardcoded list from earlier dev phase)
    private readonly BankDbContext _dbContext; 
    #endregion

        #region Constructor
        // constructor injection receives user from db now in the final version, does not create it
        // this keeps AuthService's logic stable (checking a password) by decoupling it from the class that fetches the data
        public AuthService(BankDbContext dbContext)
    {   // _users being read only means it can only be assigned once right here, and _users could never be reassigned
        // to point at a different list later. Only the contents can be updated by additions or removals of a particular user
        _dbContext = dbContext;
    }
        #endregion

        #region Authentication

        public User Login(LoginRequest request)
    {
        request.Validate();

        var matchedUser = _dbContext.Users
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
