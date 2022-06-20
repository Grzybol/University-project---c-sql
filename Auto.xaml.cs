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
    /// Logika interakcji dla klasy Przyczep.xaml
    /// </summary>

    public partial class Auto : Window
    {
        int i = 0;
        DataSet baz = new DataSet();
        SqlConnection conn = new SqlConnection();
        public Auto(DataSet baza, SqlConnection con)
        {
            baz = baza;
            conn = con;
            InitializeComponent();
            wyswietl.ItemsSource = baz.Tables[0].DefaultView;
            baz.AcceptChanges();
            if (zapisz.IsPressed)
            {

                baza = baz;

            }

        }

        private void wyswietl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;

            DataRowView row = gd.SelectedItem as DataRowView;
            if (row != null)
            {

                pro.Text = row["Producent"].ToString();
                typ.Text = row["Typ"].ToString();
                mo.Text = row["Model"].ToString();
                cena.Text = row["Cena"].ToString();
            }

            i = wyswietl.SelectedIndex;
            MessageBox.Show("Wybrane auto: " + (i + 1));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (i >= 0)
            {

                baz.Tables[0].Rows[i][1] = pro.Text;
                baz.Tables[0].Rows[i][2] = typ.Text;
                baz.Tables[0].Rows[i][3] = mo.Text;
                baz.Tables[0].Rows[i][4] = Convert.ToString(cena.Text);
                MessageBox.Show("Zmiany zostana zapisane dopiero po naciśnieciu przycisku 'Zapisz zmiany'.");
            }
            else
            {
                MessageBox.Show("Proszę wybrać auto");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)

        {
            if (string.IsNullOrEmpty(pro.Text) == false && string.IsNullOrWhiteSpace(pro.Text) == false && string.IsNullOrEmpty(typ.Text) == false && string.IsNullOrWhiteSpace(typ.Text) == false && string.IsNullOrEmpty(mo.Text) == false && string.IsNullOrWhiteSpace(mo.Text) == false && string.IsNullOrEmpty(cena.Text) == false && string.IsNullOrWhiteSpace(cena.Text) == false)
            {

                DataRow wiersz = baz.Tables[0].NewRow();
                wiersz["Producent"] = pro.Text;
                wiersz["Typ"] = typ.Text;
                wiersz["Model"] = mo.Text;
                wiersz["Cena"] = Convert.ToInt32(cena.Text);
                baz.Tables[0].Rows.Add(wiersz);
                MessageBox.Show("Zmiany zostana zapisane dopiero po naciśnieciu przycisku 'Zapisz zmiany'.");
            }
            else
            {
                MessageBox.Show("Prowszę wprowadzić dane.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlCommandBuilder builder;
            SqlDataAdapter da;
            string sql = "SELECT * FROM Auta";
            da = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(da);

            da.Update(baz.Tables[0]);
            baz.AcceptChanges();
            // da.Fill(baz.Tables[0]);
            MessageBox.Show("Zmiany zostały wprowadzone do bazy.");



        }

        private void usun_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0)
            {
                baz.Tables[0].Rows[i].Delete();
            }
            else
            {
                MessageBox.Show("Proszę wybrać pozycję z tablicy.");
            }
        }
    }
}
