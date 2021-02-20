using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole
{
    class LoginMgmt
    {

        /// <summary>
        /// Returns true if logged in succesfully, false if failed.
        /// </summary>
        /// <returns></returns>
        public static bool LoginToAccount()
        {
            //Prompt the user for their existing information
            Console.Write("\nEnter username: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter password: ");
            string password = Console.ReadLine();

            bool successfulLogin = ValidateLogin(username, password); //Checks to see if the username and password match
            if (successfulLogin)
            {
                //Login was successful. Any code to run on login goes here
                Console.WriteLine("\nLogin successful. Welcome " + username);
                return true;
            }
            else
            {
                //Login was unsuccesful.
                Console.WriteLine("\nIncorrect details provided.");
                return false;
            }



        }
        /// <summary>
        /// Compares the login information against the database to see if they match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateLogin(string username, string password)
        {
            //Get all the accounts in the database
            string[] accounts = SQLQueries.AccountsQueries.GetAccounts();
            //Iterate over the array to find a match
            for (int i = 0; i<accounts.Length; i+=2) //Array is constructed in format {username, password, etc.}, therefore iterate every second index to avoid comparing password to next username
            {
                if (username == accounts[i])
                {
                    if (password == accounts[i + 1])
                    {
                        //If the password matches the index after the username, the details are correct
                        return true;
                    }
                }
            }
            //No match was found, so the loop exits and returns false.
            return false;
        }

        /// <summary>
        /// The main login method. Prompts the user to select login or signup, then executes the appropriate methods.
        /// </summary>
        public static void Login()
        {            
            bool choice = PromptForLoginChoice();
            if (choice)
            {
                newAccount();
            }
            else
            {
                LoginToAccount();
            }
        }

        /// <summary>
        /// The main new account method. Prompts the user for auth code, then prompts for new username and password on success. Writes this information to database. Finally, prompts user to login.
        /// </summary>
        public static void newAccount()
        {
            if (authLogin()==false)//Prompt for authorisation code, if they fail within 3 attempts then the code exits
            {
                Console.WriteLine("\nPlease try again later.");
                return;
            }

            //Prompt for new username and password
            Console.Write("\nEnter a username: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter a password: ");
            string password = Console.ReadLine();
            Console.WriteLine("\nRegistering..");

            //Add the account to the database
            SQLQueries.AccountsQueries.AddAccount(username, password);

            Console.WriteLine("Registered. Please login.");
            //Prompt to login
            LoginToAccount();

        }



        /// <summary>
        /// Prompt user to login or to add new account.
        /// </summary>
        /// <returns>Returns true if new account, false if login</returns>
        static bool PromptForLoginChoice()
        {
            Console.Write("\n[Enter] to login\n[n] to add a new login\n>");
            ConsoleKeyInfo choice = Console.ReadKey();
            //Uses console key so user doesn't need to press enter afterwards
            if (choice.Key == ConsoleKey.N)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Get the user to enter an authorisation code. Predefined here.
        /// </summary>
        /// <returns>True on success, false on 3 wrong attempts.</returns>
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

                if (attempts>=3)//Maximum auth attempts
                {
                    break;
                }

            } while (successfulAuth == false);

            return successfulAuth;
        }
    }
}
