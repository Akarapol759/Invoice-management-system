using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class AREA_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select * from Area";
            return this.ExecuteReader(this.SQL);
        }
    }
}