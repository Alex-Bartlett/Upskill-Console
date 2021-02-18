using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UpskillConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLConnector.Connect(false);
            LoginMgmt.Login();

        }




        
    }  
}
