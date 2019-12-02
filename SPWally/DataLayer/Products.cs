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
        private double _wPrice;
        public double wPrice {
            get { return _wPrice; }

            set
            {
                if(value > 0.00)
                {
                    _wPrice = value;
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
                }
            }
        }
    }
}
