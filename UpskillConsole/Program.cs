using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UpskillConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLConnector.Connect();

            bool successfulLogin = LoginMgmt.LoginToAccount();

            if (successfulLogin)
            {
                Console.WriteLine("\nLogged in succesfully.");
            }
            else
            {
                Console.WriteLine("\nIncorrect login details. Please restart.");
            }
        }

        static void LoginManager(string arg, Classes.LoginDetails account)
        {

        }

        /*static bool Login()
        {
            b#ool choice = PromptForLoginChoice();
            if (choice == true)
            {
                AddNewLogin();
            }
            else
            {
                
            }
        }*/


        static bool authLogin()
        {
            string authCode = "new";
            bool successfulAuth = false;
            int attempts = 0;

            Console.Write("Enter an authorisation code");

            do
            {
                Console.Write(": "); //Prompt the user to enter auth code each attempt.

                if (Console.ReadLine() == authCode)
                {
                    successfulAuth = true;
                    Console.WriteLine("\nAuthorisation code accepted.\n");
                }
                else
                {
                    attempts++;
                    Console.WriteLine("\nInvalid code. Attempts: " + attempts);
                    successfulAuth = false;

                }
            } while (successfulAuth == false);

            return successfulAuth;
        }
    }  
}
