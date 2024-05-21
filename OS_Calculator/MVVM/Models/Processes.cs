using System;
using System.Collections.Generic;
using System.Linq;
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



	}
}
