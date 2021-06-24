using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StockControl
{
    /// <summary>
    /// Interaction logic for EditDepartment.xaml
    /// </summary>
    public partial class EditDepartment : Window
    {
        private int departmentId;
        private Grid gridPrincipal;
        private bool isSaved;

        public EditDepartment(int departmentId, Grid gridPrincipal)
        {
            InitializeComponent();
            gridPrincipal.IsEnabled = false;
            this.departmentId = departmentId;
            this.gridPrincipal = gridPrincipal;
            InitializeDepartment();
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Department newDepartment;
            try
            {
                newDepartment = new Department(txtDepartmentName.Text, Convert.ToInt32(txtEmployeeCapacity.Text), Convert.ToInt32(txtProductCapacity.Text));
                foreach (var employeeId in Data.Departments[departmentId].GetEmployeesID())
                {
                    newDepartment.AddEmployee(employeeId);
                }
                foreach (var productId in Data.Departments[departmentId].GetProducts())
                {
                    newDepartment.AddProduct(productId.Key, productId.Value);
                }
                Data.Departments.Remove(departmentId);
                Data.Departments.Add(departmentId, newDepartment);
                isSaved = true;
                this.Close();
            }
            catch (OverCapacityException ex)
            {
                var site = ex.TargetSite;
                if(site.Name == nameof(newDepartment.AddEmployee))
                {
                    MessageBox.Show($"There is more than {txtEmployeeCapacity.Text} employees in the department already.");
                }
                else
                {
                    MessageBox.Show($"There is more than {txtProductCapacity.Text} products in the department already.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//Saves the new department and closes the edit window
        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeDepartment();
        }//Resets the values of the texboxes to be the intial values
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//Closes the window (activates the OnClosing Method below)
        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
        }//Checks if the input is valid for a number
        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NameRegex.IsMatch(e.Text);
        }//Checks if the input is valid for a name

        //Extra Functions
        private void InitializeDepartment()
        {
            txtDepartmentID.Text = departmentId.ToString();
            var department = Data.Departments[departmentId];
            txtDepartmentName.Text = department.Name;
            txtEmployeeCapacity.Text = department.EmployeeCapacity.ToString();
            txtProductCapacity.Text = department.ProductCapacity.ToString();
        }//Sets all the elements to the values of the sent department
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
