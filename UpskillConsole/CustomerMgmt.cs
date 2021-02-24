using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole
{
    public class CustomerStruct
    {
        public int Customer_id { get; }
        public string First_name { get; set; }
        public string Surname { get; set; }
        public string House_number { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Mobile_number { get; set; }
        public string Home_number { get; set; }
        public string Email { get; set; }

        public CustomerStruct(string[] customer)
        {
            Customer_id = int.Parse(customer[0]);
            First_name = customer[1];
            Surname = customer[2];
            House_number = customer[3];
            Address = customer[4];
            Postcode = customer[5];
            Mobile_number = customer[6];
            Home_number = customer[7];
            Email = customer[8];
        }
    }


    class CustomerMgmt
    {
        /*public static CustomerStruct SearchCustomers()
        {
            return;
        }*/
    }
}
