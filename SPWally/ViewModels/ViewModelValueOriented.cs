using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SPWally.DataLayer;

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

        private static string _Phone;
        public string Phone
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

        private static string _OrderIDSearch;
        public string OrderIDSearch
        {
            get { return _OrderIDSearch; }
            set
            {
                if (_OrderIDSearch != value)
                {
                    _OrderIDSearch = value;
                    OnPropertyChanged();
                }
            }
        }

        public ViewModelValueOriented()
        {
            if (_Order == null)
                Order = new Orders();

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
