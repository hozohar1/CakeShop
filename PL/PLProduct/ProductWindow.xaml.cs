
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
namespace PL.PLProduct
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {

        #region Initializion
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        


        public BO.Product? Product
        {
            get { return (BO.Product?)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

        #endregion

        public ProductWindow(int id = -1)//Action<int> action)
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

            if (id == -1)//add
            {
                BtnAdd.Content = "Add";
             
                Product = new BO.Product();//add
            }
            else
            {
                DeleteBtn.Visibility = Visibility.Visible;
                BtnAdd.Content = "Update";
            
                EnteredID.IsReadOnly = true;
                Product = bl?.Product.GetById(id);//update

            }


        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {

                if (EnteredAmount.Text == "" || EnteredID.Text == "" || EnteredName.Text == "" || EnteredPrice.Text == "" || cmbCategory.SelectedItem == null)
                    throw new BO.BlEmptyException("enter value");
                if (cmbCategory.SelectedIndex == 0)
                    throw new BO.BlEmptyException("Invalid category,please choose category");

                if (BtnAdd.Content == "Add")
                {

                    bl?.Product.AddProductP(Product!);
                    messageBoxResult = MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);

                    EnteredName.Clear();
                    EnteredID.Clear();
                    EnteredPrice.Clear();
                    EnteredAmount.Clear();
                    cmbCategory.SelectedIndex = 0;
                }
                else
                {
                    bl?.Product.UpdateProductP(Product!);
                    messageBoxResult = MessageBox.Show("Product update succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();

                }
            }

            catch (BO.BlIdAlreadyExistException)
            { messageBoxResult = MessageBox.Show("id already exist", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlInvalidInputException)
            { messageBoxResult = MessageBox.Show("invalid input", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlNullPropertyException)
            { messageBoxResult = MessageBox.Show("null property", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlInCorrectIntException)
            { messageBoxResult = MessageBox.Show("incorrect int", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlInCorrectStringException)
            { messageBoxResult = MessageBox.Show("incorecct string", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlEmptyException)
            { messageBoxResult = MessageBox.Show("please enter value", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlInCorrectDoubleException)
            {
                { messageBoxResult = MessageBox.Show("please enter a price in valid format", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }


            switch (messageBoxResult)
            {
                case MessageBoxResult.OK or MessageBoxResult.Cancel:
                    break;
                default:
                    break;
            }

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void EnteredID_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other

            return;
        }

        private void EnteredPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c) || char.IsPunctuation(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other

            return;
        }

        private void EnteredAmount_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other

            return;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            EnteredName.Clear();
            EnteredID.Clear();
            EnteredPrice.Clear();
            EnteredAmount.Clear();
            cmbCategory.SelectedIndex = 0;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                bl.Product.DeleteProductP(Product.ID);
                messageBoxResult = MessageBox.Show("Product has been successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                messageBoxResult = MessageBox.Show("PRODUCT ALREADY IN ORDER PROCESS", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();

        }
    }
}
