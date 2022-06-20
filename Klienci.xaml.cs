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
    public partial class Klienci : Window
    {

        int i = 0;
        DataSet baz = new DataSet();
        SqlConnection conn = new SqlConnection();
        public Klienci(DataSet baza, SqlConnection con)
        {
            baz = baza;
            conn = con;
            InitializeComponent();
            wyswietl.ItemsSource = baz.Tables[1].DefaultView;
            baz.AcceptChanges();
            if (zapisz.IsPressed)
            {

                baza = baz;

            }

        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void wyswietl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;

            DataRowView row = gd.SelectedItem as DataRowView;
            if (row != null)
            {

                imie.Text = row["Imie"].ToString();
                nazwisko.Text = row["Nazwisko"].ToString();
                zamie.Text = row["Nr_dowodu"].ToString();
              
            }

            i = wyswietl.SelectedIndex;
            MessageBox.Show("Wybrany klient: " + (i + 1));
        }

        private void usun_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0)
            {

                baz.Tables[1].Rows[i].Delete();
            }
            else
            {
                MessageBox.Show("Proszę wybrać pozycję z tablicy.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (i >= 0)
            {

                baz.Tables[1].Rows[i][1] = Convert.ToString(imie.Text);
                baz.Tables[1].Rows[i][2] = Convert.ToString(nazwisko.Text);
                baz.Tables[1].Rows[i][3] = Convert.ToInt32(zamie.Text);
              
                MessageBox.Show("Zmiany zostana zapisane dopiero po naciśnieciu przycisku 'Zapisz zmiany'.");
            }
            else
            {
                MessageBox.Show("Proszę wybrać klienta.");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(imie.Text)==false && string.IsNullOrWhiteSpace(imie.Text)==false&& string.IsNullOrEmpty(nazwisko.Text) == false && string.IsNullOrWhiteSpace(nazwisko.Text) == false&& string.IsNullOrEmpty(zamie.Text) == false && string.IsNullOrWhiteSpace(zamie.Text) == false)
            {

                if (!baz.Tables[1].Rows.Contains(Convert.ToString(zamie.Text)))
                {


                    DataRow wiersz = baz.Tables[1].NewRow();
                    wiersz["Imie"] = imie.Text;
                    wiersz["Nazwisko"] = nazwisko.Text;
                    wiersz["Nr_dowodu"] = zamie.Text;
                    baz.Tables[1].Rows.Add(wiersz);
                    MessageBox.Show("Zmiany zostana zapisane dopiero po naciśnieciu przycisku 'Zapisz zmiany'.");
                }
                else
                {
                    MessageBox.Show("Klient z podanym numerem dowodu już istnieje!");
                }
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić dane!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlCommandBuilder builder;
            SqlDataAdapter da;
            string sql = "SELECT * FROM Klienci";
            da = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(da);

            da.Update(baz.Tables[1]);
            baz.AcceptChanges();
            MessageBox.Show("Zmiany zostały wprowadzone do bazy.");
        }
    }
}
