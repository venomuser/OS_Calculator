using OS_Calculator.MVVM.Models;
using OS_Calculator.MVVM.ViewModels;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace OS_Calculator.MVVM.Pages;

public partial class MemoryAllocation2 : ContentPage
{
    private List<Processes> _Processes;
	public MemoryAllocation2(List<Processes> processes)
	{
		InitializeComponent();
        _Processes = processes;
		BindingContext = new MemoryAllocation2ViewModel(processes);
	}

    private void ButtonPrevious_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void ButtonInit_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.Common.NumericEntryOnChange(sender, e);
    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {
        try
        {
            //int EntryCount = CountEntries(this);
            bool IsRemainingIndex = true;
            List<double?> entryValues = new List<double?>();
            int BlockIndex = 0;
           
          
                
                foreach (var view in this.GetVisualTreeDescendants())
                {
                    if (view is Entry entry)
                    {
                        
                        entryValues.Add(Convert.ToDouble(entry.Text));
                        

                    }
                }
            // -------------------------- adding to the list ----------------------------------------

            for (int i = 0; i < entryValues.Count; i++)
            {
                foreach( var list in _Processes)
                {
                    if (BlockIndex > entryValues.Count - 1)
                    {
                        IsRemainingIndex = false;
                        break;
                    }
                    list.BlockSizesMB.Clear();
                    if (i > list.NumberOfBlocks - 1)
                        break;

                    

                    for (int j = 0; j < list.NumberOfBlocks; j++)
                    {
                        if (BlockIndex > entryValues.Count - 1)
                        {
                            IsRemainingIndex = false;
                            break;
                        }
                        list.BlockSizesMB.Add(Convert.ToDouble(entryValues[BlockIndex]));
                        BlockIndex++;
                    }
                    if (!IsRemainingIndex)
                        break;
                }
                if (!IsRemainingIndex)
                    break;

            }
          
            //------------------------------- the validation part ---------------------------------------

            bool IsReady = true;
            //App.Current.MainPage.
            foreach (var proc in _Processes)
            {
                foreach (var ls in proc.BlockSizesMB)
                {
                    if (ls > 2000 || ls <= 0 || ls == null)
                    {
                        IsReady = false;
                        DisplayAlert("Error", "Block Sizes cannot be greater than 2GB or be 0 or be null!", "OK");
                        break;
                    }
                }
                if (!IsReady)
                    break;

                IsReady = true;
            }
            //IsReady = true;
            if (IsReady)
            {
                Navigation.PushModalAsync(new MemoryProperties(_Processes));
                //var resultsViewModel = new ResultsViewModel(Processes);
                //await Application.Current.MainPage.Navigation.PushAsync(new ResultsPage { BindingContext = resultsViewModel });
            }



        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Block Sizes cannot be greater than 2GB or be 0 or be null!", "OK");
        }
    }
    /*
    int CountEntries(Page page)
    {
        int entryCount = 0;
        foreach (var view in page.GetVisualTreeDescendants())
        {
            if (view is Entry)
            {
                entryCount++;
            }
        }
        return entryCount;
    }
    */
}