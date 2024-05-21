using OS_Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProcessesViewModel
    {
        public List<Processes> Processes { get; set; }


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
