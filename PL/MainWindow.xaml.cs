
using PL.PLOrder;
using PL.PLProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Initializion
        BlApi.IBl? bl = BlApi.Factory.Get();
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
       
     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AdminPasswordWindow().Show();
            //new ManagerMainWindow().Show();
            this.Hide();

        }

     
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
         

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
           

        }

        private void btnNeworder_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart
            {
                CustomerAdress=" ",
                CustomerEmail=" ",
                CustomerName=" ",
                OrderItems= new List<BO.OrderItem?>(),
                TotalPrice=0
            };
            new Catalog(cart).Show();
            Close();
        }

        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            new PL.PLOrder.OrderTracking().Show();
            Close();

        }
    }
}














 //< Button x: Name = "cartBtn" Content = "🛒" Height = "36" Margin = "65,17,699,0" VerticalAlignment = "Top" Click = "cartBtn_Click" BorderBrush = "{x:Null}" Background = "#FFFFFDFD" FontSize = "20" FontWeight = "Bold" />
 //       < StackPanel x: Name = "panel" Margin = "324,2,318,367" >
 //           < Label Content = "Filter by category" Height = "26" Width = "118" FontWeight = "Bold" />
 //           < ComboBox x: Name = "cmbCategory" ItemsSource = "{Binding Category}" Width = "120" SelectionChanged = "cmbCategory_SelectionChanged_1" BorderBrush = "#FFFCF9F9" OpacityMask = "#FFFCF6F6" >
 //               < ComboBox.Background >
 //                   < LinearGradientBrush EndPoint = "0,1" >
 //                       < GradientStop Color = "#FFF0F0F0" />
 //                       < GradientStop Color = "#FFFBFBFB" Offset = "1" />
 //                   </ LinearGradientBrush >
 //               </ ComboBox.Background >
 //           </ ComboBox >
 //       </ StackPanel >