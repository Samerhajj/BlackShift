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
using System.IO;
using System.Collections.ObjectModel;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        //Dictionary<int,Department> departments = new Dictionary<int,Department>();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        List<Product> products = new List<Product>();

        HomePage homePage;
        InventoryManagementPage managementPage;
        EmployeePage employeePage;
        ProductsPage productsPage;
        AboutUsPage aboutUsPage;

        public MainWindow()
        {
            InitializePages();
            InitializeComponent();
            MoveCursorMenu(0);
            ChooseTab(0);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            ChooseTab(index);
        }
        private void MoveCursorMenu(int index)
        {
            TransitionSlideHome.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 120 + (60 * index), 0, 0);
        }
        private void ChooseTab(int index)
        {
            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(homePage);
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(managementPage);
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(managementPage);
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(employeePage);
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(productsPage);
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(aboutUsPage);
                    break;
                default:
                    break;
            }
        }
        private void InitializePages()
        {
            homePage = new HomePage();
            managementPage = new InventoryManagementPage();
            employeePage = new EmployeePage(employees);
            productsPage = new ProductsPage(products);
            aboutUsPage = new AboutUsPage();
        }
    }
}
