using CommunityToolkit.Maui.Views;
using OS_Calculator.MVVM.Models;

namespace OS_Calculator.MVVM.Popups;

public partial class MemoryBlocksSizesPopup : Popup
{
	public MemoryBlocksSizesPopup(Memory memory, List<Processes> processes)
	{
		InitializeComponent();
		Memory _memory = memory;

	    // Initialize number of Entries in the popup


	}

    private void ButtonResult_Clicked(object sender, EventArgs e)
    {

    }
}