using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole
{
    class LoginMgmt
    {

        public static List<Classes.LoginDetails> accounts;

        static void InitialiseList()
        {
            
        }
        /// <summary>
        /// Returns true if logged in succesfully, false if failed.
        /// </summary>
        /// <returns></returns>
        public static bool LoginToAccount()
        {
            Console.Write("\nEnter username: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter password: ");
            string password = Console.ReadLine();

            string tempUser = "temp";
            string tempPass = "temp";
            
            //Change this to use real acccounts from the accounts list.
            if ((username==tempUser) && (password == tempPass))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        /// <summary>
        /// Prompt user to login or to add new account.
        /// </summary>
        /// <returns>Returns true if new account, false if login</returns>
        static bool PromptForLoginChoice()
        {
            Console.Write("\n[Enter] to login\n[n] to add a new login\n>");
            ConsoleKeyInfo choice = Console.ReadKey();

            if (choice.Key == ConsoleKey.N)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
