using OS_Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.ViewModels
{
    public class OperationsViewModel
    {
        

        public OperationsViewModel(int Quantom)
        {
            
            RoundRobin RR = new RoundRobin(Quantom);
        }
    }
}
