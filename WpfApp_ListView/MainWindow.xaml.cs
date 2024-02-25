using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SQLite;
namespace WpfApp_ListView
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        //private int _index = 0;
        private List<Customer> allData;
        
        public MainWindow()
        {
            InitializeComponent();
            ReadDatabase();

            // リスト表示する為のコード
            //_customers.Add(new Customer { Id=++_index, Name="name" + _index, Phone="phone" + _index });
            // CustomerListView.ItemsSource = _customers;
        }

        private void ReadDatabase()
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Customer>();
                allData = connection.Table<Customer>().ToList();
                CustomerListView.ItemsSource = allData;
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchTextBox.Text;
            // 検索文字列が空の場合は全データを表示
            if (string.IsNullOrEmpty(search))
            {
                CustomerListView.ItemsSource = allData;
            }
            else
            {
                var filteredData = allData.Where(item => item.Name.Contains(search)).ToList();
                CustomerListView.ItemsSource = filteredData;
            }
            //var filterList = _customers.Where(x => x.Name.Contains(SearchTextBox.Text)).ToList();
            //CustomerListView.ItemsSource = filterList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var f = new SaveWindow(null);
            f.ShowDialog();
            ReadDatabase();
            // 配列を使いデータ保持を行うコードサンプル
            //_customers.Add(new Customer { Id = ++_index, Name = "name" + _index});
        }
        

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var item = CustomerListView.SelectedItem as Customer;
            if (item == null)
            {
                MessageBox.Show("行を選択してください");
                return;
            }
            var f = new SaveWindow(item);
            f.ShowDialog();
            ReadDatabase();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = CustomerListView.SelectedItem as Customer;
            if (item == null)
            {
                MessageBox.Show("行を選択してください。");
                return;
            }

            using(var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Customer>();
                connection.Delete(item);
                ReadDatabase();
            }
        }
    }
}
