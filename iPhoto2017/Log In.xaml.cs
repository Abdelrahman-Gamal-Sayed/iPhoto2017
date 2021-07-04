using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Log_In.xaml
    /// </summary>
    public partial class Log_In : Window
    {
        public Log_In()
        {
            try
            {
                InitializeComponent();
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }catch(Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }
        DB db = new DB();
  

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    DataTable s = db.RunReader("select * from USER where NAME = '" + txtEName.Text + "'");
                    if (s.Rows.Count <= 0)
                        MessageBox.Show("Please check your Name");
                    else
                    {
                        if (s.Rows[0][2].ToString() != passbox.Password.ToString())
                            MessageBox.Show("Please check your Password");
                        else
                        {
                            USER.ID = s.Rows[0][0].ToString();
                            USER.NAME = s.Rows[0][1].ToString();
                            USER.TYPE = s.Rows[0][3].ToString();
                            iphoto2017 a = new iphoto2017();
                            a.Show();
                            this.Close();
                        }

                    }
                }
            }catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
