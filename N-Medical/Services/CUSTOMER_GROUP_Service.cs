using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class CUSTOMER_GROUP_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Cus_Group_Code, Cus_Group_Name, Cus_Group_Status, case when Cus_Group_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Customer_Group Order By Cus_Group_Name asc";
            return this.ExecuteReader(this.SQL);
        }
    }
}