using OS_Calculator.MVVM.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OS_Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MemoryPropertiesViewModel
    {
        public List<Processes> _Processes {  get; set; }
        public Memory memory { get; set; }
        public ObservableCollection<int> BlockSizes { get; set; }

        public ICommand BlockNumberChangedCommand { get; }
        public ICommand BlockSizeChangedCommand { get; }
        public MemoryPropertiesViewModel(List<Processes> processes)
        {
            _Processes = processes;
             memory = new Memory();
            memory.IsEnabled = false;
            BlockNumberChangedCommand = new Command<string>(OnBlockNumberChanged);
            BlockSizeChangedCommand = new Command<string>(OnBlockSizeChanged);
        }

        public MemoryPropertiesViewModel()
        {
            memory = new Memory();
            memory.IsEnabled = false;
            BlockNumberChangedCommand = new Command<string>(OnBlockNumberChanged);
            BlockSizeChangedCommand = new Command<string>(OnBlockSizeChanged);
        }

        private void OnBlockNumberChanged(string newBlockNumber)
        {
            if (int.TryParse(newBlockNumber, out int numberOfBlocks))
            {
                BlockSizes.Clear();
                for (int i = 0; i < numberOfBlocks; i++)
                {
                    BlockSizes.Add(0);
                }
            }
        }

        private void OnBlockSizeChanged(string newSize)
        {
            // Handle block size changes if necessary
        }
    }
}

