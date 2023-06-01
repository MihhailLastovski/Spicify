using Spicify.Models;
using Spicify.Service;
using Spicify.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Spicify.ViewModels
{
    public class LoginRegisterViewModel : BaseViewModel
    {
        public LoginRegisterViewModel() { }

        private IDatabaseService _databaseService;
        private string username;
        private string password;
        private string email;


        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        public LoginRegisterViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
        }

        private async Task LoginAsync()
        {
            var user = await _databaseService.GetUserByUsernameAsync(Username);

            if (user != null && user.Password == Password)
            {
                await Application.Current.MainPage.DisplayAlert("Успех", "Вход выполнен успешно", "OK");
                Database.CurrentUser = user;
                FavoritePage favoritePage = new FavoritePage();
                favoritePage.InitializeAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Неверные учетные данные", "OK");
            }
        }

        private async Task RegisterAsync()
        {
            var user = await _databaseService.GetUserByUsernameAsync(Username);

            if (user == null)
            {
                user = new User
                {
                    Username = Username,
                    Password = Password,
                    Email = Email,
                };

                await _databaseService.CreateUserAsync(user);

                await Application.Current.MainPage.DisplayAlert("Успех", "Пользователь зарегистрирован", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Пользователь с таким именем уже существует", "OK");
            }
        }
    }
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
