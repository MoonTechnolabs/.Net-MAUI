using MauiLoginDemo.Services;
using MauiLoginDemo.Views;

namespace MauiLoginDemo
{
    /// <summary>
    /// Represents the main entry point for the MAUI application.
    /// </summary>
    public partial class App : Application
    {
        private static DatabaseService _databaseService;

        public static DatabaseService Database => _databaseService ??= new DatabaseService();
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Sets the main page of the application.
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Set the initial page to the LoginPage inside a NavigationPage
            MainPage = new NavigationPage(new LoginPage());
        }

        /// <summary>
        /// Called when the application starts.
        /// Sets the application theme.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
            SetTheme();
        }

        /// <summary>
        /// Applies the theme based on the system's current theme setting.
        /// </summary>
        private void SetTheme()
        {
            if (Application.Current.RequestedTheme == AppTheme.Dark)
            {
                // Apply dark theme resources
                Application.Current.Resources["PageBackgroundColor"] = Application.Current.Resources["DarkBackground"];
            }
            else
            {
                // Apply light theme resources
                Application.Current.Resources["PageBackgroundColor"] = Application.Current.Resources["LightBackground"];
            }
        }
    }
}
