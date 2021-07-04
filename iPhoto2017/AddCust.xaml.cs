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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iPhoto2017
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddCust : Window
    {
        public AddCust()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            AutoNume();
           
        }


        DB db = new DB();
 
    void filldatagridCustomer()
        {
          DataTable s=  db.RunReader("select * from Customer");
            dgCust.ItemsSource = s.DefaultView;
        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void AutoNume()
        {

            DataTable tbl = db.RunReader("select max(ID) from Customer ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtNum.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtNum.Text = "1";
        }
        DateTime ss;
        private void Button_Click(object sender, RoutedEventArgs e)
        {//'2011-03-12'
            //CURRENT_TIMESTAMP 
            string gnder = "Male";
            try
            {
           ss = dpdate.SelectedDate.Value;
            }
            catch { }
            string da = ss.ToString("yyyy-MM-dd");
            if (rbFmale.IsChecked == true)
                gnder = "Female";
        
            db.RunNonQuery(@"insert into Customer (ID ,English_Name,Birth_Date,Gender,Mail,Facebook,Phone,WhatsAPP,Address,Other,Created_By,Created_Date) values
   ('" + txtNum.Text + "',N'" + txtEName.Text + "','" + da + "','" + gnder + "','" + txtmail.Text + "','" + txtfacebook.Text + "','" + txtPhone.Text + "','" + txtWhats.Text + "',N'" + txtaddress.Text + "',N'" + txtother.Text + "','" + USER.NAME + "',CURRENT_TIMESTAMP )", txtEName.Text + " Added successfully");
        }
        void Clear ()
        {
            AutoNume();

            txtEName.Text = "";
            txtPhone.Text = "";
            txtmail.Text = "";
            txtaddress.Text = "";
            //txtArabic.Text = "";
            txtWhats.Text = "";
            txtfacebook.Text = "";
            dpdate.Text = "";
            txtother.Text = "";
            rbmale.IsChecked = false;
            rbmale.IsChecked = false;


        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            add.Visibility = Visibility.Visible;
            Serch.Visibility = Visibility.Hidden;
            data.Visibility = Visibility.Hidden;
        }

        private void btnsearchandupdate_Click(object sender, RoutedEventArgs e)
        {
            add.Visibility = Visibility.Hidden;
            Serch.Visibility = Visibility.Visible;
            data.Visibility = Visibility.Hidden;

            filldatagridCustomer();
           // clearsearch();
        }

        private void btndata_Click(object sender, RoutedEventArgs e)
        {
            add.Visibility = Visibility.Hidden;
            Serch.Visibility = Visibility.Hidden;
            data.Visibility = Visibility.Visible;

            DataTable s = db.RunReader("select * from Customer");
            datadg1.ItemsSource = s.DefaultView;
        }

        private void btndata_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgCust.SelectedItem;
                SerchtxtNum.Text = row[0].ToString();
                SerchtxtENzame.Text = row[1].ToString();
                SerchtxtzPhone.Text = row[2].ToString();
                SerchtxtzPhone_Copy.Text = row[3].ToString();
                Serchtxtmail.Text = row[4].ToString();
                SerchtxtFacebook.Text = row[5].ToString();
                Serchdpdate.Text = row[6].ToString();
                if (row[7].ToString() == "Female")
                    searchrbfmal.IsChecked = true;
                else
                    searchrb.IsChecked = true;
                Serchtxtaddress.Text = row[8].ToString();
                Serchtxtother.Text = row[9].ToString();
                
            }
            catch { }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable s = db.RunReader("select * from Customer where ID like '"+ txtSearch .Text+ "%' or English_Name like '" + txtSearch.Text + "%' or Phone like '" + txtSearch.Text + "%' or WhatsAPP like '" + txtSearch.Text + "%' or Birth_Date like '" + txtSearch.Text + "%' or Gender like '" + txtSearch.Text + "%' or Other  like '" + txtSearch.Text + "%'");
            dgCust.ItemsSource = s.DefaultView;
        }
        void clearsearch()
        {
            txtSearch.Text = "";
            SerchtxtNum.Text = "";
            SerchtxtENzame.Text = "";
            SerchtxtzPhone.Text = "";
            SerchtxtzPhone_Copy.Text = "";
            SerchtxtFacebook.Text = "";
            Serchtxtmail.Text = "";
            Serchdpdate.Text = "";
            Serchtxtaddress.Text = "";
            searchrbfmal.IsChecked = false;
            searchrb.IsChecked = false;
            filldatagridCustomer();
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //CURRENT_TIMESTAMP 
            string gnder = "Male";
            DateTime ss = Serchdpdate.SelectedDate.Value;
            string da = ss.ToString("yyyy-MM-dd");
            if (searchrbfmal.IsChecked == true)
                gnder = "Female";

            db.RunNonQuery("update Customer set    English_Name =N'" + SerchtxtENzame.Text + "' , [Phone] ='" + SerchtxtzPhone.Text + "' , [WhatsAPP] ='" + SerchtxtzPhone_Copy.Text + "' , [Mail] ='" + Serchtxtmail.Text + "' ,  [Facebook] ='" + SerchtxtFacebook.Text + "' ,  [Birth_Date] ='" + da + "',  [Gender] ='" + gnder + "',  [Address] =N'" + Serchtxtaddress.Text + "',  [Other] =N'" + Serchtxtother.Text + "',  [Updated_By] ='" + USER.NAME + "',  [Updated_Date] =CURRENT_TIMESTAMP  where   ID ='" + SerchtxtNum.Text + "'", "Updated ");
            clearsearch();

        }

        private void clearsearch_cklick(object sender, RoutedEventArgs e)
        {
            clearsearch();
        }

        private void datatxtsearch_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable s = db.RunReader("select * from Customer where ID like '" + datatxtsearch.Text + "%' or English_Name like '" + datatxtsearch.Text + "%' or Phone like '" + datatxtsearch.Text + "%' or WhatsAPP like '" + datatxtsearch.Text + "%' or Birth_Date like '" + datatxtsearch.Text + "%' or Gender like '" + datatxtsearch.Text + "%' or Other  like '" + datatxtsearch.Text + "%'");
            datadg1.ItemsSource = s.DefaultView;
        }

        private void datadg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            datadg2.ItemsSource = null;
            try
            {
                DataRowView row = (DataRowView)datadg1.SelectedItem;

                DataTable ss = db.RunReader("select DISTINCT  * from CUST_SERV where CUST_ID='" + row[0].ToString() + "'");


                ss.Columns.RemoveAt(6);
                ss.Columns.RemoveAt(4);
                ss.Columns.RemoveAt(3);
                ss.Columns.RemoveAt(2);
                ss.Columns.RemoveAt(1);

                datadg2.ItemsSource = ss.DefaultView;

            }catch
            {

            }
        }

        private void datadg2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datadg2_Copy.ItemsSource = null;
            try
            {
                DataRowView row = (DataRowView)datadg2.SelectedItem;


                datadg2_Copy.ItemsSource = db.RunReader("select SERVIES_TYP.ANAME,SERVIES.NAME,CUST_SERV.QTY,CUST_SERV.Price_servies  from CUST_SERV, SERVIES_TYP ,SERVIES where SERVIES.SYRVIES_TYP_NUM=SERVIES_TYP.Code and CUST_SERV.SERV_TYP_NUM=SERVIES_TYP.Code and CUST_SERV.SERV_NUM=SERVIES.Code and CUST_SERV.Order_Number='" + row[0].ToString() + "'").DefaultView;

            }
            catch
            {

            }
        }
    }
}
