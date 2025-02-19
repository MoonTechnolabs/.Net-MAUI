using System.Windows.Input;
using MauiLoginDemo.Models;
using MauiLoginDemo.Services;
using MauiLoginDemo.Views;
using System.Threading.Tasks;
using MauiLoginDemo.Common;

namespace MauiLoginDemo.ViewModels
{
    /// <summary>
    /// ViewModel for handling user login functionality.
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        private string _userName;
        public string userName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
        {
            _databaseService = new DatabaseService();
            LoginCommand = new RelayCommand<object>(async (obj) => await LoginUserAsync());
            SignUpCommand = new RelayCommand<object>(async (obj) => await NavigateToSignupAsync());
        }

        /// <summary>
        /// Handles login by validating user credentials with SQLite database.
        /// </summary>
        private async Task LoginUserAsync()
        {
            var user = await _databaseService.GetUserAsync(userName, password);

            if (user != null)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                await (App.Current.MainPage as NavigationPage)?.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid credentials", "OK");
            }
        }

        /// <summary>
        /// Navigates to the Sign-up page.
        /// </summary>
        private async Task NavigateToSignupAsync()
        {
            await (App.Current.MainPage as NavigationPage)?.Navigation.PushAsync(new SignupPage());
        }
    }
}
