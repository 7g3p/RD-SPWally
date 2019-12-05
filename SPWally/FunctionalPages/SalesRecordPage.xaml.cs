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
    * NAME : SalesRecordPage : Page
    * PURPOSE : The salesRecordPage is to act as the coupled code behind the xaml file of the same name
    *           and is meant to display the details of the orders to the screen along with their calculated
    *           price in the format of a reciept.
    */
    public partial class SalesRecordPage : Page
    {
        //Variables
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : SalesRecordPage
        * DESCRIPTION :
        *           This is the constructor for the class and loads all necessary components
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public SalesRecordPage()
        {
            InitializeComponent();
            Loaded += SalesRecordPage_Loaded;
        }

        /*
        * FUNCTION : SalesRecordPage_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void SalesRecordPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Variables
            vmvo = new ViewModelValueOriented();
            double subtot = 0.0;

            //Display the receipt with the information about the Who, when, and where the orders were placed
            TopThanksText.Text = "Thank you for shopping at Wally's World " + vmvo.CurrentBranch.BranchName + " On " + vmvo.OrderList[0].OrderDate.ToString("dddd, dd MMMM yyyy") + ", " + vmvo.CurrentCustomer.FullName + "!";
            OrderID.Text = "OrderID: " + vmvo.OrderList[0].OrderID;

            //Displays the information for each Order in the list
            foreach (Orders ord in vmvo.OrderList)
            {
                OrderList.Items.Add("" + ord.Product.ProductName + " " + ord.Quantity + "x" + ord.SalesPrice + " = $" + (ord.Quantity * ord.SalesPrice));
                subtot += (ord.Quantity * ord.SalesPrice);
            }

            //Displays the costs for all orders
            Subtotal.Text = "Subtotal = $" + subtot;
            HST.Text = string.Format("HST (13%) = ${0:0.00}", (subtot * 0.13));
            SaleTotal.Text = string.Format("Sale Total = ${0:0.00}", (subtot + (subtot * 0.13)));

            //Displays the Paid or Refunded Status and a Thank you
            BottomThanksText.Text = "" + vmvo.OrderList[0].Status + " ー Thank You!";
        }

        /*
        * FUNCTION : Return_Click
        * DESCRIPTION :
        *           This function is the event handler for when the return button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            //clears the List of orders
            vmvo.OrderList.Clear();

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
