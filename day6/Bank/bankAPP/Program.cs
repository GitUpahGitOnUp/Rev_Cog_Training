using bankLIB.Entities;
using bankLIB.Services;
using bankLIB.Requests;

using bankLIB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

// reads appsettings.json from app output folder
// IConfigurationBuilder is a part of MS.Extenstion.Config and 
// the standard .NET way to laod settings files

IConfiguration config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();

string connectionString = config.GetConnectionString("BankDb") ?? 
        throw new InvalidOperationException("Connection string 'BankDb' not found in appsettings.json");

var optionsBuilder = new DbContextOptionsBuilder<BankDbContext>();
optionsBuilder.UseSqlServer(connectionString);

using BankDbContext dbContext = new BankDbContext(optionsBuilder.Options);

SeedDatabaseIfEmpty(dbContext);

AuthService authService = new AuthService(dbContext);

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
        Console.WriteLine("-------- Bank Menu   --------");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin ");
        Console.WriteLine("3. Exit");

        string? choice = Console.ReadLine();  // ? == nullable reference type, forces compiler to check before using it

        switch (choice)
        {
            case "1":
                    HandleCustomerLogin(authService);
                    break;
            
            case "2":
                    HandleAdminLogin(authService, dbContext);
                    break;
            
            case "3":
                    continueMenuSelection = false;
                    break;
            default:
                    Console.WriteLine("invalid choice. Please try again.");
                    break;

        }
    }
}
#endregion

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
    {
        AccNo = 1001,
        AccHolderName = "Jane Doe",
        AccBalance = 500,
        Type = AccountType.Checking
    
    };
        var savingsAccount = new Accounts
    {
        AccNo = 1002,
        AccHolderName = "Jane Doe",
        AccBalance = 2500,
        Type = AccountType.Savings
    };
        var loanAccount = new Accounts
    {
      AccNo = 1003,
      AccHolderName = "Jane Doe",
      AccBalance = -10000,
      Type = AccountType.Loan  
    };

    var demoCustomer = new Customer
    {
        UserId = 1,
        Username = "customer1",
        Password = "customer123",
        Accounts = new List<Accounts>
        {
            checkingAccount,
            savingsAccount,
            loanAccount
        }
    };

    var demoAdmin = new Admin
    {
        UserId = 2,
        Username = "admin1",
        Password = "admin123"
    };

    // .Add() doesn't hit the DB, it tells dbContext to track the new object for future saving

    dbContext.Users.Add(demoCustomer);
    dbContext.Users.Add(demoAdmin);

    // SaveChanges() is what actually sends the SQL INSERT statements
    dbContext.SaveChanges();


}

#endregion


// promps cust. for login, continue to menu if credentials are validated
#region ------- Customer Login -------
static void HandleCustomerLogin(AuthService authService)
{
    Customer? customer = PromptLogin<Customer>(authService);

    if (customer is not null)
    {
        Console.WriteLine($"Welcome to Community Wealth, {customer.Username}!");
        DisplayCustomerMenu(customer);
    }
}
#endregion


// prompts admin for login, continue to menu if validated
#region ------- Handle Admin Login -------
static void HandleAdminLogin(AuthService authService, BankDbContext dbContext)
{
    Admin? admin = PromptLogin<Admin>(authService);

    if (admin is not null)
    {
        Console.WriteLine($"Welcome back, {admin.Username}!");
        DisplayAdminMenu(admin, dbContext);
    }
}
#endregion


