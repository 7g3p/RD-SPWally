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
        public Customer CustomerID { get; set; }
        public int ProductID { get; set; }
        public int BranchID { get; set; }
        public DateTime OrderDate { get; set; }
        public double SalesPrice { 
            get { return SalesPrice; }
            
            set
            {
                if(value > 0.00)
                {
                    SalesPrice = value;
                }
            } 
        }
        public int Quantity
        {
            get { return Quantity; }

            set
            {
                if(value > 0 && !(value > Quantity))
                {
                    Quantity = value;
                }
            }
        }
        private string Status
        {
            get { return Status; }

            set
            {
                //Checks if the value to be set is valid before setting
                if (value == "PAID" || value == "RFND")
                {
                    Status = value;
                }
            }
        }
        public bool OrderPaid { get; set; }
        public bool OrderRefunded { get; set; }
    }
}
