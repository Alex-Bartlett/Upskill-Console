using System;
using System.Collections.Generic;
using System.Text;
using UpskillConsole.SQLQueries;

namespace UpskillConsole
{
    public class JobStruct
    {
        public int Job_id { get; set; }
        public string Job_reference { get; set; }
        public DateTime Date_start { get; set; } //This might be changed
        public DateTime Date_finished { get; set; } //^^
        public decimal Quoted_amount { get; set; }
        public decimal Invoiced_amount { get; set; }
        public double Hours_worked { get; set; }
        public double Days_worked { get; set; }
        public int Customer_id { get; set; }
        public int User_id { get; set; }
        public int Status { get; set; }
        public DateTime Date_due { get; set; }
        public string Staff { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        /*
        public JobStruct(string[] job)
        {
            Job_id = int.Parse(job[0]);
            Job_reference = job[1];
            Date_start = DateTime.Parse(job[2]);
            Date_finished = DateTime.Parse(job[3]);
            Quoted_amount = decimal.Parse(job[4]);
            Invoiced_amount = decimal.Parse(job[5]);
            Hours_worked = double.Parse(job[6]);
            Days_worked = double.Parse(job[7]);
            Customer_id = int.Parse(job[8]);
            User_id = int.Parse(job[9]);
            Status = int.Parse(job[10]);
            Date_due = DateTime.Parse(job[11]);
            Staff = job[12];
            Address = job[13];
            Postcode = job[14];

        }*/
        public JobStruct()
		{
            //These data types are not nullable. Therefore they must be assigned default min value instead

            //Ints
            Status = 0;
            Job_id = 0;
            Customer_id = 0;
            User_id = 0;

            //Dates
            Date_start = DateTime.MinValue;
            Date_due = DateTime.MinValue;
            Date_finished = DateTime.MinValue;

            //Doubles (I'm never actually going to use these, but it's too much work to get rid of them now.
            //Who knows, maybe at somepoint I'll make some use. But if I delete in the database, this will break so I have to keep them.
            Hours_worked = 0;
            Days_worked = 0;

            //Okay so I can't be bothered to deal with null values. I'm getting errors in ExecuteQueries to do with parameterisation. So I'm gonna set default values. Sorry
            Job_reference = "Unknown";
            Staff = "None";


		}

    }

    public class JobStructTools {
        public JobStruct CreateJobFromString(string[] job)
        {
            JobStruct jobStruct = new JobStruct();
            jobStruct.Job_id = int.Parse(job[0]);
            jobStruct.Job_reference = job[1];
            jobStruct.Date_start = DateTime.Parse(job[2]);
            jobStruct.Date_finished = DateTime.Parse(job[3]);
            jobStruct.Quoted_amount = decimal.Parse(job[4]);
            jobStruct.Invoiced_amount = decimal.Parse(job[5]);
            jobStruct.Hours_worked = double.Parse(job[6]);
            jobStruct.Days_worked = double.Parse(job[7]);
            jobStruct.Customer_id = int.Parse(job[8]);
            jobStruct.User_id = int.Parse(job[9]);
            jobStruct.Status = int.Parse(job[10]);
            jobStruct.Date_due = DateTime.Parse(job[11]);
            jobStruct.Staff = job[12];
            jobStruct.Address = job[13];
            jobStruct.Postcode = job[14];
            return jobStruct;
        }


    }

    public class JobMgmt
    {
        /// <summary>
        /// A prototype method on printing all jobs from the database to console.
        /// </summary>
        public void PrintAllJobs()
        {
            Console.Clear();
            List<JobStruct> allJobs = JobsQueries.GetAllJobs(); //Get the jobs from the database
            int longestLength = 0;

            List<string> jobReferenceStrings = new List<string>();
            for(int i = 0; i<allJobs.Count; i++)
            {
                //Format the reference string
                StringBuilder strb = new StringBuilder();
                JobStruct job = allJobs[i];
                CustomerStruct customer = CustomerQueries.GetSingleCustomer(job.Customer_id);
                strb.Append("[" + (i+1) + "] ");
                strb.Append(job.Job_reference + " ");
                strb.Append(job.Date_start.Date.ToString("dd/MM/yyyy") + " ");
                strb.Append(customer.First_name+" "+customer.Surname);
                //Add to list
                jobReferenceStrings.Add(strb.ToString());
            }

            //Set the longest length of the job references for formatting the status nicely
            foreach (string x in jobReferenceStrings)
			{
				if (x.Length > longestLength)
				{
                    longestLength = x.Length;
				}
			}

            //Display them to the screen;
            for (int i = 0; i<allJobs.Count; i++)
			{
                Console.Write(jobReferenceStrings[i]);
                WriteStatus(allJobs[i].Status, longestLength);
                Console.WriteLine();

			}

            Console.ReadKey(); //Stop it automatically returning to menu


        }

