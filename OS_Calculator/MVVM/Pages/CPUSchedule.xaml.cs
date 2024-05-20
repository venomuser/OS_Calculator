using OS_Calculator.MVVM.Models;

namespace OS_Calculator.MVVM.Pages;

public partial class CPUSchedule : ContentPage
{
	public CPUSchedule(ref List<Processes> processes)
	{
		InitializeComponent();
        BindingContext = this;
	}

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}