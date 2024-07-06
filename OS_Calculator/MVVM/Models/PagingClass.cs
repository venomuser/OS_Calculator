using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class PagingClass
    {
		private int pagesNumber;

		public int PagesNumber
		{
			get { return pagesNumber; }
			set { pagesNumber = value; }
		}

		private int? frameAddress;

		public int? FrameAddress
		{
			get { return frameAddress; }
			set { frameAddress = value; }
		}

		private int? pageSize;

		public int? PageSize
		{
			get { return pageSize; }
			set { pageSize = value; }
		}

		private string? pageName;

		public string? PageName
		{
			get { return pageName; }
			set { pageName = value; }
		}




	}
}
