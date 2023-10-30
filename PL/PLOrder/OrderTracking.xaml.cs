using BO;
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

namespace PL.PLOrder
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderTracking()
        {
            InitializeComponent();
            OrderPageBtn.Visibility = Visibility.Hidden;       


        }
        public OrderTracking( int id)
        {
            InitializeComponent();
            OrderPageBtn.Visibility = Visibility.Hidden;
            SearchBtn.Visibility= Visibility.Hidden;
            iDTextBox.IsEnabled= false;  
            OrderTrackingBO = bl.Order.OrderTrackP(id);
            trackingDataGrid.ItemsSource = OrderTrackingBO.Tracking;

        }
        public BO.OrderTracking? OrderTrackingBO
        {
            get { return (BO.OrderTracking?)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderTrackingBO", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
        private void orderNumTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

   

        private void SearchBtn_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBoxResult messageBoxResult;
            try
            {
                int id = int.Parse(iDTextBox.Text);
                OrderTrackingBO = bl.Order.OrderTrackP(id);
                trackingDataGrid.ItemsSource = OrderTrackingBO.Tracking;
                // statusTextBox.Text =orderTracking.Status.ToString();
                OrderPageBtn.Visibility = Visibility.Visible;
            }
            catch 
            {
                messageBoxResult = MessageBox.Show("Order id not found", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     
        private void OrderPageBtn_Click_1(object sender, RoutedEventArgs e)
        {
            new OrderWindow(OrderTrackingBO.ID,0).Show();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void trackingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();

        }

        private void iDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
