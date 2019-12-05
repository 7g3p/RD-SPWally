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
    /*
    * NAME : Branches : INotifyPropertyChanged
    * PURPOSE : The Branches class is meant to hold all the data taken from the database that corresponds to the branches
    */
    class Branches : INotifyPropertyChanged
    {
        private int _BranchID;
        public int BranchID
        {
            get { return _BranchID; }
            set
            {
                if (value >= 0)
                {
                    _BranchID = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _BranchName;
        public string BranchName
        {
            get { return _BranchName; }

            set
            {
                if (_BranchName != value)
                {
                    _BranchName = value;
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
