using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spicify.Models
{
    public class FavoriteRecipe
    {
        [PrimaryKey] [AutoIncrement]
        public int FavoriteID { get; set; }
        public int UserID { get; set; }
        public int RecipeID { get; set; }
    }
}
