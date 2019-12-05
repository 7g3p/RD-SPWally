using System;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using SPWally.DataLayer;
using System.Collections;

namespace SPWally.FunctionalPages
{
    /*
    * NAME : PurchaseOrder : Page
    * PURPOSE : The PurchaseOrder is to act as the coupled code behind the xaml file of the same name
    *           and is meant to be able to check that the user has selected a branch to shop from and
    *           updates the display. It also checks if the orders in the customer's order list were 
    *           successfully added to the database and navigates to the next page.
    */
    public partial class PurchaseOrder : Page
    {
        //Variables
        private List<Orders> orderList;
        private Orders newOrder;
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : PurchaseOrder
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public PurchaseOrder()
        {
            InitializeComponent();
            Loaded += PurchaseOrder_Loaded;
        }

        /*
        * FUNCTION : PurchaseOrder_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void PurchaseOrder_Loaded(object sender, RoutedEventArgs e)
        {
            //Variables
            var dataMani = new DatabaseManipulation();
            orderList = new List<Orders>();

            //Load Viewmodel into dataContext
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;

            //Subscribe the ViewModel event to the OnBranchSelected event handler
            vmvo.CurrentBranchSelected += OnBranchSelected;

            //Check if all branches are loaded into view
            if(dataMani.GetAllBranches() == false)
            {
                MessageBox.Show("Could Not Load Branches", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        * FUNCTION : OnBranchSelected
        * DESCRIPTION :
        *           This function is the event handler for the subscribed event of the Viewmodel class. IT
        *           is for when the user has selected a branch to shop from
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void OnBranchSelected(object sender, EventArgs e)
        {
            //Variables
            var dataMani = new DatabaseManipulation();

            //Check that it was able to get all products in the selected branch
            if (dataMani.GetAllProductsInBranch() == false)
            {
                MessageBox.Show("Could Not Find Any Products For The Selected Branch", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Update the event so that the products taken from the database is displayed on the screen
            vmvo.OnPropertyChanged("ProductList");
        }

        /*
        * FUNCTION : ConfirmPurchase_Click
        * DESCRIPTION :
        *           This function is the event handler for when the confirmPurchase button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void ConfirmPurchase_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            DatabaseManipulation dataMani = new DatabaseManipulation();
            vmvo.OrderList = orderList;

            //Check that the orders in the order list were able to be added to the database
            if(dataMani.AddOrdersFromList() == true)
            {
                //Update the event
                vmvo.OnPropertyChanged();

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
                MessageBox.Show("Could Not Complete All Orders.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        * FUNCTION : Cancel_Click
        * DESCRIPTION :
        *           This function is the event handler for when the Cancel Button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //"If the orderlist isn't empty the make it empty >:(" - Probably not Wally (He'd want to charge people for products they didn't order)
            if(orderList.Count() != 0)
            {
                orderList.Clear();
            }

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

        /*
        * FUNCTION : AddToOrder_Click
        * DESCRIPTION :
        *           This function is the event handler for when the AddToOrder Button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if(vmvo.ProductQuantity > 0 && vmvo.ProductQuantity < vmvo.SelectedProduct.Stock)
            {
                //Load a new Order
                newOrder = new Orders();
                newOrder.Product = vmvo.SelectedProduct;
                newOrder.Branch = vmvo.CurrentBranch;
                newOrder.Customer = vmvo.CurrentCustomer;
                newOrder.OrderDate = DateTime.Now;
                newOrder.Quantity = vmvo.ProductQuantity;
                newOrder.SalesPrice = vmvo.SelectedProduct.wPrice * 1.4;
                newOrder.Status = "PAID";

                //Add order to list
                orderList.Add(newOrder);

                //Clear current products order
                vmvo.SelectedProduct = new Products();
                vmvo.ProductQuantity = 0;

                //Update xaml currentProduct
                vmvo.OnPropertyChanged();
            }
            else
            {
                MessageBox.Show("Invalid Order Quantity", "Alert!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
