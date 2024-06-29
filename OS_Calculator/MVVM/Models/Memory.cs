using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class Memory
    {
		private int memorySize;

		public int MemorySize
		{
			get { return memorySize; }
			set { memorySize = value; }
		}

		private int memoryBlocks;

		public int MemoryBlocks
		{
			get { return memoryBlocks; }
			set { memoryBlocks = value; }
		}

		public bool IsEnabled { get; set; }
		public List<double?> BlockStorage { get; set; } = new List<double?>();


	}
}
