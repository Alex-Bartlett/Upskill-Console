using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillConsole.SQLQueries
{
    class CustomerQueries
    {
        public static CustomerStruct GetSingleCustomer(int id)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("SELECT * FROM customers WHERE customer_id = " + id);
            string query = strb.ToString();
            CustomerStruct customer = new CustomerStruct(ExecuteQueries.ExecuteReadQuery(query).ToArray());
            return customer;
        }
    }
}
