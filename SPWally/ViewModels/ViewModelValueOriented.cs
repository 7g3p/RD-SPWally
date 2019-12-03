using System;
using Caliburn.Micro;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SPWally.DataLayer;
using System.Collections.Generic;

namespace SPWally
{
    class ViewModelValueOriented : INotifyPropertyChanged
    {
        //Data members
        private static string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }

            set
            {
                if(_FirstName != value && Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _FirstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private static string _LastName;
        public string LastName
        {
            get { return _LastName; }

            set
            {
                if (_LastName != value && Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _LastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private static long _Phone;
        public long Phone
        {
            get { return _Phone; }

            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        public static Orders _Order;
        public Orders Order
        {
            get { return _Order; }
            set
            {
                _Order = value;
                OnPropertyChanged();
                _Order.PropertyChanged += SubscribeToPropertyChanged;
            }
        }

        private static int _OrderIDSearch;
        public int OrderIDSearch
        {
            get { return _OrderIDSearch; }
            set
            {
                if (_OrderIDSearch != value)
                {
                    _OrderIDSearch = value;
                    OnPropertyChanged("OrderIDSearch");
                    OnNewOrderID();
                }
            }
        }

        private static BindableCollection<Branches> _Branches;
        public BindableCollection<Branches> BranchList
        {
            get { return _Branches; }
            set
            {
                _Branches = value;
                OnPropertyChanged();
            }
        }

        private static Branches _CurrentBranch;
        public Branches CurrentBranch
        {
            get { return _CurrentBranch; }
            set
            {
                if (value.BranchID > 0)
                {
                    _CurrentBranch = value;
                    OnPropertyChanged();
                    OnCurrentBranchSelected();
                }
            }
        }

        private static BindableCollection<Products> _Products;
        public BindableCollection<Products> ProductList
        {
            get { return _Products; }
            set
            {
                _Products = value;
                OnPropertyChanged();
                _Products.CollectionChanged += _Products_CollectionChanged;
            }
        }

        private static int _ProductQuantity;
        public int ProductQuantity
        {
            get { return _ProductQuantity; }
            set
            {
                _ProductQuantity = value;
                OnPropertyChanged();
            }
        }

        private static Products _SelectedProduct;
        public Products SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }
        }

        private static Customers _CurrentCustomer;
        public Customers CurrentCustomer
        {
            get { return _CurrentCustomer; }
            set
            {
                _CurrentCustomer = value;
                OnPropertyChanged();
            }
        }

        private static List<Orders> _OrderList;
        public List<Orders> OrderList
        {
            get { return _OrderList; }
            set
            {
                _OrderList.AddRange(value);
            }
        }

        public ViewModelValueOriented()
        {
            if (_Order == null)
                Order = new Orders();

            if (_SelectedProduct == null)
                _SelectedProduct = new Products();

            if (_CurrentCustomer == null)
                _CurrentCustomer = new Customers();

            if (_OrderList == null)
                _OrderList = new List<Orders>();

        }

        public event EventHandler NewOrderID;
        private void OnNewOrderID()
        {
            NewOrderID?.Invoke(this, new EventArgs());
        }

        public event EventHandler CurrentBranchSelected;
        private void OnCurrentBranchSelected()
        {
            CurrentBranchSelected?.Invoke(this, new EventArgs());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SubscribeToPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        private void _Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }

    }
}
