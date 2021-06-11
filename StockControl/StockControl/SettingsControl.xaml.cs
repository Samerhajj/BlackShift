using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            if (!String.IsNullOrEmpty(taxBox.Text))
            {
                SettingsParams.Tax = Convert.ToDouble(taxBox.Text)/100;//tax in percentages %
            }
           
        }

        private void taxBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
        }

        private void backArrowBTN_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChooseTab(mainWindow.lastChosenPage);
            mainWindow.ListViewMenu.IsEnabled = true;
      
        }
    }
}
