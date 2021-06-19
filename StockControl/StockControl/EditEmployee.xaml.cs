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
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        private int employeeId;
        private Grid gridPrincipal;
        private bool isSaved;

        public EditEmployee(int employeeId , Grid gridPrincipal)
        {
            gridPrincipal.IsEnabled = false;
            InitializeComponent();
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbEmployeeType.ItemsSource = Enum.GetValues(typeof(Data.EmployeeTypes));
            cbDepartments.ItemsSource = Data.Departments;
            this.employeeId = employeeId;
            this.gridPrincipal = gridPrincipal;
            InitializeEmployee(employeeId);
        }
        private void cbEmployeeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = (Data.EmployeeTypes)((ComboBox)sender).SelectedItem;
            var employee = Data.Employees[employeeId];

            if (type == employee.EmployeeType)
            {
                SetSpecialtyForEmployeeType(employee);
            }
            else
            {
                switch (type)
                {
                    case Data.EmployeeTypes.WarehouseWorker:
                        var warehouseWorker = new WarehouseWorker();
                        txtSpecialty.Text = nameof(warehouseWorker.ProductsProcessed);
                        break;
                    case Data.EmployeeTypes.WarehousePacker:
                        var warehousePacker = new WarehousePacker();
                        txtSpecialty.Text = nameof(warehousePacker.ProductsPacked);
                        break;
                    case Data.EmployeeTypes.MaterialHandler:
                        var materialHandler = new MaterialHandler();
                        txtSpecialty.Text = nameof(materialHandler.MaterialHandled);
                        break;
                    default:
                        throw new ArgumentException("Employee type doesn't exist.");
                }
                txtSpecialtyValue.Text = "0";
            }
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newDepartment = (KeyValuePair<int, Department>)cbDepartments.SelectedItem;
                Employee newEmployee;
                var oldEmployee = Data.Employees[employeeId];
                var oldDepartment = Data.Departments[oldEmployee.DepartmentID];

                switch ((Data.EmployeeTypes)cbEmployeeType.SelectedItem)
                {
                    case Data.EmployeeTypes.WarehouseWorker:
                        newEmployee = new WarehouseWorker(txtEmployeeName.Text, newDepartment.Key, cbGender.Text, oldEmployee.DateOfBirth, Convert.ToInt32(txtSpecialtyValue.Text));
                        newEmployee.Raise = Convert.ToDouble(txtRaise.Text);
                        break;
                    case Data.EmployeeTypes.WarehousePacker:
                        newEmployee = new WarehousePacker(txtEmployeeName.Text, newDepartment.Key, cbGender.Text, oldEmployee.DateOfBirth, Convert.ToInt32(txtSpecialtyValue.Text));
                        newEmployee.Raise = Convert.ToDouble(txtRaise.Text);
                        break;
                    case Data.EmployeeTypes.MaterialHandler:
                        newEmployee = new MaterialHandler(txtEmployeeName.Text, newDepartment.Key, cbGender.Text, oldEmployee.DateOfBirth, Convert.ToInt32(txtSpecialtyValue.Text));
                        newEmployee.Raise = Convert.ToDouble(txtRaise.Text);
                        break;
                    default:
                        throw new ArgumentException("Employee type doesn't exist.");
                }

                if (!(newDepartment.Key == oldEmployee.DepartmentID))
                {
                    newDepartment.Value.AddEmployee(employeeId);
                    oldDepartment.RemoveEmployee(employeeId);
                }
                Data.Employees.Remove(employeeId);
                Data.Employees.Add(employeeId,newEmployee);
                isSaved = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeEmployee(employeeId);
        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NumRegex.IsMatch(e.Text);
        }
        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.NameRegex.IsMatch(e.Text);
        }
        private void double_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Data.DoubleRegex.IsMatch(e.Text);
        }

        //Extra Functions
        //This function sets all the elements to the values of the employee.
        private void InitializeEmployee(int employeeId)
        {
            txtEmployeeID.Text = employeeId.ToString(); //this element is disabled.
            var employee = Data.Employees[employeeId];
            txtEmployeeName.Text = employee.Name;
            cbGender.SelectedItem = employee.Gender;
            birthDatePicker.SelectedDate = employee.DateOfBirth; //this element is disabled.
            cbDepartments.SelectedItem = new KeyValuePair<int,Department>(employee.DepartmentID,Data.Departments[employee.DepartmentID]);
            cbEmployeeType.SelectedItem = employee.EmployeeType;
            txtRaise.Text = employee.Raise.ToString();
            SetSpecialtyForEmployeeType(employee);
        }
        //This function changes the name and the value of the specialty for the employee according to his type.
        private void SetSpecialtyForEmployeeType(Employee employee)
        {
            if (employee is WarehousePacker warehousePacker)
            {
                txtSpecialty.Text = nameof(warehousePacker.ProductsPacked);
                txtSpecialtyValue.Text = warehousePacker.ProductsPacked.ToString();
            }
            else if (employee is WarehouseWorker warehouseWorker)
            {
                txtSpecialty.Text = nameof(warehouseWorker.ProductsProcessed);
                txtSpecialtyValue.Text = warehouseWorker.ProductsProcessed.ToString();
            }
            else if (employee is MaterialHandler materialHandler)
            {
                txtSpecialty.Text = nameof(materialHandler.MaterialHandled);
                txtSpecialtyValue.Text = materialHandler.MaterialHandled.ToString();
            }
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
