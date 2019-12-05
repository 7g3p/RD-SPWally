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
    /*
    * NAME : RefundOrder : Page
    * PURPOSE : The RefundOrder is to act as the coupled code behind the xaml file of the same name
    *           and is meant to be able to check that the OrderID in the search bar and return the
    *           order's information from the database and confirms that the order was refunded before
    *           navigating to the next page
    */
    public partial class RefundOrder : Page
    {
        //Variables
        private Orders ord;
        private List<Orders> orderList;
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : RefundOrder
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public RefundOrder()
        {
            InitializeComponent();
            Loaded += RefundOrder_Loaded;
        }

        /*
        * FUNCTION : RefundOrder_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void RefundOrder_Loaded(object sender, RoutedEventArgs e)
        {
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;

            //Subscribe the method to the NewOrderID in search bar event
            vmvo.NewOrderID += Vmvo_NewOrderID;

            //Set the orderID search bar to equal the current order's ID
            vmvo.OrderIDSearch = vmvo.Order.OrderID;
            vmvo.OnPropertyChanged();
        }

        /*
        * FUNCTION : Vmvo_NewOrderID
        * DESCRIPTION :
        *           This function is the event handler for the subscribed event in the viewModel class
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       EventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Vmvo_NewOrderID(object sender, EventArgs e)
        {
            //Variables
            DatabaseManipulation dataMani = new DatabaseManipulation();

            //Checks if the Order was loaded successfully
            if (!dataMani.LoadOrder())
            {
                MessageBox.Show("Could Not Find the Specified Order.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Sets the xaml to equal the quantity
            vmvo.ProductQuantity = vmvo.Order.Quantity;

            //Call Property changed for the two variables to update the xaml
            vmvo.OnPropertyChanged("ProductQuantity");
            vmvo.OnPropertyChanged("Order");
        }

        /*
        * FUNCTION : ConfirmRefund_Click
        * DESCRIPTION :
        *           This function is the event handler for when the Confirm Refuund button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
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
                    //Code Courtesy of Shmuel Zang in codeprojects.com  
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

        /*
        * FUNCTION : Cancel_Click
        * DESCRIPTION :
        *           This function is the event handler for when the cancel button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
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
            //Code Courtesy of Shmuel Zang in codeprojects.com  
        }
    }
}
