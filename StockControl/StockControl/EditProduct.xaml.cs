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
            InitializeComponent();
            gridPrincipal.IsEnabled = false;
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
        }//Saves the new product and closes the edit window
        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeProduct();
        }//Resets the values of the texboxes to be the intial values
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//Closes the window(activates the OnClosing Method below)
        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NameRegex.IsMatch(e.Text);
        }//Checks if the input is valid for a name
        private void double_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.DoubleRegex.IsMatch(e.Text);
            if (e.Text.ToString() == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }//Checks if the input is valid for a double

        //Extra Functions
        private void InitializeProduct()
        {
            txtProductID.Text = productId.ToString();
            var product = Data.Products[productId];
            txtProductName.Text = product.Name;
            txtSellingPrice.Text = product.SellingPrice.ToString();
            txtBuyingPrice.Text = product.BuyingPrice.ToString();
            cbDepartments.SelectedItem = new KeyValuePair<int, Department>(product.DepartmentID, Data.Departments[product.DepartmentID]);
        }//Sets all the elements to the values of the sent product
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
        }//Checks if saved and notifies the user if he is leaving without saving
    }
}
