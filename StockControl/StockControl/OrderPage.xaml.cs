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
        
        public OrderPage(ObservableDictionary<int,Product> products)
        {
            InitializeComponent();
            comboBoxProducts.ItemsSource = products;
            OrdersGrid.ItemsSource = orders;
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KeyValuePair<int, Product> selectedProduct = (KeyValuePair<int, Product>)comboBoxProducts.SelectedValue;
                orders.Add(new Order(
                    Convert.ToInt32(txtOrderQuantity.Text),
                    selectedProduct.Value.BuyingPriceWithTax,
                    selectedProduct.Key,
                    selectedProduct.Value.Name));
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
            txtOrderQuantity.Text = "";
            comboBoxProducts.SelectedItem = null; 
        }
    }
}
