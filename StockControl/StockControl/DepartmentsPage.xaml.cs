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
    /// Interaction logic for DepartmentsPage.xaml
    /// </summary>
    public partial class DepartmentsPage : UserControl
    {
        List<int> selectedDepartments = new List<int>();
        //ObservableDictionary<int, Department> departments;
        MainWindow main;
        public DepartmentsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            DepartmentGrid.ItemsSource = mainWindow.departments;
            //departments = mainWindow.departments;
            main = mainWindow;
        }

        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                main.departments.Add(Convert.ToInt32(txtDepartmentID.Text), new Department()
                {
                    Name = txtDepartmentName.Text,
                    EmployeeCapacity = Convert.ToInt32(txtEmployeeCapacity.Text),
                    ProductCapacity = Convert.ToInt32(txtProductCapacity.Text)
                });
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

        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is nothing selected.
            if (selectedDepartments.Count < 1)
            {
                MessageBox.Show("There is no selected departments to delete.", "No departments selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result;
                //Checks if there is one or more employees and display the apropriate MessageBox.
                result = (selectedDepartments.Count == 1) ? MessageBox.Show($"Are you sure you want to permanently delete this department ?", "Delete one department", MessageBoxButton.YesNo)
                : MessageBox.Show($"Are you sure you want to permanently delete {selectedDepartments.Count} departments ?", "Delete multiple departments", MessageBoxButton.YesNo);

                //Checks the result of the MessageBox.
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var deparmentID in selectedDepartments)
                    {
                        foreach (var employeeID in main.departments[deparmentID].GetEmployeesID())
                        {
                            main.employees.Remove(employeeID);
                        }
                        foreach (var productID in main.departments[deparmentID].GetProducts())
                        {
                            main.products.Remove(productID.Key);
                        }
                        main.departments.Remove(deparmentID);
                    }
                    ClearData();
                }
            }
        }
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedDepartments.Add((int)((CheckBox)sender).DataContext);
        }
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedDepartments.Remove((int)((CheckBox)sender).DataContext);
        }

        //Extra Functions
        private void ClearUi()
        {
            txtDepartmentID.Text = "";
            txtDepartmentName.Text = "";
            txtEmployeeCapacity.Text = "";
            txtProductCapacity.Text = "";
        }
        private void ClearData()
        {
            selectedDepartments.Clear();
        }
    }
}
