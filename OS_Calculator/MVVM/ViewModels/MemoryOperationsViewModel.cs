using OS_Calculator.Common;
using OS_Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.MVVM.ViewModels
{
    public class MemoryOperationsViewModel
    {
        public List<Processes> _processes { get; set; }
        public Memory _memory { get; set; }
        public FirstFitBlock[] _firstFitBlock { get; set; }
        public NextFitBlock[] _nextFitBlock { get; set; }
        public BestFitBlock[] _bestFitBlock { get; set; }
        public WorstFitBlock[] _worstFitBlock { get; set; }
        public StaticBlock[] _staticBlock { get; set; }
        public int dynamicFailed { get; set; }
       public PagingClass[] _pagingClass { get; set; }
        public int Failed { get; set; }
        public int staticFailed { get; set; }
        public List<DynamicBlock> dynamicBlock { get; set; }
        private List<int?> PBlockList;
        public bool IsFF {  get; set; }
        public bool IsNF {  get; set; }
        public bool IsBF {  get; set; }
        public bool IsWF {  get; set; }
        public bool IsStaticP {  get; set; }
        public bool IsDynamicP {  get; set; }
      
        public MemoryOperationsViewModel(List<Processes> processes, Memory memory)
        {
            _processes = processes;
            _memory = memory;
            
            PBlockList = new List<int?>();
            foreach (var process in _processes)
            {
                PBlockList.AddRange(process.BlockSizesMB);
            }

            IsFF = CustomizationController.FirstFit;
            IsNF = CustomizationController.NextFit;
            IsBF = CustomizationController.BestFit;
            IsWF = CustomizationController.WorstFit;
            IsStaticP = CustomizationController.FixedPartitioning;
            IsDynamicP = CustomizationController.VariablePartitioning;

            if (IsFF == true)
            {
                FirstFit();
            }
            if (IsNF == true)
            {
                NextFit();
            }
            if (IsBF == true)
            {
                BestFit();
            }
            if (IsWF == true)
            {
                WorstFit();
            }
            if (IsStaticP == true)
            {
                StaticPartition();
            }
            if (IsDynamicP == true)
            {
                DynamicPartition();
            }

        }

        void FirstFit()
        {
            _firstFitBlock = new FirstFitBlock[_memory.BlockStorage.Count];
            for (int i = 0; i < _memory.BlockStorage.Count; i++)
            {
                _firstFitBlock[i] = new FirstFitBlock();
                _firstFitBlock[i].BlockSize = _memory.BlockStorage[i];
                _firstFitBlock[i].BlockColor = "Not Allocated";
                _firstFitBlock[i].Initial = _memory.BlockStorage[i];
            }

            Failed = 0;
            bool isAllocated = false;

            for (int j = 0; j < PBlockList.Count; j++)
            {
                for (int i = 0; i < _firstFitBlock.Length; i++)
                {
                    if (PBlockList[j] <= _firstFitBlock[i].BlockSize)
                    {
                        _firstFitBlock[i].BlockSize -= PBlockList[j];
                        _firstFitBlock[i].BlockColor = "P";
                        isAllocated = true;
                        goto Allocate;
                    }
                    else
                        isAllocated = false;
                }
            Allocate:
                if (isAllocated == false)
                {
                    Failed++;
                }
            }
        }

        void NextFit()
        {
            _nextFitBlock = new NextFitBlock[_memory.BlockStorage.Count];
            for (int s = 0; s < _memory.BlockStorage.Count; s++)
            {
                _nextFitBlock[s] = new NextFitBlock();
                _nextFitBlock[s].BlockSize = _memory.BlockStorage[s];
                _nextFitBlock[s].BlockColor = "Not Allocated";
                _nextFitBlock[s].Initial = _memory.BlockStorage[s];
            }

            Failed = 0;
            bool isAllocated = false;
            int i = 0;

            for (int j = 0; j < PBlockList.Count; j++)
            {
                if (i >= _nextFitBlock.Length)
                    i = 0;
                for (i = i; i < _nextFitBlock.Length; i++)
                {
                    if (PBlockList[j] <= _nextFitBlock[i].BlockSize)
                    {
                        _nextFitBlock[i].BlockSize -= PBlockList[j];
                        _nextFitBlock[i].BlockColor = "P";
                        isAllocated = true;
                        goto Allocate;
                    }
                    else
                        isAllocated = false;
                }
            Allocate:
                if (isAllocated == false)
                {
                    Failed++;
                }
            }

        }

        void BestFit()
        {
            _bestFitBlock = new BestFitBlock[_memory.BlockStorage.Count];
            for (int i = 0; i < _memory.BlockStorage.Count; i++)
            {
                _bestFitBlock[i] = new BestFitBlock();
                _bestFitBlock[i].BlockSize = _memory.BlockStorage[i];
                _bestFitBlock[i].BlockColor = "Not Allocated";
                _bestFitBlock[i].Initial = _memory.BlockStorage[i];
            }

            Failed = 0;
            bool isAllocated = false;
            double? smallestFit = int.MaxValue;
            double? size;

            for (int j = 0; j < PBlockList.Count; j++)
            {
                smallestFit = int.MaxValue;
                for (int i = 0; i < _bestFitBlock.Length; i++)
                {
                    if (PBlockList[j] <= _bestFitBlock[i].BlockSize)
                    {
                        size = _bestFitBlock[i].BlockSize;
                        if (size < smallestFit)
                            smallestFit = size;
                        isAllocated = true;

                    }

                }

                if (isAllocated == true)
                {
                    for (int i = 0; i < _bestFitBlock.Length; i++)
                    {
                        if (smallestFit == _bestFitBlock[i].BlockSize)
                        {
                            _bestFitBlock[i].BlockSize -= PBlockList[j];
                            _bestFitBlock[i].BlockColor = "P";
                            goto fin;
                        }
                    }
                fin:;
                }
                else
                    Failed++;
            }
        }

        void WorstFit()
        {
            _worstFitBlock = new WorstFitBlock[_memory.BlockStorage.Count];
            for (int i = 0; i < _memory.BlockStorage.Count; i++)
            {
                _worstFitBlock[i] = new WorstFitBlock();
                _worstFitBlock[i].BlockSize = _memory.BlockStorage[i];
                _worstFitBlock[i].BlockColor = "Not Allocated";
                _worstFitBlock[i].Initial = _memory.BlockStorage[i];
            }

            Failed = 0;
            bool isAllocated = false;
            double? biggestFit = int.MinValue;
            double? size;

            for (int j = 0; j < PBlockList.Count; j++)
            {
                biggestFit = int.MinValue;
                for (int i = 0; i < _worstFitBlock.Length; i++)
                {
                    if (PBlockList[j] <= _worstFitBlock[i].BlockSize)
                    {
                        size = _worstFitBlock[i].BlockSize;
                        if (size > biggestFit)
                            biggestFit = size;
                        isAllocated = true;

                    }

                }

                if (isAllocated == true)
                {
                    for (int i = 0; i < _worstFitBlock.Length; i++)
                    {
                        if (biggestFit == _worstFitBlock[i].BlockSize)
                        {
                            _worstFitBlock[i].BlockSize -= PBlockList[j];
                            _worstFitBlock[i].BlockColor = "P";
                            goto fin;
                        }
                    }
                fin:;
                }
                else
                    Failed++;
            }
        }

        void StaticPartition()
        {
            staticFailed = 0;
            bool isAllocated = false;

            _staticBlock = new StaticBlock[_memory.MemoryBlocks];
            double? _blockSize = 0;
            _blockSize = _memory.MemorySize / _memory.MemoryBlocks;
            for (int i = 0; i < _staticBlock.Length; i++)
            {
                _staticBlock[i] = new StaticBlock();
                _staticBlock[i].BlockSize = _blockSize;
            }

            for (int i = 0; i < _processes.Count; i++)
            {
                isAllocated = false;

                for (int j = 0; j < _staticBlock.Length; j++)
                {
                    if (_processes[i].ProcessSize <= _staticBlock[j].BlockSize)
                    {
                        if (_processes[i].ProcessSize == _staticBlock[j].BlockSize)
                        {
                            double? remaining = _staticBlock[j].BlockSize - _processes[i].ProcessSize;
                            _staticBlock[j].AllocatedBlock = $"P{i + 1}";
                            _staticBlock[j].BlockSize = 0;
                            isAllocated = true;

                        }
                        else
                        {
                            double? remaining = _staticBlock[j].BlockSize - _processes[i].ProcessSize;
                            _staticBlock[j].AllocatedBlock = $"P{i + 1} + {remaining}MB";
                            _staticBlock[j].BlockSize = 0;
                            isAllocated = true;

                        }
                    }
                    else if (_staticBlock[j].BlockSize == 0)
                    {

                    }
                    else
                    {
                        _staticBlock[j].AllocatedBlock = _staticBlock[j].BlockSize.ToString();
                    }
                    if (isAllocated == true && j < _staticBlock.Length)
                    {
                        int s;
                        for (s = j + 1; s < _staticBlock.Length; s++)
                        {
                            _staticBlock[s].AllocatedBlock = _staticBlock[s].BlockSize.ToString();
                        }
                        j = s;
                    }
                }
                if (isAllocated == false)
                    staticFailed++;


            }


        }

        void DynamicPartition()
        {
            dynamicFailed = 0;
            int? BlockSum = 0;
            dynamicBlock = new List<DynamicBlock> ();
            
            for(int i=0; i<_processes.Count; i++)
            {
                if (_processes[i].ProcessSize <= _memory.MemorySize)
                {
                    BlockSum += _processes[i].ProcessSize;
                    if (BlockSum <= _memory.MemorySize)
                    {
                        dynamicBlock.Add(new DynamicBlock(_processes[i].ProcessName,_processes[i].ProcessSize));
                        
                    }
                    else
                        dynamicFailed++;
                }
                else
                    dynamicFailed++;
            }
        }

        
    }
}
