using Microsoft.VisualBasic;


#region Notes
// About the variables
// this is ThreadExceptionEventArgs very first program we are going to write in C# 
// programming language. 

// String - With a capital S is a class / ref. type
// string - with a small s is a keyword / datatype

// There are 2 ways to look at data types in C#
// 1. Value types - int, double, bool, char, struct, enum
// 2. Reference types - string, object, array

// There are 2 types of data types in C#endregion
// 1. Primitive data types - int, double, bool, char - the 1 provided by C# language
// 2. UDT - User Defined Types - Non-primitive data types - string, object, array - the 1 provided by .NET framework
// 3. User-defined dtat types - classes, interfaces, delegates

// string firstName = "Angela";
// string designation = "Software Engineer Trainee";
// int age = 37;

// double salary = 100000.00
// double height = 5.3;
// bool isMarried = true;

// System.Console.WriteLine($"""My name is {firstName}, I am a {designation},my age is 
// {age}, my salary is {salary}, my height is {height}, """);
#endregion

#region Input 

System.Console.WriteLine("!~~~~~~~~~~~~~~~ Welcome to CITI Bank ~~~~~~~~~~~!");
string name = string.Empty;
System.Console.WriteLine("Please enter your name: ");
name = System.Console.ReadLine();

string city = string.Empty;
System.Console.WriteLine("Please enter your city: ");
city = System.Console.ReadLine();

int age = 0;
System.Console.WriteLine("Please enter your age: ");
age = Convert.ToInt32(System.Console.ReadLine());

bool isMarried;
System.Console.WriteLine("Are you married? (Please enter 'true' or 'false'): ");
isMarried = Convert.ToBoolean(System.Console.ReadLine());

Console.WriteLine("""
Thank you for providing your details. 
We will process your information and get back to you shortly.
""");
#endregion

#region Conditional Processing of Values
// we need to process the information provided by the user and check them against the
// bank criteria.

//condition
//name should not be empty or null
//a. Name
// name should not contain < 3 chars and > 25 chars
// user may input name in any case, then convert it to a standard format

// b. City
// city should not be empty or null, only be "New York, "Los Angeles", and "Chicago"

// c. Age
// age should be between 18 and 60
// cannot be a - num or 0
// cannot be a float
// cannot be blank / null / empty

// d. isMarried
// should be true or false, and cannot be left blank

bool validationsPassed = true;

if(name == null || name == string.Empty || name.Length < 3 || name.Length > 25)
{
    System.Console.WriteLine("Invalied name. Please enter a valid name with 3 to 5 characters.");
    validationsPassed = false;
}
else
{
    //Convert 1st letter to Uppercase and remaining -> lowercase
    name = name.Substring(0,1).ToUpper() + name.Substring(1).ToLower();
    validationsPassed = true;
}

if(city == null || city == string.Empty || (city != "New York" && city != "Los Angeles" && city != "Chicago"))
{
    System.Console.WriteLine("Invalid city. Please enter: New York, Los Angeles, or Chicago");
    validationsPassed = false;
}
else
{
    validationsPassed = true;
}
if(age < 18 || age > 60)
{
    validationsPassed = false;
    System.Console.WriteLine("Invalid age. Please enter age between 18 or 60");
}
else if (age <= 0)
{
    validationsPassed = false;
    System.Console.WriteLine("Invalid age. Age cannot be negative");
    
}
else if ( age % 1 != 0)
{
    validationsPassed = false;
    System.Console.WriteLine("Invalid age. Age cannot be a decimal number.");
}
else
{
    validationsPassed = true;
}

if(isMarried != true && isMarried != false)
{
    validationsPassed = false;
    System.Console.WriteLine("Please enter either true or false");
}
else
{
    validationsPassed = true;
}

if(validationsPassed)
{
    System.Console.WriteLine($"Approved !! Thank you {name} from {city}, age {age}, for providing that information.");
}
else
{
    System.Console.WriteLine($"regected !! {name} Please correct the errors and try again.");
}
#endregion