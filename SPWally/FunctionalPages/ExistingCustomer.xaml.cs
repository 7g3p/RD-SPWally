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
    * NAME : ExistingCustomer : Page
    * PURPOSE : The ExistingCustomer is to act as the coupled code behind the xaml file of the same name
    *           and is meant to be able to check that the customer was found, sets the currentCustomer 
    *           to the found customer and navigates to either the Refund or the Purchase order pages.
    */
    public partial class ExistingCustomer : Page
    {
        //Variables
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : ExistingCustomer
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public ExistingCustomer()
        {
            InitializeComponent();
            Loaded += ExistingCustomer_Loaded;
        }

        /*
        * FUNCTION : ExistingCustomer_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void ExistingCustomer_Loaded(object sender, RoutedEventArgs e)
        {
            //Gets a new instance of the ViewModel and sets it to the private data member and to the datacontext
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;
        }

        /*
        * FUNCTION : Purchase_Click
        * DESCRIPTION :
        *           This function is the event handler for when the Purchase button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            var dbMani = new DatabaseManipulation();
            int custID;

            //Gets the customer ID and check that the customer WAS found in the database
            if (-1 != (custID = dbMani.FindCustomer()))
            {
                //Updates the current customer with the found customer's information
                vmvo.CurrentCustomer.CustomerID = custID;
                vmvo.CurrentCustomer.FirstName = vmvo.FirstName;
                vmvo.CurrentCustomer.LastName = vmvo.LastName;
                vmvo.CurrentCustomer.Phone = vmvo.Phone;


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
                    frame.Navigate(new PurchaseOrder());
                }
                //Code Courtesy of Shmuel Zang in codeprojects.com  
            }
            else
            {
                MessageBox.Show("Could Not Find Specific Customer.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        * FUNCTION : Refund_Click
        * DESCRIPTION :
        *           This function is the event handler for when the Refund button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Refund_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            var dbMani = new DatabaseManipulation();
            int custID;

            //Gets the customer ID and check that the customer WAS found in the database
            if (-1 != (custID = dbMani.FindCustomer()))
            {
                //Updates the current customer with the found customer's information
                vmvo.CurrentCustomer.CustomerID = custID;
                vmvo.CurrentCustomer.FirstName = vmvo.FirstName;
                vmvo.CurrentCustomer.LastName = vmvo.LastName;
                vmvo.CurrentCustomer.Phone = vmvo.Phone;

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
                    frame.Navigate(new RefundOrder());
                }
                //Code Courtesy of Shmuel Zang in codeprojects.com  
            }
            else
            {
                MessageBox.Show("Could Not Find Specific Customer.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
