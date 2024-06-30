using CommunityToolkit.Maui.Views;
using OS_Calculator.MVVM.Pages;

namespace OS_Calculator.MVVM.Popups;

public partial class AlgorithmNecessaries : Popup
{
	public ResultPage pg {  get; set; }
	public AlgorithmNecessaries(ResultPage page)
	{
		pg= page;
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		if(string.IsNullOrEmpty(rrEntry.Text) || Convert.ToInt32(rrEntry.Text) <= 0 || Convert.ToInt32(rrEntry.Text) > 10000)
        {
            lblError.Text = "Error! please enter a valid number and it should be smaller than 10000!";
            lblError.IsVisible = true;
        }
        else
        {
            pg.QuantomTime = Convert.ToInt32(rrEntry.Text);
            Close();
        }
    }

    private void rrEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.Common.NumericEntryOnChange(sender, e);
    }
}