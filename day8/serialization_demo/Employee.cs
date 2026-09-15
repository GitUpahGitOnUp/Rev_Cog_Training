using System.Text.Json; // could do xml, binary
using System.Text.Json.Serialization;

namespace EmpManagement;


class Employee
{
    [JsonRequired]
    public int empNo {get; set;}

    public string empName {get; set;} = "";
    [JsonPropertyName("MonthlyPay")]

    public double empSalary {get; set;}

    [JsonPropertyName("Permanent")] // attribute
    public bool empIsActive {get; set;}

    public int empAvailableLeave {get; set;}

    public string empPassword {get; set;} // but this should nto be save in a file, extremely risky

    
    public double AppriseSalary()
    {
        empSalary = empSalary + 2000;
        return empSalary;
    }

    public int ApplyLeave(int days)
    {
        if(days > 5)
        {
            throw new Exception("Sorry, you cannot apply for more than 5 days.");
        }
        empAvailableLeave = empAvailableLeave - days;
        return empAvailableLeave;
    }

    public string SaveObject()
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
            
        };

        // saves object
        string data = JsonSerializer.Serialize(this, options);
        File.WriteAllText("Employee" + this.empNo + ".json", data);
        return "Object Saved";
    }

    public static Employee LoadObject()
    {
        string details = File.ReadAllText("Employee101.json");
        Employee emp = JsonSerializer.Deserialize<Employee>(details);
        return emp;
    }
}
