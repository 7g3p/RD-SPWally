using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SPWally.BusinessLayer
{
    class DataAccess
    {
        const string connectionString = "Server=127.0.0.1; database=wallysworld; UID=root; password=Conestoga1";
        private MySqlConnection _Connection = null;
        public MySqlConnection Connection 
        {
            //Return connection stream to database
            get { return _Connection; }
        }

        private static DataAccess _instance = null;
        public static DataAccess Instance()
        {
            //If this instance is null then create a new one and return it
            if (_instance == null)
                _instance = new DataAccess();
            return _instance;
        }

        public void ConnectToDatabase()
        {
            //Create connection to database
            _Connection = new MySqlConnection(connectionString);

            //Open connection to database
            _Connection.Open();
        }

        public void CloseConnection()
        {
            _Connection.Close();
        }


    }
}
