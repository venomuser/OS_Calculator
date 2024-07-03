using OS_Calculator.Common;

namespace OS_Calculator.MVVM.Pages;

public partial class FirstPage : ContentPage
{
	public bool FCFS {  get; set; }
	public bool RoundRobin {  get; set; }
	public bool SJF {  get; set; }
	public bool SRT {  get; set; }
	public bool HRRN {  get; set; }
	public bool Priority {  get; set; }
	public bool Lottery {  get; set; }
	public bool Paging { get; set; }
	public FirstPage()
	{
		InitializeComponent();
        this.FCFS = false;
        this.RoundRobin = false;
        this.SJF = false;
        this.SRT = false;
        this.HRRN = false;
        this.Priority = false;
        this.Lottery = false;
        this.Paging = false;
		BindingContext = this;
    }

    private void btnConfirmCustom_Clicked(object sender, EventArgs e)
    {
		List<bool> CheckList = new List<bool>();
		CheckList.Add(this.FCFS);
		CheckList.Add(this.RoundRobin);
		CheckList.Add(this.SJF);
		CheckList.Add(this.SRT);
		CheckList.Add(this.HRRN);
		CheckList.Add(this.Priority);
		CheckList.Add(this.Lottery);
		CheckList.Add(this.Paging);

		if (!CheckList.Contains(true))
		{
			DisplayAlert("Error", "At least one option should be selected!", "OK");
		}
		else
		{
			CustomizationController.FCFS = this.FCFS;
			CustomizationController.RoundRobin = this.RoundRobin;
			CustomizationController.SJF = this.SJF;
			CustomizationController.SRT = this.SRT;
			CustomizationController.HRRN = this.HRRN;
			CustomizationController.Priority = this.Priority;
			CustomizationController.Lottery = this.Lottery;
			CustomizationController.Paging = this.Paging;

			Navigation.PushModalAsync(new MainPage());
		}
    }
}