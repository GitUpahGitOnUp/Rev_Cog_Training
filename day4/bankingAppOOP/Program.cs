using Banking;
using System.IO;
using System;
using System.Collections;

#region Account objs
// Checking chk = new Checking()
// {
//     AccNo = 1234,
//     AccBalance = 11200.00,
//     AccHolderName = "Grace Hopper",
//     AccType = TypeOfAccount.Checking,
//     AccIsActive = true

// };

// Savings sav = new Savings()
// {
//     AccNo = 5678,
//     AccBalance = 33600.00,
//     AccHolderName = "Grace Hopper",
//     AccType = TypeOfAccount.Savings,
//     AccIsActive = true
// };

// Loan ln = new Loan()
// {
//     AccNo = 9101112,
//     AccBalance = 0.0,
//     AccHolderName = "Grace Hopper",
//     AccType = TypeOfAccount.Loan,
//     AccIsActive = false
// };
#endregion

bool continuteMenuSelection = true;

while (continuteMenuSelection)
{
    Console.WriteLine("Welcome to your Banking App!");
    Console.WriteLine("----- Menu ------");
    Console.WriteLine("Enter 'A' to create a new account.");
    Console.WriteLine("");
    Console.WriteLine("Enter 'B' to check your balance.");
    Console.WriteLine("");
    Console.WriteLine("Enter 'C' to exit.");

    
    string choice = Console.ReadLine().ToLower();

    

    switch (choice)
    {
        case "a":
            bool inSubMenuA = true;
            while (inSubMenuA)
            {
                Console.WriteLine("Enter 'A' to create a new Savings acount.");
                Console.WriteLine("Enter 'B' to create a new Checking account.");
                Console.WriteLine("Enter 'C' to apply for a new Loan.");
                Console.WriteLine("Enter 'D' to return to the Main Menu.");

                

                Accounts accSelected = null;

                string choice2 = Console.ReadLine().ToLower();
                #region switch 2
                switch (choice2)
                {
                    case "a":
                        
                        Savings sav = new Savings();
                        Console.WriteLine("Please enter your last name: ");
                        string accName = Console.ReadLine();
                        string accFile = accName + sav.AccType + ".txt";
                        Console.WriteLine($"AccType is: {sav.AccType}");
                        string filePath = accFile;

                        using (StreamWriter writer = new StreamWriter(accFile))
                        {
                            Console.WriteLine("Please enter your first and last name :");
                            string firstLastName = Console.ReadLine();
                            writer.WriteLine("Account Holder: " + firstLastName);

                            writer.WriteLine("Account Type: " + sav.AccType);
                            writer.WriteLine("Account Number: " + 1234);

                            Console.WriteLine("How much would you like to make for your first deposit?");
                            string initBalance = Console.ReadLine();
                            writer.WriteLine("Initial Balance: " + initBalance);

                            Console.WriteLine("Would you like to activate your account today?");
                            string activateAcc = Console.ReadLine();
                            writer.WriteLine("Account is Active? " + sav.AccIsActive);

                        }
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Savings account file created successfully.");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        break;
                        
                    

                    case "b":
                        Checking chck = new Checking();
                        Console.WriteLine("Please enter your last name: ");
                        string accNameChecking = Console.ReadLine();
                        string accFileChecking = accNameChecking + chck.AccType + ".txt";
                        string filePathChecking = accFileChecking;

                        using (StreamWriter writer = new StreamWriter(accFileChecking))
                        {
                            Console.WriteLine("Please enter your first and last name :");
                            string firstLastName = Console.ReadLine();
                            writer.WriteLine("Account Holder: " + firstLastName);

                            writer.WriteLine("Account Type: " + chck.AccType);
                            writer.WriteLine("Account Number: " + 1234);

                            Console.WriteLine("How much would you like to make for your first deposit?");
                            string initBalance = Console.ReadLine();
                            writer.WriteLine("Initial Balance: " + initBalance);

                            Console.WriteLine("Would you like to activate your account today?");
                            string activateAcc = Console.ReadLine();
                            writer.WriteLine("Account is Active? " + chck.AccIsActive);

                        }
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Checking account file created successfully.");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        break;
                        
                    
                    case "c":
                        Loan ln = new Loan();
                        Console.WriteLine("Please enter your last name: ");
                        string accNameLoan = Console.ReadLine();
                        string accFileLoan = accNameLoan + ln.AccType + ".txt";
                        string filePathLoan = accFileLoan;

                        using (StreamWriter writer = new StreamWriter(accFileLoan))
                        {
                            Console.WriteLine("Please enter your first and last name :");
                            string firstLastName = Console.ReadLine();
                            writer.WriteLine("Account Holder: " + firstLastName);

                            writer.WriteLine("Account Type: " + ln.AccType);
                            writer.WriteLine("Account Number: " + 1234);

                            Console.WriteLine("What loan ammount would you like to apply for?");
                            string loanAmount = Console.ReadLine();
                            writer.WriteLine("Initial Balance: " + loanAmount);

                            Console.WriteLine("Would you like to apply today? (Activate Loan Account)?");
                            string activateAcc = Console.ReadLine();
                            writer.WriteLine("Account is Active? " + ln.AccIsActive);

                        }
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Loan account file created successfully.");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        break;
                        
                    case "d":
                        inSubMenuA = false;
                        break;
                    }
                    #endregion
            
                }
        
            break;

        case "b":
            Console.WriteLine("Please enter your last name and account type together i.e. LastnameChecking");
            string accessFile = Console.ReadLine();
            string findFile = accessFile + ".txt";
            Console.WriteLine($"Looking for: {Path.GetFullPath(findFile)}");
            

            if (File.Exists(findFile))
            {
                bool lineFound = false;
                string[] acctInfo = File.ReadAllLines(findFile);

                foreach (string line in acctInfo)
                {
                    if(line.Contains("Initial Balance: "))
                    {
                        Console.WriteLine(line);
                        lineFound = true;
                
                    }
                }
            }

            break;
        
        case "c":

            continuteMenuSelection = false;
            break;
                        
    }
}