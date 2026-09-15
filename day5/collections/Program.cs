using System.Collections;
using System.Collections.Generic;

#region Array
// int[] myNumber = new int[10];

// for(int i = 0; i < myNumber.Length; i++)
// {
//     Console.WriteLine("Please enter your " + 1 + " Number ");
//     myNumber[i] = Convert.ToInt32(Console.ReadLine());
// }

// int additions = 0;
// int evenNumber = 0;
// int oddNumber = 0;

// for (int i = 0; i < myNumber.Length; i++ )
// {
//     additions = additions + myNumber[i];

//     if(myNumber[i] % 2 == 0)
//     {
//         evenNumber++;
//     }
//     else
//     {
//         oddNumber++;
//     }
// }

// Console.WriteLine("Addition of numbers: " + additions);

// Console.WriteLine("Even Number:" + evenNumber);
// Console.WriteLine("Odd Numbers: " + oddNumber);

#endregion

#region ArrayList

// ArrayList myList = new ArrayList();
// myList.Add(10);
// myList.Add("Nikhil");
// myList.Add(true);
// myList.Add(40);
// myList.Add(10.4);
// myList.Add(new DateTime());
// myList.Add(new {empNo=101, empName="Jack", empDesignation="Sales"});

// foreach (var item in myList)
// {
//     Console.WriteLine (item);
// }

// Console.WriteLine(myList.Count);

#endregion

#region List

// List<string> friends = new List<string>();

// friends.Add("Cole");

// string moreval = "a";

// while(moreval == "a")
// {
//     Console.Write("Add a new friend: ");
//     string newFriend = Console.ReadLine();
//     friends.Add(newFriend);
// }
// Console.WriteLine("Total friends " + friends.Count);

#endregion

#region Linked List

#endregion

#region Dictionary

Dictionary<int,string> friends = new Dictionary<int, string>();

friends.Add(1, "");
friends.Add(2, "Bill");
friends.Add(3, "Bobby");
friends.Add(4, "Billiam");
friends.Add(5, "Bobert");

foreach (var item in friends)
{
    Console.WriteLine(item.Value);
}

#endregion

#region Hashtables

// Hashtable moreFriends = new Hashtable();

// moreFriends.Add(1, "Nick");
// moreFriends.Add(2, "Harry");
// moreFriends.Add(3, "Harmony");
// moreFriends.Add(4, "Paul");
// moreFriends.Add("Five", false); //  !!!  this will run w/o error !!!!!


// foreach (var item in moreFriends.Values)
// {
//     Console.WriteLine(item);
// }


#endregion