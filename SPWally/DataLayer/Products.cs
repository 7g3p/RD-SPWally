using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SPWally.DataLayer
{
    class Products : INotifyPropertyChanged
    {
        private int _ProductID;
        public int productId
        {
            get { return _ProductID; }

            set
            {
                if (value >= 0)
                {
                    _ProductID = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }

            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _wPrice;
        public double wPrice {
            get { return _wPrice; }

            set
            {
                if(value > 0.00)
                {
                    _wPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _Stock;
        public int Stock
        {
            get { return _Stock; }

            set
            {
                if(value >= 0)
                {
                    _Stock = value;
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
