using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class DynamicBlock
    {
		private string? prcoessName;

		public string? ProcessName
		{
			get { return prcoessName; }
			set { prcoessName = value; }
		}

		private int? processSize;

		public int? ProcessSize
		{
			get { return processSize; }
			set { processSize = value; }
		}

		public DynamicBlock(string? _prcoessName, int? _processSize)
		{
			ProcessName = _prcoessName;
			ProcessSize = _processSize;
		}
	}
}
