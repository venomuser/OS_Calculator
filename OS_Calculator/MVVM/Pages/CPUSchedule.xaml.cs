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
}