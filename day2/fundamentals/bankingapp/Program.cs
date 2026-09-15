System.Console.WriteLine("!~~~~~~~~~~~~~~~~ Bank of America ~~~~~~~~~~!");
System.Console.WriteLine("1. Create Account");
System.Console.WriteLine("2. Check Balance");
System.Console.WriteLine("3. Withdraw Funds");
System.Console.WriteLine("4. Deposit Funds");
System.Console.WriteLine("5. Transfer Funds");
System.Console.WriteLine("6. View Transaction History");
System.Console.WriteLine("7. Change ATM Pin Number");
System.Console.WriteLine("8. Request Loan");
System.Console.WriteLine("9. Exit");

bool continueTransaction = true;

while (continueTransaction)
{
    System.Console.WriteLine("1. Create Account");
    System.Console.WriteLine("2. Check Balance");
    System.Console.WriteLine("3. Withdraw Funds");
    System.Console.WriteLine("4. Deposit Funds");
    System.Console.WriteLine("5. Transfer Funds");
    System.Console.WriteLine("6. View Transaction History");
    System.Console.WriteLine("7. Change ATM PIN");
    System.Console.WriteLine("8. Request Loan");
    System.Console.WriteLine("9. Exit");
    

int userChoice = Convert.ToInt32(System.Console.ReadLine());
switch (userChoice)
{
    case 1:
        Console.WriteLine("Create new accounts, will collect details and proccess acount creation.");
        break;
    case 2:
        Console.WriteLine("Check your balance.");
        break;
    case 3:
        Console.WriteLine("Withdraw funds.");
        break;
    case 4:
        Console.WriteLine("Deposit Funds.");
        break;
    case 5:
        Console.WriteLine("Transfer Funds.");
        break;
    case 6:
        Console.WriteLine("View Transaction History.");
        break;
    case 7:
        Console.WriteLine("Change ATM PIN.");
        break;
    case 8:
        Console.WriteLine("Request a Loan.");
        break;
    case 9:
        Console.WriteLine("Exit.");
        break;

}

Console.WriteLine("Press any key to continue, or 0 to exit.");
Console.ReadKey();
}
