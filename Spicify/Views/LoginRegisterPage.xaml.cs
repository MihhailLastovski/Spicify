using Spicify.Service;
using Spicify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spicify.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegisterPage : ContentPage
    {
        public LoginRegisterPage()
        {
            InitializeComponent();
            BindingContext = new LoginRegisterViewModel(new Database("database.db3"));
        }
    }
}