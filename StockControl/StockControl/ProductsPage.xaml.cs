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

namespace StockControl
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : UserControl
    {
        private Product currentProduct = new Product();
        List<int> selectedProducts = new List<int>();
        ObservableDictionary<int,Product> products;

        public ProductsPage(ObservableDictionary<int, Product> products)
        {
            InitializeComponent();
            ProductGrid.ItemsSource = products;
            this.products = products;
        }

        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product()
                {
                    Name = txtName.Text,
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    SellingPrice = Convert.ToDouble(txtPriceNoTax.Text),
                    BuyingPrice = Convert.ToDouble(txtBuyingPriceNoTax.Text)
                };
                products.Add(Convert.ToInt32(txtID.Text), product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateData();
                ClearUi();
            }
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is nothing selected.
            if (selectedProducts.Count < 1)
            {
                MessageBox.Show("There is no selected products to delete.", "No products selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result;
                //Checks if there is one or more employees and display the apropriate MessageBox.
                result = (selectedProducts.Count == 1) ? MessageBox.Show($"Are you sure you want to permanently delete this product ?", "Delete one product", MessageBoxButton.YesNo)
                : MessageBox.Show($"Are you sure you want to permanently delete {selectedProducts.Count} products ?", "Delete multiple products", MessageBoxButton.YesNo);

                //Checks the result of the MessageBox.
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in selectedProducts)
                    {
                        products.Remove(item);
                    }
                    UpdateData();
                }
            }
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(Product)((Button)sender).DataContext}");
            UpdateData();
        }
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedProducts.Add((int)((CheckBox)sender).DataContext);
        }
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedProducts.Remove(((int)((CheckBox)sender).DataContext));
        }

        //Extra Functions
        private void ClearUi()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPriceNoTax.Text = "";
            txtBuyingPriceNoTax.Text = "";
        }
        private void UpdateData()
        {
            selectedProducts.Clear();
        }
    }
}
