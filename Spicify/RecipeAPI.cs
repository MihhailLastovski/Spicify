using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Spicify
{
    public class RecipeAPI
    {
        private const string API_HOST = "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com";
        private const string API_KEY = "be43cea060mshc4dd49f4a6c7a6fp1f9a6ejsn5aa22bdcfd70";

        public static List<MyObject> SearchRecipesByIngredients(string[] ingredients)
        {
            string joinedIngredients = string.Join("%2C", ingredients);
            string url = $"https://{API_HOST}/recipes/findByIngredients?ingredients={joinedIngredients}&number=6&ignorePantry=true&ranking=1";

            return SearchRecipes(url, false);
        }

        public static List<MyObject> SearchRecipesByName(string query)
        {
            string encodedQuery = Uri.EscapeDataString(query);
            string url = $"https://{API_HOST}/recipes/search?query={encodedQuery}&number=12";

            return SearchRecipes(url, true);
        }

        private static List<MyObject> SearchRecipes(string url, bool isName)
        {
            List<int> ids = new List<int>();
            WebRequest request = WebRequest.Create(url);
            request.Headers["X-RapidAPI-Host"] = API_HOST;
            request.Headers["X-RapidAPI-Key"] = API_KEY;
            request.Method = "GET";
            request.ContentType = "application/json";
            using (WebResponse response = request.GetResponse())
            {
                JArray recipes;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    if (isName)
                    {
                        JObject recipeData = JObject.Parse(reader.ReadToEnd());
                        recipes = (JArray)recipeData["results"];
                    }
                    else 
                    {
                        recipes = JArray.Parse(reader.ReadToEnd());
                    }

                    foreach (JToken recipe in recipes)
                    {
                        ids.Add(int.Parse(recipe["id"].ToString()));
                    }

                    return GetRecipeInformation(ids);
                }
            }
        }

        private static List<MyObject> GetRecipeInformation(List<int> ints)
        {
            string ids = string.Join("%2C", ints);
            string url = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/informationBulk?ids={ids}";
            WebRequest request = WebRequest.Create(url);
            request.Headers["X-RapidAPI-Host"] = "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com";
            request.Headers["X-RapidAPI-Key"] = "be43cea060mshc4dd49f4a6c7a6fp1f9a6ejsn5aa22bdcfd70";
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                JArray recipeData = JArray.Parse(reader.ReadToEnd());
                List<MyObject> recipeList = new List<MyObject>();
                foreach (JObject recipe in recipeData)
                {
                    try 
                    {
                        string imageSource = recipe["image"].ToString();
                        string name = recipe["title"].ToString();

                        MyObject getinfo = new MyObject();
                        getinfo.Image = imageSource;
                        getinfo.Name = name;

                        string summary = recipe["summary"].ToString();
                        string cleanedSummary = StripHTMLTags(summary);
                        getinfo.Description = cleanedSummary;

                        JArray steps = (JArray)recipe["analyzedInstructions"][0]["steps"];
                        List<string> instructions = new List<string>();
                        foreach (JToken step in steps)
                        {
                            string instruction = step["step"].ToString();
                            instructions.Add(instruction);
                        }
                        getinfo.CookingInstructions = string.Join("\n", instructions);
                        JArray ingredientsArray = (JArray)recipe["extendedIngredients"];
                        List<string> ingredientList = new List<string>();
                        foreach (JToken ingredientToken in ingredientsArray)
                        {
                            string ingredientName = ingredientToken["original"].ToString();
                            ingredientList.Add(ingredientName);
                        }
                        getinfo.Ingredients = ingredientList;

                        recipeList.Add(getinfo);
                    }
                    catch { continue; }
                    
                }                  
                return recipeList;

            }
        }


        public static List<MyObject> GetRandomRecipes()
        {
            string url = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/random?number=6";
            WebRequest request = WebRequest.Create(url);
            request.Headers["X-RapidAPI-Host"] = "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com";
            request.Headers["X-RapidAPI-Key"] = "be43cea060mshc4dd49f4a6c7a6fp1f9a6ejsn5aa22bdcfd70";
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                JObject recipeData = JObject.Parse(reader.ReadToEnd());
                JArray recipes = (JArray)recipeData["recipes"];

                List<MyObject> recipeList = new List<MyObject>();

                foreach (JToken recipe in recipes)
                {
                    try 
                    {
                        string imageSource = recipe["image"].ToString();
                        string name = recipe["title"].ToString();

                        MyObject getinfo = new MyObject();
                        getinfo.Image = imageSource;
                        getinfo.Name = name;

                        string summary = recipe["summary"].ToString();
                        string cleanedSummary = StripHTMLTags(summary);
                        getinfo.Description = cleanedSummary;

                        JArray steps = (JArray)recipe["analyzedInstructions"][0]["steps"];
                        List<string> instructions = new List<string>();
                        foreach (JToken step in steps)
                        {
                            string instruction = step["step"].ToString();
                            instructions.Add(instruction);
                        }
                        getinfo.CookingInstructions = string.Join("\n", instructions);
                        JArray ingredientsArray = (JArray)recipe["extendedIngredients"];
                        List<string> ingredientList = new List<string>();
                        foreach (JToken ingredientToken in ingredientsArray)
                        {
                            string ingredientName = ingredientToken["original"].ToString();
                            ingredientList.Add(ingredientName);
                        }
                        getinfo.Ingredients = ingredientList;

                        recipeList.Add(getinfo);
                    }
                    catch { continue; }                   
                }
                return recipeList;
            }
            
    }

        public class MyObject
        {
            public string Image { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> Ingredients { get; set; }
            public string CookingInstructions { get; set; }

        }

        private static string StripHTMLTags(string input)
        {
            return Regex.Replace(input, "<.*?>", "");
        }
    }
}
