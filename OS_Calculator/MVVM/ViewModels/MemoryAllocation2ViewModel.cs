using OS_Calculator.MVVM.Models;
using PropertyChanged;
using System.Collections.Generic;
using System.Windows.Input;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MemoryAllocation2ViewModel
    {
        private bool IsReady;
        public List<Processes> Processes { get; set; }
        public ICommand GoToMemorySizeCommand { get; }

        public MemoryAllocation2ViewModel(List<Processes> processes)
        {
            Processes = processes;
            GoToMemorySizeCommand = new Command(GoToMemory);
        }

        private void GoToMemory()
        {
            foreach (var proc in Processes)
            {
                foreach(var ls in proc.BlockSizesMB)
                {
                    if(ls > 2000 || ls < 0 || ls == null)
                    {
                        IsReady = false;
                        App.Current.MainPage.DisplayAlert("Error", "Block Sizes cannot be greater than 2GB or be 0 or be null!","OK");
                        break;
                    }
                }
                if(!IsReady)
                    break;

                IsReady = true;
            }
            if (IsReady)
            {
                //var resultsViewModel = new ResultsViewModel(Processes);
                //await Application.Current.MainPage.Navigation.PushAsync(new ResultsPage { BindingContext = resultsViewModel });
            }

        }
    }
}
