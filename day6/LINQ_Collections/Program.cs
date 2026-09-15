using System.Runtime.CompilerServices;
using EmployeeManagement;

#region Hardcoded Employee List

List <Employee> eList = new List<Employee>()

{
    new Employee(){empNo=101, empName="Bob Marley", empDepartmentNo = 10, empIsPermanent=true, empSalary=100000},
    new Employee(){empNo=102, empName="Janis Joplin", empDepartmentNo = 10, empIsPermanent=true, empSalary=200000},
    new Employee(){empNo=103, empName="Betty Draper", empDepartmentNo = 10, empIsPermanent=false, empSalary=1000000},
    new Employee(){empNo=104, empName="Nina Simone", empDepartmentNo = 20, empIsPermanent=true, empSalary=500000},
    new Employee(){empNo=105, empName="Sam Cooke", empDepartmentNo = 10, empIsPermanent=false, empSalary=600000},
    new Employee(){empNo=106, empName="Tony Soprano", empDepartmentNo = 40, empIsPermanent=true, empSalary=70000},
    new Employee(){empNo=107, empName="David Lynch", empDepartmentNo = 10, empIsPermanent=true, empSalary=200000},
    new Employee(){empNo=108, empName="Kendrick Lamar", empDepartmentNo = 10, empIsPermanent=false, empSalary=2570000},
    new Employee(){empNo=109, empName="Isaiah Rashad", empDepartmentNo = 40, empIsPermanent=true, empSalary=20000000},
    new Employee(){empNo=110, empName="Jello Biafra", empDepartmentNo = 20, empIsPermanent=true, empSalary=5000500},
    new Employee(){empNo=111, empName="Glen Danzig", empDepartmentNo = 10, empIsPermanent=true, empSalary=8000500},
    new Employee(){empNo=112, empName="Parker Posey", empDepartmentNo = 30, empIsPermanent=true, empSalary=5000500},
    new Employee(){empNo=113, empName="John Steinbeck", empDepartmentNo = 10, empIsPermanent=false, empSalary=600000},
    new Employee(){empNo=114, empName="Eddie Izzard", empDepartmentNo = 40, empIsPermanent=true, empSalary=100000000},
    new Employee(){empNo=115, empName="Gary Oldman", empDepartmentNo = 30, empIsPermanent=true, empSalary=35200000},
    new Employee(){empNo=116, empName="Dolly Parton", empDepartmentNo = 20, empIsPermanent=true, empSalary=900000000},
    new Employee(){empNo=117, empName="Chappel Roan", empDepartmentNo = 20, empIsPermanent=true, empSalary=5000000},
    new Employee(){empNo=118, empName="Stevie Nicks", empDepartmentNo = 20, empIsPermanent=true, empSalary=6000000},
    new Employee(){empNo=119, empName="Shelley Duvall", empDepartmentNo = 20, empIsPermanent=true, empSalary=5000200},
    new Employee(){empNo=120, empName="Lemy Kilmister", empDepartmentNo = 20, empIsPermanent=true, empSalary=500600},
    new Employee(){empNo=121, empName="Wendy O Williams", empDepartmentNo = 20, empIsPermanent=true, empSalary=6000000}
};

#endregion

// LINQ

// LINQ starts with var keyword on the Left, is is a KEYWORD, not a variable
// LINQ is designed to look like SQL lang, but can't be exactly the same because of ISSO Standards

#region 1 - Select all the data
// this is like SELECT * from source (in this ex. source = eList, but could be DB table, or folder, or file, etc.)
//             // sort syntax
//             // filter syntax
//             // group by syntax
//             // calculations
//             // conditions

// var emp = from e in eList 

//             select e;

//     foreach (var item in emp)
//     {
//         Console.WriteLine(item.empName);
//     }
#endregion

#region 2 - Select Employee with empNo > 115

// var emp = from e in eList

//             where e.empNo > 115
//             select e;

// foreach(var item in emp)
// {
//     Console.WriteLine(item.empNo + " " + item.empName);
// }

#endregion

#region 3 - Select Employees with Permanent Position

// var emp = from e in eList
//             where e.empIsPermanent == true
//             select e;

