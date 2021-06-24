using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
namespace StockControl
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(Data.SnackbarMessageTime);

        public SettingsControl(MainWindow main)
        {
            InitializeComponent();
       }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IfTextBoxesFull())
            {
                SettingsParams.Tax = Convert.ToDouble(taxBox.Text);
                SettingsParams.MaterialHandlerWage = Convert.ToDouble(mhBox.Text);
                SettingsParams.WarehouseWorkerWage = Convert.ToDouble(wwBox.Text);
                SettingsParams.WarehousePackerWage = Convert.ToDouble(wpBox.Text);
                if(!sbNotification.IsActive) 
                    ExecuteMessage("All changes saved.");
            }
            else
            {
                ExecuteMessage("Please fill all the textboxes.");
            }
        }//Saves new paramters
        private bool IfTextBoxesFull()
        {
            foreach (var item in paramStack.Children)
            {
                if (item is TextBox textBox)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        return false;
                    }
                }
            }
            return true;
        }//Checks if all the textbox aren't empty
        private void taxBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.DoubleRegex.IsMatch(e.Text);
            if (e.Text.ToString() == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }//checks if the input is valid for a double and notifies if not
        public void initializeParams()
        {
            taxBox.Text = SettingsParams.Tax.ToString();
            mhBox.Text = SettingsParams.MaterialHandlerWage.ToString();
            wwBox.Text = SettingsParams.WarehouseWorkerWage.ToString();
            wpBox.Text = SettingsParams.WarehousePackerWage.ToString();
        }//Initializes all the textboxes with the current paramters
        private void ExecuteMessage(string message)
        {
            sbNotification.MessageQueue = messageQueue;
            sbNotification.MessageQueue.Enqueue(message);
        }//Notifies the user of a specified message
    }
}
