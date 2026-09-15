using System.IO.Compression;
using EmpManagement;

// Employee empObj = new Employee()
// {
//     empNo = 101,
//     empName = "Mike",
//     empAvailableLeave = 30,
//     empIsActive = true,
//     empPassword = "IncorrectPassword"
// };

Employee empObj = Employee.LoadObject();

bool continueWork = true;

while(continueWork)
{
    Console.WriteLine("Employee Details ");
    Console.WriteLine("Employee Number: " + empObj.empNo);
    Console.WriteLine("Employee Name: " + empObj.empName);
    Console.WriteLine("Employee Salary: " + empObj.empSalary);
    Console.WriteLine("Employ Remain Leave in Days: " + empObj.empAvailableLeave);
    Console.WriteLine("Employee is Active ?   : " + empObj.empIsActive);

    Console.WriteLine("-------------------------------------");
    Console.WriteLine("Please select from the option:   ");
    Console.WriteLine("1. Apply Leave:  ");
    Console.WriteLine("2. Apprise Salary: ");
    Console.WriteLine("3. Edit First name");
    Console.WriteLine("4. To Exit");

    int choice;
    choice = Convert.ToInt32(Console.ReadLine());

    switch(choice)
    {
        case 1:
            Console.WriteLine("Enter the number of days for leave:  ");
            int leave = Convert.ToInt32(Console.ReadLine());
            empObj.ApplyLeave(leave);
            Console.WriteLine("Leave approved.");
            break;
        
        case 2:
            empObj.AppriseSalary();
            Console.WriteLine("Congratulations!");
            break;
        
        case 3:
            Console.WriteLine("Enter New Name:   ");
            string newName = Console.ReadLine() ?? "";
            empObj.empName = newName;
            Console.WriteLine("Name changed.");
            break;
        
        case 4:
            continueWork = false;
            Console.WriteLine("Thank you, see you next time!");
            Console.WriteLine(empObj.SaveObject());
            break;
        
        default:
            Console.WriteLine("Sorry, please choose a correct option.");
            break; 
        
    }
}