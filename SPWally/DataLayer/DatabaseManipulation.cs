﻿using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Threading.Tasks;
using SPWally.BusinessLayer;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SPWally.DataLayer
{
    /*
    * NAME : DatabaseManipulation
    * PURPOSE : The DatabaseManipulation class is meant to hold all the sql commands needed for the program
    */
    class DatabaseManipulation
    {
        //Variables
        private ViewModelValueOriented boundData;

        /*
        * FUNCTION : DatabaseManipulation
        * DESCRIPTION :
        *           This is the constructor for the class
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       N/A
        */
        public DatabaseManipulation()
        {
            //Sets the private data member to equal the ViewModel
            boundData = new ViewModelValueOriented();
        }

        /*
        * FUNCTION : FindCustomer
        * DESCRIPTION :
        *           This function finds the customer in the database and informs of success or failure
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       int : returns the customerID of the found customer
        */
        public int FindCustomer()
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
                    return dbIDReturned;
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
                return dbIDReturned;
            }
        }

        /*
        * FUNCTION : AddCustomer
        * DESCRIPTION :
        *           This function Adds the customer to the database and informs of success or failure
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       int : returns the customerID of the added customer
        */
        public int AddCustomer()
        {
            //Get Database Connection
            int custID = (CountCustomers() + 1);
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query string
            string query = @"INSERT INTO Customers(CustomerID, FirstName, LastName, Phone) VALUES 
                                    (@CustomerID, @FirstName, @LastName,  @Phone); ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            //Add variabled values
            command.Parameters.AddWithValue("@CustomerID", custID);
            command.Parameters.AddWithValue("@FirstName", boundData.FirstName);
            command.Parameters.AddWithValue("@LastName", boundData.LastName);
            command.Parameters.AddWithValue("@Phone", boundData.Phone);

            //Check if the command executed Properly
            if(0 == command.ExecuteNonQuery())
            {
                conn.CloseConnection();
                return -1;
            }

            //Close connection and return true execution completes without problems
            conn.CloseConnection();
            return custID;
        }

        /*
        * FUNCTION : CountCustomers
        * DESCRIPTION :
        *           This function counts the customer number of customers in the database and informs and returns that count
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       int : returns the number of customers in the database
        */
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

        /*
        * FUNCTION : LoadOrder
        * DESCRIPTION :
        *           This function loads the order from the database based on the OrderID
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       bool : returns a success(true) or failure(false)
        */
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

        /*
        * FUNCTION : GetAllBranches
        * DESCRIPTION :
        *           This function loads the all the branches from the database
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       bool : returns a success(true) or failure(false)
        */
        public bool GetAllBranches()
        {
            //Variable
            List<Branches> allBranches = new List<Branches>();
            Branches branch;
            //Get Database Connection
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query string
            string query = @"SELECT BranchID, BranchName
                                    FROM
                                        Branch; ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

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
                    //Instantiate new Branches object
                    branch = new Branches();

                    //Fill object
                    branch.BranchID = reader.GetInt32(0);
                    branch.BranchName = reader.GetString(1);

                    //Add object to list
                    allBranches.Add(branch);
                }

                //Populate
                boundData.BranchList = new BindableCollection<Branches>(allBranches);

                //Close connection and return true if execution completes without problems
                conn.CloseConnection();
                return true;
            }
        }

        /*
        * FUNCTION : GetAllProductsInBranch
        * DESCRIPTION :
        *           This function loads the all the products from the database based on the branchID
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       bool : returns a success(true) or failure(false)
        */
        public bool GetAllProductsInBranch()
        {
            //Variable
            List<Products> allProducts = new List<Products>();
            Products product;
            //Get Database Connection
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query string
            string query = @"SELECT P.ProductID, P.ProductName, P.wPrice, P.Stock
                            FROM
                                branchLine bl
                                    INNER JOIN
                                Products P ON P.ProductID = bl.ProductID
                                    INNER JOIN
                                branch b ON b.branchID = bl.branchID
                            WHERE
                                b.branchID = @currentBranchID; ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            command.Parameters.AddWithValue("@currentBranchID", boundData.CurrentBranch.BranchID);

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
                    //Instantiate new Branches object
                    product = new Products();

                    //Fill object
                    product.productId = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.wPrice = reader.GetDouble(2);
                    product.Stock = reader.GetInt32(3);

                    //Add object to list
                    allProducts.Add(product);
                }

                //Populate
                boundData.ProductList = new BindableCollection<Products>(allProducts);

                //Close connection and return true if execution completes without problems
                conn.CloseConnection();
                return true;
            }
        }

        /*
        * FUNCTION : AddOrdersFromList
        * DESCRIPTION :
        *           This function adds all the orders from the order list to the database
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       bool : returns a success(true) or failure(false)
        */
        public bool AddOrdersFromList()
        {
            //Get Database Connection
            List<Orders> orderList = boundData.OrderList;
            var conn = DataAccess.Instance();

            foreach (Orders ord in orderList)
            {
                conn.ConnectToDatabase();

                //Create Query string
                string query = @"INSERT INTO Orders(OrderID, CustomerID, ProductID, BranchID, OrderDate, sPrice, Status, ItemQuantity) VALUES 
                                    (@OrderID, @CustomerID, @ProductID, @BranchID, @OrderDate, @sPrice, @Status, @ItemQuantity); ";

                //Create command
                var command = new MySqlCommand(query, conn.Connection);

                //Add variabled values
                command.Parameters.AddWithValue("@OrderID", (CountOrders() + 1));
                command.Parameters.AddWithValue("@CustomerID", ord.Customer.CustomerID);
                command.Parameters.AddWithValue("@ProductID", ord.Product.productId);
                command.Parameters.AddWithValue("@BranchID", ord.Branch.BranchID);
                command.Parameters.AddWithValue("@OrderDate", ord.OrderDate);
                command.Parameters.AddWithValue("@sPrice", ord.SalesPrice);
                command.Parameters.AddWithValue("@Status", ord.Status);
                command.Parameters.AddWithValue("@ItemQuantity", ord.Quantity);

                //Check if the command executed Properly
                if (0 == command.ExecuteNonQuery())
                {
                    conn.CloseConnection();
                    return false;
                }

                //Command to update the stocks according to the orders
                command.CommandText = @"UPDATE Products
                                        SET 
                                            Stock = @NewStock
                                        WHERE
                                            ProductID = @PproductID; ";

                //Check if the order is a purchase or refund
                if (ord.Status == "PAID")
                {
                    command.Parameters.AddWithValue("@NewStock", (ord.Product.Stock - ord.Quantity));
                }
                else
                {
                    command.Parameters.AddWithValue("@NewStock", (ord.Product.Stock + ord.Quantity));
                }
                command.Parameters.AddWithValue("@PproductID", ord.Product.productId);

                //Check if the command executed Properly
                if (0 == command.ExecuteNonQuery())
                {
                    conn.CloseConnection();
                    return false;
                }

                conn.CloseConnection();
            }

            //Close connection and return true execution completes without problems
            return true;
        }

        /*
        * FUNCTION : CountOrders
        * DESCRIPTION :
        *           This function counts all the orders in the database
        * PARAMETERS :
        *       N/A
        * RETURNS :
        *       int : returns the total number of orders in the database
        */
        public int CountOrders()
        {
            //Return variable
            int retCode = -1;
            //Connect to Database
            var conn = DataAccess.Instance();
            conn.ConnectToDatabase();

            //Create Query String
            string query = @"SELECT COUNT(*) FROM Orders; ";

            //Create command
            var command = new MySqlCommand(query, conn.Connection);

            //Execute Reader and read the result
            MySqlDataReader reader;
            using (reader = command.ExecuteReader())
            {
                while (reader.Read())
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
