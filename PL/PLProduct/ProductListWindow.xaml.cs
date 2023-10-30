
using BO;
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
namespace PL.PLProduct
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    /// 
    public partial class ProductListWindow : Window
    {
        //public ObservableCollection<BO.ProductForList> Products { get; set; }

        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public ProductListWindow()
        {
            InitializeComponent();
           
            CmbSellector.ItemsSource = Enum.GetValues(typeof(BO.Category)); // value in combox
            CmbSellector.SelectedIndex=0;
            

        }



        private void CmbSellector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category? category = (BO.Category?)CmbSellector.SelectedItem;
            ProductList.ItemsSource = bl?.Product.GetListedProducts(x => x?.Category == category);
            if(CmbSellector.SelectedIndex==0)
            {
                ProductList.ItemsSource = bl?.Product.GetListedProducts();
            }
        }
      
        private void btnAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();//(int productId) => Products.Add(bl?.Product.GetProductForList(productId)!)).Show();
        }
     

   
        private void ProductList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductList.SelectedItem is ProductForList productForList)
                new ProductWindow(productForList.ID).ShowDialog();
            //ProductList.ItemsSource = bl?.Product.GetListedProducts();

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Category? category = (BO.Category?)CmbSellector.SelectedItem;
            ProductList.ItemsSource = bl?.Product.GetListedProducts(x => x?.Category == category);
            if (CmbSellector.SelectedIndex == 0)
            {
                ProductList.ItemsSource = bl?.Product.GetListedProducts();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();

        }
    }
   
}
 
        
    



