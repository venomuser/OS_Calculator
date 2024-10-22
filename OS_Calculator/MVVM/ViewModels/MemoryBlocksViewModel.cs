﻿using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MemoryBlocksViewModel
    {
        private bool IsReady;

        
        public List<Processes> _Processes { get; set; }
        public MemoryBlocksViewModel(List<Processes> processes)
        {
            _Processes = processes;
            for (int i = 0; i < _Processes.Count; i++)
            {
                _Processes[i].ProcessNumber = i + 1;
            }
        }

        public ICommand NextCommand => new Command(nextCommand);

        private void nextCommand()
        {
            foreach (var proc in _Processes)
            {
                if (proc.NumberOfBlocks  == null || proc.NumberOfBlocks < 1 || proc.NumberOfBlocks > 50)
                {
                    IsReady = false;
                    App.Current.MainPage.DisplayAlert("Error", "Maximum memory blocks that you can enter is 10, and minimum is 1! Also it cannot be null!", "OK");
                    break;
                }
                IsReady = true;
            }
            if (IsReady)
            {
                
                foreach (var proc in _Processes)
                {
                    proc.BlockSizesMB.Clear();
                    for (int i = 0; i < proc.NumberOfBlocks; i++)
                    {
                        proc.BlockSizesMB.Add(0);
                    }
                }
                App.Current.MainPage.Navigation.PushModalAsync(new MemoryAllocation2(_Processes));
            }

        }
    }
}
