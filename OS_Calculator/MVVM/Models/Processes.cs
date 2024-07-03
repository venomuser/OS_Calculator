using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class Processes
    {
		private int? processUnits; // variable of burst time. It is amount of execution time

		public int? ProcessUnits // property of burst time. It is amount of execution time
        {
			get { return processUnits; }
			set { processUnits = value; }
		}

		public int? ProcessUnits10X { get { return processUnits*10; } }

		// when the processes arrives
		private int? arrivalTime;

		public int? ArrivalTime
		{
			get { return arrivalTime; }
			set { arrivalTime = value; }
		}


		//The priority of each process
		private int? priority;

		public int? Priority
		{
			get { return priority; }
			set { priority = value; }
		}

		private int processNumber;

		public int ProcessNumber
		{
			get { return processNumber; }
			set { processNumber = value; }
		}

		//Number of memory blocks

		private int? numberOfBlocks;

        

        public int? NumberOfBlocks
		{
			get { return numberOfBlocks; }
			set { numberOfBlocks = value; }
		}

        public List<int?> BlockSizesMB { get; set; } = new List<int?>(); // Sizes of each memory block


		private string?processColor;

		public string? ProcessColor
		{
			get { return processColor; }
			set { processColor = value; }
		}

		public int? SRTRemainingTime {  get; set; }

		private int? tickets;

		public int? Tickets // for Lottery algorithm
		{
			get { return tickets; }
			set { tickets = value; }
		}

		public string ProcessName { get => "P" + processNumber; }


	}
}
