// Loops
// a. For loops - this helps to iterate on a pre-defined number of times
// b. While loops - this helps to iterate on a condition but it will execute at least once
// c. Do While - this helps to iterate on a condition but it will execute at least once
// d. Foreach loop - this helps to iterate on a collection of items (array, list, dict., hashtable)

//forLoop
//         for (int i = 0; i < 10; i++)
//         {
//             Console.WriteLine(i);
//         }

// string[] techList = {"C#", "Java", "Python", "JavaScript", "C++", "PHP", "AWS", "Azure", "GCP", "Docker" };
// Console.WriteLine(techList[5]);

#region Guess the number
// int secretNumber = 7;

// Console.WriteLine("Guess the number, you have 3 attempts");
// int attempts = 0;
// for (int i = 0; i < 3; if++);
// {
//     Console.WriteLine("Enter your guess:");
//     int userGuess = Convert.ToInt32(Console.ReadLine());
//     attempts++;
//     if (userGuess == secretNumber)
//     {
        
//     }
// }

#endregion

// our req is to ask the user to enter a number, and keep entering till thenumber is 0
// imagine reading an excel file from console app, we are not sure how many rows there are
// so the program will continue reading until the user enters 0

int userInput = 0;
int addition = 0;
int evenNumber = 0;
int oddNumber = 0;
int totalNumbers = 0;
int greaterThan100 = 0;

do
{
    Console.WriteLine("Enter a number (enter 0 to exit): ");
    userInput = Convert.ToInt32(Console.ReadLine());

    if (userInput != 0)
    {
        Console.WriteLine($"You entered {userInput}");
        addition += userInput;
        totalNumbers++;

        if (userInput % 2 == 0)
        {
            evenNumber++;
        }
        else
        {
            oddNumber++;
        }

        if (userInput > 100)
        {
            greaterThan100++;
        }
    }
} while (userInput != 0);

Console.WriteLine($"Total numbers entered: {totalNumbers}");
Console.WriteLine($"Sum of all numbers: {addition}");
Console.WriteLine($"Total even number: {evenNumber}");
Console.WriteLine($"Total odd numbers: {oddNumber}");
Console.WriteLine($"Total numbers greather than 100: {greaterThan100}");