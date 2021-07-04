using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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


namespace iPhoto2017
{
    /// <summary>
    /// Interaction logic for Servies.xaml
    /// </summary>
    public partial class Servies : Window
    {
        public Servies()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";



            AutoNume();
            filldatagservier();
            filladditemcatagory();
            fillreports();
        }
        DB db = new DB();

        DateTime froms, to;
        void fillreports()
        {
            if (asdasfrom.Text == "")
                froms = Convert.ToDateTime("2000-1-1");
            else
                froms = Convert.ToDateTime(asdasfrom.Text);


            if (asdto.Text == "")
                to = Convert.ToDateTime("3000-1-1");
            else
                to = Convert.ToDateTime(asdto.Text);


            DataTable aa = db.RunReader(@"select sum(price_servies) , count(Order_Number) from CUST_SERV where CUST_SERV.SERV_TYP_NUM like '%" + cbxsearch1.Text + "%' and CUST_SERV.SERV_NUM like '%" + cbxsearch1_Copy.Text + "%' and CUST_SERV.Created_Date between '" + froms.ToShortDateString() + "' and '"+ to.ToShortDateString() + "'");

            txtserzvtypID_Copy2.Text = aa.Rows[0][0].ToString();
            txtserzvtypID_Copy1.Text = aa.Rows[0][1].ToString();
        aa = db.RunReader(@"select distinct Order_Number  from CUST_SERV where CUST_SERV.SERV_TYP_NUM like '%" + cbxsearch1.Text + "%' and CUST_SERV.SERV_NUM like '%" + cbxsearch1_Copy.Text + "%' and CUST_SERV.Created_Date between '" + froms.ToShortDateString() + "' and '" + to.ToShortDateString() + "'");


            txtserzvtypID_Copy3.Text = aa.Rows.Count.ToString();

            dgserzvtyp1.ItemsSource = db.RunReader(@"select CUST_SERV.Order_Number,Customer.English_Name,SERVIES_TYP.ANAME,SERVIES.NAME,CUST_SERV.QTY,CUST_SERV.Price_servies,CUST_SERV.Total_Price,CUST_SERV.Created_by,CUST_SERV.Created_Date
from CUST_SERV , Customer, SERVIES_TYP, SERVIES
where SERVIES.Code = CUST_SERV.SERV_NUM
and SERVIES_TYP.Code = SERVIES.SYRVIES_TYP_NUM
and CUST_SERV.SERV_TYP_NUM = SERVIES_TYP.Code
and CUST_SERV.CUST_ID = Customer.ID
and CUST_SERV.SERV_TYP_NUM like '%" + cbxsearch1.Text + "%' and CUST_SERV.SERV_NUM like '%" + cbxsearch1_Copy.Text + "%' and CUST_SERV.Created_Date between '" + froms.ToShortDateString() + "' and '" + to.ToShortDateString() + "'").DefaultView;
        }
        private void AutoNume()
        {

            DataTable tbl = db.RunReader("select max(Code) from SERVIES_TYP ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtservtypID.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtservtypID.Text = "1";
        }
        void filldatagservier()
        {
            DataTable s = db.RunReader("select * from SERVIES_TYP");
            dgservtyp.ItemsSource = s.DefaultView;
        }

        void filladditemcatagory()
        {
            DataTable s = db.RunReader("select [Code],[ANAME] from SERVIES_TYP ");
            cbxCatagory.ItemsSource = s.DefaultView;
            cbxsearch.ItemsSource = s.DefaultView;
            cbxsearch1.ItemsSource = s.DefaultView;



        }
        private void btnservtypClear_Click(object sender, RoutedEventArgs e)
        {
            txtservtyoName.Text = "";
            AutoNume();
            dgservtyp_Copy.ItemsSource = null;
            //txtservtyoName_Copy.Text = "";
            txtservtyoName_Copy1.Text = "";
            btnservtypAdd.IsEnabled = true;
            btnServtypEdite.IsEnabled = false;
            filldatagservier();
        }

        private void dgservtyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgservtyp.SelectedItems[0];

                DataTable s = db.RunReader("select * from [SERVIES] where SYRVIES_TYP_NUM ='" + row[0].ToString() + "'");
                dgservtyp_Copy.ItemsSource = s.DefaultView;

                txtservtypID.Text = row[0].ToString();
                txtservtyoName.Text = row[1].ToString();
               // txtservtyoName_Copy.Text = row[2].ToString();
                txtservtyoName_Copy1.Text = row[2].ToString();

                btnservtypAdd.IsEnabled = false;
                btnServtypEdite.IsEnabled = true;
            }
            catch { }


        }

