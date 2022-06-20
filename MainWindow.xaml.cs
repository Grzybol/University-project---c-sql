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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp20
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        DataSet baza = new DataSet("baza");




        public MainWindow()
        {
            InitializeComponent();
            string connstring = "Database=Auta;Data Source=DESKTOP-8H9J7TP\\SQLEXPRESS;Integrated Security=true";
            conn = new SqlConnection(connstring);

            try
            {
                conn.Open();
            }
            catch (SqlException)
            {
                MessageBox.Show("Nie udało się połączyć z bazą.");
            }

            DataTable Auta = baza.Tables.Add("Auta");

            DataColumn IDa = new DataColumn("IDa");
            IDa.DataType = Type.GetType("System.Int32");
            IDa.AutoIncrement = true;
            IDa.AutoIncrementStep = 1;
            IDa.AutoIncrementSeed = 0;
            baza.Tables[0].Columns.Add(IDa);
            baza.Tables[0].PrimaryKey = new DataColumn[] { baza.Tables[0].Columns["IDa"] };



            DataTable Klienci = baza.Tables.Add("Klienci");

            DataColumn IDk = new DataColumn("IDk");
            IDk.DataType = Type.GetType("System.Int32");
            IDk.AutoIncrement = true;
            IDk.AutoIncrementStep = 1;
            IDk.AutoIncrementSeed = 0;
            baza.Tables[1].Columns.Add(IDk);
            baza.Tables[1].PrimaryKey = new DataColumn[] { baza.Tables[1].Columns["IDk"] };

            DataTable Wynajete = baza.Tables.Add("Wynajete");

            DataColumn ID = new DataColumn("ID");
            ID.DataType = Type.GetType("System.Int32");
            ID.AutoIncrement = true;
            ID.AutoIncrementStep = 1;
            ID.AutoIncrementSeed = 0;
            
           baza.Tables[2].Columns.Add(ID);
            baza.Tables[2].PrimaryKey = new DataColumn[] { baza.Tables[2].Columns["ID"] };


            string pytanie = "select * from Auta";

            SqlDataAdapter adapter = new SqlDataAdapter(pytanie, conn);

            adapter.Fill(baza, "Auta");
            Auta = baza.Tables["Auta"];

            adapter.SelectCommand.CommandText = "select * from Wynajete";
            
             adapter.Fill(baza, "Wynajete");
              Wynajete = baza.Tables["Wynajete"];


            adapter.SelectCommand.CommandText = "select * from Klienci";
           
            adapter.Fill(baza, "Klienci");
            Klienci = baza.Tables["Klienci"];





            baza.AcceptChanges();




        }

        private void prz_Click(object sender, RoutedEventArgs e)
        {
            Auto a = new Auto(baza, conn);
            a.Show();
        }

        private void wyn_Click(object sender, RoutedEventArgs e)
        {
            lista l = new lista(baza, conn);
            l.Show();

        }

        private void kli_Click(object sender, RoutedEventArgs e)
        {
            Klienci k = new Klienci(baza, conn);
            k.Show();
        }

        private void wypo_Click(object sender, RoutedEventArgs e)
        {
            wypo w = new wypo(baza, conn);
            w.Show();

        }

        private void zwroc_Click(object sender, RoutedEventArgs e)
        {
            zwroc z = new zwroc(baza, conn);
            z.Show();

        }

        private void wolne_Click(object sender, RoutedEventArgs e)
        {
            wolne wo = new wolne(conn);
            wo.Show();
        }
    }
}
