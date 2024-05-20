// ViewModels/MainPageViewModel.cs
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;
using OS_Calculator.Services;
using PropertyChanged;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public List<Processes> Processes { get; set; }
        public string TxtProcessNumbers { get; set; }

        public ICommand GoToCPUPage => new Command(async () => await CPU_Page());
        public ICommand TextChangedCommand => new Command

        private async Task CPU_Page()
        {
           
                if (Convert.ToInt32(TxtProcessNumbers) > 50)
                {
                    await _navigationService.DisplayAlert("Error", "Your process number is a lot! Maximum is 50 processes.", "OK");
                }
                else if (Convert.ToInt32(TxtProcessNumbers) == 0)
                {
                    await _navigationService.DisplayAlert("Error", "The number must be greater than 0", "OK");
                }
                else
                {
                    Processes = new List<Processes>();
                    for (long i = 0; i < Convert.ToInt32(TxtProcessNumbers); i++)
                    {
                        Processes.Add(new Processes());
                    }

                    await _navigationService.PushModalAsync(new CPUSchedule
                    {
                        BindingContext = new ProcessesViewModel
                        {
                            Processes = Processes
                        }
                    });
                }
            
        }
    }
}