// generic handles login propmpt for Cust + Admin types
// and loops until login is validated or user gives up and exits
#region ------- PromptLogin Method -------
static TUser? PromptLogin<TUser>(AuthService authService)
    // where... is a compile=time check that tells the compiler that TUser must be a User or an inheritance member of User class
    // and this gives if(loggedInUser is TUser typedUser) lower in the block it's ability to validate
    where TUser : User 
{
    while (true)
    {
        Console.WriteLine("Please enter username: ");
        string? username = Console.ReadLine();

        Console.WriteLine("Please enter your password: ");
        string? password = Console.ReadLine();

        var request = new LoginRequest
        {
            Username = username ?? "",
            Password = password ?? ""
        };

        try
        {
            User loggedInUser = authService.Login(request);

            // checks that logged-in user is of the expected type
            // denies access to the respective menus
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

        Console.WriteLine("Please enter to retry, or 'exit' to cancel: ");
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


#region ------- Select Account Method -------
static Accounts? SelectAccount(Customer customer)
{
    if(customer.Accounts.Count == 0)
    {
        Console.WriteLine("No accounts found of this profile.");
        return null;
    }

    Console.WriteLine("Select an account.");

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


#region ------- Select Customer Method -------

static Customer? SelectCustomer(BankDbContext dbContext)
{   

    Console.WriteLine("Please enter a customer ID");

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
        .FirstOrDefault(c => c.UserId == customerId);

    if (matchedCustomer is null)
    {
        Console.WriteLine("No customer fount with that ID.");
    }

    return matchedCustomer;
}
#endregion

//  Customer Menu loop
//  uses try/catch for each option so one bad entry doesn't crash the entire menu
#region ------- Customer Menu -------
static void DisplayCustomerMenu(Customer customer)
{
    bool inCustomerMenu = true;

    while (inCustomerMenu)
    {
        Console.WriteLine("--------- Customer Menu ---------");
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
                    Console.WriteLine($"Blance: {selectedAccount.AccBalance:C}");

                    break;
                
                case "2":
                    
                    Accounts? withdrawAccount = SelectAccount(customer);

                    if ( withdrawAccount is null)
                    {
                        break;
                    }
                    
                    Console.WriteLine("Please enter the amount you would like to withdraw:");
                    string? withdrawInput = Console.ReadLine();

                    // ! == if this does NOT parse, so Invalid statement branch can run
                    if (!decimal.TryParse(withdrawInput, out decimal withdrawAmount))
                    {
                        Console.WriteLine("Invalid amount entered");
                        break;
                    }
                    try
                    {
                        decimal newBalance = withdrawAccount.Withdraw(withdrawAmount);

                        var withdrawTransaction = new Transaction
                        {
                            // incremented ID, scoped for this acc's own list, until ef core auto-gens its own IDs
                            TransactionId = withdrawAccount.Transactions.Count + 1, 
                            AccNo = withdrawAccount.AccNo,
                            Type = TransactionType.Withdraw,
                            Amount = withdrawAmount,
                            BalanceAfterTransaction = newBalance
                        };

                        withdrawAccount.Transactions.Add(withdrawTransaction);
                        Console.WriteLine(
                            $"Withdrawal Successful. New " +
                            $"balance: {newBalance:C}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Withdrawal failed: {ex.Message}");
                    }

                    break;
                
                case "3":
                    Accounts? depositAccount = SelectAccount(customer);

                    if (depositAccount is null)
                    {
                        break;
                    }

                    Console.WriteLine("Please enter the amount you would like to deposit: ");
                    string? depositInput = Console.ReadLine();

                    if (!decimal.TryParse(depositInput, out decimal depositAmount))
                    {
                        Console.WriteLine("Invalid amount entered.");
                        break;
                    }
                    try
                    {
                        decimal newBalance = depositAccount.Deposit(depositAmount);

                        var depositTransaction = new Transaction
                        {
                            TransactionId = depositAccount.Transactions.Count + 1,
                            AccNo = depositAccount.AccNo,
                            Type = TransactionType.Deposit,
                            Amount = depositAmount,
                            BalanceAfterTransaction = newBalance
                        };

                        depositAccount.Transactions.Add(depositTransaction);
                        Console.WriteLine($"Deposit Successful. New " +
                        $"balance: {newBalance:C}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Deposit failed: {ex.Message}");
                    }
                    break;
                
                case"4":
                    Console.WriteLine(
                        "Select the account to transfer FROM: ");
                    Accounts? fromAccount = SelectAccount(customer);

                    if (fromAccount is null)
                    {
                        break;
                    }

                    Console.WriteLine(
                        "Select the account to transfer TO: ");
                    Accounts? toAccount = SelectAccount(customer);
                    if (toAccount is null)
                    {
                        break;
                    }

                    Console.WriteLine("Please enter the amount you wish to transfer: ");

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
                            TransactionId = fromAccount.Transactions.Count + 1,
                            AccNo = fromAccount.AccNo,
                            Type = TransactionType.TransferOut,
                            Amount = transferAmount,
                            BalanceAfterTransaction = fromNewBalance  
                        };

                        var inTransaction = new Transaction
                        {
                            TransactionId = toAccount.Transactions.Count + 1,
                            AccNo = toAccount.AccNo,
                            Type = TransactionType.TransferIn,
                            Amount = transferAmount,
                            BalanceAfterTransaction = toNewBalance  
                        };

                        fromAccount.Transactions.Add(outTransaction); // saves transaction history
                        toAccount.Transactions.Add(inTransaction);
                        Console.WriteLine("Transfer successful.");

                        Console.WriteLine(
                            $"{fromAccount.AccNo} new " +
                            $"balance:  {toNewBalance:C}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Transfer failed: {ex.Message}");
                    }
                    break;
                
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
                
                case "6":
                    Accounts? checkAccount = SelectAccount(customer);

                    if (checkAccount is null)
                    {
                        break;
                    }

                    var checkRequest = new ServiceRequest
                    {
                        RequestId = checkAccount.ServiceRequests.Count +1,
                        AccNo = checkAccount.AccNo,
                        Type = ServiceRequestType.CheckBook
                        
                    };

                    checkAccount.ServiceRequests.Add(checkRequest);

                    Console.WriteLine("Check book requested. Your " +
                    $"request ID is {checkRequest.RequestId}");
                    break;
                
                case "7":
                    Console.WriteLine("Please enter your current password:");

                    string? currentPasswordInput = Console.ReadLine();


                    if (!customer.ValidateLogin(currentPasswordInput ?? ""))
                    {
                        Console.WriteLine("Current password is incorrect");
                        break;
                    }

                    Console.WriteLine("Please enter your new password: ");

                    string? newPasswordInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newPasswordInput))
                    {
                        Console.WriteLine("New password cannot be empty.");
                        break;
                    }

                    Console.WriteLine("Please confirm your new password: ");
                    string? confirmPasswordInput = Console.ReadLine();

                    if (newPasswordInput != confirmPasswordInput)
                    {
                        Console.WriteLine("Passwords do not match. Your password was not changed.");
                    }

                    customer.Password = newPasswordInput;

                    Console.WriteLine("Your password was changed successfully.");
                    break;
                case "8":
                    inCustomerMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try agian.");
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
        Console.WriteLine("1. Create New Account");
        Console.WriteLine("2. Delete Account");
        Console.WriteLine("3. Edit Account Details");
        Console.WriteLine("4. Display Summary");
        Console.WriteLine("5. Reset Customer Password");
        Console.WriteLine("6. Approve Check Book Request");
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
                    
                    Console.WriteLine("Enter a new account number:");
                    string? newAccNoInput = Console.ReadLine();
                    
                    // if account no. is not found breaks instead of throwing exception
                    if (!int.TryParse(newAccNoInput, out int newAccNo))
                    {
                        Console.WriteLine("Invalid account number");
                        break;
                    }

                    Console.WriteLine("Please enter the account holder name:");
                    string? newHolderName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newHolderName))
                    {
                        Console.WriteLine("The holder name cannot be empty.");
                        break;
                    }
                    
                    Console.WriteLine("Please select the account type:");
                    Console.WriteLine("1. Checking");
                    Console.WriteLine("2. Savings");
                    Console.WriteLine("3. Loan");
                    string? typeInput = Console.ReadLine();

                    AccountType newAccType;

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
                            return;
                    }

                    // obj. initializer for new account calls account constructor to create the obj. in memory
                    // sets several acc. properties right away, defaults acc. balance -> 0
                    // 'var' relies on type inference with the compiler using what's at the right side of '=' to call the correct Account constructor
                    var newAccount = new Accounts
                    {
                        AccNo = newAccNo,
                        AccHolderName = newHolderName,
                        AccBalance = 0,
                        Type = newAccType
                    };

                    // this attaches the customer account to the new Accounts object in their own list, so the customer
                    // really owns their account
                    targetCustomer.Accounts.Add(newAccount);

                    Console.WriteLine(
                        $"Account {newAccount.AccNo}" +
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
                    if (accountToDelete.Type != 0)
                    {
                        if (accountToDelete.Type != AccountType.Loan && accountToDelete.AccBalance < 0)
                        {
                            Console.WriteLine("Cannot delete: this account is overdrawn." +
                            $"Balance: " +
                            $"{accountToDelete.AccBalance:C}");  
                        }
                        else
                        {
                            Console.WriteLine("Connot delete and account with a non-zero balance. " +
                            $"Current Balance: " +
                            $"{accountToDelete.AccBalance:C}");
                        }
                        break;
                    }

                    Console.WriteLine(
                        $"Are you sure you want to delete account {accountToDelete.AccNo}?" +
                        "yes/no");

                        string? confirmDelete = Console.ReadLine();

                        if(!string.Equals(confirmDelete, "yes", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Deletion cancelled.");
                        break;
                    }

                    deleteTargetCustomer.Accounts.Remove(accountToDelete);

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

                        Console.WriteLine("Account " +
                        $"{accountToEdit.AccNo}" + 
                        $"holder name updated to " +
                        $"{accountToEdit.AccHolderName}.");
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
                            Console.WriteLine("No exsisting accounts to summarize.");
                            continue;
                        }

                        foreach (var acc in c.Accounts)
                        {
                            Console.WriteLine($" {acc.Type}" +
                            $"(#{acc.AccNo}): " +
                            $"{acc.AccBalance:C}");
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
                    string? resetPasswordInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(resetPasswordInput))
                    {
                        Console.WriteLine("The new password cannot be empty.");
                        break;
                    }

                    resetTargetCustomer.Password = resetPasswordInput;

                    Console.WriteLine("Customer password has been reset" +
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

                    if (approveAccount.Type != AccountType.Loan && approveAccount.AccBalance < 0)
                    {
                        Console.WriteLine("Cannot approve: account is overdrawn." +
                        $"Balance:  {approveAccount.AccBalance:C}");
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
                        Console.WriteLine("{i + 1}. Request " +
                        $"{r.RequestId} - " +
                        $"{r.Type} " +
                        $"({r.DateRequested})");
                    }

                    Console.WriteLine("Select a request to approve:");
                    string? requestInput = Console.ReadLine();

                    if(int.TryParse(requestInput, out int requestIndex) && requestIndex >= 1 && requestIndex <= pendingRequests.Count)
                    {
                        pendingRequests[requestIndex - 1].Status = ServiceRequestStatus.Approved;

                        Console.WriteLine("Request Approved.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                    break;
                #endregion
                
                case "7":
                    inAdminMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try agian.");
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