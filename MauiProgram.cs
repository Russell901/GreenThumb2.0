using Firebase.Auth;
using Firebase.Auth.Providers;
using GreenThumb2._0.ViewModels;
using GreenThumb2._0.Views;
using Microsoft.Extensions.Logging;

namespace GreenThumb2._0
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //setting up firebase auth and auth providers
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyBrsTmTXwMJxIi4pVQ8belp2Gk8mSrgX5Y",
                AuthDomain = "greenthumb-39d82.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            }));

            //registering views
            builder.Services.AddSingleton<AuthenticationPage>();
            builder.Services.AddSingleton<SignupPage>();
            builder.Services.AddSingleton<OnboardingPage>();
            //builder.Services.AddSingleton<AuthenticationPage>();

            //registering view models
            builder.Services.AddSingleton<AuthViewModel>();
            builder.Services.AddSingleton<SignupViewModel>();
            builder.Services.AddSingleton<OnboardingViewModel>();

            return builder.Build();
        }
    }
}
