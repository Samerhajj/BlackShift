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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.ToLower() == "admin" && txtUsername.Text.ToLower() == "admin")
            {
                MainPage mainpage = new MainPage();
                Window main = new Window();
                main.Content = mainpage;
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show ("Incorrect username or password,please use admin as username and password");
                txtUsername.Text = "";
                txtPassword.Clear();
            }

        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Clear();
        }
    }
}
