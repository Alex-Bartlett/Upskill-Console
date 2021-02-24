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
                try
                {
                    SqlCommand command = new SqlCommand(commandString, connection);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    command.Connection.Close();
                    return affectedRows;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes an non-query using a preconstructed SqlCommand to allow for parameterisation. Requires pre-connection to retrieve connection. For this, use SQLConnector.Connect(true).
        /// </summary>
        /// <param name="command">Command to be executed.</param>
        /// <returns>Amount of rows affected.</returns>
        public static int ExecuteParameterisedNonQuery(SqlCommand command)
        {
            SqlConnection connection = SQLConnector.Connect(true);
            using (connection)
            {
                try
                {
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    return affectedRows;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    throw;
                }
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

                    int fieldCount;

                    while (reader.Read())
                    {
                        fieldCount = reader.FieldCount;

                        for (int i = 0; i < fieldCount; i++) //This is what I changed. <= -1
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

        /// <summary>
        /// Returns a 2-dimensional list of results, each array in list is a row of the database.
        /// </summary>
        /// <param name="commandString">The sql query to be executed.</param>
        /// <returns></returns>
        public static List<string[]> ExecuteNestedReadQuery(string commandString)
        {
            List<string[]> results = new List<string[]>();

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

                        string[] subResults = new string[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++) 
                        {
                            subResults[i] = (reader[i].ToString());
                        }

                        results.Add(subResults);
                    }
                    reader.Close();
                    return results;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    throw;
                }
            }

        }
    }
}
