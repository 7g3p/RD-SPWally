using System;
using Caliburn.Micro;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SPWally.DataLayer;
using System.Collections.Generic;

namespace SPWally
{
    /*
    * NAME : ViewModelValueOriented : INotifyPropertyChanged
    * PURPOSE : The ViewModelValueOriented is to hold all the bound data of the xaml pages with private static variables
    *           that can be accessed from new instances of this class without the need to return anything. It also contains
    *           events and event handles for events that need to be triggered in the xaml.cs code.
    */
    class ViewModelValueOriented : INotifyPropertyChanged
    {
        //Data members
        /*
        * FUNCTION : FirstName
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       string : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : LastName
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       string : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : Phone
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       long : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : Order
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       Orders : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : OrderIDSearch
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       int : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : BranchList
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       BindableCollection<Branches> : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : CurrentBranch
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       Branches : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : ProductList
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       BindableCollection<Products> : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : ProductQuantity
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       int : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : SelectedProduct
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       Products : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : CurrentCustomer
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       Customers : Returns the static variable of the same name
        */
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

        /*
        * FUNCTION : OrderList
        * DESCRIPTION :
        *           This function checks the input and gets/sets from the coupled static variables
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       List<Orders> : Returns the static variable of the same name
        */
        private static List<Orders> _OrderList;
        public List<Orders> OrderList
        {
            get { return _OrderList; }
            set
            {
                _OrderList.AddRange(value);
            }
        }

        /*
        * FUNCTION : ViewModelValueOriented
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
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

        /*
        * FUNCTION : OnNewOrderID
        * DESCRIPTION :
        *           This function triggers the NewOrderID event
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        private void OnNewOrderID()
        {
            NewOrderID?.Invoke(this, new EventArgs());
        }


        public event EventHandler CurrentBranchSelected;

        /*
        * FUNCTION : OnCurrentBranchSelected
        * DESCRIPTION :
        *           This function triggers the CurrentBranchSelected event
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        private void OnCurrentBranchSelected()
        {
            CurrentBranchSelected?.Invoke(this, new EventArgs());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /*
        * FUNCTION : OnPropertyChanged
        * DESCRIPTION :
        *           This function triggers the propertyChanged event
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*
        * FUNCTION : SubscribeToPropertyChanged
        * DESCRIPTION :
        *           This function is the event handler that calls the OnPropertyChanged event handler
        * PARAMETERS :
        *       object sender: the object sending the event
        *       PropertyChangedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void SubscribeToPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        /*
        * FUNCTION : FirstName
        * DESCRIPTION :
        *           This function is the event handler that calls the OnPropertyChanged event handler
        * PARAMETERS :
        *       object sender : the object sending the event
        *       NotifyCollectionChangedEventArgs e : the event
        * RETURNS :
        *       N/A
        */
        private void _Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }

    }
}
