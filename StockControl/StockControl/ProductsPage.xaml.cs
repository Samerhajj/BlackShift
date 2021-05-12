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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class ProductsPage : UserControl
    {
        private Product currentProduct = new Product();
        List<Product> products;

        public ProductsPage(List<Product> products)
        {
            InitializeComponent();
          
            ProductGrid.ItemsSource = products;
            this.products = products;
        }
      
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                products.Add(new Product()
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    Price = Convert.ToDouble(txtPriceNoTax.Text),
                    PriceTax = currentProduct.PriceWithTax(Convert.ToDouble(txtPriceNoTax.Text))
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ProductGrid.ItemsSource = null;
            ProductGrid.ItemsSource = products;
            ClearUi();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem != null)
            {


                ProductGrid.Items.Remove(ProductGrid.SelectedItem);
            }
        }
        private void ClearUi()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPriceNoTax.Text = "";
        }
    }
}
