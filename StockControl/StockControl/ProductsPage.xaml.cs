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
        List<int> selectedProductsIDs = new List<int>();
        ObservableDictionary<int,Product> products;
        ObservableDictionary<int, Department> departments;

        public ProductsPage(ObservableDictionary<int, Product> products, ObservableDictionary<int, Department> departments)
        {
            InitializeComponent();
            ProductGrid.ItemsSource = products;
            this.products = products;
            cbDepartment.ItemsSource = departments;
            this.departments = departments;
        }

        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddProduct();
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ClearUi();
            }
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(Product)((Button)sender).DataContext}");
            ClearData();
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is nothing selected.
            if (selectedProductsIDs.Count < 1)
            {
                MessageBox.Show("There is no selected products to delete.", "No products selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result;
                //Checks if there is one or more employees and display the apropriate MessageBox.
                result = (selectedProductsIDs.Count == 1) ? MessageBox.Show($"Are you sure you want to permanently delete this product ?", "Delete one product", MessageBoxButton.YesNo)
                : MessageBox.Show($"Are you sure you want to permanently delete {selectedProductsIDs.Count} products ?", "Delete multiple products", MessageBoxButton.YesNo);

                //Checks the result of the MessageBox.
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var productID in selectedProductsIDs)
                    {
                        int depId = products[productID].DepartmentID;
                        departments[depId].RemoveProduct(productID);
                        products.Remove(productID);
                    }
                    ClearData();
                }
            }
        }
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedProductsIDs.Add((int)((CheckBox)sender).DataContext);
        }
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedProductsIDs.Remove((int)((CheckBox)sender).DataContext);
        }

        //Extra Functions
        private void ClearUi()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtPriceNoTax.Text = "";
            txtBuyingPriceNoTax.Text = "";
            cbDepartment.SelectedItem = null;
        }
        private void ClearData()
        {
            selectedProductsIDs.Clear();
        }
        private void AddProduct()
        {
            int productId = Convert.ToInt32(txtID.Text);
            var department = (KeyValuePair<int, Department>)cbDepartment.SelectedItem;

            department.Value.AddProduct(productId, 0);
            Product product = new Product()
            {
                Name = txtName.Text,
                DepartmentID = department.Key,
                SellingPrice = Convert.ToDouble(txtPriceNoTax.Text),
                BuyingPrice = Convert.ToDouble(txtBuyingPriceNoTax.Text)
            };
            products.Add(productId, product);

        }
    }
}
