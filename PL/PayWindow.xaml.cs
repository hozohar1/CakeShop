using BO;
using PL.PLOrder;
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
    /// Interaction logic for PayWindow.xaml
    /// </summary>
    public partial class PayWindow : Window
    {
        #region Initializion 
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

       
        public BO.Cart? CartPL
        {
            get { return (BO.Cart?)GetValue(CartPLProperty); }
            set { SetValue(CartPLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartPL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartPLProperty =
            DependencyProperty.Register("CartPL", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));

        #endregion

        #region constructor
        public PayWindow(Cart cart)
        {
            CartPL = cart;
        
            InitializeComponent();
          
        }
        #endregion

        #region Actions on controls

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult messageBoxResult;
            try
            {
                bl.Cart.ConfirmCartOrder(CartPL, CartPL.CustomerName, CartPL.CustomerEmail, CartPL.CustomerAdress);
              //  messageBoxResult = MessageBox.Show("Order updated succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                new PL.orderImg().Show();
               
                Close();
            }

            catch (BO.BlInvalidInputException) { messageBoxResult = MessageBox.Show("Please enter a valid email address", "succefully", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (BO.BlInCorrectIntException) { messageBoxResult = MessageBox.Show("the cart is empty", "succefully", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (BO.BlNullPropertyException) { messageBoxResult = MessageBox.Show("", "succefully", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (BO.BlInCorrectStringException) { messageBoxResult = MessageBox.Show("Please enter a valid name/address", "succefully", MessageBoxButton.OK, MessageBoxImage.Information); }

          }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();

        }
        #endregion
    }
}

