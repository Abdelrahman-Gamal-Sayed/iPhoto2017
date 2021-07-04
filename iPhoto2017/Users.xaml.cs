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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            AutoNume();
            DataTable tbl = db.RunReader("select * from [USER] ");

            datagrid.ItemsSource = tbl.DefaultView;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        DB db = new DB();
        string num;
        private void AutoNume()
        {

            DataTable tbl = db.RunReader("select max([Id]) from [USER] ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                num = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                num = "1";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passbox.Password == passbox_Copy.Password)
            {

                DataTable tbl = db.RunReader("select * from [USER] where NAME ='"+txtEName.Text+"'");
                if (tbl.Rows.Count == 0)
                {
                    db.RunNonQuery(@"insert into [USER] ([Id],[NAME] ,[PASS],[TYPE]) values ('" + num + "','" + txtEName.Text + "','" + passbox.Password.ToString() + "','" + cbx.Text + "' )", " Added successfully");
                    txtEName.Text = "";
                    passbox.Password = "";
                    passbox_Copy.Password = "";
                    cbx.SelectedIndex = -1;
                    DataTable ss = db.RunReader("select * from [USER] ");

                    datagrid.ItemsSource = ss.DefaultView;
                }
                else
                    MessageBox.Show("username already exists !!");

            }
            else
                MessageBox.Show("Confirm Password Wrong !!");

            AutoNume();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                txtEName_Copy.Text = row[0].ToString();
            }
            catch { }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"DELETE FROM [USER] WHERE [Id] ='" + txtEName_Copy.Text + "'", " successfully");
            DataTable tbl = db.RunReader("select * from [USER] ");

            datagrid.ItemsSource = tbl.DefaultView;

        }
    }
}
