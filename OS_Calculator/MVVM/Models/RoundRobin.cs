using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class RoundRobin
    {
		private int quantom;

		public int Quantom
		{
			get { return quantom; }
			set { quantom = value; }
		}
		public RoundRobin(int quantom)
		{
			Quantom = quantom;
		}
	}
}
