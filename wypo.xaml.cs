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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp20
{
    /// <summary>
    /// Logika interakcji dla klasy wypo.xaml
    /// </summary>
    public partial class wypo : Window
    {
        int y = 0;
        DataSet baz = new DataSet();
        SqlConnection conn = new SqlConnection();
        public wypo(DataSet baza, SqlConnection con)
        {
            baz = baza;
            conn = con;
            InitializeComponent();
            baz.AcceptChanges();
        }



        private void zatw_Click(object sender, RoutedEventArgs e)
        {
            if(baz.Tables[0].Rows.Contains(Convert.ToInt32(ida.Text))&& baz.Tables[1].Rows.Contains(Convert.ToInt32(idk.Text)) && !baz.Tables[3].Rows.Contains(Convert.ToInt32(idk.Text)))
            {
                string pk = ida.Text;
                DataRow ta = baz.Tables[0].Rows.Find(pk);

                int x = Convert.ToInt32(czas.Text);
                int z = Convert.ToInt32(ta[4]);
                MessageBox.Show(ta[4].ToString());
                 y = x * z;

                oplata.Text =""+y ;
                wyp.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Wybrano zle ID auta/klienta");
            }
        }

        private void czas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ida.Text) == false && string.IsNullOrWhiteSpace(ida.Text) == false && string.IsNullOrEmpty(idk.Text) == false && string.IsNullOrWhiteSpace(idk.Text) == false && string.IsNullOrEmpty(data.Text) == false && string.IsNullOrWhiteSpace(data.Text) == false && string.IsNullOrEmpty(czas.Text) == false && string.IsNullOrWhiteSpace(czas.Text) == false)
            {
                DataRow wiersz = baz.Tables[2].NewRow();
                wiersz["IDa"] = ida.Text;
                wiersz["IDk"] = idk.Text;
                wiersz["data_wynajmu"] = data.Text;
                wiersz["czas_wynajmu"] = czas.Text;
                wiersz["Zakonczono"] = 0;
                baz.Tables[2].Rows.Add(wiersz);

                SqlCommandBuilder builder;
                SqlDataAdapter da;
                string sql = "SELECT * FROM Wynajete";
                da = new SqlDataAdapter(sql, conn);
                builder = new SqlCommandBuilder(da);

                da.Update(baz.Tables[2]);
                baz.AcceptChanges();
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić dane!");
            }



            MessageBox.Show("Auto zostało wypożyczone. Czas wypożyczeni (dni): " + czas.Text + ". Data wypożyczenia: " + data.Text+". Opłata: "+y);
        }
    }
}
