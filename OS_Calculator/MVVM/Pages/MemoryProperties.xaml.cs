using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.ViewModels;

namespace OS_Calculator.MVVM.Pages;

public partial class MemoryProperties : ContentPage
{
	public MemoryProperties(List<Processes> processes)
	{
		InitializeComponent();
		BindingContext = new MemoryPropertiesViewModel(processes);
	}
}