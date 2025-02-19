namespace MauiLoginDemo.ViewModels
{
    /// <summary>
    /// ViewModel for the Home Page.
    /// </summary>
    public class HomePageViewModel : BaseViewModel
    {
        private string _welcomeLabel;

        /// <summary>
        /// Gets or sets the welcome label text.
        /// </summary>
        public string welcomeLabel
        {
            get { return _welcomeLabel; }
            set
            {
                _welcomeLabel = value;
                OnPropertyChanged(nameof(welcomeLabel));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageViewModel"/> class.
        /// Sets the default welcome label text.
        /// </summary>
        public HomePageViewModel()
        {
            welcomeLabel = "Welcome to Home Page";
        }
    }
}
