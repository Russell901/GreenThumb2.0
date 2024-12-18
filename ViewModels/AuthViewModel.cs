using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace GreenThumb2._0.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _isLoading;

        public AuthViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
        }

        [RelayCommand]
        private async Task Login()
        {
            // Reset previous error
            ErrorMessage = string.Empty;

            // Validate inputs
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                IsLoading = true;

                // Attempt login
                var result = await _authClient.SignInWithEmailAndPasswordAsync(Email, Password);

                // Optional: Store user information if needed
                // You might want to implement a service to manage authentication state
                // e.g., _authService.SetCurrentUser(result.User);

                // Navigate to homepage
                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (FirebaseAuthException ex)
            {
                // Handle specific Firebase authentication errors
                ErrorMessage = MapFirebaseErrorMessage(ex);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                ErrorMessage = "An unexpected error occurred. Please try again.";
                // Optionally log the full exception
                System.Diagnostics.Debug.WriteLine($"Login Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool ValidateInputs()
        {
            // Email validation
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email is required.";
                return false;
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password is required.";
                return false;
            }

            return true;
        }

        private string MapFirebaseErrorMessage(FirebaseAuthException ex)
        {
            return ex.Reason switch
            {
                AuthErrorReason.UserNotFound => "No account found with this email.",
                AuthErrorReason.InvalidEmailAddress => "Invalid email address.",
                AuthErrorReason.WrongPassword => "Incorrect password. Please try again.",
                _ => "Authentication failed. Please try again."
            };
        }

        [RelayCommand]
        private async Task NavigateSignUp()
        {
            await Shell.Current.GoToAsync("//SignUp");
        }
    }
}