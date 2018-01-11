using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class REMARK_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Id, Remark, Remark_Status, case when Remark_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Remark Order By Remark asc";
            return this.ExecuteReader(this.SQL);
        }
    }
}