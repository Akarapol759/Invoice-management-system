using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class LOGIN_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public void changePassword(string userName, string pass)
        {
            SQL.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
            SQL.Parameters.Add("@pass", SqlDbType.VarChar).Value = pass;
            SQL.CommandText = "UPDATE LOGIN SET PASSWORD = @pass where Username = @userName";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}