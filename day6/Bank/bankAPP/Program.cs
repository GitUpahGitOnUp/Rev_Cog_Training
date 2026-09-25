using bankLIB.Entities;
using bankLIB.Services;
using bankLIB.Requests;

using bankLIB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// ConfigBuilder is the .NET class for loading the application settings, setbase path points to where to look
// AddJsonFile reads appsettings.json from app output folder
IConfiguration config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();

string connectionString = config.GetConnectionString("BankDb") ?? 
        throw new InvalidOperationException("Connection string 'BankDb' not found in appsettings.json");

var optionsBuilder = new DbContextOptionsBuilder<BankDbContext>();
optionsBuilder.UseSqlServer(connectionString);

// debug logging text to show boo-boos, whoops-a-doodles, and confounding behavior
//optionsBuilder.LogTo(Console.WriteLine);

using BankDbContext dbContext = new BankDbContext(optionsBuilder.Options);

SeedDatabaseIfEmpty(dbContext);

AuthService authService = new AuthService(dbContext);

#region ------- Seed Database If Empty ------- 

// only seeds demo data if the db is totally empty, gets called ea. time at startup

static void SeedDatabaseIfEmpty(BankDbContext dbContext)
{
    // .Any() == LINQ method, returns True if the collection has at least 1 item
    // false if empty. It's cheaper than call .Count < 0 as it stops as soon as it encounters 1 item

    if (dbContext.Users.Any())
    {
        return;
    }

    var checkingAccount = new Accounts
    (
        1001,
        "Jane Doe",
        AccountType.Checking,
        500
    
    );
        var savingsAccount = new Accounts
    (
        1002,
        "Jane Doe",
        AccountType.Savings,
        2500
    );
        var loanAccount = new Accounts
    (
       1003,
      "Jane Doe",
      AccountType.Loan,  
     -10000
    )
    {
      InterestRate = 6.5m,
      LoanTermYears = 15
    };

    var demoCustomer = new Customer
    {
        FirstName = "Jane",
        MiddleInitial = "M",
        LastName = "Doe",
        Username = "customer1",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer123!"),
        Accounts = new List<Accounts>
        {
            checkingAccount,
            savingsAccount,
            loanAccount
        }
    };

    var demoAdmin = new Admin
    {
        FirstName = "Alexandra",
        MiddleInitial = "S",
        LastName = "Rivera",
        Username = "admin1",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!")
    };

    // .Add() doesn't hit the DB, it tells dbContext to track the new object for future saving

    dbContext.Users.Add(demoCustomer);
    dbContext.Users.Add(demoAdmin);

    // SaveChanges() is what actually sends the SQL INSERT statements
    dbContext.SaveChanges();


}

#endregion


// prompts admin for login, continue to menu if validated
#region ------- Handle Admin Login -------
static void HandleAdminLogin(AuthService authService, BankDbContext dbContext)
{
    Admin? admin = PromptLogin<Admin>(authService);

    if (admin is not null)
    {
        Console.WriteLine($"Welcome back, {admin.FirstName}!");
        Console.WriteLine("");
        DisplayAdminMenu(admin, dbContext);
    }
}
#endregion


// prompts cust. for login, continue to menu if credentials are validated
#region ------- Handle Customer Login -------

static void HandleCustomerLogin(AuthService authService, BankDbContext dbContext)
{
    Customer? customer = PromptLogin<Customer>(authService);

    if (customer is not null)
    {
        Console.WriteLine($"Welcome back, {customer.FirstName}! Please make a selection:");
        Console.WriteLine("");
        DisplayCustomerMenu(customer, dbContext);
    }
}
#endregion


// generic handles login prompt for Cust + Admin types
// and loops until login is validated or user gives up and exits
#region ------- PromptLogin Method -------

