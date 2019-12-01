using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPWally.BusinessLayer;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SPWally.DataLayer
{
    class DatabaseManipulation
    {
        private ViewModelValueOriented boundData;
        public DatabaseManipulation()
        {
            boundData = new ViewModelValueOriented();
        }

        public bool FindCustomer()
        {
            //Variables
            int dbIDReturned = -1;
            string dbFirstNameReturned;
            string dbLastNameReturned;
            long dbPhoneReturned;

            //Get Database Connection
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query string
            string query = @"SELECT CustomerID, FirstName, LastName, Phone 
                                    FROM Customers 
                                    WHERE FirstName = @FirstName 
                                    AND LastName = @LastName 
                                    AND Phone = @Phone; ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            //Add variabled values
            command.Parameters.AddWithValue("@FirstName", boundData.FirstName);
            command.Parameters.AddWithValue("@LastName", boundData.LastName);
            command.Parameters.AddWithValue("@Phone", boundData.Phone);

            //Execute the command
            MySqlDataReader reader;
            using (reader = command.ExecuteReader())
            {

                //Check if Customer was found; if not then close connection and return false
                if (reader.HasRows == false)
                {
                    conn.CloseConnection();
                    return false;
                }

                //Read Data into variables
                while (reader.Read())
                {
                    dbIDReturned = reader.GetInt32(0);
                    dbFirstNameReturned = reader.GetString(1);
                    dbLastNameReturned = reader.GetString(2);
                    dbPhoneReturned = reader.GetInt64(3);
                }

                //Close connection and return true execution completes without problems
                conn.CloseConnection();
                return true;
            }
        }

        public bool AddCustomer()
        {
            //Get Database Connection
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query string
            string query = @"INSERT INTO Customers(CustomerID, FirstName, LastName, Phone) VALUES 
                                    (@CustomerID, @FirstName, @LastName,  @Phone); ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            //Add variabled values
            command.Parameters.AddWithValue("@CustomerID", (CountCustomers() + 1));
            command.Parameters.AddWithValue("@FirstName", boundData.FirstName);
            command.Parameters.AddWithValue("@LastName", boundData.LastName);
            command.Parameters.AddWithValue("@Phone", boundData.Phone);

            //Check if the command executed Properly
            if(0 == command.ExecuteNonQuery())
            {
                conn.CloseConnection();
                return false;
            }

            //Close connection and return true execution completes without problems
            conn.CloseConnection();
            return true;
        }

        public int CountCustomers()
        {
            //Return variable
            int retCode = -1;
            //Connect to Database
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query String
            string query = @"SELECT COUNT(*) FROM Customers; ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            //Execute Reader and read the result
            MySqlDataReader reader;
            using(reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    retCode = reader.GetInt32(0);
                }
            }

            //Close connection and return the count
            conn.CloseConnection();
            return retCode;
        }

    }
}
