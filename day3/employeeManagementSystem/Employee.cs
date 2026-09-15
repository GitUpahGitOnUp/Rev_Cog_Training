using System.Dynamic;
using System.Reflection;

namespace employeeManagementSystem
{
#region Employee Class Properties
    public class Employee
    {
        public string EmpFirstName {get; set;} = "";
        public string EmpLastName {get; set;} = "";
        public string EmpEmail {get; set;} = "";
        public int EmpSSN {get; set;}
        public double EmpSalary {get; set;}
        public double EmpBonus {get; set;}
        public double EmpBenefits {get; set;}
        public double TotalCompensation {get; set;}
        public bool IsCurrentEmp {get; set;} = true;
        public int HireDate {get; set;}
#endregion

    public double CalcTotalCompensation(double salary, double bonus, double benefits)
        {
            TotalCompensation = salary + bonus - benefits;
            return TotalCompensation;
        }

    public string CreateEmpEmail(string firstName, string lastName, int dateHired)
        {
            EmpEmail = firstName + lastName + dateHired + "@companyname.com";
            return EmpEmail;
        }

    }
}