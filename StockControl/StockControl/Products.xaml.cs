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
            List<Product> products = new List<Product>();
            products.Add(new Product() { ID = 1, Name = "Ashley", Quantity = 3, Price = 3.4 });
            products.Add(new Product() { ID = 2, Name = "Ashlsey", Quantity = 32, Price = 32.4 });
            products.Add(new Product() { ID = 3, Name = "Ashley", Quantity = 3, Price = 31.4 });
            products.Add(new Product() { ID = 4, Name = "Ashley", Quantity = 3, Price = 34.4 });
            products.Add(new Product() { ID = 5, Name = "Ashley", Quantity = 3, Price = 32.4 });
            products.Add(new Product() { ID = 6, Name = "Ashley", Quantity = 3, Price = 32.4 });
            ProductGrid.ItemsSource = products;
        }
        

        private void Grid_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
