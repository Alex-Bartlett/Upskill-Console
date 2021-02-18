using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace UpskillConsole.SQLQueries
{
    class AccountsQueries
    {
        public static void AddAccount(string user, string pass)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("INSERT INTO accounts (username, password) VALUES ");
            strb.Append("('" + user + "', '" + pass + "')");
            string query = strb.ToString();
            try
            {
                int affected = ExecuteQueries.ExecuteNonQuery(query);
                Console.WriteLine(affected + " rows affected. Added " + user);
            }
            catch(SqlException e)
            {
                Console.WriteLine("Error: " + e);
                LoginMgmt.newAccount();
            }
        }
    }
}
