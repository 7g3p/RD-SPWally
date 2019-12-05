using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPWally.FunctionalPages
{
    /*
    * NAME : MainPage : Page
    * PURPOSE : The MainPage is to act as the coupled code behind the xaml file of the same name
    *           and is meant to be able to simply be a homes screen to navigate to the other screens
    */
    public partial class MainPage : Page
    {
        //Variables
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : MainPage
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        /*
        * FUNCTION : MainPage_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Variable
            vmvo = new ViewModelValueOriented();

            //Clear all data in static variables from the previous use
            vmvo.CurrentBranch = new DataLayer.Branches();
            vmvo.CurrentCustomer = new DataLayer.Customers();
            vmvo.FirstName = "";
            vmvo.LastName = "";
            vmvo.Order = new DataLayer.Orders();
            vmvo.OrderIDSearch = 0;
            vmvo.Phone = 0;
            vmvo.ProductQuantity = 0;
            vmvo.SelectedProduct = new DataLayer.Products();
        }

        /*
        * FUNCTION : ExistingCustomer_Click
        * DESCRIPTION :
        *           This function is the event handler for when the Existing Customer Button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void ExistingCustomer_Click(object sender, RoutedEventArgs e)
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
                frame.Navigate(new ExistingCustomer());
            }
            //Code Courtesy of Shmuel Zang in codeprojects.com  
        }

        /*
        * FUNCTION : NewCustomer_Click
        * DESCRIPTION :
        *           This function is the event handler for when the New Customer button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void NewCustomer_Click(object sender, RoutedEventArgs e)
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
                frame.Navigate(new RegisterCustomer());
            }
            //Code Courtesy of Shmuel Zang in codeprojects.com  
        }

        /*
        * FUNCTION : LookOrder_Click
        * DESCRIPTION :
        *           This function is the event handler for when the look-up Order button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void LookOrder_Click(object sender, RoutedEventArgs e)
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
                frame.Navigate(new LookupOrders());
            }
            //Code Courtesy of Shmuel Zang in codeprojects.com  
        }

        /*
        * FUNCTION : InvLevels_Click
        * DESCRIPTION :
        *           This function is the event handler for when the Inventory Levels button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void InvLevels_Click(object sender, RoutedEventArgs e)
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
                frame.Navigate(new InventoryLevels());
            }
            //Code Courtesy of Shmuel Zang in codeprojects.com  
        }
    }
}
