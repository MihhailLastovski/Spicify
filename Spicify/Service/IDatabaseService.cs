using Spicify.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spicify.Service
{
    public interface IDatabaseService
    {
        Task<int> CreateUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetUsersAsync();
        Task<int> CreateRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetRecipesAsync();
        Task<int> CreateFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe);
        Task<List<FavoriteRecipe>> GetFavoriteRecipesAsync();
        Task<FavoriteRecipe> GetFavoriteRecipe(int recipeID);
        Task AddFavoriteRecipe(FavoriteRecipe favoriteRecipe);
        Task DeleteFavoriteRecipe(FavoriteRecipe favoriteRecipe);
        Task<List<int>> GetFavoriteRecipeIdsAsync(int userId);
    }
}
