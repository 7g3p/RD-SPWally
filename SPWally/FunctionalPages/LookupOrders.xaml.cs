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
        public LookupOrders()
        {
            InitializeComponent();
            Loaded += LookupOrders_Loaded;
        }

        private void LookupOrders_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModelValueOriented();
        }

        private void Refund_Click(object sender, RoutedEventArgs e)
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
            //Code Courtesy of Shmuel Zang in codeprojects.com https://www.codeproject.com/Questions/281551/frame-navigation-in-WPF
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManipulation dataMani = new DatabaseManipulation();

            if(!dataMani.LoadOrder())
            { 
                MessageBox.Show("Could Not Find the Specified Order.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
