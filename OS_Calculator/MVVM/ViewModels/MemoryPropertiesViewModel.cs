using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;
using OS_Calculator.MVVM.Popups;
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

        private Page page;
       
        public ICommand btnOK => new Command(OK_Pressed);

        public ICommand btnBack => new Command(Back_Pressed);

        public ICommand btnRoot => new Command(Root_Pressed);

        private void Root_Pressed(object obj)
        {
            App.Current.MainPage = new NavigationPage(new FirstPage());
        }

        private void Back_Pressed(object obj)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        private void OK_Pressed(object obj)
        {
            if (memory.MemorySize > 500000 || memory.MemorySize < 5 || string.IsNullOrEmpty(memory.MemorySize.ToString()))
            {
                App.Current.MainPage.DisplayAlert("Error", "Memory size should not be greater than 500 GB or Smaller than 5 MB or null!", "OK");
            }
            else if (memory.MemoryBlocks < 1 || string.IsNullOrEmpty(memory.MemoryBlocks.ToString()) || memory.MemoryBlocks > 50)
            {
                App.Current.MainPage.DisplayAlert("Error", "Number of RAM blocks should not be greater than 50 or smaller than 1 or null!", "OK");
            }
            else
            {
                memory.BlockStorage.Clear();
                for(int i=0; i<memory.MemoryBlocks; i++)
                {
                    memory.BlockStorage.Add(0);
                    
                }

                

                page.ShowPopup(new MemoryBlocksSizesPopup(memory, _Processes));
                
            }
        }

        public MemoryPropertiesViewModel(Page pg, List<Processes> processes)
        {
            page = pg;
            _Processes = processes;
             memory = new Memory();
            memory.IsEnabled = false;
            
            
        }

       

       
    }
}

