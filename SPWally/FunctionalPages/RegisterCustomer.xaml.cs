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
using SPWally.FunctionalPages;
using SPWally.DataLayer;

namespace SPWally.FunctionalPages
{
    /*
    * NAME : RegisterCustomer : Page
    * PURPOSE : The RegisterCustomer is to act as the coupled code behind the xaml file of the same name
    *           and is meant to be able to check if the information in the xaml was able to interact with
    *           the database correctly and navigate to the next page.
    */
    public partial class RegisterCustomer : Page
    {
        //Variables
        private ViewModelValueOriented vmvo;

        /*
        * FUNCTION : RegisterCustomer
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public RegisterCustomer()
        {
            InitializeComponent();
            Loaded += RegisterCustomer_Loaded;
        }

        /*
        * FUNCTION : RegisterCustomer_Loaded
        * DESCRIPTION :
        *           This function is the event handler for when the page first loads
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void RegisterCustomer_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the viewMode and set it to a private variable and to the datacontext of the page
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;
        }

        /*
        * FUNCTION : Return_Click
        * DESCRIPTION :
        *           This function is the event handler for when the ConfirmCustomer button is clicked
        * PARAMETERS :
        *       object sender : the object that sent the event
        *       RountedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void ConfirmCust_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            DatabaseManipulation dataMani = new DatabaseManipulation();
            int custID;

            //Get the customer ID from the database and check that it was NOT able to find the customer
            if ((custID = dataMani.FindCustomer()) == -1)
            {
                //Get the customerID from the database and check that it WAS able to add the new customer to the database
                if ((custID = dataMani.AddCustomer()) != -1)
                {
                    //Load the static CurrentCustomer in the viewModel with the current customer's information
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
                    MessageBox.Show("Error: Could Not Add Customer To Database", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Customer Already Exists", "Alert!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
