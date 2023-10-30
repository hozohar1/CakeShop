using BlApi;
using PL.PLOrder;
using PL.PLProduct;
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
namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        #region Initializion
        BlApi.IBl? bl = BlApi.Factory.Get();
        #endregion
        public ManagerMainWindow()
        {
            InitializeComponent();
        }

        private void ListOfProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
            this.Hide();
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
           // new OrderWindow().Show();
           new OrdersListWindow().Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new simulatorWindow().Show();
           
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();

        }
    }
}
