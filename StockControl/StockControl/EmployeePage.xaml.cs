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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class EmployeePage : UserControl
    {
        ObservableCollection<Employee> employees;
        public EmployeePage(ObservableCollection<Employee> employee)
        {
            InitializeComponent();
            //EmployeeGrid.ItemsSource = employee;
            this.employees = employee;
        }

        //Events
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void addEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employees.Add(new Employee()
                {
                    ID = Convert.ToInt32(txtEmployeeID.Text),
                    Name = txtEmployeeName.Text,
                    DateOfBirth = ((DateTime)calBirthDate.SelectedDate).Date,
                    Gender = txtGender.Text,
                    Department = txtDepartment.Text
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateDatagrid();
            ClearUi();
        }
        private void DatePopup_Click(object sender, RoutedEventArgs e)
        {
            popupDate.IsOpen = true;
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Tag is: {((Employee)((Button)sender).DataContext).ID}");
            UpdateDatagrid();
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

        private void UpdateDatagrid()
        {
            EmployeeGrid.ItemsSource = null;
            EmployeeGrid.ItemsSource = employees;
        }
    }
}
