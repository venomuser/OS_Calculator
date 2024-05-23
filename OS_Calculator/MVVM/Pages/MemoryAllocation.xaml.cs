using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.ViewModels;

namespace OS_Calculator.MVVM.Pages;

public partial class MemoryAllocation : ContentPage
{
	public MemoryAllocation(List<Processes> processes)
	{
		InitializeComponent();
		BindingContext = new MemoryBlocksViewModel(processes);
	}

    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.Common.NumericEntryOnChange(sender, e);
    }

    private void ButtonPrevious_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void ButtonRoot_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}