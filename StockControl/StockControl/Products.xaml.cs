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
            List<Product> products = new List<Product>()
            {
                new Product(1, "Ashley", 3.4, 3),
                new Product(2, "Ashlsey", 32.4, 32),
                new Product(3, "Ashley", 31.4 ,3),
                new Product(4, "Ashley", 34.4, 3),
                new Product(5, "Ashley", 32.4 ,3),
                new Product(6, "Ashley", 32.4, 3),
            };
            ProductGrid.ItemsSource = products;
        }
        

        private void Grid_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
