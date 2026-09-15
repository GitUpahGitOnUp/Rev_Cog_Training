using EmployeeManagement;


#region Hard-coded SavingList

List <SavingsAccount> savList = new List<SavingsAccount>();

savList.Add(new SavingsAccount(){AccNo = 1, AccHolderName = "Octavia Butler", AccBalance = 27500, AccIsActive = true, AccBranchNo = 001});
savList.Add(new SavingsAccount(){AccNo = 2, AccHolderName = "Al Swearengen", AccBalance = 1500000, AccIsActive = true, AccBranchNo = 001});
savList.Add(new SavingsAccount(){AccNo = 3, AccHolderName = "Trixie Star", AccBalance = 250000, AccIsActive = true, AccBranchNo = 001});
savList.Add(new SavingsAccount(){AccNo = 4, AccHolderName = "Sol Star", AccBalance = 450000, AccIsActive = true, AccBranchNo = 001});
savList.Add(new SavingsAccount(){AccNo = 5, AccHolderName = "Alma Ellsworth", AccBalance = 500000000, AccIsActive = true, AccBranchNo = 001});
savList.Add(new SavingsAccount(){AccNo = 6, AccHolderName = "Arnette Hostetler", AccBalance = 1000000, AccIsActive = false, AccBranchNo = 001});
savList.Add(new SavingsAccount(){AccNo = 7, AccHolderName = "Seth Bullock", AccBalance = 2000000, AccIsActive = true, AccBranchNo = 002});
savList.Add(new SavingsAccount(){AccNo = 8, AccHolderName = "Wild Bill Hickock", AccBalance = 500, AccIsActive = false, AccBranchNo = 003});
savList.Add(new SavingsAccount(){AccNo = 9, AccHolderName = "Calamity Jane", AccBalance = 1, AccIsActive = true, AccBranchNo = 003});
savList.Add(new SavingsAccount(){AccNo = 10, AccHolderName = "Joanie Stubbs", AccBalance = 200000, AccIsActive = true, AccBranchNo = 005});
savList.Add(new SavingsAccount(){AccNo = 11, AccHolderName = "Whitney Ellsworth", AccBalance = 230000, AccIsActive = false, AccBranchNo =001});
savList.Add(new SavingsAccount(){AccNo = 12, AccHolderName = "E.B. Farnum", AccBalance = 30000, AccIsActive = true, AccBranchNo = 002});
savList.Add(new SavingsAccount(){AccNo = 13, AccHolderName = "Dan Dority", AccBalance = 200000, AccIsActive = true, AccBranchNo = 006});
savList.Add(new SavingsAccount(){AccNo = 14, AccHolderName = "Charlie Utter", AccBalance = 400000, AccIsActive = true, AccBranchNo = 003});
savList.Add(new SavingsAccount(){AccNo = 15, AccHolderName = "Martha Bullock", AccBalance = 500, AccIsActive = true, AccBranchNo = 004});

#endregion


bool continueMenuSelection = true;

