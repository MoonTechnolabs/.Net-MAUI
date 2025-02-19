# .NET MAUI Login & Signup with SQLite

## Overview
This project is a **.NET MAUI** (Multi-platform App UI) application that provides a **Login & Signup** system using **SQLite** for local storage. Users can sign up, and their credentials (username, email, password) will be stored in an SQLite database. During login, the app validates user credentials and navigates to the home page upon successful authentication.

## What is .NET MAUI?
**.NET MAUI (Multi-platform App UI)** is a cross-platform framework for building native applications using C# and XAML. It enables developers to build apps for **iOS, Android, Windows, and macOS** from a **single codebase**.

### Key Features of .NET MAUI:
- **Single project** for multiple platforms
- **Native performance** and UI rendering
- **MVVM (Model-View-ViewModel)** architecture support
- **Integrated dependency injection**
- **SQLite and local data storage** 

---

## Features Implemented
✅ User Signup & Storage in SQLite  
✅ User Login & Authentication  
✅ Navigation between pages  
✅ MVVM Architecture Implementation    
✅ Secure Data Management with SQLite  

---

## Prerequisites
Before running this project, ensure you have the following installed:

- **.NET SDK** (latest version) - 8.0  
- **Visual Studio 2022** (with .NET MAUI workload)  
- **SQLite NuGet Package** (`sqlite-net-pcl`)  
- **Android/iOS Emulator** or a physical device  

To install SQLite in the project, run:
```sh
 dotnet add package sqlite-net-pcl
```

---

## Project Structure
### **1. Models**  
Defines the structure of the user data stored in SQLite.
```csharp
public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Unique]
    public string UserName { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }
}
```

### **2. Services**  
Handles database operations such as saving and retrieving user data.
```csharp
public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    
    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<User>().Wait();
    }
    
    public Task<int> AddUserAsync(User user) => _database.InsertAsync(user);
    public Task<User> GetUserAsync(string username, string password) => 
        _database.Table<User>().Where(u => u.UserName == username && u.Password == password).FirstOrDefaultAsync();
}
```

### **3. ViewModels**  
Implements the MVVM pattern for Login & Signup operations.
#### **SignupViewModel**
Handles user registration and validates user input.
```csharp
public class SignupViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;
    public ICommand SignUpCommand { get; }
    
    public SignupViewModel()
    {
        _databaseService = new DatabaseService();
        SignUpCommand = new Command(async () => await RegisterUserAsync());
    }
    
    private async Task RegisterUserAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "All fields are required", "OK");
            return;
        }
        
        if (!IsValidEmail(Email))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid email format", "OK");
            return;
        }
        
        try
        {
            var user = new User { UserName = UserName, Email = Email, Password = Password };
            await _databaseService.AddUserAsync(user);
            await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        catch (SQLite.SQLiteException ex) when (ex.Result == SQLite.SQLite3.Result.Constraint && ex.Message.Contains("UNIQUE"))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "User Name is already taken", "OK");
        }
        catch (System.Exception)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred", "OK");
        }
    }
    
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
```

#### **LoginViewModel**
Validates user login credentials.
```csharp
public class LoginViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;
    public ICommand LoginCommand { get; }
    
    public LoginViewModel()
    {
        _databaseService = new DatabaseService();
        LoginCommand = new RelayCommand<object>(async (obj) => await LoginUserAsync());
    }
    
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
}
```

### **4. Views**  
**LoginPage.xaml** and **SignupPage.xaml** contain UI elements for authentication.
```xml
<Entry Placeholder="Enter Username" Text="{Binding userName}" />
<Entry Placeholder="Enter Password" Text="{Binding password}" IsPassword="True" />
<Button Text="Login" Command="{Binding LoginCommand}" />
<Button Text="Sign Up" Command="{Binding SignUpCommand}" />
```

---

## How to Run the Project
1. **Clone the repository**
```sh
git clone https://github.com/MoonTechnolabs/.Net-MAUI.git
```
2. **Install dependencies**
```sh
dotnet restore
```
3. **Run the application**
```sh
dotnet build
dotnet run
```
4. **Select an emulator/device** in Visual Studio and launch the app.

---

## Conclusion
This .NET MAUI project demonstrates **SQLite-based user authentication** with **MVVM architecture**. It enables **cross-platform login/signup functionality** while following best practices. 🚀  

For any issues, feel free to open an **issue** or contribute to the project! 😊
