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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class EmployeePage : UserControl
    {
        private List<Employee> employee;
        public EmployeePage(List<Employee> employee)
        {
            InitializeComponent();
            EmployeeGrid.ItemsSource = employee;
            this.employee = employee;

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employee.Add(new Employee()
                {
                    ID = Convert.ToInt32(txtEmployeeID.Text),
                    Name = txtEmployeeName.Text,
                    DateOfBirth=((DateTime)calBirthDate.SelectedDate).Date,
                    Sex = txtSex.Text,
                    Department = txtDepartment.Text
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            EmployeeGrid.ItemsSource = null;
            EmployeeGrid.ItemsSource = employee;
            ClearUi();
        }
    private void ClearUi()
    {
        txtEmployeeID.Text = "";
        txtEmployeeName.Text = "";
        calBirthDate.SelectedDate = DateTime.Today;
        calBirthDate.DisplayDate = DateTime.Today;
        txtSex.Text = "";
        txtDepartment.Text = "";
    }

    private void DatePopup_Click(object sender, RoutedEventArgs e)
    {
        popupDate.IsOpen = true;
    }
}
}

