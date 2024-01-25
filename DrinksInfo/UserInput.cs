namespace DrinksInfo;

public class UserInput
{
    DrinksService drinksService = new();

    internal void GetCategoriesInput()
    {
        drinksService.GetCategories();
    }
}