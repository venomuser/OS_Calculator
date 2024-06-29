using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Popups;
using OS_Calculator.MVVM.ViewModels;

namespace OS_Calculator.MVVM.Pages;

public partial class MemoryProperties : ContentPage
{
    List<Processes> Processes { get; set; }
	Memory memory {  get; set; }

    public MemoryProperties(List<Processes> processes)
	{
		this.Processes = processes;
		InitializeComponent();

		Page page = this;
		
		BindingContext = new MemoryPropertiesViewModel(page,processes);
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.Common.NumericEntryOnChange(sender, e);
    }
}