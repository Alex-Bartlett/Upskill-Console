using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole
{
    //This class manages the main menu. Its main function is only called from Program.cs.
    //Its purpose is to display the menu and call the correct methods to navigate the program.
    class Menu
    {
        static bool endLoop = false;
        //The main function.
        public static void ShowMenu()//Rename this
        {
            do
            {
                //Begin by clearing the console.
                ClearConsole();

                //Display the options to the user.
                DisplayMenu();

                //Navigate the user.
                Navigator();
            }
            while (endLoop == false);
        }

        //Clear the console
        private static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Write out all the options for the menu.
        /// </summary>
        private static void DisplayMenu()
        {
            //All the strings are written here so the longest one can be used for underscore sizing.
            string menu = "Menu";
            string search = "[1] Search";
            string addJob = "[2] Add a new job";
            string viewAllJobs = "[3] View all jobs";
            string inProgressJobs = "[4] View in-progress jobs";
            string manageCustomers = "[5] Manage your customers";
            string logout = "[0] Logout";

            string[] allOptions = new string[] { menu, search, addJob, viewAllJobs, inProgressJobs, manageCustomers }; //Add all options here. Also add button to Navigator method.
            //Set a length for the sizing of the underscores.
            int lineLength = GetLongestLength(allOptions)+6;

            //Begin formatting
            Console.WriteLine(); //Drop a line.
            Console.SetCursorPosition((lineLength / 2 - menu.Length/2), Console.CursorTop); //Centre the menu
            Console.WriteLine(menu);
            DrawLine(lineLength, "=");
            Console.WriteLine();//Drop a line.

            //Search
            //User enters a search term. Calls view all jobs function and view all customers and then returns a filtered result based on their reference/name.
            Console.WriteLine(search);

            //Add job
            //Runs program where the user fills in the data for the job and appends it to the database. 
            //They have a chance to view over the data and change any parts by pressing numbers for each part.
            Console.WriteLine(addJob);

            //View all jobs HIGH PRIORITY
            //A function that retrieves all the jobs and returns them.
            Console.WriteLine(viewAllJobs);

            //In-progress jobs
            //Calls the view all jobs function and filters them by in-progress.
            Console.WriteLine(inProgressJobs);

            //Manage customers
            //Sends the user to a new menu where they can view all customers(and delete), search through, or add customers.
            Console.WriteLine(manageCustomers);

            //Logout
            //Returns a boolean to logout, removes the infinite loop warning for the menu.
            Console.WriteLine(logout);

            //End formatting
            Console.WriteLine(); //Drop a line.
            DrawLine(lineLength, "=");
        }

        /// <summary>
        /// Gets the user to enter an input and navigates them to where they asked.
        /// </summary>
        private static void Navigator()
        {
            Console.Write("\n>");
            ConsoleKeyInfo choice = Console.ReadKey();
            Console.WriteLine(); //Drop a line

            JobMgmt jobMgmt = new JobMgmt();

            try
            {
                switch (int.Parse(choice.KeyChar.ToString()))
                {
                    //Search
                    case 1:
                        {
                            //Add method
                            break;
                        }
                    //Add job
                    case 2:
                        {
                            //Add method
                            jobMgmt.AddJob();
                            break;
                        }
                    //View all jobs
                    case 3:
                        {
                            //Add method
                            jobMgmt.PrintAllJobs();
                            break;
                        }
                    //In-progress jobs
                    case 4:
                        {
                            //Add method
                            break;
                        }
                    //Manage customers
                    case 5:
                        {
                            //Add method
                            break;
                        }
                    case 0:
                        {
                            endLoop = true;
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Option does not exist. Try again.");
                            Navigator();
                            break;
                        }
                }
            }
            catch(Exception e)
            {
                if (e is FormatException)
                {
                    Console.WriteLine("Format exception. Option does not exist. Try again.");
                    Navigator();
                }
                else
                {
                    Console.WriteLine("An error occured. Please report.");
                    throw;
                }
            
            }
        }




        /// <summary>
        /// Calculate the longest length of an array of strings.
        /// </summary>
        /// <param name="strings">Array of strings to be compared.</param>
        /// <returns>Length of the longest string.</returns>
        private static int GetLongestLength(string[] strings)
        {
            int longestString = 0;
            for(int i = 0; i<strings.Length; i++)
            {
                if (strings[i].Length > longestString)
                {
                    longestString = strings[i].Length;
                }
            }
            return longestString;
        }

        /// <summary>
        /// Draws an underscore using equals signs. Adds a new line at the end.
        /// </summary>
        /// <param name="length">The length of the line.</param>
        /// <param name="character">The character to use for the line.</param>
        private static void DrawLine(int length, string character)
        {
            for(int i = 0; i<length; i++)
            {
                Console.Write(character);
            }
            Console.WriteLine();
        }

    }
}
/* References used:
 * 
 * https://stackoverflow.com/questions/6006618/how-to-draw-a-rectangle-in-console-application Used to learn how to draw the rectangle for menu.
 * 
 * 
 * 
 * 
 */
