using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class WAREHOUSE_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Warehouse_Code, Warehouse_Name, Warehouse_Status, case when Warehouse_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Warehouse Order By Warehouse_Name asc";
            return this.ExecuteReader(this.SQL);
        }
    }
}