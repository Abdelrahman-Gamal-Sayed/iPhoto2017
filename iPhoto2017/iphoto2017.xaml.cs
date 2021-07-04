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
using System.Windows.Shapes;

namespace iPhoto2017
{
    /// <summary>
    /// Interaction logic for iphoto2017.xaml
    /// </summary>
    public partial class iphoto2017 : Window
    {
        public iphoto2017()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (USER.TYPE != "admin")
                btnAdd_Copy2.Visibility = Visibility.Hidden;



            //double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            //double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            //double windowWidth = this.Width;
            //double windowHeight = this.Height;
            //this.Left = (screenWidth / 2) - (windowWidth / 2);
            //this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCust a = new AddCust();
            a.ShowDialog();

        }

        private void btnAdd_Copy_Click(object sender, RoutedEventArgs e)
        {
            Search a = new Search();
            a.ShowDialog();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Servies a = new Servies();
            a.ShowDialog();
        }

        private void btnAdd_Copy2_Click(object sender, RoutedEventArgs e)
        {
            if (USER.TYPE == "admin")
            {
                Users a = new Users();
                a.ShowDialog();
            }
            else
                btnAdd_Copy2.IsEnabled = false;


        }

        private void btntakeorder_Click(object sender, RoutedEventArgs e)
        {
            Take_Order s = new Take_Order();
            s.ShowDialog();
        }
    }
}
