using CommunityToolkit.Maui.Views;
using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Popups;
using OS_Calculator.MVVM.ViewModels;
using System.Diagnostics;

namespace OS_Calculator.MVVM.Pages;

public partial class ResultPage : TabbedPage
{
    public int QuantomTime { get; set; }
    protected List<Processes> _processes { get; set; }
    public ResultPage(List<Processes> processes)
    {
        _processes = processes;
        InitializeComponent();
        this.ShowPopup(new AlgorithmNecessaries(this));
        

    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    private void btnShowResults_Clicked(object sender, EventArgs e)
    {
           
            BindingContext = new OperationsViewModel(QuantomTime, _processes);
    }
}