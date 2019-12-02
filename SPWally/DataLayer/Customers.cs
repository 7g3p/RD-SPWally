using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPWally.DataLayer
{
    class Customers
    {
        public int CustomerID { get; set; }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _FirstName = value;
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
                }
            }
        }
        private string _FullName;
        public string FullName
        {
            get { return _FullName; }

            set
            {
                _FullName = FirstName + " " + LastName;
            }
        }
        public long Phone{ get; set ;}
    }
}