// foreach (var item in emp)
// {
//     Console.WriteLine(item.empNo + " " + item.empName + " is permanent.");
// }

#endregion

#region  4 - Select employee where empNo deptNo = 20 and salary > 12000

// var emp = from e in eList
//             where e.empDepartmentNo == 20 && e.empSalary > 20000
//             select e;

// foreach (var item in emp)
// {
//     Console.WriteLine(item.empNo + " " + item.empDepartmentNo + " " + item.empSalary);
// }

#endregion

#region 5 - Select employees whose name starts with a particular letter


// var emp = from e in eList
//             where e.empName.StartsWith("N")
//             select e;

// foreach(var item in emp)
// {
//     Console.WriteLine(item.empName);
// }

#endregion

#region 6 - sort the data --defaults to ascending order

// var emp = from e in eList
//             where e.empDepartmentNo > 20
//             orderby e.empName descending         // explicitly changed to descending order here
//             select e;

// foreach(var item in emp)
// {
//     Console.WriteLine(item.empNo + " " + item.empName);
// }
#endregion

#region 7 - filter and sort together

// var emp = from e in eList
//             where e.empDepartmentNo > 20
//             orderby e.empDepartmentNo
//             select e;

// foreach(var item in emp)
// {
//     Console.WriteLine(item.empName + " " + item.empDepartmentNo);
// }
#endregion

#region 8 - give total employees based on some 

// var totalemp = (from e in eList
//                  where e.empIsPermanent && e.empDepartmentNo == 20
//                  select e.empNo).Count();

// Console.WriteLine(totalemp);
#endregion

#region 9 - what is the minimum salary

// var maxPay = (from e in eList
//             select e.empSalary).Max();

// Console.WriteLine(maxPay);

#endregion

#region 10 - Calculations

// var calculated = from e in eList
//                     select new
//                     {
//                         Monthly_Salary = e.empSalary.ToString("C"),
//                         Annual_Salary = e.empSalary * 12,
//                         Bonus = e.empSalary  * 0.2,
//                         Allowance = 200 
//                     };

// foreach(var item in calculated)
// {
//     Console.WriteLine(" ");
//     Console.WriteLine("Monthly Salary:  " + item.Monthly_Salary);
//     Console.WriteLine("Annual Salary:   " + (item.Annual_Salary).ToString("C"));
//     Console.WriteLine("Bonus:   " + (item.Bonus).ToString("C"));
//     Console.WriteLine("Allowance:   " + (item.Allowance).ToString("C"));
//     Console.WriteLine(" ");
//     Console.WriteLine("------------------------------------");
// }

#endregion

#region 11 - Group By

// var employmentSummary = eList.GroupBy(e => e.empDepartmentNo);

// foreach(var item in employmentSummary)
// {
//     Console.WriteLine(item.Key + " : " + item.Count()); // this will print unique department numbers + count of num of emps
//     foreach(var f in item)
//     {
//         Console.WriteLine(f.empName + " " + f.empNo);
//     }
//     Console.WriteLine("--------------------------------------");
// }

#endregion

#region  what is the maximum?
//  var minPay = (from e in eList
//                 select e.empSalary).Min();

// Console.WriteLine(minPay);

#endregion

#region  what is the total num of employees in deptNo 20?

// var empDepTotal = (from e in eList
//                     where e.empDepartmentNo == 20
//                     select e).Count();

// Console.WriteLine(empDepTotal);

#endregion


#region  what is the average salary paid to non-permanent employees

var empNonSalaryTotalEmps = (from e in eList
                    where e.empIsPermanent == false
                    select e.empIsPermanent).Count();

var empNonSalaryTotal = (from e in eList
                          where e.empIsPermanent == false
                          select e.empSalary).Sum();

var empNonSalaryCalcs = from e in eList
                            where e.empIsPermanent == false
                            select new
                            {
                                Average_NonPermanentSalary = (empNonSalaryTotal/ empNonSalaryTotalEmps).ToString("C")
                            };

Console.WriteLine(empNonSalaryCalcs);

#endregion

// what is average pay in deptNo 30?

// employees having "ar" in the middle of their name (anywhere)


// employees having "as" second character in their name (use google to help with this one)