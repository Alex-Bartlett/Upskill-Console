using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole.Classes
{
    public class Job
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
        public double Days_worked()
        {
            return Hours_worked / 24;
        }
        public int Customer_id { get; set; }
        public int User_id { get; set; }


    }
}
