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

namespace SPWally
{
    class ViewModelValueOriented : INotifyPropertyChanged
    {
        //Data members
        private static string _OrderIDSearch;
        public string OrderIDSearch
        {
            get { return _OrderIDSearch; }
            set
            {
                if(_OrderIDSearch != value)
                {
                    _OrderIDSearch = value;
                    OnPropertyChanged();
                }
            }
        }

        private static string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }

            set
            {
                if(_FirstName != value && Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _FirstName = value;
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
                }
            }
        }

        private static string _Customer;
        public string Customer
        {
            get { return _Customer; }

            set { _Customer = FirstName + " " + LastName + "; " + Phone; }
        }





        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
