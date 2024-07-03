
using Microsoft.Extensions.DependencyInjection;
using OS_Calculator.MVVM.Pages;
using OS_Calculator.MVVM.ViewModels;
namespace OS_Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

      
            MainPage = new FirstPage();
        }

        
    }
}
