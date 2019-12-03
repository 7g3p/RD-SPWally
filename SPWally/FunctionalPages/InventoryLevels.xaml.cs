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
    /// Interaction logic for InventoryLevels.xaml
    /// </summary>
    public partial class InventoryLevels : Page
    {
        private ViewModelValueOriented vmvo;
        public InventoryLevels()
        {
            InitializeComponent();
            Loaded += InventoryLevels_Loaded;
        }

        private void InventoryLevels_Loaded(object sender, RoutedEventArgs e)
        {
            //Variables
            var dataMani = new DatabaseManipulation();

            //Load Viewmodel into dataContext
            vmvo = new ViewModelValueOriented();
            DataContext = vmvo;

            vmvo.CurrentBranchSelected += OnBranchSelected;

            //Check if all branches are loaded into view
            if (dataMani.GetAllBranches() == false)
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


    }
}
