using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for AdminPasswordWindow.xaml
    /// </summary>
    public partial class AdminPasswordWindow : Window
    {
       
        public AdminPasswordWindow()
        {
            InitializeComponent();
        }

        private void enterNameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult messageBoxResult;




            //if (enterNameTxt.Text != "hodaya" || enterPasswordTxt.Text != "123")
            //{

            //    messageBoxResult = MessageBox.Show("User name or password not found", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    this.Close();
            //    new MainWindow().Show();
            //}
            //else
            
                new ManagerMainWindow().Show();
                this.Close();

                //}


            }

        }
   
}