        private void btnservtypAdd_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"insert into [SERVIES_TYP] ([Code] ,[ANAME],[DETAILS]) values
   ('" + txtservtypID.Text + "',N'" + txtservtyoName.Text + "',N'" + txtservtyoName_Copy1.Text + "' )"," Added successfully");
            btnservtypAdd.IsEnabled = false;
            btnServtypEdite.IsEnabled = true;
            filldatagservier();
            filladditemcatagory();
        }

        private void btnServtypEdite_Click(object sender, RoutedEventArgs e)
        {

            db.RunNonQuery(@"update [SERVIES_TYP]  set  [ANAME]=N'"+ txtservtyoName .Text+ "',[DETAILS]=N'"+ txtservtyoName_Copy1 .Text+ "' where [Code]='"+ txtservtypID .Text+ "'", " Added successfully");


            filldatagservier();
            filladditemcatagory();
        }

        private void cbxCatagory_DropDownClosed(object sender, EventArgs e)
        {
            txtserzvtypID.Text = "";
            try
            {
               // MessageBox.Show(cbxCatagory.Text);
                DataTable tbl = db.RunReader("select max(Code) from [SERVIES] where SYRVIES_TYP_NUM ='" + cbxCatagory.Text + "' ");
                if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                    txtserzvtypID.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
                else
                    txtserzvtypID.Text = "1";

            }
            catch { }
        }

        private void btnszervtypClear_Click(object sender, RoutedEventArgs e)
        {
            cbxCatagory.SelectedIndex = -1;
            txtserzvtypID.Text = "";
            txztservtyoName.Text = "";
            txztservtyoName_Copy.Text = "";
            txztservtyoName_Copy1.Text = "";
            txztservtyoName_Copy2.Text = "";
            filladditemcatagory();
        }

        private void btnservztypAdd_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"insert into [SERVIES] ([Code],[NAME],[PRICE],[QUNTATY],[OPTINAL],[SYRVIES_TYP_NUM]) values
   ('" + txtserzvtypID.Text + "',N'" + txztservtyoName.Text + "','" + txztservtyoName_Copy1.Text + "','" + txztservtyoName_Copy.Text + "',N'" + txztservtyoName_Copy2.Text + "','" + cbxCatagory.Text + "' )", " Added successfully");
        }

        private void cbxsearch_DropDownClosed(object sender, EventArgs e)
        {
            DataTable s = db.RunReader("select * from [SERVIES] where SYRVIES_TYP_NUM ='" + cbxsearch.Text + "'");
            dgserzvtyp.ItemsSource = s.DefaultView;
        }

        private void dgserzvtyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgserzvtyp.SelectedItems[0];
                txtserzvtypID_Copy.Text = row[0].ToString();
                txztservtyoName_Copy3.Text = row[1].ToString();
                txztservtyoName_Copy4.Text = row[3].ToString();
                txztservtyoName_Copy5.Text = row[2].ToString();
                txztservtyoName_Copy6.Text = row[4].ToString();
            }
            catch { }

        }

        private void btnszervtypClaear_Click(object sender, RoutedEventArgs e)
        {
            txtserzvtypID_Copy.Text = "";
            txztservtyoName_Copy3.Text = "";
            txztservtyoName_Copy4.Text = "";
            txztservtyoName_Copy5.Text = "";
            txztservtyoName_Copy6.Text = "";
            dgserzvtyp.ItemsSource = null;
            cbxsearch.SelectedIndex = -1;
            filladditemcatagory();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fillreports();
        }

        private void cbxsearch1_DropDownClosed(object sender, EventArgs e)
        {
            cbxsearch1_Copy.ItemsSource = db.RunReader("select * from [SERVIES] where SYRVIES_TYP_NUM ='" + cbxsearch1.Text + "'").DefaultView;
  
        }

        private void btnservztypaAdd_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update SERVIES set    [NAME] =N'" + txztservtyoName_Copy3.Text + "' , [PRICE] ='" + txztservtyoName_Copy5.Text + "' ,  [QUNTATY] ='" + txztservtyoName_Copy4.Text + "' ,     [OPTINAL]   =N'" + txztservtyoName_Copy6.Text + "' where   Code ='" + txtserzvtypID_Copy.Text + "' and SYRVIES_TYP_NUM ='" + cbxsearch.Text + "'", "Updated ");
      
        }
    }
}
