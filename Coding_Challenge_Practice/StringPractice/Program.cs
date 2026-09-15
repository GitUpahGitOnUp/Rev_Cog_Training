public class Program
{
    public static string ReverseString(string input)
    {
        if(input == null)
        {
            return null;
        }
        else if(input == "")
        {
            return "";
        }

        else
        {
            Char[] characters = input.ToCharArray();

            int left = 0;
            int right = characters.Length - 1;

            while(left < right)
            {
                char temp = characters[left];
                characters[left] = characters[right];
                characters[right] = temp;

                left++;
                right--;
            }
            return new string(characters);
        }
    }

    public static bool IsPalindrome(string input)
    {

        if(string.IsNullOrEmpty(input))
        {
            return false;
        }
        string cleaned = input.ToLower();

        Char[] cleanedChar = cleaned.ToCharArray();

        int left = 0;
        int right = cleanedChar.Length - 1;

        while(left < right)
        {
            if(cleanedChar[left] == ' ')
            {
                left++;
                continue;
            }
            if(cleanedChar[right] == ' ')
            {
                right--;
                continue;
            }

            if(cleanedChar[left] != cleanedChar[right])
            {
                return false;
            }

            left++;
            right--;
        }
        return true;
    }

    public static int CountCharacters(string input, char target)
    {
        if(string.IsNullOrEmpty(input))
        {
            return 0;
        }

        // foreach (target char...)
        int count = 0;


        return count;
    }


    public static void Main(string[] args)
    {
        //Console.WriteLine(ReverseString("magic"));
        Console.WriteLine(IsPalindrome("A man a nam a"));
        Console.WriteLine(IsPalindrome("nope"));
    }
}