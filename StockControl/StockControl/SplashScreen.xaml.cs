using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
       

        public SplashScreen()
        {
            Thread.Sleep(300);
            InitializeComponent();
           
            Loading();
        }
        DispatcherTimer timer = new DispatcherTimer();
        SoundPlayer soundPlayer = new SoundPlayer();

        private void Media_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void timer_tick(object sender,EventArgs e)
        {
            timer.Stop();
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
        void Loading()
        {
            timer.Tick += timer_tick;
            timer.Interval = TimeSpan.FromMilliseconds(1350);
            timer.Start();
        }
    }
}
