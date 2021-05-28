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

namespace StockControl
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : UserControl
    {
        ObservableDictionary<int, Employee> employees;
        List<int> selectedEmployees = new List<int>();

        public EmployeePage(ObservableDictionary<int, Employee> employees)
        {
            InitializeComponent();
            EmployeeGrid.ItemsSource = employees;
            this.employees = employees;
        }

        //Events
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employees.Add(Convert.ToInt32(txtEmployeeID.Text), new Employee()
                {
                    Name = txtEmployeeName.Text,
                    DateOfBirth = ((DateTime)calBirthDate.SelectedDate).Date,
                    Gender = txtGender.Text,
                    Department = txtDepartment.Text
                });
            }
            catch (ArgumentException)
            {
                MessageBox.Show("The employee id is already taken.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Id can only include numbers.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateData();
            ClearUi();
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is nothing selected.
            if(selectedEmployees.Count < 1)
            {
                MessageBox.Show("There is no selected employees to delete.", "No employees selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result;
                //Checks if there is one or more employees and display the apropriate MessageBox.
                result = (selectedEmployees.Count == 1) ? MessageBox.Show($"Are you sure you want to permanently delete this employee ?", "Delete one employee", MessageBoxButton.YesNo)
                : MessageBox.Show($"Are you sure you want to permanently delete {selectedEmployees.Count} employees ?", "Delete multiple employees", MessageBoxButton.YesNo);

                //Checks the result of the MessageBox.
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in selectedEmployees)
                    {
                        employees.Remove(item);
                    }
                    UpdateData();
                }
            }
        }
        private void DatePopup_Click(object sender, RoutedEventArgs e)
        {
            popupDate.IsOpen = true;
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(Employee)((Button)sender).DataContext}");
            UpdateData();
        }
        private void selectCb_Checked(object sender, RoutedEventArgs e)
        {
            selectedEmployees.Add((int)(((CheckBox)sender).DataContext));
        }
        private void selectCb_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedEmployees.Remove((int)(((CheckBox)sender).DataContext));
        }

        //Extra Functions
        private void ClearUi()
        {
            txtEmployeeID.Text = "";
            txtEmployeeName.Text = "";
            calBirthDate.SelectedDate = DateTime.Today;
            calBirthDate.DisplayDate = DateTime.Today;
            txtGender.Text = "";
            txtDepartment.Text = "";
        }
        private void UpdateData()
        {
            //EmployeeGrid.ItemsSource = null;
            //EmployeeGrid.ItemsSource = employees;
            selectedEmployees.Clear();
        }
    }
}
