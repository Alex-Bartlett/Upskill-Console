using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace UpskillConsole
{
    class SQLConnector
    {
        /// <summary>
        /// Send true if you do not want connection messages.
        /// </summary>
        /// <param name="noErrors"> send false to receive confirmations of connections.</param>
        /// <returns></returns>
        public static SqlConnection Connect(bool noErrors)
        {
            Console.WriteLine("Getting Connection ...");


            //your connection string 
            string connString = @"Server = tcp:upskilljobmgmt.database.windows.net,1433; Initial Catalog = Upskill; Persist Security Info = False; User ID = AdminAccount; Password =@!llCQkr35Pb; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);

            if (noErrors == false)
            {
                try
                {
                    Console.WriteLine("Opening Connection ...");

                    //open connection
                    conn.Open();

                    Console.WriteLine("Connection successful!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                conn.Close();
            }
            return conn;
        }
    }
}
