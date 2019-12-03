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
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class PurchaseOrder : Page
    {
        public DataGrid dg;
        private List<Orders> orderList;
        private Orders newOrder;
        private ViewModelValueOriented vmvo;
        public PurchaseOrder()
        {
            InitializeComponent();
            Loaded += PurchaseOrder_Loaded;
        }

        private void PurchaseOrder_Loaded(object sender, RoutedEventArgs e)
        {
            //Variables
            var dataMani = new DatabaseManipulation();
            orderList = new List<Orders>();

            //Load Viewmodel into dataContext
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;

            vmvo.CurrentBranchSelected += OnBranchSelected;

            //Check if all branches are loaded into view
            if(dataMani.GetAllBranches() == false)
            {
                MessageBox.Show("Could Not Load Branches", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnBranchSelected(object sender, EventArgs e)
        {
            var dataMani = new DatabaseManipulation();

            if (dataMani.GetAllProductsInBranch() == false)
            {
                MessageBox.Show("Could Not Find Any Products For The Selected Branch", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            vmvo.OnPropertyChanged("ProductList");
        }

        private void ConfirmPurchase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManipulation dataMani = new DatabaseManipulation();
            vmvo.OrderList = orderList;

            if(dataMani.AddOrdersFromList() == true)
            {
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
                //Code Courtesy of Shmuel Zang in codeprojects.com https://www.codeproject.com/Questions/281551/frame-navigation-in-WPF
            }
            else
            {
                MessageBox.Show("Could Not Complete All Orders.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
            //Code Courtesy of Shmuel Zang in codeprojects.com https://www.codeproject.com/Questions/281551/frame-navigation-in-WPF
        }

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
