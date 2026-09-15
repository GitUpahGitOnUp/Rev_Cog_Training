using System.Numerics;
using employeeManagementSystem;

Employee emp1 = new Employee()
{
  EmpFirstName = "Janis",
  EmpLastName = "Joplin",
  EmpEmail = "",
  EmpSSN = 715858797,  
  EmpSalary = 69500.00,
  EmpBonus = 0,
  EmpBenefits = -300,
  TotalCompensation = 69200.00,
  IsCurrentEmp = true,
  HireDate = 11012025
};


Console.WriteLine("Enter '1' to view your employee email and '2' to view your total compensation as of this date. Enter 3 to exit.");

int choice = Convert.ToInt32(Console.ReadLine());

switch (choice)
{
    case 1:
        // Console.WriteLine("Please enter your first name");
        // string firstName = Console.ReadLine();
        // Console.WriteLine("Please enter your last name.");
        // string lastName = Console.ReadLine();
        // Console.WriteLine("Please enter the date you were hired in MMDDYYYY format.");
        // int date_hired = Convert.ToInt32(Console.ReadLine());
        string firstName = emp1.EmpFirstName;
        string lastName = emp1.EmpLastName;
        int date_hired = emp1.HireDate;

        emp1.CreateEmpEmail(firstName, lastName, date_hired);
        Console.WriteLine("Your employee email is: " + emp1.EmpEmail);
        break;
    
    case 2:

        double show_salary = emp1.EmpSalary;
        double show_bonus = emp1.EmpBonus;
        double show_benefits = emp1.EmpBenefits;

        emp1.CalcTotalCompensation(show_salary, show_benefits, show_bonus);
        Console.WriteLine("Your total compensation as of this date is " + emp1.TotalCompensation);
        break;

    case 3:
        
        break;
}


