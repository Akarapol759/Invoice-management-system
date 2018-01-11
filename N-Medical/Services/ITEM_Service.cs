using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class ITEM_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData(string itemType)
        {
            if (string.IsNullOrEmpty(itemType))
            {
                SQL.CommandText = "Select A.Item_Code, A.Item_Name, A.Item_Description, A.Item_Group_Id, B.Item_Group_Name, A.Item_Type, case when A.Item_Type = 'I' then 'Inventory' else 'Equipment' end as Item_Type_Name, A.Item_Status, case when A.Item_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Item A inner join Item_Group B on A.Item_Group_Id = B.Id order by A.Item_Code asc";
            }
            else
            {
                SQL.CommandText = "Select A.Item_Code, A.Item_Name, A.Item_Description, A.Item_Group_Id, B.Item_Group_Name, A.Item_Type, case when A.Item_Type = 'I' then 'Inventory' else 'Equipment' end as Item_Type_Name, A.Item_Status, case when A.Item_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Item A inner join Item_Group B on A.Item_Group_Id = B.Id Where A.Item_Type in ('" + itemType + "')  order by A.Item_Code asc";
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataByItemCode(string itemCode)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = itemCode;
            SQL.CommandText = "Select A.Item_Code, A.Item_Name, A.Item_Description, A.Item_Group_Id, B.Item_Group_Name, A.Item_Type, case when A.Item_Type = 'I' then 'Inventory' else 'Equipment' end as Item_Type_Name, A.Item_Status, case when A.Item_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Item A inner join Item_Group B on A.Item_Group_Id = B.Id Where A.Item_Code = @itemCode order by A.Item_Code asc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataDup(string itemCode, string itemType)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = itemCode;
            SQL.Parameters.Add("@itemType", SqlDbType.VarChar).Value = itemType;
            SQL.CommandText = "Select * from Item Where Item_Code = @itemCode and Item_Type = @itemType";
            return this.ExecuteReader(this.SQL);
        }
        public void insertItemData(string itemCode, string itemName, string itemDescription, string itemType, string itemGroup, string itemStatus, string userCreate)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
            SQL.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = itemName;
            SQL.Parameters.Add("@itemDescription", SqlDbType.NVarChar).Value = itemDescription;
            SQL.Parameters.Add("@itemType", SqlDbType.NVarChar).Value = itemType;
            SQL.Parameters.Add("@itemGroup", SqlDbType.Int).Value = itemGroup;
            SQL.Parameters.Add("@itemStatus", SqlDbType.NVarChar).Value = itemStatus;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Item (Item_Code, Item_Name, Item_Description, Item_Type, Item_Group_Id, Item_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@itemCode, @itemName, @itemDescription, @itemType, @itemGroup, @itemStatus, @userCreate, @userCreate, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateItemData(string itemCode, string itemName, string itemDescription, string itemStatus, string userUpdate)
        {
            SQL.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
            SQL.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = itemName;
            SQL.Parameters.Add("@itemDescription", SqlDbType.NVarChar).Value = itemDescription;
            SQL.Parameters.Add("@itemStatus", SqlDbType.NVarChar).Value = itemStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Item Set Item_Name = @itemName, Item_Description = @itemDescription, Item_Status = @itemStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Item_Code = @itemCode";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}