using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Spicify.Models
{
    public class Data
    {
        public string NameLabel { get; set; }
        public ImageSource ImageSource { get; set; }
        public bool IsFavorite { get; set; }
        public ImageSource ImageButton { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public string CookingInstructions { get; set; }


    }
}
