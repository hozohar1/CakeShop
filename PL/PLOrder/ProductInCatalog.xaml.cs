using BlApi;
using BO;
using PL.CartPL;
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
    /// Interaction logic for ProductInCatalog.xaml
    /// </summary>
    /// 
    public partial class ProductInCatalog : Window
    {

        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart c;


        public BO.ProductItem prodCurrent
        {
            get { return (BO.ProductItem)GetValue(prodCurrentProperty); }
            set { SetValue(prodCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  prodCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty prodCurrentProperty =
            DependencyProperty.Register(" prodCurrent", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));



        public ProductInCatalog(BO.Cart cart, int id)
        {
            InitializeComponent();
            try
            {
                prodCurrent = bl.Product.GetProductItem(id, cart);
                c=cart;
                //       bl.Cart.AddProductCart(c,prodCurrent.ID);
            }
            catch (BO.BlIdNotExistException ex)
            { MessageBox.Show("try again"); }
            this.DataContext = prodCurrent;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;


            try
            {
                int y = bl.Product.GetById(prodCurrent.ID).Amount;
              if (y > 0)
                {
                bl.Cart.AddProductCart(c, prodCurrent.ID);
                    Product product = new Product();
                    product = bl.Product.GetById(prodCurrent.ID);
                    product.Amount = y - 1;// update product amount--
                    bl.Product.UpdateProductP(product); 
                    prodCurrent.CartQuantity++;
                    prodCurrent = bl.Product.GetProductItem(prodCurrent.ID, prodCurrent.CartQuantity++);

                    cartQuantityLabel.Content = prodCurrent.CartQuantity;
               
                    new CartWindow(c);
                     }
             else
                {
                    prodCurrent.IsAvailable=false;
                    isAvailableCheckBox.IsChecked=false;
                    messageBoxResult = MessageBox.Show("sorry out of stock 🤧", "out of stock", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }
            catch (BO.BlNullPropertyException) { messageBoxResult = MessageBox.Show("out of stoke", "succefully", MessageBoxButton.OK, MessageBoxImage.Information); }

            //     }


            }

            private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        //   Catalog catalog = new Catalog(c);
            this.Close();

          //  catalog.ShowDialog();
            
        }

       
    }
}
