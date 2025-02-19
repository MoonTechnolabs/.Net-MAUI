using System.Windows.Input;
using MauiLoginDemo.Models;
using MauiLoginDemo.Services;
using MauiLoginDemo.Views;
using MauiLoginDemo.Common;
using System.Text.RegularExpressions;

namespace MauiLoginDemo.ViewModels
{
    /// <summary>
    /// ViewModel for handling user sign-up functionality.
    /// </summary>
    public class SignupViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

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

        private string _email;
        public string email
        {
            get => _email;
            set
            {
                _email = value;
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
        /// Initializes a new instance of the <see cref="SignupViewModel"/> class.
        /// </summary>
        public SignupViewModel()
        {
            _databaseService = new DatabaseService();
            SignUpCommand = new RelayCommand<object>(async (obj) => await RegisterUserAsync());
        }

        /// <summary>
        /// Handles the sign-up process by storing user data in SQLite and navigating to LoginPage.
        /// </summary>
        private async Task RegisterUserAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "OK");
                    return;
                }

                // Email validation using Regex
                if (!IsValidEmail(email))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid email format", "OK");
                    return;
                }

                var user = new User { UserName = userName, Email = email, Password = password };
                await _databaseService.AddUserAsync(user);

                await App.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");

                // Navigate to login page
                await (App.Current.MainPage as NavigationPage)?.Navigation.PushAsync(new LoginPage());
            }
            catch (SQLite.SQLiteException ex) when (ex.Result == SQLite.SQLite3.Result.Constraint && ex.Message.Contains("UNIQUE"))
            {
                await App.Current.MainPage.DisplayAlert("Error", "User Name is already taken", "OK");
            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred", "OK");
            }

        }
        /// <summary>
        /// Validates an email format using regex.
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",  // Basic email pattern
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch
            {
                return false;
            }
        }
    }
}
