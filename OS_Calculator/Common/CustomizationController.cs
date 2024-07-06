using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.Common
{
    public static class CustomizationController
    {
        //CPU Scheduling Algorithms customization
        public static bool FCFS;
        public static bool RoundRobin;
        public static bool SJF;
        public static bool SRT;
        public static bool HRRN;
        public static bool Priority;
        public static bool Lottery;

        //Memory Allocation Algorithms Customization
        public static bool Paging;
        public static bool FixedPartitioning;
        public static bool VariablePartitioning;
        public static bool FirstFit;
        public static bool NextFit;
        public static bool BestFit;
        public static bool WorstFit;
        public static bool Segmentation;

        //Controlling Methods
        //Page navigation forward
        public static void PageNavigate(List<Processes> processes, bool cpuDone = false)
        {
            List<bool> CPUcheckList = new List<bool>();
            List<bool> RamCheckList = new List<bool>();
            //ram list
            RamCheckList.Add(Paging);
            RamCheckList.Add(Segmentation);
            RamCheckList.Add(FirstFit);
            RamCheckList.Add(NextFit);
            RamCheckList.Add(BestFit);
            RamCheckList.Add(WorstFit);
            RamCheckList.Add(FixedPartitioning);
            RamCheckList.Add(VariablePartitioning);
            //cpu list
            CPUcheckList.Add(FCFS);
            CPUcheckList.Add(RoundRobin);
            CPUcheckList.Add(SJF);
            CPUcheckList.Add(SRT);
            CPUcheckList.Add(HRRN);
            CPUcheckList.Add(Priority);
            CPUcheckList.Add(Lottery);

            if (CPUcheckList.Contains(true))
            {
                if (cpuDone == false)
                {
                    App.Current.MainPage.Navigation.PushModalAsync(new CPUSchedule(ref processes));
                }
                else if (RamCheckList.Contains(true))
                    App.Current.MainPage.Navigation.PushModalAsync(new MemoryAllocation(processes));
                else
                    App.Current.MainPage = new NavigationPage(new ResultPage(processes));
            }

            
            else if (RamCheckList.Contains(true))
            {
                
               App.Current.MainPage.Navigation.PushModalAsync(new MemoryAllocation(processes));
     
            }

           
        }
    }
}
