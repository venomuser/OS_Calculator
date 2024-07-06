using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class StaticBlock
    {
		private double? blockSize;

		public double? BlockSize
		{
			get { return blockSize; }
			set { blockSize = value; }
		}

		private string? allocatedBlock;

		public string? AllocatedBlock
		{
			get { return allocatedBlock; }
			set
			{
				allocatedBlock = value;
			}
		}


	}
}
