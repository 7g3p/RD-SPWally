using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SPWally.DataLayer;

namespace SPWally.FunctionalPages
{
    /// <summary>
    /// Interaction logic for LookupOrders.xaml
    /// </summary>
    public partial class LookupOrders : Page
    {
        //Variables
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : LookupOrders
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public LookupOrders()
        {
            InitializeComponent();
            Loaded += LookupOrders_Loaded;
        }

        /*
        * FUNCTION : LookupOrders_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void LookupOrders_Loaded(object sender, RoutedEventArgs e)
        {
            //Stores a new instance of the ViewModel into a private data member and into the datacontext
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;
        }

        /*
        * FUNCTION : Refund_Click
        * DESCRIPTION :
        *           This function is the event handler for when the refund button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Refund_Click(object sender, RoutedEventArgs e)
        {
            //Checks that the order that the user wishes to refund is not already refunded
            if (vmvo.Order.Status == "PAID")
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
                    frame.Navigate(new RefundOrder());
                }
                //Code Courtesy of Shmuel Zang in codeprojects.com  
            }
            else
            {
                MessageBox.Show("You Cannot Refund a Refunded Order", "Alert!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /*
        * FUNCTION : Find_Click
        * DESCRIPTION :
        *           This function is the event handler for when the find button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            //variables
            DatabaseManipulation dataMani = new DatabaseManipulation();

            //checks that all orders were found in the database
            if(!dataMani.LoadOrder())
            { 
                MessageBox.Show("Could Not Find the Specified Order.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
