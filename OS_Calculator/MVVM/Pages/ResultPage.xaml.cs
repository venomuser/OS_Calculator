namespace OS_Calculator.MVVM.Pages;

public partial class ResultPage : TabbedPage
{
	public ResultPage()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}