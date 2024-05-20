using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class Processes
    {
		private int processUnits;

		public int ProcessUnits
		{
			get { return processUnits; }
			set { processUnits = value; }
		}

		private int arrivalTime;

		public int ArrivalTime
		{
			get { return arrivalTime; }
			set { arrivalTime = value; }
		}

	}
}
