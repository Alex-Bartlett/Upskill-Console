using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UpskillConsole
{
    //This is the full change list.
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            //Establish connection to the database and warn user of any errors.
            SQLConnector.Connect(false);
            //Get the user to login
            if (!LoginMgmt.Login())
            {
                return; //If the login is unsuccessful, the program will exit here. 
                //This would only return false if there is an error somewhere, but regardless, the user shouldn't be able to access the program without logging in.
            }
            Menu.ShowMenu();

        }




        
    }  
}
