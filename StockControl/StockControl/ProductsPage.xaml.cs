using MaterialDesignThemes.Wpf;
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
        List<int> selectedProductsID = new List<int>();
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(Data.SnackbarMessageTime);

        public ProductsPage()
        {
            InitializeComponent();
            ProductGrid.ItemsSource = Data.Products;
            cbDepartment.ItemsSource = Data.Departments;
        }
        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtID.Text))
                {
                    throw new ArgumentNullException("", "The id was not entered.");
                }
                else if ((KeyValuePair<int, Department>?)cbDepartment.SelectedItem is null)
                {
                    throw new ArgumentNullException("", "The department was not choosen.");
                }
                else if (String.IsNullOrEmpty(txtSellingPriceNoTax.Text)) 
                {
                    throw new ArgumentNullException("", "The selling price was not entered.");
                }
                else if (String.IsNullOrEmpty(txtBuyingPriceNoTax.Text))
                {
                    throw new ArgumentNullException("", "The buying price was not entered.");
                }
                int productId = Convert.ToInt32(txtID.Text);
                var department = (KeyValuePair<int, Department>)cbDepartment.SelectedItem;
                Product product = new Product(txtName.Text, department.Key, Convert.ToDouble(txtSellingPriceNoTax.Text), Convert.ToDouble(txtBuyingPriceNoTax.Text));

                if (!Data.Products.ContainsKey(productId))
                {
                    department.Value.AddProduct(productId);
                    Data.Products.Add(productId, product);
                    ClearUI();
                    ClearSelection();
                    ExecuteMessage("Product added successfully.");
                }
                else
                {
                    throw new ArgumentException("The product id is already taken.");
                }
            }
            catch (Exception ex)
            {
                if (!sbNotification.IsActive)
                    ExecuteMessage(ex.Message);
            }
        }//Adds a product
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is nothing selected.
            if (selectedProductsID.Count < 1)
            {
                MessageBox.Show("There is no selected products to delete.", "No products selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result;
                //Checks if there is one or more employees and display the apropriate MessageBox.
                result = (selectedProductsID.Count == 1) ? MessageBox.Show($"Are you sure you want to permanently delete this product ?", "Delete one product", MessageBoxButton.YesNo)
                : MessageBox.Show($"Are you sure you want to permanently delete {selectedProductsID.Count} products ?", "Delete multiple products", MessageBoxButton.YesNo);

                //Checks the result of the MessageBox.
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var productID in selectedProductsID)
                    {
                        int depId = Data.Products[productID].DepartmentID;
                        Data.Departments[depId].RemoveProduct(productID);
                        Data.Products.Remove(productID);
                    }
                    if (selectedProductsID.Count == 1)
                    {
                        ExecuteMessage("The product was deleted successfully.");
                    }
                    else ExecuteMessage($"{selectedProductsID.Count} products were deleted successfully.");
                    ClearSelection();
                }
            }
        }//Removes product/products
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var editPage = new EditProduct((int)((Button)sender).DataContext, (Grid)this.Parent);
            editPage.Show();
            ClearSelection();
        }//Edit product
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedProductsID.Add((int)((CheckBox)sender).DataContext);
            if (selectedProductsID.Count == 1)
            {
                Deletebtn.Visibility = Visibility.Visible;
                ProductGrid.CanUserSortColumns = false;
            }
        }//Product checked
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedProductsID.Remove((int)((CheckBox)sender).DataContext);
            if (selectedProductsID.Count <= 0)
            {
                Deletebtn.Visibility = Visibility.Hidden;
                ProductGrid.CanUserSortColumns = true;
            }
        }//Product unchecked
        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
            if (e.Handled && !sbNotification.IsActive)
                ExecuteMessage($"{(string)((TextBox)sender).Tag} can include numbers only");
        }//checks if the input is valid for a number and notifies if not
        private void double_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.DoubleRegex.IsMatch(e.Text);
            if(e.Text.ToString() == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
            else if (e.Handled && !sbNotification.IsActive)
                ExecuteMessage($"{(string)((TextBox)sender).Tag} can include numbers only");
        }//checks if the input is valid for a double and notifies if not
        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NameRegex.IsMatch(e.Text);
            if (e.Handled && !sbNotification.IsActive)
                ExecuteMessage($"{(string)((TextBox)sender).Tag} can't include \"{e.Text}\"");
        }//checks if the input is valid for a name and notifies if not

        //Extra Functions
        private void ClearUI()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtSellingPriceNoTax.Text = "";
            txtBuyingPriceNoTax.Text = "";
            cbDepartment.SelectedItem = null;
        }//Clears the input UI
        public void ClearSelection()
        {
            selectedProductsID.Clear();
            Deletebtn.Visibility = Visibility.Hidden;
            ProductGrid.CanUserSortColumns = true;
            ProductGrid.ItemsSource = null;
            ProductGrid.ItemsSource = Data.Products;
        }//Clears the selection and resets the grid 
        private void ExecuteMessage(string message)
        {
            sbNotification.MessageQueue = messageQueue;
            sbNotification.MessageQueue.Enqueue(message);
        }//Notifies the user of a specified message
    }
}
