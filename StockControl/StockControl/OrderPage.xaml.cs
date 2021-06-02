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

namespace StockControl
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : UserControl
    {
        ObservableCollection<Order> orders = new ObservableCollection<Order>();
        ObservableDictionary<int, Department> departments;

        public OrderPage(ObservableDictionary<int,Product> products, ObservableDictionary<int, Department> departments)
        {
            InitializeComponent();
            comboBoxProducts.ItemsSource = products;
            OrdersGrid.ItemsSource = orders;
            this.departments = departments;
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderProduct();
            }
            catch (FormatException)
            {
                _ = (txtOrderQuantity.Text.Trim() == "") ? MessageBox.Show("The intended quantity was not entered.") : MessageBox.Show("The quantity should contain numbers only.");
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("The intended product was not selected.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            ClearUI();
            }
        }

        //Extra Functions
        private void ClearUI()
        {
            txtOrderQuantity.Text = string.Empty;
            comboBoxProducts.SelectedItem = null; 
        }
        private void OrderProduct()
        {
            KeyValuePair<int, Product> selectedProduct = (KeyValuePair<int, Product>)comboBoxProducts.SelectedValue;
            int quantity = Convert.ToInt32(txtOrderQuantity.Text);
            departments[selectedProduct.Value.DepartmentID].AddQuantity(selectedProduct.Key, quantity);
            orders.Add(new Order(
                quantity,
                selectedProduct.Value.BuyingPriceWithTax,
                selectedProduct.Key,
                selectedProduct.Value.Name));
        }
    }
}
