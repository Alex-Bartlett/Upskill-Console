﻿using System;
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
            Console.Write("\nEnter username: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter password: ");
            string password = Console.ReadLine();

            string tempUser = "temp";
            string tempPass = "temp";
            
            //Change this to use real acccounts from the accounts list.
            if ((username==tempUser) && (password == tempPass))
            {
                Console.WriteLine("Login successful.");
                return true;
            }
            else
            {
                return false;
            }
            
        }

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

        public static void newAccount()
        {
            if (authLogin()==false)
            {
                Console.WriteLine("\nPlease try again later.");
                return;
            }

            Console.Write("\nEnter a username: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter a password: ");
            string password = Console.ReadLine();
            Console.WriteLine("\nRegistering..");

            SQLQueries.AccountsQueries.AddAccount(username, password);

            Console.WriteLine("Registered. Please login.");

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

            if (choice.Key == ConsoleKey.N)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

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