using BO;
using PL.PLOrder;
using System;
using System.Collections;
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
    /// Interaction logic for orderImg.xaml
    /// </summary>
    public partial class orderImg : Window
    {
        #region Initializion

        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        #endregion
        public orderImg()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
          

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart
            {
                CustomerAdress = " ",
                CustomerEmail = " ",
                CustomerName = " ",
                OrderItems = new List<BO.OrderItem?>(),
                TotalPrice = 0
            };
            new Catalog(cart).Show();
            Close();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.instagram.com/h_and_m_sweets_and_cakes0000"); }
    }
}
