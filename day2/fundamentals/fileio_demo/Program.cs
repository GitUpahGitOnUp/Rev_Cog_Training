using System.IO;

Console.WriteLine("File IO Demo");

#region Create and Write to a file


// // this is a book
// FileStream myFile = new FileStream("myFile.txt", FileMode.Create, FileAccess.Write);

// // this is a pen
// StreamWriter myPen = new StreamWriter(myFile);

// // let's write
// myPen.WriteLine("Hello, my name is Angela. Welcome to my book");

// myPen.WriteLine("I am a software engineer in training and I love to code.");

// Console.WriteLine("What is your hobby?");
// string hobby = "";

// hobby = Console.ReadLine();
// myPen.WriteLine(hobby);

// // close the pen
// myPen.Close();
// myFile.Close();
#endregion

#region Read From a File

FileStream myBook = new FileStream("myFile.txt", FileMode.Open, FileAccess.Read);

// reads from a file
StreamReader myReader = new StreamReader(myBook);

Console.WriteLine(myReader.ReadToEnd());


myReader.Close();
myBook.Close();
#endregion