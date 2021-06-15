using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Data;
using System.Globalization;
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
            BindCurrency();
           
        }
        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            //add rows in the datatable with text and value
            dtCurrency.Rows.Add("--USD--","en-US");
            dtCurrency.Rows.Add("--EUR--","fr-FR");
            dtCurrency.Rows.Add("--ISR--","he-IL");

            CurrencyChanger.ItemsSource = dtCurrency.DefaultView;
            CurrencyChanger.DisplayMemberPath = "Text";
            CurrencyChanger.SelectedValuePath = "Value";
            CurrencyChanger.SelectedIndex = 0;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(taxBox.Text))
            {
                SettingsParams.Tax = Convert.ToDouble(taxBox.Text)/100;//tax in percentages %
                SettingsParams.MaterialHandlerWage = Convert.ToDouble(mhBox.Text);
                SettingsParams.WarehouseWorkerWage = Convert.ToDouble(wwBox.Text);
                SettingsParams.WarehousePackerWage = Convert.ToDouble(wpBox.Text);
                SettingsParams.Culture = new CultureInfo(CurrencyChanger.SelectedValue.ToString());
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

        private void CurrencyChanger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
