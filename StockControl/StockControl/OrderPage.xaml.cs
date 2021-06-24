using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : UserControl
    {
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(Data.SnackbarMessageTime);

        public OrderPage()
        {
            InitializeComponent();
            comboBoxProducts.ItemsSource = Data.Products;
            OrdersGrid.ItemsSource = Data.Orders;
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((KeyValuePair<int, Product>?)comboBoxProducts.SelectedValue is null)
                {
                    throw new ArgumentNullException("", "The intended product was not selected.");
                }
                if (String.IsNullOrEmpty(txtOrderQuantity.Text))
                {
                    throw new ArgumentNullException("", "The intended quantity was not entered.");
                }
                var selectedProduct = (KeyValuePair<int, Product>)comboBoxProducts.SelectedValue;
                var quantity = Convert.ToInt32(txtOrderQuantity.Text);
                var order = new Order(
                    selectedProduct.Key,
                    selectedProduct.Value,
                    quantity);
                Data.Departments[selectedProduct.Value.DepartmentID].AddQuantity(selectedProduct.Key, quantity);

                Data.Orders.Add(order);
                ClearUI();
                ExecuteMessage("Product ordered successfully.");
            }
            catch (Exception ex)
            {
                ExecuteMessage(ex.Message);
            }
        }//Makes a new order
        private void txtOrderQuantity_CheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
            if (e.Handled && !sbNotification.IsActive)
                ExecuteMessage($"Quantity can include numbers only");
        }//checks if the input is valid for a number

        //Extra Functions
        private void ClearUI()
        {
            txtOrderQuantity.Text = string.Empty;
            comboBoxProducts.SelectedItem = null;
        }//Clears the input UI
        private void ExecuteMessage(string message)
        {
            sbNotification.MessageQueue = messageQueue;
            sbNotification.MessageQueue.Enqueue(message);
        }//Notifies the user of a specified message
    }
}
