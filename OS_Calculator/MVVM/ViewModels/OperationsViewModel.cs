using OS_Calculator.Common;
using OS_Calculator.MVVM.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class OperationsViewModel
    {

        private readonly int quantom;
        public int Quantom { get { return quantom; } }

        public List<Processes> _processes { get; set; }

        private List<int?> _completionTime;
        public bool IsFCFS { get; set; }
        public bool IsRR { get; set; }
        public bool IsSJF { get; set; }
        public bool IsSRT { get; set; }
        public bool IsHRRN { get; set; }
        public bool IsPriority { get; set; }
        public bool IsLottery { get; set; }

        public Dictionary<string, double?> FCFSWaitingTime { get; set; }
        public Dictionary<string, double?> RRWaitingTime { get; set; }
        public Dictionary<string, double?> SJFWaitingTime { get; set; }
        public Dictionary<string, double?> SRTWaitingTime { get; set; }
        public Dictionary<string, double?> HRRNWaitingTime { get; set; }
        public Dictionary<string, double?> PriorityWaitingTime { get; set; }
        public Dictionary<string, double?> MLQWaitingTime { get; set; }
        public Dictionary<string, double?> MLFQWaitingTime { get; set; }

        public Dictionary<string, double?> LotteryWaitingTime { get; set; }

        public Dictionary<string, double?> Queue1WaitingTime = new Dictionary<string, double?>();
        public Dictionary<string, double?> Queue2WaitingTime = new Dictionary<string, double?>();
        public FCFSChart[] chart { get; set; }
        public List<Tuple<string?, string?, int?, string>> RRchart { get; set; }
        public List<Tuple<string, string, int?, string, int?>> SJFChart { get; set; }
        public List<Tuple<string, string, int?, string>> SRTChart { get; set; }
        public List<Tuple<string, int?, int?, string, string>> HRRNchart { get; set; }
        public List<Tuple<string, int?, int?, string, string>> PriorityChart { get; set; }
        public List<Tuple<string, int?, string, string>> LotteryChart { get; set; }

        

        public OperationsViewModel(int Quantom, List<Processes> processes)
        {
            IsFCFS = CustomizationController.FCFS;
            IsRR = CustomizationController.RoundRobin;
            IsSJF = CustomizationController.SJF;
            IsSRT = CustomizationController.SRT;
            IsHRRN = CustomizationController.HRRN;
            IsPriority = CustomizationController.Priority;
            IsLottery = CustomizationController.Lottery;


            Random random = new Random();
            _processes = processes;
            chart = new FCFSChart[_processes.Count + 1];
            RRchart = new();
            SJFChart = new();
            SRTChart = new();
            HRRNchart = new();
            PriorityChart = new();
            LotteryChart = new();

            GenerateTimeline();
            foreach (var process in _processes)
            {

            // Generate random values for R, G, and B components
            Colors:
                int red = random.Next(0, 256);
                int green = random.Next(0, 256);
                if (red > 240 && green > 240)
                {
                    goto Colors;
                }
                int blue = random.Next(0, 256);
                // Convert to hexadecimal and format as a string
                string hexColor = $"#{red:X2}{green:X2}{blue:X2}";
                process.ProcessColor = hexColor;
            }
            quantom = Quantom;
            FCFSWaitingTime = new Dictionary<string?, double?>();
            _completionTime = new List<int?>();
            RRWaitingTime = new Dictionary<string?, double?>();
            SJFWaitingTime = new Dictionary<string?, double?>();
            SRTWaitingTime = new Dictionary<string?, double?>();
            HRRNWaitingTime = new Dictionary<string?, double?>();
            PriorityWaitingTime = new Dictionary<string?, double?>();
            MLQWaitingTime = new Dictionary<string?, double?>();
            MLFQWaitingTime = new Dictionary<string?, double?>();
            LotteryWaitingTime = new Dictionary<string?, double?>();
            Queue1WaitingTime = new Dictionary<string?, double?>();
            Queue2WaitingTime = new Dictionary<string?, double?>();

            Thread t2 = new Thread(new ThreadStart(() =>
               {
                  
                   if (IsFCFS == true)
                   {
                       FCFS(_processes);
                   }
                   if(IsRR == true) {
                       RoundRobin(_processes, quantom);
                   }

                   if (IsSJF == true)
                   {
                       SJF(_processes);
                   }
                   if (IsSRT == true)
                   {
                       SRT(_processes);
                   }
                   if(IsHRRN == true)
                   {
                       HRRN(_processes);
                   }

                   if(IsPriority == true)
                   {
                       PriorityScheduling(_processes);
                   }

                   if (IsLottery == true)
                   {
                       LotteryScheduling(_processes);
                   }

                   
            //MLQ(_processes, quantom);
            //MLFQ(_processes, 3, new int[] { quantom, quantom / 2, (quantom / 2) / 2 });
            AverageWaitingTime();
                   
            }));
            t2.Start();
        }


        private void GenerateTimeline()
        {
            int? currentTime = 0;
            int i;
            for (i = 0; i < _processes.Count; i++)
            {
                chart[i] = new FCFSChart();
                chart[i].FCFSTimeLine = currentTime.ToString();
                currentTime += _processes[i].ProcessUnits;
                chart[i].FCFSUnitsX = _processes[i].ProcessUnits10X;


            }
            chart[i] = new FCFSChart();
            chart[i].FCFSTimeLine = currentTime.ToString(); // Add end time
        }

        void AverageWaitingTime()
        {
            double? averageWaitingTime = 0;
            //for fcfs
            foreach (var fc in FCFSWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / FCFSWaitingTime.Count;
            FCFSWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for round robin
            foreach (var fc in RRWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / RRWaitingTime.Count;
            RRWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for sjf
            foreach (var fc in SJFWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / SJFWaitingTime.Count;
            SJFWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for srt
            foreach (var fc in SRTWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / SRTWaitingTime.Count;
            SRTWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for hrrn
            foreach (var fc in HRRNWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / HRRNWaitingTime.Count;
            HRRNWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for priority
            foreach (var fc in PriorityWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / PriorityWaitingTime.Count;
            PriorityWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for lottery
            foreach (var fc in LotteryWaitingTime)
            {
                averageWaitingTime += fc.Value;
            }
            averageWaitingTime = averageWaitingTime / LotteryWaitingTime.Count;
            LotteryWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
            averageWaitingTime = 0;
            //for mlq
            /* foreach (var fc in MLQWaitingTime)
             {
                 averageWaitingTime += fc.Value;
             }
             averageWaitingTime = averageWaitingTime / MLQWaitingTime.Count;
             MLQWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
             averageWaitingTime = 0;
             //for mlfq
             foreach (var fc in MLFQWaitingTime)
             {
                 averageWaitingTime += fc.Value;
             }
             averageWaitingTime = averageWaitingTime / MLFQWaitingTime.Count;
             MLFQWaitingTime.Add("Average Waiting Time: ", averageWaitingTime);
             averageWaitingTime = 0; */
        }

        void FCFS(List<Processes> proc)
        {
            //_completionTime.Clear();
            proc = proc.OrderBy(p => p.ArrivalTime).ToList();
            string keyName;
            int? currentTime = 0;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? WaitingTime = 0;
            int index = 0;

            foreach (var process in proc)
            {
                index++;
                if (currentTime < process.ArrivalTime)
                {
                    currentTime = process.ArrivalTime;
                }

                CompletionTime = currentTime + process.ProcessUnits;
                TurnaroundTime = CompletionTime - process.ArrivalTime;
                WaitingTime = TurnaroundTime - process.ProcessUnits;

                currentTime += process.ProcessUnits;
                keyName = $"P{index} Waiting Time: ";
                FCFSWaitingTime.Add(keyName, WaitingTime);
                Queue1WaitingTime.Add(keyName, WaitingTime);
                _completionTime.Add(CompletionTime);
            }

        }

        void RoundRobin(List<Processes> processes, int timeQuantum)
        {
            //_completionTime.Clear();
            string keyName;
            int? currentTime = 0;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? WaitingTime = 0;

            Queue<Processes> readyQueue = new Queue<Processes>();
            Queue<Processes> suspendedQueue = new Queue<Processes>();
            // Sort processes by arrival time
            processes = processes.OrderBy(p => p.ArrivalTime).ToList();
            // List to store remaining burst times
            List<int?> remainingBurstTimes = processes.Select(p => p.ProcessUnits).ToList();
            // Index of next process to arrive
            int nextArrivalIndex = 0;
            // Continue until all processes are completed

            while (readyQueue.Any() || nextArrivalIndex < processes.Count)
            {
            ChangeRound:
                // Add all processes that have arrived by current time to the ready queue
                while (nextArrivalIndex < processes.Count && processes[nextArrivalIndex].ArrivalTime <= currentTime)
                {
                    readyQueue.Enqueue(processes[nextArrivalIndex]);
                    nextArrivalIndex++;
                }
                if (readyQueue.Any())
                {
                    // Get the next process from the ready queue
                    Processes currentProcess = readyQueue.Dequeue();

                    // Find the index of the current process
                    int processIndex = processes.IndexOf(currentProcess);

                    // Calculate the time slice for the current process
                    int timeSlice = Math.Min(timeQuantum, (int)remainingBurstTimes[processIndex]);
                    string p = $"P{processIndex + 1}";
                    RRchart.Add(new Tuple<string?, string?, int?, string>(p, currentProcess.ProcessColor, currentTime, "#000000"));
                    // Update the current time
                    currentTime += timeSlice;

                    // Reduce the remaining burst time
                    remainingBurstTimes[processIndex] -= timeSlice;

                    // If the process is completed
                    if (remainingBurstTimes[processIndex] == 0)

                    {
                        CompletionTime = currentTime;
                        TurnaroundTime = CompletionTime - currentProcess.ArrivalTime;
                        WaitingTime = TurnaroundTime - currentProcess.ProcessUnits;
                        keyName = $"P{processIndex + 1} Waiting Time: ";
                        RRWaitingTime.Add(keyName, WaitingTime);

                        //Queue1WaitingTime.Add(keyName, WaitingTime);
                        _completionTime.Add(CompletionTime);
                        if (nextArrivalIndex < processes.Count && processes[nextArrivalIndex].ArrivalTime <= currentTime)
                        {
                            while (nextArrivalIndex < processes.Count && processes[nextArrivalIndex].ArrivalTime <= currentTime)
                            {
                                readyQueue.Enqueue(processes[nextArrivalIndex]);
                                nextArrivalIndex++;
                            }
                        }
                        else if (suspendedQueue.Any())
                        {
                            readyQueue.Enqueue(suspendedQueue.Dequeue());
                        }
                    }
                    else
                    {
                        if (nextArrivalIndex < processes.Count && processes[nextArrivalIndex].ArrivalTime <= currentTime)
                        {
                            while (nextArrivalIndex < processes.Count && processes[nextArrivalIndex].ArrivalTime <= currentTime)
                            {
                                readyQueue.Enqueue(processes[nextArrivalIndex]);
                                nextArrivalIndex++;
                            }
                            suspendedQueue.Enqueue(currentProcess);
                            goto ChangeRound;
                        }
                        // If the process is not completed, add it back to the ready queue
                        readyQueue.Enqueue(currentProcess);
                    }
                }
                else
                {
                    // If no process is in the ready queue, advance time to the next process arrival

                    currentTime = processes[nextArrivalIndex].ArrivalTime;

                }
            }
            RRchart.Add(new Tuple<string?, string?, int?, string>("", "Transparent", currentTime, "#ffffff"));

        }

        void SJF(List<Processes> processes)
        {
            string keyName;
            int? currentTime = 0;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? WaitingTime = 0;
            int index = 0;
            // Sort processes by arrival time
            processes = processes.OrderBy(p => p.ArrivalTime).ToList();

            int completedProcesses = 0;
            int n = processes.Count;
            bool[] isCompleted = new bool[n];

            while (completedProcesses < n)
            {
                //index++;
                int shortestIndex = -1;
                int? shortestBurstTime = int.MaxValue;

                for (int i = 0; i < n; i++)
                {
                    if (processes[i].ArrivalTime <= currentTime && !isCompleted[i])
                    {
                        if (processes[i].ProcessUnits < shortestBurstTime)
                        {
                            shortestBurstTime = processes[i].ProcessUnits;
                            shortestIndex = i;
                        }
                    }
                }

                if (shortestIndex != -1)
                {
                    Processes currentProcess = processes[shortestIndex];
                    index = processes.IndexOf(currentProcess);
                    string Pname = $"P{index + 1}";
                    SJFChart.Add(new(Pname, currentProcess.ProcessColor, currentTime, "#000000", currentProcess.ProcessUnits10X));
                    currentTime += currentProcess.ProcessUnits;

                    CompletionTime = currentTime;
                    TurnaroundTime = CompletionTime - currentProcess.ArrivalTime;
                    WaitingTime = TurnaroundTime - currentProcess.ProcessUnits;



                    isCompleted[shortestIndex] = true;
                    completedProcesses++;
                    keyName = $"P{index + 1} Waiting Time: ";

                    SJFWaitingTime.Add(keyName, WaitingTime);

                }
                else
                {
                    currentTime++;
                }
            }
            SJFChart.Add(new("", "Transparent", currentTime, "#ffffff", 0));
        }

        void SRT(List<Processes> processes)
        {
            string keyName;
            int? currentTime = 0;

            int n = processes.Count;
            processes = processes.OrderBy(p => p.ArrivalTime).ToList();

            //int?[] remainingBurstTimes = processes.Select(p => p.ProcessUnits).ToArray();
            //int?[] completionTimes = new int?[n];

            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? WaitingTime = 0;

            //bool[] isCompleted = new bool[n];

            for (int i = 0; i < processes.Count; i++)
            {
                processes[i].SRTRemainingTime = processes[i].ProcessUnits;
            }

            int completedProcesses = 0;

            while (completedProcesses != n)
            {
                Processes currentProcess = null;
                foreach (var process in processes)
                {
                    if (process.ArrivalTime <= currentTime && process.SRTRemainingTime > 0)
                    {
                        if (currentProcess == null || process.SRTRemainingTime < currentProcess.SRTRemainingTime)
                        {
                            currentProcess = process;
                        }
                    }
                }

                if (currentProcess != null)
                {
                    string Pname = $"P{currentProcess.ProcessNumber}";
                    SRTChart.Add(new(Pname, currentProcess.ProcessColor, currentTime, "#000000"));
                    currentTime++;
                    currentProcess.SRTRemainingTime--;

                    if (currentProcess.SRTRemainingTime == 0)
                    {
                        completedProcesses++;
                        CompletionTime = currentTime;
                        TurnaroundTime = CompletionTime - currentProcess.ArrivalTime;
                        WaitingTime = TurnaroundTime - currentProcess.ProcessUnits;
                        keyName = $"P{currentProcess.ProcessNumber} Waiting Time: ";
                        SRTWaitingTime.Add(keyName, WaitingTime);
                    }
                }
                else
                {
                    currentTime++;
                }





            }
            SRTChart.Add(new("", "Transparent", currentTime, "#ffffff"));
        }
        void HRRN(List<Processes> processes)
        {
            string keyName;
            int? currentTime = 0;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? WaitingTime = 0;
            int index = 0;

            int n = processes.Count;
            processes = processes.OrderBy(p => p.ArrivalTime).ToList();

            int completedProcesses = 0;
            bool[] isCompleted = new bool[n];

            while (completedProcesses < n)
            {
                int selectedProcessIndex = -1;
                double? highestResponseRatio = -1;

                for (int i = 0; i < n; i++)
                {
                    if (processes[i].ArrivalTime <= currentTime && !isCompleted[i])
                    {
                        int? waitingTime = currentTime - processes[i].ArrivalTime;
                        double? responseRatio = (double)(waitingTime + processes[i].ProcessUnits) / processes[i].ProcessUnits;

                        if (responseRatio > highestResponseRatio)
                        {
                            highestResponseRatio = responseRatio;
                            selectedProcessIndex = i;
                        }
                    }
                }

                if (selectedProcessIndex != -1)
                {
                    Processes currentProcess = processes[selectedProcessIndex];
                    HRRNchart.Add(new("P" + currentProcess.ProcessNumber, currentProcess.ProcessUnits10X, currentTime, currentProcess.ProcessColor, "#000000"));
                    currentTime += currentProcess.ProcessUnits;
                    index = processes.IndexOf(currentProcess);

                    CompletionTime = currentTime;
                    TurnaroundTime = CompletionTime - currentProcess.ArrivalTime;
                    WaitingTime = TurnaroundTime - currentProcess.ProcessUnits;

                    isCompleted[selectedProcessIndex] = true;
                    completedProcesses++;

                    keyName = $"P{index + 1} Waiting Time: ";
                    HRRNWaitingTime.Add(keyName, WaitingTime);
                }
                else
                {
                    currentTime++;
                }
            }
            HRRNchart.Add(new("", 0, currentTime, "Transparent", "#ffffff"));
        }

        void PriorityScheduling(List<Processes> processes)
        {
            string keyName;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? WaitingTime = 0;
            int n = processes.Count;
            int index = 0;
            processes = processes.OrderBy(p => p.ArrivalTime).ToList();

            int? currentTime = 0;
            int completedProcesses = 0;
            bool[] isCompleted = new bool[n];

            while (completedProcesses < n)
            {
                int highestPriorityIndex = -1;
                int? highestPriority = int.MaxValue;

                for (int i = 0; i < n; i++)
                {
                    if (processes[i].ArrivalTime <= currentTime && !isCompleted[i] && processes[i].Priority < highestPriority)
                    {
                        highestPriority = processes[i].Priority;
                        highestPriorityIndex = i;
                    }
                }

                if (highestPriorityIndex != -1)
                {
                    Processes currentProcess = processes[highestPriorityIndex];
                    PriorityChart.Add(new("P" + currentProcess.ProcessNumber, currentProcess.ProcessUnits10X, currentTime, currentProcess.ProcessColor, "#000000"));
                    currentTime += currentProcess.ProcessUnits;
                    index = processes.IndexOf(currentProcess);

                    CompletionTime = currentTime;
                    TurnaroundTime = CompletionTime - currentProcess.ArrivalTime;
                    WaitingTime = TurnaroundTime - currentProcess.ProcessUnits;

                    isCompleted[highestPriorityIndex] = true;
                    completedProcesses++;

                    keyName = $"P{index + 1} Waiting Time: ";
                    PriorityWaitingTime.Add(keyName, WaitingTime);
                }
                else
                {
                    currentTime++;
                }
            }
            PriorityChart.Add(new("", 0, currentTime, "Transparent", "#ffffff"));
        }
        #region Cancelled
        void MLQ(List<Processes> processes, int timeQuantum)
        {
            List<Processes> systemQueue = processes.Where(p => p.Priority == 0).OrderBy(p => p.ArrivalTime).ToList();
            List<Processes> userQueue = processes.Where(p => p.Priority == 1).OrderBy(p => p.ArrivalTime).ToList();

            _completionTime.Clear();
            // Apply Round Robin scheduling to the system queue
            RoundRobin(systemQueue, timeQuantum);

            // Apply FCFS scheduling to the user queue
            FCFS(userQueue);

            // Merge the results
            List<Processes> allProcesses = new List<Processes>();
            allProcesses.AddRange(systemQueue);
            allProcesses.AddRange(userQueue);

            //allProcesses = allProcesses.OrderBy(_completionTime[]).ToList();
            allProcesses = allProcesses.OrderBy(p => _completionTime[p.ProcessNumber]).ToList();

            // Display the results
            //DisplayResults(allProcesses);
        }

        void MLFQ(List<Processes> processes, int numQueues, int[] timeQuantums)
        {
            int? RemainingTime = 0;
            int? WaitingTime = 0;
            string keyName;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            // Initialize queues
            List<Queue<Processes>> queues = new List<Queue<Processes>>();
            for (int i = 0; i < numQueues; i++)
            {
                queues.Add(new Queue<Processes>());
            }

            // Initialize remaining time for each process
            foreach (var process in processes)
            {
                RemainingTime = process.ProcessUnits.HasValue ? process.ProcessUnits.Value : 0;
            }

            int currentTime = 0;
            int completedProcesses = 0;
            int n = processes.Count;

            // Add processes to the first queue based on their arrival time
            foreach (var process in processes.Where(p => p.ArrivalTime == 0))
            {
                queues[0].Enqueue(process);
            }

            while (completedProcesses < n)
            {
                bool foundProcess = false;

                for (int i = 0; i < numQueues; i++)
                {
                    if (queues[i].Count > 0)
                    {
                        var process = queues[i].Dequeue();
                        foundProcess = true;

                        int quantum = timeQuantums[i];
                        int executionTime = Math.Min((byte)RemainingTime, quantum);

                        RemainingTime -= executionTime;
                        currentTime += executionTime;

                        // Update waiting time for all processes in all queues
                        foreach (var queue in queues)
                        {
                            foreach (var p in queue)
                            {
                                WaitingTime += executionTime;
                            }
                        }

                        // If process is completed
                        if (RemainingTime == 0)
                        {
                            CompletionTime = currentTime;
                            TurnaroundTime = CompletionTime - (process.ArrivalTime.HasValue ? process.ArrivalTime.Value : 0);
                            completedProcesses++;

                            keyName = $"P{completedProcesses} Waiting Time: ";
                            MLFQWaitingTime.Add(keyName, WaitingTime);
                        }
                        else
                        {
                            // Downgrade to the next queue if it exists
                            if (i < numQueues - 1)
                            {
                                queues[i + 1].Enqueue(process);
                            }
                            else
                            {
                                queues[i].Enqueue(process); // Re-enqueue in the same queue if it's the last one
                            }
                        }

                        break; // Move to the next time unit
                    }
                }

                if (!foundProcess)
                {
                    currentTime++;
                    foreach (var process in processes.Where(p => p.ArrivalTime == currentTime))
                    {
                        queues[0].Enqueue(process);
                    }
                }
            }

        }
        #endregion


        void LotteryScheduling(List<Processes> processes)
        {
            Random rand = new Random();
            int time = 0;
            int completedProcesses = 0;
            int n = processes.Count;

            int? WaitingTime = 0;
            string keyName;
            int? CompletionTime = 0;
            int? TurnaroundTime = 0;
            int? RemainingTime = 0;

            for (int i = 0; i<processes.Count; i++)
            {
                processes[i].SRTRemainingTime = processes[i].ProcessUnits;
            }

            while (completedProcesses < n)
            {
                // Filter processes that have arrived and are not completed
                var availableProcesses = processes.Where(p => p.ArrivalTime <= time && p.SRTRemainingTime > 0).ToList();

                if (availableProcesses.Count == 0)
                {
                    time++;
                    continue;
                }

                // Calculate total number of tickets
                int? totalTickets = availableProcesses.Sum(p => p.Tickets);

                // Draw a random ticket
                int? winningTicket = rand.Next(1, (int)totalTickets + 1);
                int? currentSum = 0;
                Processes selectedProcess = null;

                foreach (var process in availableProcesses)
                {
                    currentSum += process.Tickets;
                    if (currentSum >= winningTicket)
                    {
                        selectedProcess = process;
                        break;
                    }
                }

                
                // Execute the selected process for one unit of time
                selectedProcess.SRTRemainingTime--;
                LotteryChart.Add(new("P" + selectedProcess.ProcessNumber, time, selectedProcess.ProcessColor, "#000000"));
                time++;

                // If process is completed
                if (selectedProcess.SRTRemainingTime == 0)
                {
                    CompletionTime = time;
                    TurnaroundTime = CompletionTime - selectedProcess.ArrivalTime;
                    WaitingTime = TurnaroundTime - selectedProcess.ProcessUnits;
                    completedProcesses++;
                    LotteryWaitingTime.Add("P" + selectedProcess.ProcessNumber+" Waiting Time: ", WaitingTime);
                }
            }

            LotteryChart.Add(new("", time, "Transparent", "#ffffff"));
        }
    }
}

