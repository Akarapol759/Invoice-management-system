using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class VENDER_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select VENDER_CODE, VENDER_FULL_NAME, VENDER_SHORT_NAME, VENDER_GROUP_CODE, case when VENDER_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Vender";
            return this.ExecuteReader(this.SQL);
        }
    }
}