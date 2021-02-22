using System;
using System.Collections.Generic;
using System.Text;

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
            StringBuilder strb = new StringBuilder();
            strb.Append("SELECT * FROM jobs");
            string query = strb.ToString();
            List<JobStruct> allJobs = new List<JobStruct>(); //The list that will be appended to and returned.
            List<string[]> results = ExecuteQueries.ExecuteNestedReadQuery(query);
            foreach(string[] row in results)
            {
                JobStruct job = new JobStruct(row);
                allJobs.Add(job);
            }
            return allJobs;
        }

        
    }
}
