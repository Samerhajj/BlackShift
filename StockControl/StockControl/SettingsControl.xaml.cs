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
        MainWindow mainWindow;
        public SettingsControl(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main; 

       }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IfTextBoxesFull())
            {
                SettingsParams.Tax = Convert.ToDouble(taxBox.Text);
                SettingsParams.MaterialHandlerWage = Convert.ToDouble(mhBox.Text);
                SettingsParams.WarehouseWorkerWage = Convert.ToDouble(wwBox.Text);
                SettingsParams.WarehousePackerWage = Convert.ToDouble(wpBox.Text);
            }
            else
            {
                MessageBox.Show("Please fill all the textboxes.");
            }
        }

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
        }
           
        private void taxBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.DoubleRegex.IsMatch(e.Text);
            if (e.Text.ToString() == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void backArrowBTN_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChooseTab(mainWindow.lastChosenPage);
            mainWindow.ListViewMenu.IsEnabled = true;
      
        }
        public void initializeParams()
        {
            taxBox.Text = SettingsParams.Tax.ToString();
            mhBox.Text = SettingsParams.MaterialHandlerWage.ToString();
            wwBox.Text = SettingsParams.WarehouseWorkerWage.ToString();
            wpBox.Text = SettingsParams.WarehousePackerWage.ToString();
        }
    }
}
