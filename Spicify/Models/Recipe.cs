using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spicify.Models
{
    public class Recipe
    {
        [PrimaryKey] [AutoIncrement]
        public int RecipeID { get; set; }
        public string RecipeTitle { get; set; }
        public string ImageSource { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
        public string Ingredients { get; set; }
        public string CookingInstruction { get; set; }
        public int InitialRecipeID { get; set; }
        public int UserID { get; set; }
    }

}
