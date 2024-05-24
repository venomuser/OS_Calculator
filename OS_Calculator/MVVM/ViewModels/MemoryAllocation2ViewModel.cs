using OS_Calculator.MVVM.Models;
using PropertyChanged;
using System.Collections.Generic;
using System.Windows.Input;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MemoryAllocation2ViewModel
    {
        public List<Processes> Processes { get; set; }
        public ICommand GoToResultsCommand { get; }

        public MemoryAllocation2ViewModel(List<Processes> processes)
        {
            Processes = processes;
            GoToResultsCommand = new Command(GoToResults);
        }

        private async void GoToResults()
        {
            //var resultsViewModel = new ResultsViewModel(Processes);
            //await Application.Current.MainPage.Navigation.PushAsync(new ResultsPage { BindingContext = resultsViewModel });
        }
    }
}
