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
using System.Windows.Shapes;
using System.ComponentModel;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        private int productId;
        private Grid gridPrincipal;
        private bool isSaved;

        public EditProduct(int productId, Grid gridPrincipal)
        {
            gridPrincipal.IsEnabled = false;
            InitializeComponent();
            cbDepartments.ItemsSource = Data.Departments;
            this.productId = productId;
            this.gridPrincipal = gridPrincipal;
            InitializeProduct();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newDepartment = (KeyValuePair<int, Department>)cbDepartments.SelectedItem;
                var oldProduct = Data.Products[productId];
                var oldDepartment = Data.Departments[oldProduct.DepartmentID];
                Product newProduct = new Product(txtProductName.Text, newDepartment.Key, Convert.ToDouble(txtSellingPrice.Text), Convert.ToDouble(txtBuyingPrice.Text));

                if (newDepartment.Key != oldProduct.DepartmentID)
                {
                    newDepartment.Value.AddProduct(productId, oldDepartment.GetQuantity(productId));
                    oldDepartment.RemoveProduct(productId);
                }

                Data.Products.Remove(productId);
                Data.Products.Add(productId, newProduct);
                isSaved = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeProduct();
        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void InitializeProduct()
        {
            txtProductID.Text = productId.ToString();
            var product = Data.Products[productId];
            txtProductName.Text = product.Name;
            txtSellingPrice.Text = product.SellingPrice.ToString();
            txtBuyingPrice.Text = product.BuyingPrice.ToString();
            cbDepartments.SelectedItem = new KeyValuePair<int, Department>(product.DepartmentID, Data.Departments[product.DepartmentID]);
        }
        private void txtProductName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex Name Match
        }
        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!isSaved)
            {
                var result = MessageBox.Show("Are you sure you want to exit?\nAll the changes will not be saved.", "Exiting without saving", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    gridPrincipal.IsEnabled = true;
                    base.OnClosing(e);
                }
                else
                {
                    e.Cancel = true;
                    base.OnClosing(e);
                }
            }
            else
            {
                gridPrincipal.IsEnabled = true;
                base.OnClosing(e);
            }
        }
    }
}
