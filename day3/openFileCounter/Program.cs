using System.IO;
Console.WriteLine("");
Console.WriteLine("Welcome to the Open File Counter");
Console.WriteLine("");
Console.WriteLine("----------------------------------");
// file will hold count for # of times the program has ran
string filePath = "fileCount.txt";

int count = 0;

// .Exists reads existing count
if (File.Exists(filePath)) // checks for prior count saved on disk
{
    string fileContent = File.ReadAllText(filePath);

    if (!int.TryParse(fileContent.Trim(), out count))
    {
        count = 0; 

    }

}

count++;

// update count
File.WriteAllText(filePath, count.ToString());

Console.WriteLine($"This program has been opened {count} time(s).");


