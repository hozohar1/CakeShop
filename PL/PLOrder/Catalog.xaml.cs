using BO;
using PL.CartPL;
using PL.PLProduct;
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
using System.Windows.Shapes;

namespace PL.PLOrder
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {

        #region Initializion 
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public ObservableCollection<ProductItem?> productItems
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemsProperty =
            DependencyProperty.Register("productItems", typeof(ObservableCollection<ProductItem?>), typeof(Catalog));


        BO.Cart Cart1 = new Cart();

        #endregion

        #region constructor
        public Catalog(BO.Cart cart)
        {
            InitializeComponent();
            catalog.ItemsSource = bl?.Product.GetProductItem();
            Cart1 = cart;
            Category? category = new BO.Category?();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }
        #endregion

        #region Actions on controls
        private void cartBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new CartWindow(Cart1).ShowDialog();
        }

        private void cmbCategory_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Category? category = (BO.Category?)cmbCategory.SelectedItem;
            catalog.ItemsSource = bl?.Product.GetProductItem(x => x?.Category == category);
            if (cmbCategory.SelectedIndex == 0)
            {
                catalog.ItemsSource = bl?.Product.GetProductItem();
            }

        }

        private void catalog_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem = catalog.SelectedItem as BO.ProductItem;
            if (productItem != null)
            {

                ProductInCatalog productInCatalog = new ProductInCatalog(Cart1, productItem.ID);
                //  this.Close();

                productInCatalog.ShowDialog();
            }
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }
        #endregion
    }
}





