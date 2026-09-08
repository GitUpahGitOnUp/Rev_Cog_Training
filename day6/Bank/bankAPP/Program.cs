using bankLIB;

Accounts acc = new Accounts();
acc.accBalance = 5000;
acc.accName = "Ang";
acc.accNo = 101;

Console.WriteLine("Your Account Balance is:    " + acc.checkBalance());
Console.WriteLine("After Withdrawal:   " + acc.Withdraw(800));
Console.WriteLine("After Deposit:  " + acc.Deposit(12000));
Console.WriteLine("");
Console.WriteLine("Your New Balance is: " + acc.checkBalance());
