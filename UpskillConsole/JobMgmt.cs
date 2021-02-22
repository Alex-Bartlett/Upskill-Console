using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole
{
    public class JobStruct
    {
        public int Job_id { get; }
        public string Job_reference { get; set; }
        public bool Complete { get; set; }
        public string Company { get; set; }
        public DateTime Date_start { get; set; } //This might be changed
        public DateTime Date_finished { get; set; } //^^
        public decimal Quoted_amount { get; set; }
        public decimal Invoiced_amount { get; set; }
        public double Hours_worked { get; set; }
        public double Days_worked { get; set; }
        public int Customer_id { get; set; }
        public int User_id { get; set; }

        public JobStruct(string[] job)
        {
            Job_id = int.Parse(job[0]);
            Job_reference = job[1];
            Complete = bool.Parse(job[2]);
            Company = job[3];
            Date_start = DateTime.Parse(job[4]);
            Date_finished = DateTime.Parse(job[5]);
            Quoted_amount = decimal.Parse(job[6]);
            Invoiced_amount = decimal.Parse(job[7]);
            Hours_worked = double.Parse(job[8]);
            Days_worked = double.Parse(job[9]);
            Customer_id = int.Parse(job[10]);
            User_id = int.Parse(job[11]);

        }

    }

    public class JobMgmt
    {
        public static void PrintAllJobs()
        {
            Console.Clear();
            List<JobStruct> allJobs = SQLQueries.JobsQueries.GetAllJobs();
            for(int i = 0; i<allJobs.Count; i++)
            {
                JobStruct job = allJobs[i];
                CustomerStruct customer = SQLQueries.CustomerQueries.GetSingleCustomer(job.Customer_id);
                Console.Write("[" + (i+1) + "] ");
                Console.Write(job.Job_reference + " ");
                Console.Write(job.Date_start.Date.ToString("dd/MM/yyyy") + " ");
                Console.Write(customer.First_name+" "+customer.Surname);
                Console.WriteLine();
            }

            
        }
    }
}
