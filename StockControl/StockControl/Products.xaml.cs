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
    public partial class Products : UserControl
    {
        public Products()
        {
            InitializeComponent();
          
            ProductGrid.ItemsSource = products;
        }
        Product productss = new Product();

     
        private List<Product> products = new List<Product>();
      
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
      
            products.Add(new Product()
            {
                ID = Convert.ToInt32(txtID.Text),
                Name = txtName.Text,
                Quantity = Convert.ToInt32(txtQuantity.Text),
                Price = Convert.ToDouble(txtPriceNoTax.Text),
                PriceTax =productss.PriceWithTax(Convert.ToDouble(txtPriceNoTax.Text))+Convert.ToDouble(txtPriceNoTax.Text)

            }) ;

            ProductGrid.ItemsSource = null;
            ProductGrid.ItemsSource = products;
            ClearUi();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductGrid.SelectedItem;
            if(selectedItem!=null)
            {
             
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
