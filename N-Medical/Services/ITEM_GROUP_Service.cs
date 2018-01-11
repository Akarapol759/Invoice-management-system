using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class ITEM_GROUP_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Id, Item_Group_Name, Item_Group_Status, case when Item_Group_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Item_Group";
            return this.ExecuteReader(this.SQL);
        }
    }
}