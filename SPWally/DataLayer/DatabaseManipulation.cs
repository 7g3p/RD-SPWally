﻿using System;
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

        public bool LoadOrder()
        {
            //Get Database Connection
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query string
            string query = @"SELECT O.OrderID, C.CustomerID, C.FirstName, C.LastName, C.Phone, P.productID, P.ProductName, p.wPrice, P.Stock, B.BranchID, B.BranchName, O.OrderDate, O.sPrice, O.Status, O.ItemQuantity
                                    FROM
                                        Orders O
                                    INNER JOIN
                                        Customers C ON C.CustomerID = O.CustomerID
                                    INNER JOIN
                                        Products P ON P.ProductID = O.ProductID
                                    INNER JOIN
                                        Branch B ON B.BranchID = O.BranchID
                                    WHERE
                                        O.OrderID = @OrderIDSearch; ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            //Add variabled values
            command.Parameters.AddWithValue("@OrderIDSearch", boundData.OrderIDSearch);

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
                    boundData.Order.OrderID = reader.GetInt32(0);
                    boundData.Order.Customer.CustomerID = reader.GetInt32(1);
                    boundData.Order.Customer.FirstName = reader.GetString(2);
                    boundData.Order.Customer.LastName = reader.GetString(3);
                    boundData.Order.Customer.FullName = reader.GetString(2) + " " + reader.GetString(3);
                    boundData.Order.Customer.Phone = reader.GetInt64(4);
                    boundData.Order.Product.productId = reader.GetInt32(5);
                    boundData.Order.Product.ProductName = reader.GetString(6);
                    boundData.Order.Product.wPrice = reader.GetDouble(7);
                    boundData.Order.Product.Stock = reader.GetInt32(8);
                    boundData.Order.Branch.BranchID = reader.GetInt32(9);
                    boundData.Order.Branch.BranchName = reader.GetString(10);
                    boundData.Order.OrderDate = reader.GetDateTime(11);
                    boundData.Order.SalesPrice = reader.GetDouble(12);
                    boundData.Order.Status = reader.GetString(13);
                    boundData.Order.Quantity = reader.GetInt32(14);
                }

                //Close connection and return true execution completes without problems
                conn.CloseConnection();
                return true;
            }
        }

    }
}
