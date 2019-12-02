using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SPWally.DataLayer
{
    class Customers : INotifyPropertyChanged
    {
        private int _CustomerID;
        public int CustomerID
        {
            get { return _CustomerID; }

            set
            {
                if (value >= 0)
                {
                    _CustomerID = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _FullName;
        public string FullName
        {
            get { return _FullName; }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _FullName = value;
                }
                else
                {
                    _FullName = _FirstName + " " + _LastName;
                }
                OnPropertyChanged();
            }
        }
        private long _Phone;
        public long Phone
        {
            get { return _Phone; }

            set
            {
                if (value >= 0.00)
                {
                    _Phone = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
