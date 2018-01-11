using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class SALE_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Sale.Sale_Code, Sale.Sale_Name, Sale.Sale_Position, Sale.Sale_Email, Sale.Sale_Tel, Sale.Sale_Area, Area.Area_Name, Sale.Sale_Status, case when Sale.Sale_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Sale left join Area on Sale.Sale_Area = Area.Area_Code Order By Sale_Name asc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataDup(string saleCode)
        {
            SQL.Parameters.Add("@saleCode", SqlDbType.VarChar).Value = saleCode;
            SQL.CommandText = "Select * from Sale where Sale_Code = @saleCode";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataBySaleCode(string saleCode)
        {
            SQL.Parameters.Add("@saleCode", SqlDbType.VarChar).Value = saleCode;
            SQL.CommandText = "Select * from Sale where Sale_Code = @saleCode";
            return this.ExecuteReader(this.SQL);
        }
        public void insertSaleData(string saleCode, string saleName, string salePosition, string saleEmail, string saleTel, string saleArea, string saleStatus, string userCreate)
        {
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@saleName", SqlDbType.NVarChar).Value = saleName;
            SQL.Parameters.Add("@salePosition", SqlDbType.NVarChar).Value = salePosition;
            SQL.Parameters.Add("@saleEmail", SqlDbType.NVarChar).Value = saleEmail;
            SQL.Parameters.Add("@saleTel", SqlDbType.NVarChar).Value = saleTel;
            SQL.Parameters.Add("@saleArea", SqlDbType.NVarChar).Value = saleArea;
            SQL.Parameters.Add("@saleStatus", SqlDbType.NVarChar).Value = saleStatus;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Sale (Sale_Code, Sale_Name, Sale_Position, Sale_Email, Sale_Tel, Sale_Area, Sale_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@saleCode, @saleName, @salePosition, @saleEmail, @saleTel, @saleArea, @saleStatus, @userCreate, @userCreate, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateSaleData(string saleCode, string saleName, string salePosition, string saleEmail, string saleTel, string saleArea, string saleStatus, string userUpdate)
        {
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@saleName", SqlDbType.NVarChar).Value = saleName;
            SQL.Parameters.Add("@salePosition", SqlDbType.NVarChar).Value = salePosition;
            SQL.Parameters.Add("@saleEmail", SqlDbType.NVarChar).Value = saleEmail;
            SQL.Parameters.Add("@saleTel", SqlDbType.NVarChar).Value = saleTel;
            SQL.Parameters.Add("@saleArea", SqlDbType.NVarChar).Value = saleArea;
            SQL.Parameters.Add("@saleStatus", SqlDbType.NVarChar).Value = saleStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Sale Set Sale_Name = @saleName, Sale_Position = @salePosition, Sale_Email = @saleEmail, Sale_Tel = @saleTel, Sale_Area = @saleArea, Sale_Status = @saleStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Item_Code = @itemCode";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}