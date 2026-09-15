using System.IO;
using System;


bool continueMenuSelection = true;

while (continueMenuSelection)
{
    Console.WriteLine("");
    Console.WriteLine("Welcome to the Guest Management System");
    Console.WriteLine("");
    Console.WriteLine("Please Select 'A' if you are a new guest");
    Console.WriteLine("And select 'B' to view guest details");
    Console.WriteLine("Select 'Q' to quit.");
    Console.WriteLine("");
  

    string userChoice = Console.ReadLine().ToLower();

    switch (userChoice)
    {
        case "a":
            // needs to create file name w. SSN
            // maybe store files in a list - nope, that got rewritten on reruns

            Console.Write("Please enter your Social Security Number: ");
            string ssn = Console.ReadLine();
            // var to create file name based on SSN
            string userFile = ssn + ".txt";
            string filePath = userFile;

            using (StreamWriter writer = new StreamWriter(userFile))
            {    
                Console.Write("Please enter your first name: ");
                string firstName = Console.ReadLine();
                writer.WriteLine("First Name: " + firstName);

                Console.Write("Please enter your last name: ");
                string lastName = Console.ReadLine();
                writer.WriteLine("Last Name: " + lastName);

                Console.Write("Please enter your email: ");
                string email = Console.ReadLine();
                writer.WriteLine("Email: " + email);

                Console.Write("Please enter your phone number: ");
                string phone = Console.ReadLine();
                writer.WriteLine("Phone Number: " + phone);

                Console.Write("You can enter guest notes here: ");
                string notes = Console.ReadLine();
                writer.WriteLine("Notes: " + notes);

            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Guest file saved."); // message to let me know if it's working

            break;


        case "b":
            // needs to read from guest files to check for ssn match ****
            // maybe iterate through list of files, read them and check for match
            Console.Write("Please enter your social security number to view guest info: ");
            string getAccess = Console.ReadLine();
            string findFile = getAccess + ".txt";

            if (File.Exists(findFile))
            {
                string[] guestInfo = File.ReadAllLines(findFile);
                foreach (string line in guestInfo)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No guest information was found for that SSN.");
                Console.WriteLine("Please verify your SSN and try again.");
            }

            break;
        
        case "q":
            continueMenuSelection = false;
            break;

        default:
            Console.WriteLine("Please enter A, B, C, or Q");
            break;
    }
}