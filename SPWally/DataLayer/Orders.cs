using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SPWally.DataLayer
{
    class Orders
    {
        // Data members
        public int OrderID { get; set; }
        public Customers Customer { get; set; }
        public Products Product { get; set; }
        public Branches Branch { get; set; }
        public DateTime OrderDate { get; set; }
        private double _SalesPrice;
        public double SalesPrice { 
            get { return _SalesPrice; }
            
            set
            {
                if(value >= 0.00)
                {
                    _SalesPrice = value;
                }
            } 
        }
        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }

            set
            {
                if(value > 0 && !(value > _Quantity))
                {
                    _Quantity = value;
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
                }
            }
        }

        public Orders()
        {
            Customer = new Customers();
            Product = new Products();
            Branch = new Branches();
        }
    }
}
