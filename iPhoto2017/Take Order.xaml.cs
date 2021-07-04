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
    /// Interaction logic for Take_Order.xaml
    /// </summary>
    /// 
    
    public partial class Take_Order : Window
    {
        public Take_Order()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            autonym();
            filltyp();


       
        }
     
        DB db = new DB();

        void autonym()
        {
            DataTable tbl = db.RunReader("select max(ID) from Customer ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtNum.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtNum.Text = "1";
            tbl = db.RunReader("select max(Order_Number) from [CUST_SERV] ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtNum2.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtNum2.Text = "1";

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

        void filltyp(){cbx.ItemsSource = db.RunReader("select [Code],[ANAME] from SERVIES_TYP ").DefaultView;}

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable s = db.RunReader("select * from [Customer] where [Phone]='" + txtPhone.Text + "' or [WhatsAPP]='" + txtPhone.Text + "'");
            if (s.Rows.Count > 0 && txtPhone.Text!="")
            {
                txtNum.Text = s.Rows[0][0].ToString();
                txtEName.Text = s.Rows[0][1].ToString();
                txtPhone.Text = s.Rows[0][2].ToString();
                txtWhats.Text = s.Rows[0][3].ToString();
                txtmail.Text = s.Rows[0][4].ToString();
                txtfacebook.Text = s.Rows[0][5].ToString();
                dpdate.Text = s.Rows[0][6].ToString();
                if (s.Rows[0][7].ToString() == "Female")
                    rbFmale.IsChecked = true;
                else
                    rbmale.IsChecked = true;
                txtaddress.Text = s.Rows[0][8].ToString();
                txtother.Text = s.Rows[0][9].ToString();
            }
        }

        private void txtWhats_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable s = db.RunReader("select * from [Customer] where [Phone]='" + txtWhats.Text + "' or [WhatsAPP]='" + txtWhats.Text + "'");
            if (s.Rows.Count > 0 && txtWhats.Text!="")
            {
                txtNum.Text = s.Rows[0][0].ToString();
                txtEName.Text = s.Rows[0][1].ToString();
                txtPhone.Text = s.Rows[0][2].ToString();
                txtWhats.Text = s.Rows[0][3].ToString();
                txtmail.Text = s.Rows[0][4].ToString();
                txtfacebook.Text = s.Rows[0][5].ToString();
                dpdate.Text = s.Rows[0][6].ToString();
                if (s.Rows[0][7].ToString() == "Female")
                    rbFmale.IsChecked = true;
                else
                    rbmale.IsChecked = true;
                txtaddress.Text = s.Rows[0][8].ToString();
                txtother.Text = s.Rows[0][9].ToString();
            }
        }

        private void cbx_DropDownClosed(object sender, EventArgs e) { try { cbx_Copy.ItemsSource = db.RunReader("select * from [SERVIES] where SYRVIES_TYP_NUM ='" + cbx.Text + "'").DefaultView; } catch { } }
        List<Items> items = new List<Items>();
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtzNum.Text == string.Empty)
                    txtzNum.Text = (Convert.ToInt32(txtzNum_Copy1.Text) * Convert.ToInt32(txtzNum_Copy.Text)).ToString();
                else
                    txtzNum.Text = (Convert.ToInt32(txtzNum.Text.ToString()) + Convert.ToInt32(txtzNum_Copy1.Text) * Convert.ToInt32(txtzNum_Copy.Text)).ToString();

                var data = new Items { SYRVIES_TYP_NUM = cbx.Text, Code = cbx_Copy.Text, NAME = currentname, QTY = txtzNum_Copy.Text, PRICE = (Convert.ToInt32(txtzNum_Copy1.Text) * Convert.ToInt32(txtzNum_Copy.Text)).ToString() };


                foreach (var item in items)
                {
                    if (item.SYRVIES_TYP_NUM == data.SYRVIES_TYP_NUM && item.Code == data.Code)
                    {
                        MessageBox.Show("cant add same item twice");
                        return;
                    }
                }

         
                    items.Add(data);

                    dgr.Items.Add(data);

                    cbx.SelectedIndex = -1;
                    cbx_Copy.SelectedIndex = -1;
                    txtzNum_Copy.Text = "1";
                    txtzNum_Copy1.Text = "";
               


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }






        }

        private void txtzNum_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtzNum_Copy.Text != string.Empty)
                {
                    if (txtzNum_Copy.Text != "0")
                    {
                        Convert.ToInt32(txtzNum_Copy.Text);

                      //  txtzNum_Copy1.Text = (Convert.ToInt32(txtzNum_Copy.Text) * Convert.ToInt32(txtzNum_Copy1.Text)).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Number > 0");
                        txtzNum_Copy.Text = "1";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Numbers Only");
                txtzNum_Copy.Text = "1";


            }
        }

        private void txtPhone2_Copy1_TextChanged(object sender, TextChangedEventArgs e){try{if (txtPhone2_Copy1.Text != string.Empty)Convert.ToDouble(txtPhone2_Copy1.Text);}catch{MessageBox.Show("Numbers Only");}}
        DateTime ss;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtPhone.Text = "";
            txtWhats.Text = "";

            DataTable tbl = db.RunReader("select max(ID) from Customer ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtNum.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtNum.Text = "1";

            txtEName.Text = "";
            
                txtmail.Text = "";
                txtaddress.Text = "";
                //txtArabic.Text = "";
           
                txtfacebook.Text = "";
                dpdate.Text = "";
                txtother.Text = "";
                rbmale.IsChecked = false;
                rbmale.IsChecked = false;


        }
        DataTable CurrentItem;
        string currentname;
        private void cbx_Copy_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                CurrentItem = db.RunReader("select PRICE , NAME from SERVIES  where SYRVIES_TYP_NUM='" + cbx.Text + "' and Code='" + cbx_Copy.Text + "'");
                txtzNum_Copy1.Text = CurrentItem.Rows[0][0].ToString();
                currentname = CurrentItem.Rows[0][1].ToString();
            }catch
            { }

        }

        private void txtzNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPhone2_Copy1.Text = txtzNum.Text;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            cbx.SelectedIndex = -1;
            cbx_Copy.SelectedIndex = -1;
            txtzNum_Copy.Text = "1";
            txtzNum_Copy1.Text = "";
            dgr.Items.Clear();
          
            txtzNum.Text = "";
            items.Clear();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            cbx.SelectedIndex = -1;
            cbx_Copy.SelectedIndex = -1;
            txtzNum_Copy.Text = "1";
            txtzNum_Copy1.Text = "";
            dgr.Items.Clear();
            txtzNum.Text = "";
            items.Clear();


            txtPhone.Text = "";
            txtWhats.Text = "";

            DataTable tbl = db.RunReader("select max(ID) from Customer ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtNum.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtNum.Text = "1";

            txtEName.Text = "";

            txtmail.Text = "";
            txtaddress.Text = "";
            //txtArabic.Text = "";

            txtfacebook.Text = "";
            dpdate.Text = "";
            txtother.Text = "";
            rbmale.IsChecked = false;
            rbmale.IsChecked = false;
            txtPhone2_Copy.Text = "";
            txtPhone2_Copy1.Text = "";

            tbl = db.RunReader("select max(Order_Number) from [CUST_SERV] ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtNum2.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtNum2.Text = "1";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
          
            foreach (var item in items)
            {
                db.RunNonQuery(@"insert into [CUST_SERV] (Order_Number ,CUST_ID,SERV_TYP_NUM,SERV_NUM,QTY,Price_servies) values
   ('" + txtNum2.Text + "','" + txtNum.Text + "','" + item.SYRVIES_TYP_NUM + "','" + item.Code + "','" + item.QTY + "','" + item.PRICE + "' )");

            }



            db.RunNonQuery("update [CUST_SERV] set     [Option] =N'" + txtPhone2_Copy.Text + "' , [Total_Price] ='" + txtPhone2_Copy1.Text + "' , [Created_by] ='" +USER.NAME + "' , [Created_Date] =CURRENT_TIMESTAMP   where   Order_Number ='" + txtNum2.Text + "'", "Done ");



        }
    }
}
