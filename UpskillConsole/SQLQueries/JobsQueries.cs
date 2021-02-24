using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace UpskillConsole.SQLQueries
{
    //This class contains the sql queries required for JobManagement.cs
    class JobsQueries
    {        
        /// <summary>
        /// Returns a list containing all jobs in JobStruct format.
        /// </summary>
        /// <returns></returns>
        public static List<JobStruct> GetAllJobs()
        {
            JobStructTools jobStructTools = new();
            StringBuilder strb = new StringBuilder();
            strb.Append("SELECT * FROM jobs");
            string query = strb.ToString();
            List<JobStruct> allJobs = new List<JobStruct>(); //The list that will be appended to and returned.
            List<string[]> results = ExecuteQueries.ExecuteNestedReadQuery(query);
            foreach(string[] row in results)
            {
                JobStruct job = jobStructTools.CreateJobFromString(row);
                allJobs.Add(job);
            }
            return allJobs;
        }


        /// <summary>
        /// Adds a JobStruct to the jobs database. Only takes one at a time, so to add multiple, iterate.
        /// </summary>
        /// <param name="job">Job to be appended.</param>
        public static void AddJob(JobStruct job)
        {
            //Retrieve the connection
            SqlConnection connection = SQLConnector.Connect(true);
            //Deconstruct the struct
            StringBuilder strb = new StringBuilder();
            //Build the query
            strb.Append("INSERT INTO jobs VALUES ("); //If this errors, specify all values except job_id in INSERT INTO(etc.)
            strb.Append("@Job_reference,");
            strb.Append("@Date_start,");
            strb.Append("@Date_finished,");
            strb.Append("@Quoted_amount,");
            strb.Append("@Invoiced_amount,");
            strb.Append("@Hours_worked,");
            strb.Append("@Days_worked,");
            strb.Append("@Customer_id,");
            strb.Append("@User_id,");
            strb.Append("@Status,");
            strb.Append("@Date_due,");
            strb.Append("@Staff,");
            strb.Append("@Address,");
            strb.Append("@Postcode");
            strb.Append(")");

            string query = strb.ToString();
            //Build the command
            SqlCommand command = new SqlCommand(query, connection);
            //Add parameters
            try
            {
                command.Parameters.Add("@Job_reference", SqlDbType.VarChar).Value = job.Job_reference;
                command.Parameters.Add("@Date_start", SqlDbType.Date).Value = job.Date_start;
                command.Parameters.Add("@Date_finished", SqlDbType.Date).Value = job.Date_finished;
                command.Parameters.Add("@Quoted_amount", SqlDbType.Decimal).Value = job.Quoted_amount;
                command.Parameters.Add("@Invoiced_amount", SqlDbType.Decimal).Value = job.Invoiced_amount;
                command.Parameters.Add("@Hours_worked", SqlDbType.Decimal).Value = job.Hours_worked;
                command.Parameters.Add("@Days_worked", SqlDbType.Decimal).Value = job.Days_worked;
                command.Parameters.Add("@Customer_id", SqlDbType.Int).Value = job.Customer_id;
                command.Parameters.Add("@User_id", SqlDbType.Int).Value = job.User_id;
                command.Parameters.Add("@Status", SqlDbType.Int).Value = job.Status;
                command.Parameters.Add("@Date_due", SqlDbType.Date).Value = job.Date_due;
                command.Parameters.Add("@Staff", SqlDbType.VarChar).Value = job.Staff;
                command.Parameters.Add("@Address", SqlDbType.VarChar).Value = job.Address;
                command.Parameters.Add("@Postcode", SqlDbType.VarChar).Value = job.Postcode;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error occured formatting query:\n" + e);
                throw;
            }

            int rowsAffected = ExecuteQueries.ExecuteParameterisedNonQuery(command);
            Console.WriteLine(rowsAffected + " rows affected.");
        }

        
    }
}
