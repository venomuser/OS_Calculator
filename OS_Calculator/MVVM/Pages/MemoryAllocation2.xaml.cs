using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.ViewModels;

namespace OS_Calculator.MVVM.Pages;

public partial class MemoryAllocation2 : ContentPage
{
	public MemoryAllocation2(List<Processes> processes)
	{
		InitializeComponent();
		BindingContext = new MemoryAllocation2ViewModel(processes);
	}

    private void ButtonPrevious_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void ButtonInit_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.Common.NumericEntryOnChange(sender, e);
    }

}