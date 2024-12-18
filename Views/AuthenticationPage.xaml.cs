using GreenThumb2._0.ViewModels;

namespace GreenThumb2._0.Views;

public partial class AuthenticationPage : ContentPage
{
	public AuthenticationPage(AuthViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        Shell.SetNavBarIsVisible(this, false);
    }
}