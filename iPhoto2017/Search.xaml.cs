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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
            start();

        }

        DB db = new DB();
        DataTable dt;
        void start()
        {
             dt = db.RunReader("select * from Cust order by NUM");
            dgCust.ItemsSource = dt.DefaultView;
        }

    

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
     

                dt = db.RunReader("select * from Cust where NUM  like '%" + txtSearch.Text + "%'  or ENAME like '%" + txtSearch.Text + "%' or PHONE like '%" + txtSearch.Text + "%' or ORDERTYPE like '%" + txtSearch.Text + "%' or PICE like '%" + txtSearch.Text + "%' or DATEE like '%" + txtSearch.Text + "%' or MAIL  like '%" + txtSearch.Text + "%'  order by NUM");
                dgCust.ItemsSource = dt.DefaultView;
        }

        private void dgCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgCust.SelectedItems[0];
                txtNum.Text = row[0].ToString();
                txtAName.Text = row[1].ToString();
                txtPhone.Text = row[2].ToString();
                txtOrder.Text = row[3].ToString();
                TxtPrice.Text = row[4].ToString();
                dpdate.Text = row[5].ToString();
                txtMail.Text = row[6].ToString();
            }
            catch { }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
          db.RunNonQuery("update Cust set    ENAME =N'" + txtAName.Text + "' , PHONE ='" + txtPhone.Text + "' , ORDERTYPE =N'" + txtOrder.Text + "' , PICE ='" + TxtPrice.Text + "' ,  MAIL ='" + txtMail.Text + "'  where   NUM ='"+txtNum.Text+"'","Updated ");
           
            txtAName.Text = "";
            txtPhone.Text = "";
            dpdate.Text = "";
            txtOrder.Text = "";
            TxtPrice.Text = "";
            txtMail.Text = "";
            txtSearch.Text = "";
            txtNum.Text = "";
            start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            start();
            txtAName.Text = "";
            txtPhone.Text = "";
            dpdate.Text = "";
            txtOrder.Text = "";
            TxtPrice.Text = "";
            txtMail.Text = "";
            txtSearch.Text = "";
            txtNum.Text = "";
        }
    }
}
