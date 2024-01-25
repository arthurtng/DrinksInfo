namespace DrinksInfo;

public class UserInput
{
    DrinksService drinksService = new();

    internal void GetCategoriesInput()
    {
        drinksService.GetCategories();
        
        Console.WriteLine("Choose category:");

        try
        {
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid category");
                category = Console.ReadLine();
            }

            GetDrinksInput(category);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting drinks category, try again. ({e.Message})");
            GetCategoriesInput();
        }
    }

    private void GetDrinksInput(string category)
    {
        var drinks = drinksService.GetDrinksByCategory(category);
        
        Console.WriteLine("Choose a drink or go back to category menu by typing 0:");

        string drink = Console.ReadLine();
        
        if (drink == "0") GetCategoriesInput();

        while (!Validator.IsIdValid(drink))
        {
            Console.WriteLine("\nInvalid selection, try again: \n");
            drink = Console.ReadLine();
        }
        
        if(!drinks.Any(x => x.idDrink == drink)) 
        {
            Console.WriteLine("Drink doesn't exist.");
            GetDrinksInput(category);
        }
        
        drinksService.GetDrink(drink);
        
        Console.WriteLine("Press any key to go back to categories menu");
        Console.ReadKey();
        if (!Console.KeyAvailable) GetCategoriesInput();
    }
}