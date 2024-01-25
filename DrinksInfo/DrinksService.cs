using DrinksInfo.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DrinksInfo;

public class DrinksService
{
    public void GetCategories()
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest("list.php?c=list");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var rawResponse = response.Result.Content;
            if (rawResponse == null)
            {
                Console.WriteLine("Error: response from API is null");
                return;
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
    }
}