using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class COSTCENTER_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select COST_CENTER_CODE, COST_CENTER_TYPE, COST_CENTER_NAME, case when COST_CENTER_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from CostCenter";
            return this.ExecuteReader(this.SQL);
        }
    }
}