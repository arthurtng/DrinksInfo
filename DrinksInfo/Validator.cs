namespace DrinksInfo;

public class Validator
{
    internal static bool IsStringValid(string stringInput)
    {
        if (String.IsNullOrEmpty(stringInput))
        {
            return false;
        }

        foreach (char c in stringInput)
        {
            if (!Char.IsLetter(c) && c != '/' && c != ' ')
            {
                return false;
            }
        }

        return true;
    }

    internal static bool IsIdValid(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return false;
        }
        
        foreach (char c in id)
        {
            if (!Char.IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }
}