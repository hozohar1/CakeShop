using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using BlApi;
using BO;
using PL.PLOrder;

namespace PL.CartPL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
     //  public BO.Cart? CartPL;


        public BO.Cart? CartPLL
        {
            get { return (BO.Cart?)GetValue(CartPLLProperty); }
            set { SetValue(CartPLLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartPLL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartPLLProperty =
            DependencyProperty.Register("CartPLL", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


        public CartWindow(Cart cart)
        {
            CartPLL = cart;
            InitializeComponent();
            orderItemDataGrid.ItemsSource = CartPLL.OrderItems;

        }
    

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            object h = orderItemDataGrid.SelectedItem;
            BO.OrderItem orderItem = (OrderItem)h;
            Product product = new Product();
            product = bl.Product.GetById(orderItem.ProductID);
            product.Amount++;// update product amount++
            bl.Product.UpdateProductP(product);

            CartPLL = bl.Cart.UpdateAmountProductCart(CartPLL, orderItem.ID, orderItem.Amount - 1); //update amount-1
            orderItemDataGrid.ItemsSource = CartPLL.OrderItems;
            totalPriceTextBox.Text = CartPLL.TotalPrice.ToString();
           

            orderItemDataGrid.Items.Refresh();
          
        }
        //private void Window_Activated(object sender, EventArgs e)
        //{
        //    orderItemDataGrid.ItemsSource = CartPL.OrderItems;
        //}
        private void totalPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            new PL.PLOrder.Catalog(CartPLL).Show();
            Close();
            // this.Close();   
        }
      

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (CartPLL.TotalPrice == 0)
            {
                { messageBoxResult = MessageBox.Show("the cart is empry", "succefully", MessageBoxButton.OK, MessageBoxImage.Information); }

            }
            else
            {
                new PL.PayWindow(CartPLL).Show();
                Close();
            }
        }

        private void orderItemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
