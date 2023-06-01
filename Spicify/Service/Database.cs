using Spicify.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicify.Service
{
    public class Database : IDatabaseService
    {
        private SQLiteAsyncConnection _database;
        public static User CurrentUser { get; set; }

        public Database(string dbPath)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(documentsPath, dbPath);
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<FavoriteRecipe>().Wait();
        }

        public async Task<int> CreateUserAsync(User user)
        {
            return await _database.InsertAsync(user);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        public async Task<int> CreateRecipeAsync(Recipe recipe)
        {
            return await _database.InsertAsync(recipe);
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _database.Table<Recipe>().ToListAsync();
        }

        public async Task<int> CreateFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe)
        {
            return await _database.InsertAsync(favoriteRecipe);
        }

        public async Task<List<FavoriteRecipe>> GetFavoriteRecipesAsync()
        {
            return await _database.Table<FavoriteRecipe>().ToListAsync();
        }

        public async Task<List<int>> GetFavoriteRecipeIdsAsync(int userId)
        {
            List<FavoriteRecipe> favoriteRecipes = await _database.Table<FavoriteRecipe>()
                .Where(fr => fr.UserID == userId)
                .ToListAsync();

            List<int> favoriteRecipeIds = favoriteRecipes.Select(fr => fr.RecipeID).ToList();
            return favoriteRecipeIds;
        }



        public async Task<FavoriteRecipe> GetFavoriteRecipe(int recipeID)
        {
            return await _database.Table<FavoriteRecipe>()
                                   .FirstOrDefaultAsync(fr => fr.RecipeID == recipeID && fr.UserID == CurrentUser.Id);
        }

        public async Task AddFavoriteRecipe(FavoriteRecipe favoriteRecipe)
        {
            await _database.InsertAsync(favoriteRecipe);
        }

        public async Task DeleteFavoriteRecipe(FavoriteRecipe favoriteRecipe)
        {
            await _database.DeleteAsync(favoriteRecipe);
        }
    }

}
