using System;
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

namespace SPWally.FunctionalPages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class PurchaseOrder : Page
    {
        ViewModelValueOriented vmvo;
        public PurchaseOrder()
        {
            InitializeComponent();
            Loaded += PurchaseOrder_Loaded;
        }

        private void PurchaseOrder_Loaded(object sender, RoutedEventArgs e)
        {
            //Variables
            var dataMani = new DatabaseManipulation();

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
        }

        private void ConfirmPurchase_Click(object sender, RoutedEventArgs e)
        {
            var dataMani = new DatabaseManipulation();

            if (dataMani.GetAllProductsInBranch() == false)
            {
                MessageBox.Show("Could Not Find Any Products For The Selected Branch", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
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
