using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class PRICE_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Price.Id, Customer.Cus_Group_Code, Price.Cus_Code, Customer.Cus_Name, Price.Item_Code, Item.Item_Name, Price.Unit_Price, Price.Sale_Code_BB, Price.Price_Status, case when Price.Price_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Price left join Item on Price.Item_Code = Item.Item_Code left join Customer on Price.Cus_Code = Customer.Cus_Code ";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataByCusCode(string cusCode)
        {
            SQL.Parameters.Add("@cusCode", SqlDbType.VarChar).Value = cusCode;
            SQL.CommandText = "Select Price.Id, Customer.Cus_Group_Code, Price.Cus_Code, Customer.Cus_Name, Price.Item_Code, Item.Item_Name, Price.Unit_Price, Price.Sale_Code_BB, Price.Price_Status, case when Price.Price_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Price left join Item on Price.Item_Code = Item.Item_Code left join Customer on Price.Cus_Code = Customer.Cus_Code where Price.Cus_Code = @cusCode";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataDup(string itemCode, string cusCode)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = itemCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.VarChar).Value = cusCode;
            SQL.CommandText = "Select * from Price Where Item_Code = @itemCode and Cus_Code = @cusCode";
            return this.ExecuteReader(this.SQL);
        }
        public void insertPriceData(string itemCode, string cusCode, string unitPrice, string saleCode, string priceStatus, string userCreate)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@unitPrice", SqlDbType.Float).Value = unitPrice;
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@priceStatus", SqlDbType.NVarChar).Value = priceStatus;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Price (Item_Code, Cus_Code, Unit_Price, Sale_Code_BB, Price_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@itemCode, @cusCode, @unitPrice, @saleCode, @priceStatus, @userCreate, @userCreate, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updatePriceData(string itemCode, string cusCode, string unitPrice, string saleCode, string priceStatus, string userUpdate)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@unitPrice", SqlDbType.Float).Value = unitPrice;
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@priceStatus", SqlDbType.NVarChar).Value = priceStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Price Set Unit_Price = @unitPrice, Sale_Code_BB = @saleCode, Price_Status = @priceStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Item_Code = @itemCode and Cus_Code = @cusCode";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}