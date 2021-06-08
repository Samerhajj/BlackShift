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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using MaterialDesignThemes.Wpf;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : UserControl
    {
        SortedSet<int> selectedEmployeesID = new SortedSet<int>();
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(Data.SnackbarMessageTime);

        public EmployeePage()
        {
            InitializeComponent();
            EmployeeDtg.ItemsSource = Data.Employees;
            cbDepartment.ItemsSource = Data.Departments;
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbEmployeeType.ItemsSource = Enum.GetValues(typeof(Data.EmployeeTypes));
        }

        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtEmployeeID.Text))
                {
                    throw new ArgumentNullException("", "The id was not entered.");
                }
                else if ((KeyValuePair<int, Department>?)cbDepartment.SelectedItem is null)
                {
                    throw new ArgumentNullException("", "The department was not choosen.");
                }
                else if ((Data.EmployeeTypes?)cbEmployeeType.SelectedItem is null)
                {
                    throw new ArgumentNullException("", "The employee type was not choosen.");
                }
                else if (birthDatePicker.SelectedDate is null)
                {
                    throw new ArgumentNullException("", "The date of birth was not choosen.");
                }

                int employeeId = Convert.ToInt32(txtEmployeeID.Text);
                var department = (KeyValuePair<int, Department>)cbDepartment.SelectedItem;

                CreateEmployeeWithType((Data.EmployeeTypes)cbEmployeeType.SelectedItem, txtEmployeeName.Text, department.Key, (string)cbGender.SelectedItem, (DateTime)birthDatePicker.SelectedDate, out Employee employee);

                if (!Data.Employees.ContainsKey(employeeId))
                {
                    department.Value.AddEmployee(employeeId);//Could throw OverCapacityException.
                    Data.Employees.Add(employeeId, employee);
                }
                else
                {
                    throw new ArgumentException("The employee id is already taken.");
                }

                ClearSelection();
                ClearUI();
                ExecuteMessage("Employee added successfully.");
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
            if (selectedEmployeesID.Count < 1)
            {
                MessageBox.Show("There is no selected employees to delete.", "No employees selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result;
                //Checks if there is one or more employees and display the apropriate MessageBox.
                result = (selectedEmployeesID.Count == 1) ? MessageBox.Show($"Are you sure you want to permanently delete this employee ?", "Delete one employee", MessageBoxButton.YesNo)
                : MessageBox.Show($"Are you sure you want to permanently delete {selectedEmployeesID.Count} employees ?", "Delete multiple employees", MessageBoxButton.YesNo);

                //Checks the result of the MessageBox.
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var empId in selectedEmployeesID)
                    {
                        int depId = Data.Employees[empId].DepartmentID;
                        Data.Departments[depId].RemoveEmployee(empId);
                        Data.Employees.Remove(empId);
                    }
                    if (selectedEmployeesID.Count == 1)
                    {
                        ExecuteMessage("The employee was deleted successfully.");
                    }
                    else ExecuteMessage($"{selectedEmployeesID.Count} Employees were deleted successfully.");
                    ClearSelection();
                }
            }
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(Employee)((Button)sender).DataContext}");
            ClearSelection();
        }
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedEmployeesID.Add((int)((CheckBox)sender).DataContext);
            if (selectedEmployeesID.Count == 1)
            {
                Deletebtn.Visibility = Visibility.Visible;
                deleteIco.Kind = PackIconKind.AccountMinus;
                EmployeeDtg.CanUserSortColumns = false;
            }
            else if (selectedEmployeesID.Count > 1)
            {
                deleteIco.Kind = PackIconKind.AccountMultipleMinus;
            }
        }
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedEmployeesID.Remove((int)((CheckBox)sender).DataContext);
            if (selectedEmployeesID.Count <= 0)
            {
                Deletebtn.Visibility = Visibility.Hidden;
                EmployeeDtg.CanUserSortColumns = true;
            }
            else if (selectedEmployeesID.Count == 1)
            {
                deleteIco.Kind = PackIconKind.AccountMinus;
            }
            else
            {
                deleteIco.Kind = PackIconKind.AccountMultipleMinus;
            }
        }
        //VVV Code Reuse VVV
        private void txtEmployeeID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
            //TextFieldAssist.SetUnderlineBrush((DependencyObject)sender, Brushes.Red);
            if(e.Handled && !sbNotification.IsActive)
                ExecuteMessage("The id must include numbers only.");

        }

        //Extra Functions
        private void ClearUI()
        {
            txtEmployeeID.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            birthDatePicker.SelectedDate = null;
            cbGender.SelectedItem = null;
            cbDepartment.SelectedItem = null;
            cbEmployeeType.SelectedItem = null;
        }
        public void ClearSelection()
        {
            selectedEmployeesID.Clear();
            Deletebtn.Visibility = Visibility.Hidden;
            EmployeeDtg.CanUserSortColumns = true;
            EmployeeDtg.ItemsSource = null;
            EmployeeDtg.ItemsSource = Data.Employees;
        }
        private void CreateEmployeeWithType(Data.EmployeeTypes employeeType, string name, int departmentId, string gender, DateTime dateOfBirth, out Employee employee)
        {
            switch (employeeType)
            {
                case Data.EmployeeTypes.WarehouseWorker:
                    employee = new WarehouseWorker(name, departmentId, gender, dateOfBirth);
                    break;
                case Data.EmployeeTypes.WarehousePacker:
                    employee = new WarehousePacker(name, departmentId, gender, dateOfBirth);
                    break;
                case Data.EmployeeTypes.MaterialHandler:
                    employee = new MaterialHandler(name, departmentId, gender, dateOfBirth);
                    break;
                default:
                    throw new ArgumentException("Employee type doesn't exist.");
            }
        }
        private void ExecuteMessage(string message)
        {
            sbNotification.MessageQueue = messageQueue;
            sbNotification.MessageQueue.Enqueue(message);
        }
    }
}
