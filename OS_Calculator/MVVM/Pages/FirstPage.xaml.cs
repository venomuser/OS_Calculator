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

	public bool FirstFit { get; set; }
	public bool NextFit { get; set; }
	public bool BestFit { get; set; }
	public bool WorstFit { get; set; }
	public bool StaticPart { get; set; }
	public bool DynamicPart { get; set; }
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
     
        this.FirstFit = false;
        this.NextFit = false;
        this.BestFit = false;
        this.WorstFit = false;
        this.StaticPart = false;
        this.DynamicPart = false;
      
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
		
		CheckList.Add(this.FirstFit);
		CheckList.Add(this.NextFit);
		CheckList.Add(this.BestFit);
		CheckList.Add(this.WorstFit);
		CheckList.Add(this.StaticPart);
		CheckList.Add(this.DynamicPart);

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
			
			CustomizationController.FirstFit = this.FirstFit;
			CustomizationController.NextFit = this.NextFit;
			CustomizationController.BestFit = this.BestFit;
			CustomizationController.WorstFit = this.WorstFit;
			CustomizationController.FixedPartitioning = this.StaticPart;
			CustomizationController.VariablePartitioning = this.DynamicPart;
			

			Navigation.PushModalAsync(new MainPage());
		}
    }
}