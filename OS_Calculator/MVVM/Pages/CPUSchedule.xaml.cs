using OS_Calculator.Common;
using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.ViewModels;

namespace OS_Calculator.MVVM.Pages;

public partial class CPUSchedule : ContentPage
{
	public CPUSchedule(ref List<Processes> processes)
	{
		InitializeComponent();
        BindingContext = new ProcessesViewModel(ref processes);
	}

    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    private void ButtonPrevious_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.Common.NumericEntryOnChange(sender, e);
    }

    private void ButtonHome_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new FirstPage());
    }
}