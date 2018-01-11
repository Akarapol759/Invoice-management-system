using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class CUSTOMER_TYPE_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Customer_Type_Code, Customer_Type_Name, Customer_Type_Status, case when Customer_Type_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Customer_Type Order By Customer_Type_Name asc";
            return this.ExecuteReader(this.SQL);
        }
    }
}