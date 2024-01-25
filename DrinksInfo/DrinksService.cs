using System.Reflection;
using DrinksInfo.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DrinksInfo;

public class DrinksService
{
    private const string BaseUrl = "http://www.thecocktaildb.com/api/json/v1/1/";
    private readonly RestClient _restClient = new RestClient(BaseUrl);
    public List<Category> GetCategories()
    {
        var response = _restClient.ExecuteAsync(new RestRequest("list.php?c=list")).Result;
        
        List<Category> categories = new();

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var rawResponse = response.Content;
            if (rawResponse == null)
            {
                Console.WriteLine("Error: response from API is null");
                return categories;
            }

            var serialized = JsonConvert.DeserializeObject<Categories>(rawResponse);

            if (serialized != null)
            {
                List<Category> returnedList = serialized.CategoriesList;
                TableVisualisationEngine.ShowTable(returnedList, "Categories Menu");
            }
            else
            {
                Console.WriteLine("Error: could not deserialise response from API");
            }
        }
        return categories;
    }

    internal List<Drink> GetDrinksByCategory(string category)
    {
        var response = _restClient.ExecuteAsync(new RestRequest("filter.php?c=" + category)).Result;
        
        List<Drink> drinks = new();

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Content;
            if (String.IsNullOrEmpty(rawResponse))
            {
                throw new Exception("Response from server has no content.");
            }
            var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);
            if (serialize != null)
            {
                drinks = serialize.DrinksList;
            }
            TableVisualisationEngine.ShowTable(drinks, "Drinks Menu");
        }

        return drinks;
    }

    internal void GetDrink(string drink)
    {
        var response = _restClient.ExecuteAsync(new RestRequest("lookup.php?i=" + drink)).Result;
        
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Content;
            
            var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);
            
            List<DrinkDetail> returnedList = serialize.DrinkDetailList;
            
            DrinkDetail drinkDetail = returnedList[0];
            
            List<object> prepList = new();

            string formattedName = "";

            foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
            {
                if (prop.Name.Contains("str"))
                {
                    formattedName = prop.Name.Substring(3);
                }

                if (!String.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                {
                    prepList.Add(new
                    {
                        Key = formattedName,
                        Value = prop.GetValue(drinkDetail)
                    });
                }
            }
            
            TableVisualisationEngine.ShowTable(prepList, drinkDetail.strDrink);
        }
    }
}
