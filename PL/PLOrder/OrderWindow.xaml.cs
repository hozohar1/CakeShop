using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();






        public BO.Order? curOrder
        {
            get { return (BO.Order?)GetValue(curOrderProperty); }
            set { SetValue(curOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for curOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty curOrderProperty =
            DependencyProperty.Register("curOrder", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));


        public OrderWindow(int id = -1, int h=-1)
        {
            InitializeComponent();
            
           orderStatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
             if (id != -1)
            {
                curOrder = bl?.Order.GetOrderInfo(id);
                orderItemDataGrid.ItemsSource = curOrder?.OrderItems;
                orderStatusComboBox.Text = curOrder?.OrderStatus.ToString();
                orderStatusComboBox.IsEnabled=false;
              
            }
            if (id == -1)
            {
                curOrder = new BO.Order();

            }
            if(h==0)// order tracking
            {
                UpdateShipDateBtn.Visibility = Visibility.Hidden;
                UpdateDeliverBtn.Visibility = Visibility.Hidden;
            }
        }

    

        private void UpdateShipDateBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                bl?.Order.UpDateOrderShipP(curOrder.ID);
                messageBoxResult = MessageBox.Show("Order ship updated succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
               }
            catch (BO.BlIncorrectDateException )
            {
                messageBoxResult = MessageBox.Show("The order has already been sent", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDeliverBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                bl?.Order.UpDateOrderDeliveryP(curOrder!.ID);
                messageBoxResult = MessageBox.Show("Order delivery updated succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
               }
            catch (BO.BlIncorrectDateException x)
            {
                messageBoxResult = MessageBox.Show("The order has already been delivered", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void orderItemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

 

 
}
    

