using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class DELIVERY_ROUTE_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Delivery_Route_Code, Delivery_Route_Name, Delivery_Route_Status, case when Delivery_Route_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Delivery_Route Order By Delivery_Route_Name asc";
            return this.ExecuteReader(this.SQL);
        }
    }
}