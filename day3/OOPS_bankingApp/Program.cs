// where we create objects

using OOPS_bankingApp;

Accounts acc1 = new Accounts()
{
    AccNo = 101,
    AccName = "Jane",
    AccountBalance = 1000,
    email = "janesaffliction@email.com",
    isActive = true
};

Console.WriteLine("Account Balance is " + acc1.AccountBalance);
Console.WriteLine("Choose an Operation: 1 for Deposit \n 2 for Withdraw");

int choice = Convert.ToInt32(Console.ReadLine());

switch (choice)
{
    case 1:
        Console.WriteLine("Enter amount you want to deposit: ");
        int depositAmount = Convert.ToInt32(Console.ReadLine());
        acc1.Deposit(depositAmount);
        break;

    case 2:
        Console.WriteLine("Enter amount to Deposit");
        int withdrawAmount = Convert.ToInt32(Console.ReadLine());
        acc1.Withdraw(withdrawAmount);
        break;
}