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
    /// Interaction logic for SalesRecordPage.xaml
    /// </summary>
    public partial class SalesRecordPage : Page
    {
        private ViewModelValueOriented vmvo;
        public SalesRecordPage()
        {
            InitializeComponent();
            Loaded += SalesRecordPage_Loaded;
        }

        private void SalesRecordPage_Loaded(object sender, RoutedEventArgs e)
        {
            vmvo = new ViewModelValueOriented();
            double subtot = 0.0;

            TopThanksText.Text = "Thank you for shopping at Wally's World " + vmvo.CurrentBranch.BranchName + " On " + vmvo.OrderList[0].OrderDate.ToString("dddd, dd MMMM yyyy") + ", " + vmvo.CurrentCustomer.FullName + "!";
            OrderID.Text = "OrderID: " + vmvo.OrderList[0].OrderID;

            foreach (Orders ord in vmvo.OrderList)
            {
                OrderList.Items.Add("" + ord.Product.ProductName + " " + ord.Quantity + "x" + ord.SalesPrice + " = $" + (ord.Quantity * ord.SalesPrice));
                subtot += (ord.Quantity * ord.SalesPrice);
            }

            Subtotal.Text = "Subtotal = $" + subtot;
            HST.Text = string.Format("HST (13%) = ${0:0.00}", (subtot * 0.13));
            SaleTotal.Text = string.Format("Sale Total = ${0:0.00}", (subtot + (subtot * 0.13)));

            BottomThanksText.Text = "" + vmvo.OrderList[0].Status + " ー Thank You!";
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
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
            //Code Courtesy of Shmuel Zang in codeprojects.com https://www.codeproject.com/Questions/281551/frame-navigation-in-WPF
        }
    }
}
