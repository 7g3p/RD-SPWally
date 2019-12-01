using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPWally.DataLayer
{
    class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName
        {
            get { return FirstName; }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    FirstName = value;
                }
            }
        }
        public string LastName
        {
            get { return LastName; }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    LastName = value;
                }
            }
        }
        public int Phone
        {
            get { return Phone; }

            set { Phone = value;}
        }
    }
}
