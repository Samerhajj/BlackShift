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
    /// Interaction logic for DepartmentsPage.xaml
    /// </summary>
    public partial class DepartmentsPage : UserControl
    {
        List<int> selectedDepartments = new List<int>();
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(Data.SnackbarMessageTime);

        public DepartmentsPage()
        {
            InitializeComponent();
            DepartmentGrid.ItemsSource = Data.Departments;
        }

        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtDepartmentID.Text))
                {
                    throw new ArgumentNullException("", "The department id was not entered.");
                }
                else if(String.IsNullOrEmpty(txtEmployeeCapacity.Text))
                {
                    throw new ArgumentNullException("", "The employee capacity was not entered.");
                }
                else if (String.IsNullOrEmpty(txtProductCapacity.Text))
                {
                    throw new ArgumentNullException("", "The product capacity was not entered.");
                }

                var departmentId = Convert.ToInt32(txtDepartmentID.Text);
                var department = new Department(txtDepartmentName.Text, Convert.ToInt32(txtEmployeeCapacity.Text), Convert.ToInt32(txtProductCapacity.Text));
                if (!Data.Departments.ContainsKey(departmentId))
                {
                    Data.Departments.Add(departmentId, department);
                    ClearSelection();
                    ClearUI();
                    ExecuteMessage("Department added successfully.");
                }
                else
                {
                    throw new ArgumentException("The department id is already taken.");
                }
            }
            catch (Exception ex)
            {
                if (!sbNotification.IsActive)
                    ExecuteMessage(ex.Message);
            }
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
                        foreach (var employeeID in Data.Departments[deparmentID].GetEmployeesID())
                        {
                            Data.Employees.Remove(employeeID);
                        }
                        foreach (var productID in Data.Departments[deparmentID].GetProducts())
                        {
                            Data.Products.Remove(productID.Key);
                        }
                        Data.Departments.Remove(deparmentID);
                    }
                    if (selectedDepartments.Count == 1)
                    {
                        ExecuteMessage("The department was deleted successfully.");
                    }
                    else ExecuteMessage($"{selectedDepartments.Count} departments were deleted successfully.");
                    ClearSelection();
                }
            }
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var editPage = new EditDepartment((int)((Button)sender).DataContext, (Grid)this.Parent);
            editPage.Show();
            ClearSelection();
        }
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedDepartments.Add((int)((CheckBox)sender).DataContext);
            if (selectedDepartments.Count == 1)
            {
                Deletebtn.Visibility = Visibility.Visible;
                DepartmentGrid.CanUserSortColumns = false;
            }
        }
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedDepartments.Remove((int)((CheckBox)sender).DataContext);
            if (selectedDepartments.Count <= 0)
            {
                Deletebtn.Visibility = Visibility.Hidden;
                DepartmentGrid.CanUserSortColumns = true;
            }
        }
        //VVV Code Reuse VVV
        private void NumberCheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
            if (e.Handled && !sbNotification.IsActive)
                    ExecuteMessage($"{(string)((TextBox)sender).Tag} can include numbers only.");
        }
        private void NameCheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NameRegex.IsMatch(e.Text);
            if (e.Handled && !sbNotification.IsActive)
                ExecuteMessage($"{(string)((TextBox)sender).Tag} can't include \"{e.Text}\"");
        }

        //Extra Functions
        private void ClearUI()
        {
            txtDepartmentID.Text = "";
            txtDepartmentName.Text = "";
            txtEmployeeCapacity.Text = "";
            txtProductCapacity.Text = "";
        }
        public void ClearSelection()
        {
            selectedDepartments.Clear();
            Deletebtn.Visibility = Visibility.Hidden;
            DepartmentGrid.CanUserSortColumns = true;
            DepartmentGrid.ItemsSource = null;
            DepartmentGrid.ItemsSource = Data.Departments;
        }
        private void ExecuteMessage(string message)
        {
            sbNotification.MessageQueue = messageQueue;
            sbNotification.MessageQueue.Enqueue(message);
        }
    }
}
