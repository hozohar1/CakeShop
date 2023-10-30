using BO;
using PL.PLOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for simulatorWindow.xaml
    /// </summary>
 

    public partial class simulatorWindow : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        bool allDelivered = false;
        DateTime GlobalTime = DateTime.Now;
        DateTime startTime = DateTime.Now;


        public List<BO.OrderForList?> ListOrders
        {
            get { return (List<BO.OrderForList?>)GetValue(ListOrdersProperty); }
            set { SetValue(ListOrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lstOrders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListOrdersProperty =
            DependencyProperty.Register("ListOrders", typeof(List<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));

       // List<BO.OrderForList?> ListOrders =new List<BO.OrderForList?>();
        BackgroundWorker bwTracking;
        public simulatorWindow()
        {
            InitializeComponent();
            ListOrders = bl.Order.GetListedOrderP().ToList();
            orderForListDataGrid.ItemsSource = bl?.Order.GetListedOrderP();

            bwTracking = new BackgroundWorker();
            bwTracking.DoWork += BwOrder_DoWork;
            bwTracking.ProgressChanged += Window_Activated;
            bwTracking.ProgressChanged += BwOrder_ProgressChanged;
           
            bwTracking.RunWorkerCompleted += BwTracking_RunWorkerCompleted;

       bwTracking.WorkerReportsProgress = true;
        bwTracking.WorkerSupportsCancellation = true;
        }

        private void BwOrder_ProgressChanged(object? sender, ProgressChangedEventArgs e)//
        {


            var tmp = bl.Order.GetListedOrderP().ToList();
            foreach (var item in tmp)
            {
                BO.Order demoOrder = bl.Order.GetOrderInfo(item?.ID ?? throw new NullReferenceException());
              

                if (GlobalTime - demoOrder.OrderDate >= new TimeSpan(4, 0, 0, 0) && demoOrder.OrderStatus == BO.OrderStatus.Ordered)
                    bl.Order.UpDateOrderShipP(demoOrder.ID);
               
                if (GlobalTime - demoOrder.OrderDate >= new TimeSpan(10, 0, 0, 0) && demoOrder.OrderStatus == BO.OrderStatus.Shipped)
                    bl.Order.UpDateOrderDeliveryP(demoOrder.ID);
              

                //ListOrders = bl.Order.GetListedOrderP().ToList();
                //orderForListDataGrid.Items.Refresh();   
            }
            if (ListOrders.All(x => x?.OrderStatus == BO.OrderStatus.Delivered))
            {
                if (bwTracking.WorkerSupportsCancellation == true)
                    bwTracking.CancelAsync(); 
                allDelivered = true;
            }
            ListOrders = bl.Order.GetListedOrderP().ToList();

        }
        private void Window_Activated(object sender, EventArgs e)
        {

            orderForListDataGrid.ItemsSource = bl?.Order.GetListedOrderP();

        }
        private void BwOrder_DoWork(object? sender, DoWorkEventArgs e)
        {
           
            GlobalTime = DateTime.Now; 
            while (true)
            {
                if (bwTracking.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                if (bwTracking.WorkerReportsProgress == true)
                    bwTracking.ReportProgress(1642321);
                GlobalTime= GlobalTime.AddHours(4);
                Thread.Sleep(1000);
            }
        }
        private void BwTracking_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            
            if (allDelivered == true)
            {
                MessageBox.Show("YAY! All orders are delivered");
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show("Have a nice day🙂");
            }
            this.Cursor = Cursors.Arrow;
        }
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void orderForListDataGrid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            object h = orderForListDataGrid.SelectedItem;
            BO.OrderForList order = (OrderForList)h;
            new PL.PLOrder.OrderTracking(order.ID).Show();

        }

        private void progBarTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          
        }

        private void startBtn(object sender, RoutedEventArgs e)
        {

            if (bwTracking.IsBusy != true)
            {
                this.Cursor = Cursors.Wait;
                bwTracking.RunWorkerAsync();
            }
        }

    

        private void StopBtn(object sender, RoutedEventArgs e)
        {
            if (bwTracking.WorkerSupportsCancellation == true)
                bwTracking.CancelAsync(); // Cancel the asynchronous operation.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HOME_Click(object sender, RoutedEventArgs e)
        {

        }
    }
  
}
