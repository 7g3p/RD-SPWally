using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace SPWally.DataLayer
{
    class Orders : INotifyPropertyChanged
    {
        // Data members
        private int _OrderID;
        public int OrderID
        {
            get { return _OrderID; }
            set
            {
                if (value >= 0)
                {
                    _OrderID = value;
                    OnPropertyChanged();
                }
            }
        }
        private Customers _Customer;
        public Customers Customer
        {
            get { return _Customer; }
            set
            {
                _Customer = value;
                OnPropertyChanged();
                Customer.PropertyChanged += SubscribeToPropertyChanged;
            }
        }
        private Products _Product;
        public Products Product
        {
            get { return _Product; }
            set
            {
                _Product = value;
                OnPropertyChanged();
            }
        }
        private Branches _Branch;
        public Branches Branch
        {
            get { return _Branch; }
            set
            {
                _Branch = value;
                OnPropertyChanged();
            }
        }
        private DateTime _OrderDate;
        public DateTime OrderDate 
        { 
            get { return _OrderDate; } 
            set 
            { 
                _OrderDate = value;
                OnPropertyChanged();
            } 
        }
        private double _SalesPrice;
        public double SalesPrice
        {
            get { return _SalesPrice; }

            set
            {
                if (value >= 0.00)
                {
                    _SalesPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }

            set
            {
                if (value > 0)
                {
                    _Quantity = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Status;
        public string Status
        {
            get { return _Status; }

            set
            {
                //Checks if the value to be set is valid before setting
                if (value == "PAID" || value == "RFND")
                {
                    _Status = value;
                    OnPropertyChanged();
                }
            }
        }

        public Orders()
        {
            if (Customer == null)
                Customer = new Customers();
            if (Product == null)
                Product = new Products();
            if (Branch == null)
                Branch = new Branches();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SubscribeToPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged();
        }

    }
}