while (continueMenuSelection)
{

    Console.WriteLine("Welcome to your Banking App!");
    Console.WriteLine("");
    Console.WriteLine("------     Main Menu    ------");
    Console.WriteLine("");
    Console.WriteLine("Enter 'A' to Add a New Account");
    Console.WriteLine("");
    Console.WriteLine("Enter 'B' to View Account Details");
    Console.WriteLine("");
    Console.WriteLine("Enter 'C' to Make a Withdrawal");
    Console.WriteLine("");
    Console.WriteLine("Enter 'D' to Make a Deposit");
    Console.WriteLine("");
    Console.WriteLine("Enter 'E' to Transfer Funds to a Different Account");
    Console.WriteLine("");
    Console.WriteLine("Enter 'F' to See Your Summary For ALl Accounts");
    Console.WriteLine("");
    Console.WriteLine("Enter 'G' to Exit");
    Console.WriteLine("");

    string choice = Console.ReadLine().ToLower();

    SavingsAccount savAccSelected = null; // logic to hold which acc. info is being called
    SavingsAccount savAccSelected2 = null; // logic to hold second account for making transfers

    int searchAccNo = 0; // declare here so I can reuse in diff. switch cases
    int searchAccNo2 = 0; // also for making transfers

    switch (choice)
    {
        
        case "a":
            // asks user for Acc. Details to create new account + saves obj to List SavingsList
            SavingsAccount savAcc = new SavingsAccount();
            
            int newAccNo = savList.LastOrDefault().AccNo + 1; // looks at the last obj.'s AccNo and increments by 1 to keep them unique. Works becuase I hard coded them in order
            Console.WriteLine("Your Account Number is: " + newAccNo);
            savAcc.AccNo = newAccNo;
            Console.WriteLine("");
            Console.WriteLine("Please Enter Your First and Last Name:   ");
            string accName = Console.ReadLine();
            savAcc.AccHolderName = accName;

            Console.WriteLine("");
            Console.WriteLine("Please Enter Your Starting Balance (Without Commas, please):     ");
            double accBalanceStart = Convert.ToDouble(Console.ReadLine());
            savAcc.AccBalance = accBalanceStart;

            Console.WriteLine("");
            savAcc.AccIsActive = true;
            Console.WriteLine("Your Account is Now Active!");

            Console.WriteLine("");
            Console.WriteLine("Please Select Your Desired Home Branch: either 001, 002, 003, or 004.");
            Console.WriteLine("");
            int setBranchNo = Convert.ToInt32(Console.ReadLine());
            savAcc.AccBranchNo = setBranchNo;

            savList.Add(savAcc); 
            break;
        
        case "b":
            // allows user to view Acc. Details by AccNo lookup
            
            Console.WriteLine("Please Enter the Account Number of the Account You Wish to View: ");
            Console.WriteLine("");
            searchAccNo = Convert.ToInt32(Console.ReadLine());
            savAccSelected = savList.FirstOrDefault(acc => acc.AccNo == searchAccNo);
            // verify that AccNo exists / not null
            if (savAccSelected == null)
            {
                Console.WriteLine("No account matches the account number you entered. Please verify the number and try again.");
            }
            else
            {
                Console.WriteLine($"Account Number: {savAccSelected.AccNo}");
                Console.WriteLine("");
                Console.WriteLine($"Name on Account:    {savAccSelected.AccHolderName}");
                Console.WriteLine("");
                Console.WriteLine($"Account Balance:     {savAccSelected.AccBalance:C}");
                Console.WriteLine("");
                Console.WriteLine($"Account Status:     {savAccSelected.AccIsActive}");
                Console.WriteLine("");
                Console.WriteLine($"Account Home Branch:    {savAccSelected.AccBranchNo}");
                Console.WriteLine("");
            }
            break;
        
        case "c":
            // allows user to make a withdrawal
            Console.WriteLine("To Make a Withdrawal, Please Enter the Account Number:   ");
            Console.WriteLine("");
            searchAccNo = Convert.ToInt32(Console.ReadLine());
            savAccSelected = savList.FirstOrDefault(acc => acc.AccNo == searchAccNo);
            // verify AccNo exists
            if (savAccSelected == null)
            {
                Console.WriteLine("No account matches the account number you entered. Please verify the number and try again.");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Your Starting Balance Was:  {savAccSelected.CheckBalance():C}");
                Console.WriteLine("");
                Console.WriteLine("Please Enter the Amount (Greater Than $100) You Would Like to Withdraw:      ");
                Console.WriteLine("");
                int withdrawalAmt = Convert.ToInt32(Console.ReadLine());
                savAccSelected.Withdraw(withdrawalAmt);

                Console.WriteLine("");
                Console.WriteLine($"Your Current Balance is:    {savAccSelected.CheckBalance():C}");

            }
            break;
        
        case "d":
            // allows user to make a deposit
            Console.WriteLine("To Make a Deposit, Please Enter Your Account Number:     ");
            searchAccNo = Convert.ToInt32(Console.ReadLine());
            savAccSelected = savList.FirstOrDefault(acc => acc.AccNo == searchAccNo);
            // verify AccNo exists
            if (savAccSelected == null)
            {
                Console.WriteLine("No account matches the account number you entered. Please verify the number and try again.");
            }
            else
            {
                Console.WriteLine($"Your Starting Balance Today Was:    {savAccSelected.CheckBalance():C}");
                Console.WriteLine("");
                Console.WriteLine("Please Enter the Amount You Would Like to Deposit:       ");
                Console.WriteLine("");
                int depositAmt = Convert.ToInt32(Console.ReadLine());
                savAccSelected.Deposit(depositAmt);
                Console.WriteLine("");
                Console.WriteLine($"Your Current Balance is:    {savAccSelected.CheckBalance():C}");
            }
            break;
        
        case "e":
            // allows user to tranfer $$ from 1 acc => another
            Console.WriteLine("To Start a Transfer, Please Enter the Account Number You Are Transferring *From*:    ");
            searchAccNo = Convert.ToInt32(Console.ReadLine());
            savAccSelected = savList.FirstOrDefault(acc => acc.AccNo == searchAccNo);
            
            Console.WriteLine("");
            Console.WriteLine("Please Enter the Account Number You Are Transferring *To*:   ");
            searchAccNo2 = Convert.ToInt32(Console.ReadLine());
            savAccSelected2 = savList.FirstOrDefault(acc => acc.AccNo == searchAccNo2);

            if(savAccSelected == null || savAccSelected2 == null)
            {
                Console.WriteLine("Those account numbers do not match existing account records. Please verify the numbers and try again.");
            }
            else
            {
                Console.WriteLine("Please Enter the Amount You Would Like Transfer:   ");
                int transferAmt = Convert.ToInt32(Console.ReadLine());
                
                savAccSelected.Withdraw(transferAmt); // withdrws transfer amount from transferer's acc
                savAccSelected2.Deposit(transferAmt); // deposits transfer amout to transferee's acc

                Console.WriteLine("");
                Console.WriteLine($"The Account Balance of the Transferer:  {savAccSelected.CheckBalance():C}");
                Console.WriteLine("");
                Console.WriteLine($"The Account Balance of the Transferee:  {savAccSelected2.CheckBalance():C}");
                Console.WriteLine(""); 
            }
            break;
        
        case "f":

            bool inSummarySubMenu = true;
            while (inSummarySubMenu)
            {
                Console.WriteLine("Enter 'A' to See The Total Number of Accounts.");
                Console.WriteLine("");
                Console.WriteLine("Enter 'B' to See The Total Balance For All Acccounts at This Bank.");
                Console.WriteLine("");
                Console.WriteLine("Enter 'C' to See The Total Number of Active Accounts.");
                Console.WriteLine("");
                Console.WriteLine("Enter 'D' to See The Total Number of Inactive Accounts: ");
                Console.WriteLine("");
                Console.WriteLine("Enter 'E' to return to the Main Menu.");
                Console.WriteLine("");

                string choice2 = Console.ReadLine().ToLower();

                switch (choice2)
                {
                    case "a":
                        // view total num of accounts
                        int accTotal = 0;

                        foreach (var item in savList)
                        {
                            accTotal++;
                        }
                        Console.WriteLine($"The Total Number of Accounts at This Bank: {accTotal}");
                        Console.WriteLine("");
                        break;
                    
                    case "b":
                    // view total balance for all accounts
                        double accBalanceTotal = 0.0;

                        foreach (var acc in savList)
                        {
                            accBalanceTotal += acc.AccBalance;
                        }

                        Console.WriteLine($"Total Balance for All Accounts:    {accBalanceTotal:C}");
                        Console.WriteLine("");            
                        break;
                    
                    case "c":
                    // total num of active accounts at bank
                        int totalActiveAcc = 0;

                        foreach (var item in savList)
                        {
                            if (item.AccIsActive)
                            {
                                totalActiveAcc++;
                            }
                        }
                        Console.WriteLine($"Totl Number of Active Accounts: {totalActiveAcc}");
                        Console.WriteLine("");
                        break;
                    
                    case "d":
                        int totalInActiveAcc = 0;

                        foreach (var item in savList)
                        {
                            if (!item.AccIsActive)
                            {
                                totalInActiveAcc++;
                            }
                        }
                        Console.WriteLine($"Total Number of Inactive Accounts:     {totalInActiveAcc}");
                        Console.WriteLine("");
                        break;
                    
                    case "e":
                        inSummarySubMenu = false;
                        break;
                }
            }
        break;
        case "g":
            continueMenuSelection = false;
            break;
    }
}