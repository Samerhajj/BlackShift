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
using System.Threading;
namespace StockControl
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        bool isLogged = false;
        //why are we using async/await:
        //when turning a method into async it allows us to write the code as a sequence of statements
        //the compiler waits for a task (where the await keyword is written) before jumping to the next line of code.
        //async/await is good in this case because it runs the task and wait for it to finish (on a different Thread).
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (isLogged != true)
            {
                if (txtPassword.Password.ToLower() == "admin" && txtUsername.Text.ToLower() == "admin")
                {

                    isLogged = true;
                    login_popup.IsOpen = true;
                    //tasks normaly includes a different Thread than the main thread,
                    //there for we had to add asyc/await to force the main thread to wait for the task to finish.
                    //The Lambda operator => separates the input parameters on the left side from the lambda body on the right side.
                    await Task.Run(() =>
                    {
                        Thread.Sleep(2000);
                    });
                    MessageBox.Show("Admin Confirmed, enjoy your stay");

                    Window mainWindow = new Window();
                    mainWindow.Content = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password,\nPlease use admin as username and password", "Incorrecnt Input", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    txtUsername.Text = "";
                    txtPassword.Clear();
                }
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Clear();
        }

        private void txtPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter )
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
