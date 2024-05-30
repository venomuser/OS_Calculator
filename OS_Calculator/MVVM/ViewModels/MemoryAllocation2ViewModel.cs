using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MemoryAllocation2ViewModel { 
        private object? val;
        private bool IsReady;


        public List<Processes> Processes { get; set; }
        public ICommand GoToMemorySizeCommand => new Command(GoToMemory);

        public MemoryAllocation2ViewModel(List<Processes> processes)
        {
            Processes = processes;
           
        }

        private void GoToMemory()
        {
            //App.Current.MainPage.
            foreach (var proc in Processes)
            {
                foreach(var ls in proc.BlockSizesMB)
                {
                    if(ls > 2000 || ls <= 0 || ls == null)
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
            //IsReady = true;
            if (IsReady)
            {
                App.Current.MainPage.Navigation.PushModalAsync(new MemoryProperties(Processes));
                //var resultsViewModel = new ResultsViewModel(Processes);
                //await Application.Current.MainPage.Navigation.PushAsync(new ResultsPage { BindingContext = resultsViewModel });
            }

            

        }

     
    }
}
