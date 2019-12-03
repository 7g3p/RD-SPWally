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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SPWally.DataLayer;

namespace SPWally.FunctionalPages
{
    /// <summary>
    /// Interaction logic for RefundOrder.xaml
    /// </summary>
    public partial class RefundOrder : Page
    {
        private Orders ord;
        private List<Orders> orderList;
        private ViewModelValueOriented vmvo;
        public RefundOrder()
        {
            InitializeComponent();
            Loaded += RefundOrder_Loaded;
        }

        private void RefundOrder_Loaded(object sender, RoutedEventArgs e)
        {
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;

            vmvo.NewOrderID += Vmvo_NewOrderID;

            vmvo.OrderIDSearch = vmvo.Order.OrderID;
            vmvo.OnPropertyChanged();
        }

        private void Vmvo_NewOrderID(object sender, EventArgs e)
        {
            DatabaseManipulation dataMani = new DatabaseManipulation();

            if (!dataMani.LoadOrder())
            {
                MessageBox.Show("Could Not Find the Specified Order.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            vmvo.ProductQuantity = vmvo.Order.Quantity;

            vmvo.OnPropertyChanged("ProductQuantity");
            vmvo.OnPropertyChanged("Order");
        }

        private void ConfirmRefund_Click(object sender, RoutedEventArgs e)
        {
            //Database Command class
            DatabaseManipulation dataMani = new DatabaseManipulation();

            if(vmvo.Order.Quantity > vmvo.ProductQuantity && vmvo.ProductQuantity > 0)
            {
                //Adjust Order's Quantity and set order to refund
                vmvo.Order.Quantity = vmvo.ProductQuantity;
                vmvo.Order.Status = "RFND";

                //Moving over the Static order to a local order
                ord = vmvo.Order;
                ord.OrderDate = DateTime.Now;
                ord.SalesPrice = 0.0;
                vmvo.CurrentBranch = ord.Branch;
                orderList = new List<Orders>();

                //Clearing the OrderList just in case and Adding order to order list
                orderList.Add(ord);
                vmvo.OrderList = orderList;

                //Clear Static Order
                vmvo.Order = new Orders();

                if (dataMani.AddOrdersFromList() == true)
                {
                    // Find the frame.
                    Frame frame = null;
                    DependencyObject parent = VisualTreeHelper.GetParent(this);

                    // Cycles through to MainWindow frame
                    while (parent != null && frame == null)
                    {
                        frame = parent as Frame;
                        parent = VisualTreeHelper.GetParent(parent);
                    }

                    // Change the page of the frame.
                    if (frame != null)
                    {
                        frame.Navigate(new SalesRecordPage());
                    }
                    //Code Courtesy of Shmuel Zang in codeprojects.com https://www.codeproject.com/Questions/281551/frame-navigation-in-WPF
                }
                else
                {
                    MessageBox.Show("Could Not Return Your Order", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Quantity of Products Returned Cannot Be More Than What Was Ordered.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Find the frame.
            Frame frame = null;
            DependencyObject parent = VisualTreeHelper.GetParent(this);

            // Cycles through to MainWindow frame
            while (parent != null && frame == null)
            {
                frame = parent as Frame;
                parent = VisualTreeHelper.GetParent(parent);
            }

            // Change the page of the frame.
            if (frame != null)
            {
                frame.Navigate(new MainPage());
            }
            //Code Courtesy of Shmuel Zang in codeprojects.com https://www.codeproject.com/Questions/281551/frame-navigation-in-WPF
        }
    }
}