        private void WriteStatus(int status, int length)
		{
            Console.SetCursorPosition(length + 5, Console.CursorTop);
            Console.Write(" [");
            switch (status)
			{
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Pending");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Started");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Completed");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Invoiced");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("ERROR");
                    break;
			}
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
		}

        /// <summary>
        /// The primary method for getting a user to enter a new job.
        /// </summary>
        public void AddJob()
        {
            //JobStruct job = RetrieveJobDetails();

            JobStruct job = new JobStruct();
            do
            {
                DisplayOptions(job);
            }
            while (job.Job_id == 0);

            //Add final information
            job.Job_reference = "000" + job.Job_id + "_" + job.Address.Replace(" ", "").Substring(0, 10);
            job.User_id = 1;
            JobsQueries.AddJob(job);
        }
        

        /// <summary>
        /// Creates a JobStruct with empty values, min values or 0s where applicable. By adding default values to the JobStruct class, this should be deprecated.
        /// </summary>
        /// <returns></returns>
        private JobStruct GenerateDummyJob()
        {
            string[] dummyJob = new string[] { "0", "", DateTime.MinValue.ToShortDateString(), DateTime.MinValue.ToShortDateString(), "0", "0", "0", "0", "1", "1", "0",DateTime.MinValue.ToShortDateString(), "",  "", ""};

            return new JobStructTools().CreateJobFromString(dummyJob);
        }

        /// <summary>
        /// Diplays the data fields in the console with buttons to fill information in.
        /// </summary>
        private void DisplayOptions(JobStruct job)
		{
            DrawTitle();
            bool requiredFieldEmpty = false;
            if (job.Customer_id > 0) { //If the customer has already been selected for this entry
                CustomerStruct customer = SQLQueries.CustomerQueries.GetSingleCustomer(job.Customer_id);
                string name;
                if (customer.First_name.Length > 0)
                {
                    string initial = customer.First_name.Substring(0, 1); //Get just the first initial if there is a first name
                    string surname = customer.Surname;
                    name = initial + ". " + surname;
                }
				else
				{
                    name = customer.Surname;
				}
                Console.Write("[1] Customer: "+name);
            }
			else //If the customer needs to be selected
            {
                requiredFieldEmpty = true;
                Console.Write("[1] Customer: ");
                InsertRedAsterisk(); //Highlight that it is a required field

            }

            Console.Write("\n[2] Start date: ");

            if (job.Date_start != DateTime.MinValue) //If start date has already been selected, display it
			{
                Console.Write(job.Date_start.ToShortDateString());
			}

            Console.Write("\n[3] Due date: ");

            if (job.Date_due != DateTime.MinValue)
			{
                Console.Write(job.Date_due.ToShortDateString());
			}

            Console.Write("\n[4] Finished date: ");

            if (job.Date_finished != DateTime.MinValue)
			{
                Console.Write(job.Date_finished.ToShortDateString());
			}

            if (job.Address is not null)
            {
                Console.Write("\n[5] Address: ");
                Console.Write(job.Address+", "+job.Postcode);
            }
			else
			{
                requiredFieldEmpty = true;
                Console.Write("\n[5] Address: ");
                InsertRedAsterisk(); //This is a required field
            }

            Console.Write("\n[6] Staff: ");

			if (job.Staff is not null)
			{
                Console.Write("Assigned"); //!!This needs to be completed
			}

            if (job.Status > 0)
            {
                Console.Write("\n[7] Status: ");

				switch (job.Status)
				{
                    case 1:
						Console.Write("Pending");
                        break;
                    case 2:
                        Console.Write("Started");
                        break;
                    case 3:
                        Console.Write("Finished");
                        break;
                    case 4:
                        Console.Write("Invoiced");
                        break;
                    default:
                        Console.Write("ERROR"); //This will only show if the status is higher than 4, which shouldn't happen.
                        break;

				}
			}
			else
			{
                requiredFieldEmpty = true;
                Console.Write("\n[7] Status: ");
                InsertRedAsterisk(); //This is a required field
            }

            if (requiredFieldEmpty == false) //Only show the submit option if all required fields have data
            {
                Console.Write("\n\n[0] Submit\n");
			}
			else
            {
                Console.Write("\n\n");
                InsertRedAsterisk();
                Console.Write("Required field. Complete to submit.\n");
			}
            SelectOption(job, requiredFieldEmpty);
		}
        
