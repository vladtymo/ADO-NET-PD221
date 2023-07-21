using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace _02_dataset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connStr = null;
        private SqlDataAdapter adapter = null;
        private DataSet dataSet = null;

        public MainWindow()
        {
            InitializeComponent();

            connStr = ConfigurationManager.ConnectionStrings["SportShopDb"].ConnectionString;
        }

        private void LoadData()
        {
            string cmd = "select * from Products;";
            adapter = new(cmd, connStr);

            // generate INSERT, UPDATE, DELETE commands
            new SqlCommandBuilder(adapter);

            // read data from DB and store it locally
            dataSet = new();
            adapter.Fill(dataSet);

            // do work, make changes
            //MessageBox.Show(dataSet.Tables[0].Rows[0]["Name"].ToString());
            grid.ItemsSource = dataSet.Tables[0].DefaultView;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // submit changes to DB (insert, update, delete)
            adapter.Update(dataSet);
        }
    }
}
