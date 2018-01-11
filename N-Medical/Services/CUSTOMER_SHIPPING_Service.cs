using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class CUSTOMER_SHIPPING_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Cus_Shipping_Code, Cus_Code, Cus_Shipping_Detail, Cus_Shipping_Status, case when Cus_Shipping_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Customer_Shipping";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataByCusCode(string cusCode)
        {
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.CommandText = "Select Cus_Shipping_Code, Cus_Code, Cus_Shipping_Detail, Cus_Shipping_Status, case when Cus_Shipping_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Customer_Shipping where Cus_Code = @cusCode";
            return this.ExecuteReader(this.SQL);
        }
        public void insertShippingData(string shipCode, string cusCode, string shipAddress, string shipStatus, string userCreate)
        {
            SQL.Parameters.Add("@shipCode", SqlDbType.NVarChar).Value = shipCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@shipAddress", SqlDbType.NVarChar).Value = shipAddress;
            SQL.Parameters.Add("@shipStatus", SqlDbType.NVarChar).Value = shipStatus;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Customer_Shipping (Cus_Shipping_Code, Cus_Code, Cus_Shipping_Detail, Cus_Shipping_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@shipCode, @cusCode, @shipAddress, @shipStatus, @userCreate, @userCreate, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateShippingData(string shipCode, string cusCode, string shipAddress, string shipStatus, string userUpdate)
        {
            SQL.Parameters.Add("@shipCode", SqlDbType.NVarChar).Value = shipCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@shipAddress", SqlDbType.NVarChar).Value = shipAddress;
            SQL.Parameters.Add("@shipStatus", SqlDbType.NVarChar).Value = shipStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Customer_Shipping Set Cus_Shipping_Detail = @shipAddress, Cus_Shipping_Status = @shipStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Cus_Shipping_Code = @shipCode and Cus_Code = @cusCode";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}