        private void SelectOption(JobStruct job, bool requiredFieldEmpty)
		{
            while (true) //Repeat until the user enters a valid input
            {
                Console.Write("\n>");

                ConsoleKeyInfo key = Console.ReadKey();
                try
                {
                    int choice = int.Parse(key.KeyChar.ToString()); //Convert to string and then to int
                    switch (choice)
                    {
                        case 1: //Customer ID
                            SelectACustomer(job);
                            return;
                        case 2: //Start date
                            job.Date_start = GetADate();
                            return;
                        case 3: //Due date
                            job.Date_due = GetADate();
                            return;
                        case 4: //Finished date
                            job.Date_finished = GetADate();
                            return;
                        case 5: //Address
                            SelectAddress(job);
                            return;
                        case 6: //Staff
                            return;
                        case 7: //Status
                            SelectStatus(job);
                            return;
                        case 0:
                            if (!requiredFieldEmpty)
                            {
                                job.Job_id = 1; //Set the job id to 1 to indicate that the job is finished being set up. JobStruct job is the only constant between SelectOption and AddJob.
                            }
                            return;
                    }
                }
                catch { Console.WriteLine("\nInvalid option"); }
            }
		}


		/// <summary>
        /// Prompts the user to select a customer and updates the customer field in the job.
        /// </summary>
        /// <param name="job">The job to be updated</param>
		private void SelectACustomer(JobStruct job)
		{
            Console.Clear(); //Clear the console
            //This needs to be reworked for searching here onwards!!!
            Console.Write("\nEnter customer ID: ");
            int input = int.Parse(Console.ReadLine()); //I'm not going to do any error catches here because its temporary code
            if (input is > 0 and <= 2)//Only customer ids 1 + 2 exist at the point of writing this
			{
                job.Customer_id = input;
			}
            Console.WriteLine(); //Drop a line
		}

        private void SelectAddress(JobStruct job)
		{
            string address;
            string postcode;
            do
            {
                Console.Clear();
                Console.Write("Enter job postcode: ");
                postcode = Console.ReadLine();
                Console.WriteLine("\n\nPostcode: " + postcode);
                Console.Write("Is this correct? [Y]/[N]");
            } while (!GetYOrNFromUser());

            do
            {
                Console.Clear();
                Console.WriteLine("Include: House number, street, town.");
                Console.Write("Enter job address: ");
                address = Console.ReadLine();
                Console.WriteLine("\n\nAddress: " + address + ", " + postcode);
                Console.Write("Is this correct? [Y]/[N]");
            } while (!GetYOrNFromUser());
            job.Address = address;
            job.Postcode = postcode;
                    
		}

        /// <summary>
        /// Prompt the user to set the status of the job
        /// </summary>
        /// <param name="job">Job to set status of</param>
        private void SelectStatus(JobStruct job)
		{
            while (true)
            {
                Console.Clear(); //Clear the console
                Console.WriteLine("[1] Pending");
                Console.WriteLine("[2] Started");
                Console.WriteLine("[3] Completed");
                Console.WriteLine("[4] Invoiced");
                Console.Write("\n> ");
                ConsoleKeyInfo key = Console.ReadKey();
                int choice = int.Parse(key.KeyChar.ToString());

                if (choice is > 0 and <= 4)
				{
                    job.Status = choice;
                    return;
				}
            }
		}

        /// <summary>
        /// Inserts a red asterisk on the current line
        /// </summary>
        private void InsertRedAsterisk()
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Prompt the user to input Y or N. Loops until recognised input.
        /// </summary>
        /// <returns>true if Y, false if N</returns>
        private bool GetYOrNFromUser()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    return false;
                }
                else
                {
                    Console.Write("\nPress [Y]es or [N]o on your keyboard: ");
                }
            } while (true);//Loop until the user enters a valid option.
        }

        /// <summary>
        /// Sets a date in job struct by prompting user for input.
        /// </summary>
        /// <returns>DateTime of the selected date</returns>
        private static DateTime GetADate()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n---Please enter in integer format---");
                Console.WriteLine("---ie. 30, 01, 2021---");
                Console.Write("\nDay: ");
                int day = int.Parse(Console.ReadLine());
                Console.Write("\nMonth: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("\nYear: ");
                int year = int.Parse(Console.ReadLine());

                string formatted = year + "-" + month + "-" + day;

                if (DateTime.TryParse(formatted, out DateTime result))
                {
                    Console.Write(result.ToString());
                    return result;
                }
				else
				{
                    Console.WriteLine("\nPlease try again. ");
				}
            }
        }

        private void DrawTitle()
        {
            Console.Clear(); //Clear the console.
            Console.WriteLine("New job ");
            Console.WriteLine("===================");
        }

        public void SearchJobs()
        {

        }
    }
}
