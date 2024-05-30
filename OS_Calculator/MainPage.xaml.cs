using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;

namespace OS_Calculator
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            btnCPUpage.IsEnabled = false;
        }

        private void OnNumericEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                // Ensure each character is a digit
                bool isValid = e.NewTextValue.All(char.IsDigit);

                if (!isValid)
                {
                    // Revert to the old value if new input is not valid
                    ((Entry)sender).Text = e.OldTextValue;
                }
                else
                {
                    btnCPUpage.IsEnabled = true;
                }

            }
            else
            {
                btnCPUpage.IsEnabled = false;
            }
        }

        private void btnCPUpage_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtProcessNumber.Text) > 15)
                    DisplayAlert("Error", "Your process number is a lot! Maximum is 10 processes.", "OK");
                else
                {


                    if (Convert.ToInt32(txtProcessNumber.Text) == 0)
                    {
                        DisplayAlert("Error", "The number must be greater than 0", "OK");
                    }
                    else
                    {
                        List<Processes> processes = new List<Processes>();
                        for (long i = 0; i < Convert.ToInt32(txtProcessNumber.Text); i++)
                        {
                            processes.Add(new Processes());
                        }


                        Navigation.PushModalAsync(new CPUSchedule(ref processes));


                    }
                }
            }
            catch (OverflowException ex)
            {
                DisplayAlert("Error", "Your process number is invalid! Maximum is 50 processes and minimum is 1 process.", "OK");
            }



        }
    }

}
