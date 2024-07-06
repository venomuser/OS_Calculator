using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.Models
{
    public class NextFitBlock
    {
        private double? blockSize;

        public double? BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        private string blockColor;

        public string BlockColor
        {
            get { return blockColor; }
            set { blockColor = value; }
        }

        private double? initial;

        public double? Initial
        {
            get { return initial; }
            set { initial = value; }
        }
    }
}
