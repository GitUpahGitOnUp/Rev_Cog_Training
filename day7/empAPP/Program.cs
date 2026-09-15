using empAPP.DB;

using var db = new EmployeeManagementNewDbContext();

// var employees = db.Employees.ToList();

// foreach (var emp in employees)
// {
//     Console.WriteLine($"{emp.EmpNo} - {emp.EmpName} - {emp.EmpDesignation} - {emp.EmpSalary}");
// }

// var departments = db.Depts.ToList();

// foreach (var dep in departments)
// {
//     Console.WriteLine($"");
// }
#region search employees and depts
// var myDepts = from e in db.Depts
//                 select e;

// foreach(var item in myDepts)
// {
//     Console.WriteLine("Total Departments : " + 4);
// }

// var allEmps = from e in db.Employees
//                 orderby e.EmpDesignation
//                 select e;
// foreach(var item in allEmps)
// {
//     Console.WriteLine(item.EmpNo + " " + item.EmpName + " " + item.EmpDesignation);
// }

// Console.WriteLine("Enter employee number to view details: ");

// int eno = Convert.ToInt32(Console.ReadLine());

// var edetails = (from e in db.Employees
//                     where e.EmpNo == eno
//                     select e).Single();

// Console.WriteLine(edetails.EmpNo);

// Console.WriteLine(edetails.EmpName);
// Console.WriteLine(edetails.EmpDesignation);
// Console.WriteLine(edetails.EmpSalary);
// Console.WriteLine(edetails.EmpIsPermenant);
#endregion

#region - Add a new Employee

// 1st Create a new employee

// can also change to collect info from user and add emps that way

// Employee newEmp = new Employee()
// {
//     EmpNo = 13, 
//     EmpName = "Dolly Parton",
//     EmpDept = 20,
//     EmpDesignation = "Developer",
//     EmpIsPermenant=true,
//     EmpSalary=65000
// };

// db.Employees.Add(newEmp); // this is added in memory, RAM
// db.SaveChanges(); // push changes made in memory into the database

// Console.WriteLine("New Employee Added Successfully");
#endregion

#region - Delete an employee

// Console.WriteLine("Please enter the employee number for whom you want to delete: ");

// int emp_To_delete = Convert.ToInt32(Console.ReadLine());

// var emp = db.Employees.FirstOrDefault(e => e.EmpNo == emp_To_delete); // no need to select or read the employee, we just need to point to that employee

// // we also have to delete from memory
// if(emp != null)
// {
//     db.Employees.Remove(emp);
//     Console.WriteLine("Employee was deleted successfully");
// }
// else
// {
//     Console.WriteLine("Employee with No : " + emp_To_delete + " Not found in the system");
// }

// db.SaveChanges(); // push changes to the DB

#endregion

#region - Update

// // var emp = db.Employees.FirstOrDefault(e => e.EmpNo == 10);

// // emp.EmpName = "Prof." + emp.EmpName;
// // emp.EmpSalary = 9000;

// // db.SaveChanges();
// // Console.WriteLine("Employee Details Updated");

// #endregion

// #region

// var allemp = from e in db.Employees
//                 select e;
            
// foreach (var item in allemp)
// {
//     item.EmpSalary = item.EmpSalary + 250;
// }

// db.SaveChanges();

// Console.WriteLine("Salary for all employees are updated");

#endregion

#region - Practice: Filtering with Where for High Earners 

// you have using var db = new EmployeeManagementNewDbContext(); up at the top, no olvides que tienes que hacerlo de nuevo 

// var highEarners = db.Employees.Where(e => e.EmpSalary > 8000).ToList();

// foreach( var emp in highEarners)
//     Console.WriteLine($"{emp.EmpName} - {emp.EmpSalary:C}");

#endregion

#region - Practice: Sorting with OrderBy / OrderByDescending

// var bySalaryDesc = db.Employees
//         .OrderByDescending(e => e.EmpSalary)
//         .ToList();
// foreach (var emp in bySalaryDesc)
// Console.WriteLine($"{emp.EmpName} - {emp.EmpSalary:C}");

#endregion

#region - Practice with Select, grab only what ya need
// var names = db.Employees
// .Select(e => e.EmpName)
// .ToList();

// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }

#endregion

#region - Practice Aggregates - Sum, Average, Max, Min, Count

int? totalPayroll = db.Employees.Sum(e => e.EmpSalary); // tried using decimal totalPayroll = ... but it is int?(nullable int) in the Emp scaffold because DB column allows Null, EF Core make the C# property nullable to match

double? avgSalary = db.Employees.Average(e => e.EmpSalary); // double? (== nullable, same as comment above)

int? headcount = db.Employees.Count();
int? topSalary = db.Employees.Max(e => e.EmpSalary);

Console.WriteLine($"Total payroll: {totalPayroll:C}");
Console.WriteLine($"Average Salary: {avgSalary:C}");
Console.WriteLine($"Headcount: {headcount}");
Console.WriteLine($"Top salary: {topSalary:C}");

#endregion  