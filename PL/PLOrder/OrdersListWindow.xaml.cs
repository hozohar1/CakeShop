using BO;
using PL.PLProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
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

namespace PL.PLOrder
{
    /// <summary>
    /// Interaction logic for OrdersListWindow.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();



   

        public OrdersListWindow()
        {
            InitializeComponent();

            orderForListDataGrid.ItemsSource = bl?.Order.GetListedOrderP();

        }

      
        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(orderForListDataGrid.SelectedItem is OrderForList orderForList)
            {
                new OrderWindow(orderForList.ID,-1).ShowDialog();  
            }
           
        }

       

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }

        private void orderForListDataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void orderForListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Window_Activated(object sender, EventArgs e)
        {

            orderForListDataGrid.ItemsSource = bl?.Order.GetListedOrderP();
          
               
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();

        }
    }
}
