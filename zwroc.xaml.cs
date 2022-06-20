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
    /// Logika interakcji dla klasy zwroc.xaml
    /// </summary>
    public partial class zwroc : Window
    {
        int y = 0,i=0;
        DataSet baz = new DataSet();
        SqlConnection conn = new SqlConnection();
        public zwroc(DataSet baza, SqlConnection con)
        {
            baz = baza;
            conn = con;
            InitializeComponent();
            DataTable pom = new DataTable();
            
            
            string pytanie = "select ID,IDk,Wynajete.IDa,data_wynajmu ,Zakonczono,(Cena*czas_wynajmu) as Oplata from Wynajete join Auta on Wynajete.IDa=Auta.IDa where Zakonczono like 0";

            SqlDataAdapter adapter = new SqlDataAdapter(pytanie, conn);

            adapter.Fill(pom);
            pom.AcceptChanges();
            wyswietl.ItemsSource = pom.DefaultView;
            baz.AcceptChanges();

        }

        private void szuk_Click(object sender, RoutedEventArgs e)
        {

            
            baz.Tables[2].Rows[i][5] = 1;
                
            MessageBox.Show("Zmiany zostały zapisane");
            SqlCommandBuilder builder;
            SqlDataAdapter da;
            string sql = "SELECT * FROM Wynajete";
            da = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(da);

            da.Update(baz.Tables[2]);
            baz.AcceptChanges();


        }

        private void zatw_Click(object sender, RoutedEventArgs e)
        {
            if (i >= 0)
            {

                MessageBox.Show("Przed zwrotem auta proszę wynieść opłatę.");
                 zwr.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Proszę wybrać auto");

            }
        }

        private void wyswietl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;

            DataRowView row = gd.SelectedItem as DataRowView;
            if (row != null)
            {

                idk.Text = row["IDk"].ToString();
                ida.Text = row["IDa"].ToString();
                op.Text = row["Oplata"].ToString();
                
            }

            i = wyswietl.SelectedIndex;
            MessageBox.Show("Wybrane auto: " + (i + 1));
        }
    }
}
