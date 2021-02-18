using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

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
    }
}
