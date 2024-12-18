using GreenThumb2._0.ViewModels;

namespace GreenThumb2._0.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage(SignupViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}