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
        List<Department> departments = new List<Department>();
        public DepartmentsPage()
        {
            InitializeComponent();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                departments.Add(new Department()
                {
                    ID = Convert.ToInt32(txtDepartmentID.Text),
                    Name = txtDepartmentName.Text
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateDatagrid();
            ClearUi();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ClearUi()
        {
            txtDepartmentID.Text = "";
            txtDepartmentName.Text = "";
            txtEmployeeCapacity.Text = "";
            txtProductCapacity.Text = "";
        }

        private void UpdateDatagrid()
        {
            DepartmentGrid.ItemsSource = null;
            DepartmentGrid.ItemsSource = departments;
        }
    }
}
