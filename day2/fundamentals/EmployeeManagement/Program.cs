#region  Task 
// Create a new console application - EmployeeManagement
// cw(''''welcome message");

// upon starting the ap, user will see 3 options
// A. Admin
// B. Employee
// C. Guest

// when 'A' is selected, display:
    // a. Create new employee
    // b. Change employee details
    // c. Announce Activity
    // d. Delete Employee
    // e. View All Employees
    // f. Back to previous menu
    // g. exit
// when b is selected display
   // a. view my details
   // b. Apply leave
   // c. Submit reimbursement
   // d. view project details
   // e. view today's task and activities
   // f. Previous Menu
   // g. Exit
// When 'C' is selected
    // a. About organization
    // b. View Open Positions
    // c. Contact Information
    // e. Previous menu
    // g. Exit
// Validate the user

//  when user chooses 'a', ask to enter username and password
// if username: revadmin
//    password: revadmin$123#
// display the menu else display invalid credentials, press enter to continue and display 
// the main menu

// user chooses 'b' ask for username and pwd
//  username : revemp2409
// password : revadmin$123#emp
#endregion

bool continueMenuSelection = true;

while (continueMenuSelection)
{
System.Console.WriteLine("Select one of the lettered options below: ");
System.Console.WriteLine("Enter 'a' for Admin");
System.Console.WriteLine("Enter 'b' for Employee");
System.Console.WriteLine("Enter 'c' for Guest");
System.Console.WriteLine("Enter 'd' to Exit");
    

string userChoice = System.Console.ReadLine().ToLower();
switch (userChoice)
{
    case "a":
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();
        if (username == "revadmin" && password == "revadmin$123#")
            {
                Console.WriteLine(" 'a' Create new employee");
                Console.WriteLine(" 'b' Change employee details");
                Console.WriteLine(" 'c' Announce activity ");
                Console.WriteLine(" 'd' Delete employee");
                Console.WriteLine(" 'e' View all employees");
                Console.ReadKey();
                
            }
            else
            {
                Console.WriteLine("Invalid credentials");
            }

        break;

    case "b":
        Console.Write("Enter username: ");
        string username_emp = Console.ReadLine();

        Console.Write("Enter password: ");
        string password_emp = Console.ReadLine();
        if (username_emp == "revemp2409" && password_emp == "revadmin$123#emp")
            {
                Console.ReadKey();
                Console.WriteLine("'a' View my details");
                Console.WriteLine("'b' Apply leave");
                Console.WriteLine("'c' View my details");
                Console.WriteLine("'d' View Project details");
                Console.WriteLine("'e' View today's activities");
                Console.WriteLine("'f' View Previous Menu");
                Console.WriteLine("'g' Exit");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
        break;

    case "c":
        Console.WriteLine("'a' About the organization");
        Console.WriteLine("'b' View open positions");
        Console.WriteLine("'c' Contact information");
        Console.WriteLine("'d' Previous menu");
        Console.WriteLine("'e' Exit");
        break;
    
    case "d":
        System.Console.WriteLine("Thank you for visiting, goodbye!");
        continueMenuSelection = false;
        break;
    default:
        System.Console.WriteLine("Invalid choice. Please try again.");
        break;

}
//Console.WriteLine("Press any key to continue, or 0 to exit.");
//Console.ReadKey();
}