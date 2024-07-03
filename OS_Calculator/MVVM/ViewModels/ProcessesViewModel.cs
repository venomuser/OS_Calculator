using OS_Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.Windows.Input;
using OS_Calculator.MVVM.Pages;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProcessesViewModel
    {
        private bool IsReady;

        public List<Processes> Processes { get; set; }

        public ICommand GoToMemoryDataCommand => new Command(NextCommand);

        private void NextCommand(object obj)
        {
            foreach(var proc in Processes)
            {
                if(proc.ArrivalTime < 0 || proc.ArrivalTime == null || proc.ProcessUnits <=0 || proc.ProcessUnits == null || proc.Priority <=0 || proc.Priority == null || proc.Tickets <=0 || proc.Tickets == null)
                {
                    IsReady = false;
                    App.Current.MainPage.DisplayAlert("Error", "The entries should not equal to or be smaller than 0 or should not be null or it should not be greater than 50 or it should be just Integer type!","OK");
                    break;
                }
                else if(proc.ArrivalTime > 1000 || proc.ProcessUnits > 1000 || proc.Priority > 1000 || proc.Tickets > 1000)
                {
                    IsReady = false;
                    App.Current.MainPage.DisplayAlert("Error", "The entries should not be greater than 1000!", "OK");
                    break;
                }
                IsReady = true;
            }
            if (IsReady)
                App.Current.MainPage.Navigation.PushModalAsync(new MemoryAllocation(Processes));
        }

        public ProcessesViewModel(ref List<Processes> processes)
        {
            Processes = processes;

            for (int i = 0; i < Processes.Count; i++)
            {
                Processes[i].ProcessNumber = i + 1;
            }
        }


    }
}
