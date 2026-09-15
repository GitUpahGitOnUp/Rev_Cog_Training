using bankLIB;

bool continueMenuSelection = true;

while(continueMenuSelection)
{
    Console.WriteLine("-------- Bank Menu   --------");
    Console.WriteLine("To Log in as a Customer, Please Enter 'A':    ");
    Console.WriteLine("To Log in as an Admin, Please Enter 'B':    ");
    Console.WriteLine("To Exit the Application, Please Enter 'X':    ");

    string choice = Console.ReadLine().ToLower();

    switch (choice)
    {
        case"a":
            Console.WriteLine("Please enter your username:  ");
            Console.ReadLine();

            Console.WriteLine("Please enter your password");
            Console.ReadLine();

            // if (logic/mechanism to validate credentials){}
            
            // need to grap customer obj instance, something like:
            //var user = db.Users.FirstOrDefault(u => u.Username == enteredUsername);


            string choiceCustomer = Console.ReadLine().ToLower();
            bool inCustomerMenu = true;
            while (inCustomerMenu)
            {
                switch (choiceCustomer)
                {
                    case"a":            // Account Details

                        Console.WriteLine($"Account Number:     ");
                        Console.WriteLine($"Account Holder Name:    ");
                        Console.WriteLine($"Account Balance");
                        Console.WriteLine($"");
                        Console.WriteLine($"");
                        break;

                    case"b":            // Withdraw
                        Console.WriteLine("To Make a Withdrawal, Please Enter the Account Number:   ");
                        Console.WriteLine("");

                    //     Console.WriteLine("");
                    //     Console.WriteLine($"Your Starting Balance Was:  {user.CheckBalance():C}");
                    //     Console.WriteLine("");
                    //     Console.WriteLine("Please Enter the Amount You Would Like to Withdraw:      ");
                    //     Console.WriteLine("");
                    //     int withdrawalAmt = Convert.ToInt32(Console.ReadLine());
                    //     user.Withdraw(withdrawalAmt);

                    //     Console.WriteLine("");
                    //     Console.WriteLine($"Your Current Balance is:    {user.CheckBalance():C}");

                        break;

                    case"c":            // Deposit
                    // allows user to make a deposit

                    //     Console.WriteLine($"Your Starting Balance Today Was:    {user.CheckBalance():C}");
                    //     Console.WriteLine("");
                    //     Console.WriteLine("Please Enter the Amount You Would Like to Deposit:       ");
                    //     Console.WriteLine("");
                    //     int depositAmt = Convert.ToInt32(Console.ReadLine());
                    //     savAccSelected.Deposit(depositAmt);
                    //     Console.WriteLine("");
                    //     Console.WriteLine($"Your Current Balance is:    {user.CheckBalance():C}");
                        

                        break;
                    
                    case"d":            // Transfer
                        // Console.WriteLine("To Start a Transfer, Please Enter the Account Number You Are Transferring *From*:    ");
                        // searchAccNo = Convert.ToInt32(Console.ReadLine());
                        // AccSelected = user.FirstOrDefault(acc => acc.AccNo == searchAccNo);
                        
                        // Console.WriteLine("");
                        // Console.WriteLine("Please Enter the Account Number You Are Transferring *To*:   ");
                        // searchAccNo2 = Convert.ToInt32(Console.ReadLine());
                        // AccSelected2 = user.FirstOrDefault(acc => acc.AccNo == searchAccNo2);

                        // if(AccSelected == null || AccSelected2 == null)
                        // {
                        //     Console.WriteLine("Those account numbers do not match existing account records. Please verify the numbers and try again.");
                        // }
                        // else
                        // {
                        //     Console.WriteLine("Please Enter the Amount You Would Like Transfer:   ");
                        //     int transferAmt = Convert.ToInt32(Console.ReadLine());
                            
                        //     AccSelected.Withdraw(transferAmt); // withdrws transfer amount from transferer's acc
                        //     AccSelected2.Deposit(transferAmt); // deposits transfer amout to transferee's acc

                        //     Console.WriteLine("");
                        //     Console.WriteLine($"The Account Balance of the Transferer:  {AccSelected.CheckBalance():C}");
                        //     Console.WriteLine("");
                        //     Console.WriteLine($"The Account Balance of the Transferee:  {AccSelected2.CheckBalance():C}");
                        //     Console.WriteLine(""); 
                        // }
                        break;

                    case"e":            // Last 5 Transactions

                        
                        break;
                    
                    case"f":            // Request Check Book

                        break;

                    case"g":            // Change Password

                        break;

                    case"h":        // Exit and Return to Main Menu

                        inCustomerMenu = false;
                        break;
                }
            }
            break;
        
        case"b":
            break;

        case"x":

            break;

    }
}