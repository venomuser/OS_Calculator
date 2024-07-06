using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.Pages;

namespace OS_Calculator.MVVM.Popups;

public partial class MemoryBlocksSizesPopup : Popup
{
    Memory _memory { get; set; }
    List<Processes> Processes { get; set; }
   
    public MemoryBlocksSizesPopup(Memory memory, List<Processes> processes)
    {
        InitializeComponent();
        _memory = memory;
        Processes = processes;
        btnResult.IsEnabled = false;

        // Initialize number of Entries in the popup


    }

    private void ButtonResult_Clicked(object sender, EventArgs e)
    {
        try
        {
           
           
            List<double?> entryValues = new List<double?>();
            double? size = 0;



            foreach (var view in this.GetVisualTreeDescendants())
            {
                if (view is Entry entry)
                {
                    if (Convert.ToDouble(entry.Text) <= 0 || Convert.ToDouble(entry.Text) > 100000)
                    {
                        lblError.Text = "Error! You should input valid value to the entries!";
                        lblError.IsVisible = true;
                        goto Finish;
                        
                    }
                    else
                    {
                        entryValues.Add(Convert.ToDouble(entry.Text));
                        
                    }


                }
            }
            foreach (var block in entryValues)
            {
                size += block;
            }
            if (size > _memory.MemorySize)
            {
                lblError.Text = "Error! Blocks Sizes are bigger than memory size!";
                lblError.IsVisible = true;
                goto Finish;
            }
            // -------------------------- adding to the list ----------------------------------------
            _memory.BlockStorage.Clear();
            foreach (var item in entryValues)
            {     
                _memory.BlockStorage.Add(item);
            }
            
            
            Close();
            
            App.Current.MainPage = new NavigationPage(new ResultPage(Processes,_memory));
        //App.Current.MainPage.Navigation.PushModalAsync(new ResultPage(Processes));

        Finish:;
           
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.IsVisible = true;
        }
    }

    private void OnNumericEntryChanged(object sender, TextChangedEventArgs e)
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
                btnResult.IsEnabled = true;
            }

        }

       
        else
        {
            btnResult.IsEnabled = false;
        }
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}