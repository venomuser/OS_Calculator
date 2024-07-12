using CommunityToolkit.Maui.Views;
using OS_Calculator.Common;
using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Popups;
using OS_Calculator.MVVM.ViewModels;
using System.Diagnostics;

namespace OS_Calculator.MVVM.Pages;

public partial class ResultPage : TabbedPage
{
    public int QuantomTime { get; set; }
    protected List<Processes> _processes { get; set; }
    public ResultPage(List<Processes> processes, Memory memory = null)
    {
        _processes = processes;
        InitializeComponent();
        if (CustomizationController.RoundRobin == true)
        {
            this.ShowPopup(new AlgorithmNecessaries(this));
        }
        if (CustomizationController.FCFS == false)
        {
            FCFSdataGrid.IsVisible = false;
            FCFSdataGrid.IsEnabled = false;
        }
        if (CustomizationController.RoundRobin == false)
        {
            RRdataGrid.IsVisible = false;
            RRdataGrid.IsEnabled = false;
        }
        if (CustomizationController.SJF == false)
        {
            SJFdataGrid.IsVisible = false;
            SJFdataGrid.IsEnabled = false;
        }
        if (CustomizationController.SRT == false)
        {
            SRTdataGrid.IsVisible = false;
            SRTdataGrid.IsEnabled = false;
        }
        if (CustomizationController.HRRN == false)
        {
            HRRNdataGrid.IsVisible = false;
            HRRNdataGrid.IsEnabled = false;
        }
        if (CustomizationController.Priority == false)
        {
            PrioritydataGrid.IsVisible = false;
            PrioritydataGrid.IsEnabled = false;
        }
        if (CustomizationController.Lottery == false)
        {
            LotterydataGrid.IsVisible = false;
            LotterydataGrid.IsEnabled = false;
        }

        //if memory algorithms selected, then this should happen
        RamContentPage.BindingContext = new MemoryOperationsViewModel(processes, memory);
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    private void btnShowResults_Clicked(object sender, EventArgs e)
    {
           
            CpuContentPage.BindingContext = new CpuOperationsViewModel(QuantomTime, _processes);
        FCFSGanttView.IsVisible = CustomizationController.FCFS;
        RRGanttView.IsVisible = CustomizationController.RoundRobin;
        RRWaitingTimeView.IsVisible = CustomizationController.RoundRobin;
        SJFGanttView.IsVisible = CustomizationController.SJF;
        HRRNGanttView.IsVisible = CustomizationController.HRRN;
        PriorityGanttView.IsVisible = CustomizationController.Priority;
    }

    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new FirstPage());
    }
}