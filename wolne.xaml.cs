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
    /// Logika interakcji dla klasy wolne.xaml
    /// </summary>
    public partial class wolne : Window
    {
        DataTable pom = new DataTable();
        SqlConnection conn = new SqlConnection();
        public wolne(SqlConnection con)
        {

            conn = con;
            InitializeComponent();


            string pytanie = "select IDa from Auta except select Wynajete.IDa from Wynajete where Zakonczono like 0";





            SqlDataAdapter adapter = new SqlDataAdapter(pytanie, conn);

            adapter.Fill(pom);
            pom.AcceptChanges();
            wyswietl.ItemsSource = pom.DefaultView;

        }

        private void wyswietl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
