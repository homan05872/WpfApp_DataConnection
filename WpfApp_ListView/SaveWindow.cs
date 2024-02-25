using SQLite;
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

namespace WpfApp_ListView
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class SaveWindow : Window
    {
        private Customer _customer;
        public SaveWindow(Customer customer)
        {
            InitializeComponent();

            _customer = customer;

            if(customer != null )
            {
                this.NameTextBox.Text = customer.Name;
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameTextBox.Text.Trim().Length < 1 )
            {
                MessageBox.Show("名前を入力してください");
                return;
            }

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Customer>();
                if (_customer == null)
                {
                    connection.Insert(new Customer(NameTextBox.Text));
                }
                else
                {
                    connection.Update(new Customer(_customer.Id, NameTextBox.Text));
                }

                Close();
            }
        }
    }
}
