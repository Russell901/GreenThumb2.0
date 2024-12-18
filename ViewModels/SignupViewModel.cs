using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System.Text.RegularExpressions;

namespace GreenThumb2._0.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _isLoading;

        public SignupViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
        }

        [RelayCommand]
        private async Task SignUp()
        {
            ErrorMessage = string.Empty;

            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                IsLoading = true;

                await _authClient.CreateUserWithEmailAndPasswordAsync(Email, Password, Username);

                // Show success message TODO: set up a more robust popup/toast system
                await Shell.Current.DisplayAlert("Success", "Account created successfully!", "OK");
                await Shell.Current.GoToAsync("//AuthPage");
            }
            catch (FirebaseAuthException ex)
            {
                ErrorMessage = MapFirebaseErrorMessage(ex);
            }
            catch (Exception ex)
            {
                ErrorMessage = "An unexpected error occurred. Please try again.";
                System.Diagnostics.Debug.WriteLine($"Signup Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Username is required.";
                return false;
            }

            if (Username.Length < 3)
            {
                ErrorMessage = "Username must be at least 3 characters long.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email is required.";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Please enter a valid email address.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password is required.";
                return false;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long.";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private string MapFirebaseErrorMessage(FirebaseAuthException ex)
        {
            return ex.Reason switch
            {
                AuthErrorReason.EmailExists => "An account with this email already exists.",
                AuthErrorReason.InvalidEmailAddress => "Invalid email address.",
                AuthErrorReason.WeakPassword => "Password is too weak. Please choose a stronger password.",
                _ => "Registration failed. Please try again."
            };
        }

        [RelayCommand]
        private async Task NavigateAuth()
        {
            await Shell.Current.GoToAsync("//AuthPage");
        }
    }
}