static TUser? PromptLogin<TUser>(AuthService authService)
    // where... is a compile-time check that tells the compiler that TUser must be a User or an inheritance member of User class
    // and this gives if(loggedInUser is TUser typedUser) lower in the block it's ability to validate
    where TUser : User 
{
    while (true)
    {
        Console.Write("Please enter username:   ");
        string? username = Console.ReadLine();
        Console.WriteLine("");

        Console.Write("Please enter your password:  ");
        string? password = ReadPassword();
        Console.WriteLine("");

        var request = new LoginRequest
        {
            Username = username ?? "",
            Password = password 
        };

        try
        {
            User loggedInUser = authService.Login(request);

            // checks that logged-in user is of the expected type i.e. 
            // if a customer type is trying to access the customer menu
            // if it's not the right user type, it denies access to menus
            if (loggedInUser is TUser typedUser)
            {
                return typedUser;
            }

            Console.WriteLine("Invalid credentials for this login.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
        }

        Console.Write("Please enter to retry, or 'exit' to cancel: ");
        string? retry = Console.ReadLine();

        if (string.Equals(
            retry, "exit",
            StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
    }
}
#endregion


#region ------- Read Password Securely ------- 

static string ReadPassword()
{
    string password = "";

    while (true)
    {
        // intercept: 'true' means that the key press is captured w/o being 
        // printed to the console
        ConsoleKeyInfo key = Console.ReadKey(intercept: true);

        if (key.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }

        if(key.Key == ConsoleKey.Backspace && password.Length > 0)
        {
            // \b moves the cursor back one space, then that space overwrites
            // the visible *, then \b again moves back so that the next char types 
            // in the correct place
            password = password.Substring(0, password.Length - 1);

            Console.Write("\b\b");
            continue;
        }
        // filters out control chars like Enter, Tab, Esc, etc.
        // and only acts on printable chars
        if (!char.IsControl(key.KeyChar))
        {
            password += key.KeyChar;
            Console.Write("*");
        }
    }

    return password;
}

#endregion


#region ------- Select Account Method -------
static Accounts? SelectAccount(Customer customer)
{
    if(customer.Accounts.Count == 0)
    {
        Console.WriteLine("No accounts found for this profile.");
        return null;
    }

    Console.WriteLine("Please select an account:");

    // prints a numbered menu list of accounts for user to select from
    for (int i = 0; i < customer.Accounts.Count; i++)
    {
        Accounts acc = customer.Accounts[i];
        Console.WriteLine($"{i + 1}. {acc.Type}" + $"(Account {acc.AccNo})");
    }

    string? input = Console.ReadLine();

    // TryParse takes user input and attempts to convert it to an int and returs T/F. 'out' allows 2 return values
    //  - the success/failure of parsing + the int entered
    if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= customer.Accounts.Count)
    {
        return customer.Accounts[selectedIndex - 1];
    }

    Console.WriteLine("Invalid selection.");
    return null;
}
#endregion


#region ------- Select Customer Method For Admin Tasks -------

static Customer? SelectCustomer(BankDbContext dbContext)
{   

    Console.Write("Please enter a customer ID:  ");
    Console.WriteLine("");

    string? input = Console.ReadLine();

    // TryParse will display error message, return null if invalid cust. Id# is entered
    if (!int.TryParse(input, out int customerId))
    {
        Console.WriteLine("Invalid customer ID.");
        return null;
    }

    // FirstOrDefault and OfType... == LINQ method that matches
    // a UserId, returns null if no match is found so that no exception is thrown

    var matchedCustomer = dbContext.Users
        .OfType<Customer>()
        .Include(c => c.Accounts) // ! Eager loading, w/o it, the list would come back empty every time due to lack of EF fetch
        .ThenInclude(a => a.Transactions)
        .Include(c => c.Accounts)
        .ThenInclude(a => a.ServiceRequests)
        .FirstOrDefault(c => c.UserId == customerId);

    if (matchedCustomer is null)
    {
        Console.WriteLine("No customer found with that ID.");
    }

    return matchedCustomer;
}
#endregion


// top level safety net to try to make this app air-tight
// to catch an unanticipated error and not crash the whole dang thing
#region ------- Welcome Menu - Run Application -------

try
{
    RunApplication(authService, dbContext);
}
catch (Exception ex)
{
    Console.WriteLine($"There was an unexpected error: {ex.Message}");
    Console.WriteLine("The application will now exit.");
}


static void RunApplication(AuthService authService, BankDbContext dbContext)
{
    
    bool continueMenuSelection = true;

    while(continueMenuSelection)
    {
        Console.WriteLine("");
        Console.WriteLine("-------- Welcome to Community Wealth Credit Union  --------");
        Console.WriteLine("");
        Console.WriteLine("Please select from the options below to login: ");
        Console.WriteLine("");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin ");
        Console.WriteLine("3. Exit");

        string? choice = Console.ReadLine();  // ? == nullable reference type, forces compiler to check before using it

        switch (choice)
        {
            case "1":
                    HandleCustomerLogin(authService, dbContext);
                    break;
            
            case "2":
                    HandleAdminLogin(authService, dbContext);
                    break;
            
            case "3":
                    continueMenuSelection = false;
                    break;
            default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;

        }
    }
}
#endregion


//  Customer Menu loop
//  uses try/catch for each option so a bad entry doesn't crash the entire menu
#region ------- Customer Menu -------

static void DisplayCustomerMenu(Customer customer, BankDbContext dbContext)
{
    bool inCustomerMenu = true;

    while (inCustomerMenu)
    {
        Console.WriteLine("--------- Customer Menu ---------");
        Console.WriteLine("");
        Console.WriteLine("1. Check Account Details");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.WriteLine("4. Transfer");
        Console.WriteLine("5. Last 5 Transactions");
        Console.WriteLine("6. Request Check Book");
        Console.WriteLine("7. Change Password");
        Console.WriteLine("8. Exit");
        
        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
               
                #region 1. Account Summary Details

                case "1":

                    // shows the cust. a numbered list of their accs. and returns there selection, or null is none exists
                    Accounts? selectedAccount = SelectAccount(customer);

                    if (selectedAccount is null)
                    {
                        break;
                    }
                    Console.WriteLine("---------- Account Details ---------- ");

                    Console.WriteLine($"Account Number: {selectedAccount.AccNo}");
                    Console.WriteLine($"Account Type: {selectedAccount.Type}");
                    Console.WriteLine($"Account Holder: {selectedAccount.AccHolderName}");
                    Console.WriteLine($"Balance: " +
                    $"{(selectedAccount.AccBalance < 0 ? "-" : "")}" +
                    $"${Math.Abs(selectedAccount.AccBalance):N2}");

                    break;
                #endregion

                #region 2. Make A Withdrawal

                case "2":
                    
                    Accounts? withdrawAccount = SelectAccount(customer);

                    if ( withdrawAccount is null)
                    {
                        break;
                    }
                    
                    Console.Write("Please enter the amount you would like to withdraw:  ");
                    string? withdrawInput = Console.ReadLine();
                    Console.WriteLine("");

                    // ! == if this does NOT parse, so Invalid statement branch can run
                    if (!decimal.TryParse(withdrawInput, out decimal withdrawAmount))
                    {
                        Console.WriteLine("Invalid amount entered");
                        Console.WriteLine("");
                        break;
                    }
                    try
                    {
                        decimal newBalance = withdrawAccount.Withdraw(withdrawAmount);

                        var withdrawTransaction = new Transaction
                        {
                            AccNo = withdrawAccount.AccNo,
                            Type = TransactionType.Withdraw,
                            Amount = withdrawAmount,
                            BalanceAfterTransaction = newBalance
                        };

                        withdrawAccount.Transactions.Add(withdrawTransaction);
                        Console.WriteLine($"Withdrawal Successful. New balance: {newBalance:C}");
                        Console.WriteLine("");
                        Console.WriteLine("");

                        // commit the withdrawal *and* the transaction record -> SQL Server
                        // this is what moves it from only changing the in-memory obj.
                        dbContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Withdrawal failed: {ex.Message}");
                    }

                    break;
                #endregion

                #region 3. Make A Deposit

                case "3":
                    Accounts? depositAccount = SelectAccount(customer);

                    if (depositAccount is null)
                    {
                        break;
                    }

                    Console.Write("Please enter the amount you would like to deposit:   ");
                    string? depositInput = Console.ReadLine();
                    Console.WriteLine("");

                    if (!decimal.TryParse(depositInput, out decimal depositAmount))
                    {
                        Console.WriteLine("Invalid amount entered.");
                        Console.WriteLine("");
                        break;
                    }
                    try
                    {
                        decimal newBalance = depositAccount.Deposit(depositAmount);

                        var depositTransaction = new Transaction
                        {
                            AccNo = depositAccount.AccNo,
                            Type = TransactionType.Deposit,
                            Amount = depositAmount,
                            BalanceAfterTransaction = newBalance
                        };

                        depositAccount.Transactions.Add(depositTransaction);
                        
                        // commits deposit and transaction record -> SQL Server
                        dbContext.SaveChanges();

                        Console.WriteLine($"Deposit Successful. New balance: {newBalance:C}");
                        Console.WriteLine("");
                    }
                    catch (Exception ex)
                    {
                        Console.Write($"Deposit failed: {ex.Message}");
                        Console.WriteLine("");
                    }
                    break;
                
                #endregion

                #region 4. Make A Transfer

                case"4":
                    Console.Write("Select the account to transfer FROM: ");
                    Accounts? fromAccount = SelectAccount(customer);


                    if (fromAccount is null)
                    {
                        break;
                    }

                    Console.Write("Select the account to transfer TO: ");
                    Accounts? toAccount = SelectAccount(customer);

                    if (toAccount is null)
                    {
                        break;
                    }

                    Console.Write("Please enter the amount you wish to transfer:    ");
                    Console.WriteLine("");

                    string? transferInput = Console.ReadLine();

                    if(!decimal.TryParse(transferInput, out decimal transferAmount))
                    {
                        Console.WriteLine("Invalid amount entered.");
                        break;
                    }

                    // builds transfer request from user input
                    // Validate() checks for from-to account sameness, invalid acc nums, negative nums, etc.
                    var TransferRequest = new TransferRequest
                    {
                        FromAccNo = fromAccount.AccNo,
                        ToAccNo = toAccount.AccNo,
                        Amount = transferAmount
                    };

                    try
                    {   
                        TransferRequest.Validate();

                        decimal fromNewBalance = fromAccount.Withdraw(transferAmount);

                        decimal toNewBalance = toAccount.Deposit(transferAmount);
                        
                        // 1 transaction PER account, to maintain ea. acc.'s own trans. history
                        var outTransaction = new Transaction
                        {
                            AccNo = fromAccount.AccNo,
                            Type = TransactionType.TransferOut,
                            Amount = transferAmount,
                            BalanceAfterTransaction = fromNewBalance  
                        };

                        var inTransaction = new Transaction
                        {
                            AccNo = toAccount.AccNo,
                            Type = TransactionType.TransferIn,
                            Amount = transferAmount,
                            BalanceAfterTransaction = toNewBalance  
                        };

                        fromAccount.Transactions.Add(outTransaction); // saves transaction history
                        toAccount.Transactions.Add(inTransaction);

                        
                        // commits the transfer and the transaction records -> SQL Server
                        dbContext.SaveChanges();

                        Console.WriteLine("Transfer successful.");

                        Console.WriteLine($"Transfered From Account:  {fromAccount.AccNo} New balance:  {fromNewBalance:C}");
                        Console.WriteLine("");
                        Console.WriteLine($"Transfered To Account:  {toAccount.AccNo}  New Balance:   {toNewBalance:C}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Transfer failed: {ex.Message}");
                    }
                    break;
                
                #endregion

                #region 5. Last 5 Transactions

                case "5":
                    Accounts? historyAccount = SelectAccount(customer);

                    if (historyAccount is null)
                    {
                        break;
                    }

                    if (historyAccount.Transactions.Count == 0)
                    {
                        Console.WriteLine("No transactions on this account yet.");
                        break;
                    }
                    Console.WriteLine("----------- Last 5 transactions: -----------");

                    // LINQ methods retrieves the most recent transactions based on timestamp
                    var recentTransactions = historyAccount.Transactions.OrderByDescending
                    (
                        t => t.Timestamp  

                    ).Take(5);

                    foreach (var t in recentTransactions)
                    {
                        Console.WriteLine(
                            $"{t.Timestamp}: {t.Type} " +
                            $"{t.Amount:C} - Balance " +
                            $"after: " +
                            $"{t.BalanceAfterTransaction:C}" 
                        );
                    }
                    break;
                #endregion

                #region 6. Request A Check Book

                case "6":
                    Accounts? checkAccount = SelectAccount(customer);

                    if (checkAccount is null)
                    {
                        break;
                    }

                    var checkRequest = new ServiceRequest
                    {
                        AccNo = checkAccount.AccNo,
                        Type = ServiceRequestType.CheckBook
                        
                    };

                    checkAccount.ServiceRequests.Add(checkRequest);

                    // commits request status  -> SQL Server
                    dbContext.SaveChanges();

                    Console.WriteLine($"Check book requested. Your request ID is {checkRequest.RequestId}");
                    Console.WriteLine("");
                    break;

                #endregion

                #region 7. Change Password

                case "7":
                    Console.Write("Please enter your current password:  ");
                    Console.WriteLine("");

                    string? currentPasswordInput = ReadPassword();


                    if (!customer.ValidateLogin(currentPasswordInput ?? ""))
                    {
                        Console.WriteLine("Current password is incorrect");
                        Console.WriteLine("");
                        break;
                    }

                    Console.WriteLine("Your new password must have *at least one* : uppercase letter, lowercase letter, number and special character.");
                    Console.WriteLine("");
                    Console.Write("Please enter your new password:  ");
                    Console.WriteLine("");

                    string? newPasswordInput = ReadPassword();

                    if (string.IsNullOrWhiteSpace(newPasswordInput))
                    {
                        Console.WriteLine("New password cannot be empty.");
                        break;
                    }

                    Console.Write("Please confirm your new password: ");
                    Console.WriteLine("");
                    string? confirmPasswordInput = ReadPassword();

                    if (newPasswordInput != confirmPasswordInput)
                    {
                        Console.WriteLine("Passwords do not match. Your password was not changed.");
                        Console.WriteLine("");
                        break;
                    }
                    // change password request obj. to check for valid password format
                    var changePasswordRequest = new ChangePasswordRequest {NewPassword = newPasswordInput};
                    
                    try
                    {
                        changePasswordRequest.Validate();
                    }
                    
                    catch (Exception ex)
                    {
                        Console.WriteLine($"That password did not satisfy creation rules: {ex.Message}");
                        break;
                    }

                    customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPasswordInput);

                    // commits updated password -> SQL Server
                    dbContext.SaveChanges();

                    Console.WriteLine("Your password was changed successfully.");
                    Console.WriteLine("");
                    break;

                #endregion

                case "8":
                    inCustomerMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
#endregion


// Admin menu loop, taking a User list to hold all users and preserve a single record of truth
#region ------- Admin Menu -------

static void DisplayAdminMenu(Admin admin, BankDbContext dbContext)
{
    bool inAdminMenu = true;

    while (inAdminMenu)
    {
        Console.WriteLine("--------- Admin Menu ---------");
        Console.WriteLine("");
        Console.WriteLine("1. Create New Account");
        Console.WriteLine("2. Delete Account");
        Console.WriteLine("3. Edit Account Details");
        Console.WriteLine("4. Display Summary");
        Console.WriteLine("5. Reset Customer Password");
        Console.WriteLine("6. Approve / Deny Check Book Request");
        Console.WriteLine("7. Exit");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                #region 1. Create A New Account

                case "1":

                    Customer? targetCustomer = SelectCustomer(dbContext);

                    if (targetCustomer is null)
                    {
                        break;
                    }
                    
                    Console.Write("Enter a new account number:  ");
                    string? newAccNoInput = Console.ReadLine();
                    Console.WriteLine("");
                    
                    // TryParse checks that the input is a valid integer format and prints an error instead of exiting the menu
                    if (!int.TryParse(newAccNoInput, out int newAccNo))
                    {
                        Console.WriteLine("Invalid account number");
                        Console.WriteLine("");
                        break;
                    }

                    // Checks to ensure Acc. No. does not already exist
                    if (dbContext.Accounts.Any(a => a.AccNo == newAccNo))
                    {
                        Console.WriteLine("An account with that number already exists. Please select a different one.");
                        Console.WriteLine("");
                        break;
                    }

                    Console.Write("Please enter the account holder name:    ");
                    string? newHolderName = Console.ReadLine();
                    Console.WriteLine("");

                    if (string.IsNullOrWhiteSpace(newHolderName))
                    {
                        Console.WriteLine("The account holder name cannot be empty.");
                        break;
                    }
                    
                    Console.WriteLine("Please select the account type:");
                    Console.WriteLine("1. Checking");
                    Console.WriteLine("2. Savings");
                    Console.WriteLine("3. Loan");

                    string? typeInput = Console.ReadLine();
                    Console.WriteLine("");

                    AccountType newAccType;
                    bool validType = true;  // bool will help break out of Assign Type switch for invalid Acc. Type input

                    switch (typeInput)
                    {
                        case "1":
                            newAccType = AccountType.Checking;
                            break;
                        
                        case "2":
                            newAccType = AccountType.Savings;
                            break;
                        
                        case "3":
                            newAccType = AccountType.Loan;
                            break;
                        default:
                            Console.WriteLine("Invalid type.");
                            newAccType = default; // assigned here to appease the compiler, it's never actually read due to validType guard
                            validType = false;
                            break;
                    }

                    // break out of Acc. Type invalid entry back -> Admin menu
                    if (!validType)
                    {
                        break; // breaks the OUTER switch's case 1 -> the admin menu loop
                    }

                    // obj. initializer for new account calls account constructor to create the obj. in memory
                    // sets several acc. properties right away, defaults acc. balance -> 0
                    // 'var' relies on type inference with the compiler using what's at the right side of '=' to call the correct Account constructor
                    var newAccount = new Accounts(newAccNo, newHolderName, newAccType);
                    

                    // this attaches the customer account to the new Accounts object in their own list, so the customer
                    // really owns their account
                    targetCustomer.Accounts.Add(newAccount);

                    // commits new account from in-memory obj -> SQL Server
                    dbContext.SaveChanges();

                    Console.WriteLine(
                        $"Account {newAccount.AccNo} " +
                        $"created for " +
                        $"{targetCustomer.Username}.");

                    break;  
        #endregion

                #region 2. Delete Account

                case "2":
                    Customer? deleteTargetCustomer = SelectCustomer(dbContext);

                    if (deleteTargetCustomer is null)
                    {
                        break;
                    }

                    Accounts? accountToDelete = SelectAccount(deleteTargetCustomer);

                    if (accountToDelete is null)
                    {
                        break;
                    }
                    // Gaurdrails for Checkings and Savings Accounts with a non-zero balance, excluding loan accounts
                    // Can't delete accounts with a positive or negaitive balance
                    if (accountToDelete.AccBalance != 0)
                    {
                        if (accountToDelete.Type != AccountType.Loan && accountToDelete.AccBalance < 0)
                        {
                            Console.WriteLine("Cannot delete: this account is overdrawn." +
                            $"Balance: " +
                            $"{accountToDelete.AccBalance:C}");  
                        }
                        else
                        {
                            Console.WriteLine("Cannot delete an account with a non-zero balance. " +
                            $"Current Balance: " +
                            $"{accountToDelete.AccBalance:C}");
                        }
                        break;
                    }

                    Console.WriteLine(
                        $"Are you sure you want to delete account {accountToDelete.AccNo}?" +
                        " Please enter yes/no");

                        string? confirmDelete = Console.ReadLine();

                        if(!string.Equals(confirmDelete, "yes", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Deletion cancelled.");
                        break;
                    }

                    deleteTargetCustomer.Accounts.Remove(accountToDelete);

                    // commits deleted account to SQL Server
                    dbContext.SaveChanges();

                    Console.WriteLine($"Account  {accountToDelete.AccNo} deleted.");

                    break;
                #endregion
                
                #region 3. Edit Account Details

                case "3":
                    Customer? editTargetCustomer = SelectCustomer(dbContext);

                    if (editTargetCustomer is null)
                    {
                        break;
                    }

                    Accounts? accountToEdit = SelectAccount(editTargetCustomer);

                    if(accountToEdit is null)
                    {
                        break;
                    }

                    Console.WriteLine("Please enter a new account holder name: ");
                    string? newName = Console.ReadLine();

                    // UserProfileRequest gets built from the found acount + new name, calls Validate() to catch
                    // empty names before any next steps

                    var profileRequest = new UserProfileRequest
                    {
                        AccNo = accountToEdit.AccNo,
                        NewAccHolderName  = newName ?? ""
                    };

                    try
                    {
                        profileRequest.Validate();

                        accountToEdit.AccHolderName = profileRequest.NewAccHolderName;

                        // saves updated name -> SQL Server
                        dbContext.SaveChanges();

                        Console.WriteLine($"Account {accountToEdit.AccNo} holder name updated to {accountToEdit.AccHolderName}.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Update failed: " +
                        $"{ex.Message}");
                    }
                    break;
                #endregion
            
                #region 4. Show Account Summary

                case"4":
                    Console.WriteLine("------ Account Summary ------- ");

                    var allCustomers = dbContext.Users
                            .OfType<Customer>()
                            .Include(c => c.Accounts)
                            .ToList();

                    foreach (var c in allCustomers)
                    {
                        Console.WriteLine(
                            $"Customer: {c.Username}" +
                            $"(ID: {c.UserId})"
                        );

                        if (c.Accounts.Count == 0)
                        {
                            Console.WriteLine("No existing accounts to summarize.");
                            continue;
                        }

                        foreach (var acc in c.Accounts)
                        {
                            Console.WriteLine($" {acc.Type}" +
                            $"(#{acc.AccNo}): " +
                            $"{(acc.AccBalance < 0 ? "-" : "")}" +
                            $"${Math.Abs(acc.AccBalance):N2}");
                        }
                    }
                    break;
                #endregion

                #region 5. Reset Customer Password

                case "5":
                    Customer? resetTargetCustomer = SelectCustomer(dbContext);

                    if (resetTargetCustomer is null)
                    {
                        break;
                    }

                    Console.WriteLine("Enter a new password for " +
                    $"{resetTargetCustomer.Username}:");
                    string? resetPasswordInput = ReadPassword();

                    if (string.IsNullOrWhiteSpace(resetPasswordInput))
                    {
                        Console.WriteLine("The new password cannot be empty.");
                        break;
                    }
    
                    Console.Write("Please confirm your new password: ");
                    Console.WriteLine("");
                    string? confirmPasswordInput = ReadPassword();

                    if (resetPasswordInput != confirmPasswordInput)
                    {
                        Console.WriteLine("Passwords do not match. Your password was not changed.");
                        Console.WriteLine("");
                        break;
                    }

                    var resetPasswordRequest = new ChangePasswordRequest {NewPassword = resetPasswordInput};

                    try
                    {
                        resetPasswordRequest.Validate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"That password did not satisfy creation rules: {ex.Message}");
                        break;
                    }

                    resetTargetCustomer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordInput);

                    // commits new password -> SQL Server
                    dbContext.SaveChanges();

                    Console.WriteLine("Customer password has been reset " +
                    $"for {resetTargetCustomer.Username}");
                    break;
                
                #endregion
                
                #region 6. Approve / Deny Check Book Request

                case "6":
                    Customer? approveTargetCustomer = SelectCustomer(dbContext);

                    if (approveTargetCustomer is null)
                    {
                        break;
                    }

                    Accounts? approveAccount = SelectAccount(approveTargetCustomer);

                    if (approveAccount is null)
                    {
                        break;
                    }

                    var approveServiceRequest = new ServiceRequestDecision
                    {
                        AccType = approveAccount.Type,
                        AccBalance = approveAccount.AccBalance
                    };

                    try
                    {
                        approveServiceRequest.Validate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Cannot approve or deny:  {ex.Message}");
                        break;
                    }

                    var pendingRequests = approveAccount.ServiceRequests
                            .Where(r => r.Status == ServiceRequestStatus.Pending)
                            .ToList();
                    
                    if (pendingRequests.Count == 0)
                    {
                        Console.WriteLine("No pending requests on this account");
                        break;
                    }

                    Console.WriteLine("Pending requests: ");

                    for (int i = 0; i < pendingRequests.Count; i++)
                    {
                        var r = pendingRequests[i];
                        Console.WriteLine($"{i + 1}. Request{r.RequestId} - {r.Type} On: ({r.DateRequested})");
                    }

                    Console.WriteLine("To select a request, enter the request number:");
                    string? requestInput = Console.ReadLine();

                    // bail out early with the existing "Invalid selection" message if the
                    // request number itself doesn't parse or is out of range - same guard as before
                    if (!(int.TryParse(requestInput, out int requestIndex) && requestIndex >= 1 && requestIndex <= pendingRequests.Count))
                    {
                        Console.WriteLine("Invalid selection.");
                        break;
                    }

                    // NEW: ask whether this specific request should be approved or denied,
                    // instead of always approving. Reuses the existing ServiceRequestStatus.Rejected
                    // value that was already defined on the enum but never actually used anywhere.
                    Console.WriteLine("1. Approve");
                    Console.WriteLine("2. Deny");
                    Console.Write("Select an action:  ");
                    string? actionInput = Console.ReadLine();
                    Console.WriteLine("");

                    switch (actionInput)
                    {
                        case "1":
                            pendingRequests[requestIndex - 1].Status = ServiceRequestStatus.Approved;

                            // commits approval -> SQL Server
                            dbContext.SaveChanges();

                            Console.WriteLine("Request Approved.");
                            break;

                        case "2":
                            pendingRequests[requestIndex - 1].Status = ServiceRequestStatus.Rejected;

                            // commits denial -> SQL Server
                            dbContext.SaveChanges();

                            Console.WriteLine("Request Denied.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. No action was taken on this request.");
                            break;
                    }
                    break;
                #endregion
                
                case "7":
                    inAdminMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }       
    }
}
#endregion