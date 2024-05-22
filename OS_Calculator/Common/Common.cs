using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Calculator.Common
{
    public static class Common
    {
        public static void NumericEntryOnChange(object sender, TextChangedEventArgs e)
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
                    //btnCPUpage.IsEnabled = true;
                }

            }
            else
            {
                //btnCPUpage.IsEnabled = false;
            }
        }
    }
}
