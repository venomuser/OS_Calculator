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
}