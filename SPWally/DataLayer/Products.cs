using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWally.DataLayer
{
    class Products
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        public double wPrice {
            get { return wPrice; }

            set
            {
                if(value > 0.00)
                {
                    wPrice = value;
                }
            }
        }
        public int Stock
        {
            get { return Stock; }

            set
            {
                if(value >= 0)
                {
                    Stock = value;
                }
            }
        }
    }
}
