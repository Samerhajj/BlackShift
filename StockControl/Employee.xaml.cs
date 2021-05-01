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
    public partial class Employee : UserControl
    {
        public Employee(List<Employ> employee)
        {
            InitializeComponent();
            EmployeeGrid.ItemsSource = employee;
            this.employee = employee;
        }
        private List<Employ> employee;


        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                employee.Add(new Employ()
                {
                    ID = Convert.ToInt32(txtEmployeeID.Text),
                    Name = txtEmployeeName.Text,
                    DateOfBirth=txtDateOfBirth.Text,
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
            txtDateOfBirth.Text = "";
            txtSex.Text = "";
            txtDepartment.Text = "";
        }
    }
}
