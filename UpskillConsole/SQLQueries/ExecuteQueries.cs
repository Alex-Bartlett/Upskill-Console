using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace UpskillConsole.SQLQueries
{
    class ExecuteQueries
    {

        public static int ExecuteNonQuery(string commandString)
        {
            SqlConnection connection = SQLConnector.Connect(true);
            using (connection)
            {
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Connection.Open();
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
        }
        /// <summary>
        /// Returns a list format of results from query.
        /// </summary>
        /// <param name="commandString">The sql query to be executed.</param>
        /// <returns></returns>
        public static List<string> ExecuteReadQuery(string commandString)
        {
            List<string> results = new List<string>();

            SqlConnection connection = SQLConnector.Connect(true);
            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(commandString, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i <= reader.FieldCount - 1; i++)
                        {
                            results.Add(reader[i].ToString());
                        }
                    }
                    reader.Close();
                    return results;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    throw;
                }
            }
        }
    }
}
