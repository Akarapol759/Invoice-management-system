using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class CUSTOMER_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "select CUSTOMER.Cus_Group_Code, CUSTOMER_GROUP.Cus_Group_Name, CUSTOMER.Cus_Code, CUSTOMER.Cus_Name, CUSTOMER.Cus_Billing, CUSTOMER.Cus_Contact, CUSTOMER.Cus_Tel, CUSTOMER.Cus_Term, CUSTOMER.Cus_Vat, CUSTOMER.Cus_Status, CUSTOMER.Cus_Type, case when CUSTOMER.Cus_Term = 'CR' then 'Credit' else 'Cash' end as Cus_Term_Name, case when CUSTOMER.Cus_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from CUSTOMER inner join CUSTOMER_GROUP on CUSTOMER.Cus_Group_Code = CUSTOMER_GROUP.Cus_Group_Code Order By Cus_Name asc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkCustomerDup(string cusCode)
        {
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.CommandText = "select CUSTOMER.Cus_Group_Code, CUSTOMER_GROUP.Cus_Group_Name, CUSTOMER.Cus_Code, CUSTOMER.Cus_Name, CUSTOMER.Cus_Billing, CUSTOMER.Cus_Contact, CUSTOMER.Cus_Tel, CUSTOMER.Cus_Term, CUSTOMER.Cus_Vat, CUSTOMER.Cus_Status, CUSTOMER.Cus_Type, case when CUSTOMER.Cus_Term = 'CR' then 'Credit' else 'Cash' end as Cus_Term_Name, case when CUSTOMER.Cus_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from CUSTOMER inner join CUSTOMER_GROUP on CUSTOMER.Cus_Group_Code = CUSTOMER_GROUP.Cus_Group_Code Order where Cus_Code = @cusCode By Cus_Name asc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataByCusCode(string cusCode)
        {
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.CommandText = "select CUSTOMER.Cus_Group_Code, CUSTOMER_GROUP.Cus_Group_Name, CUSTOMER.Cus_Code, CUSTOMER.Cus_Name, CUSTOMER.Cus_Billing, CUSTOMER.Cus_Contact, CUSTOMER.Cus_Tel, CUSTOMER.Cus_Term, CUSTOMER.Cus_Vat, CUSTOMER.Cus_Status, CUSTOMER.Cus_Type, case when CUSTOMER.Cus_Term = 'CR' then 'Credit' else 'Cash' end as Cus_Term_Name, case when CUSTOMER.Cus_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from CUSTOMER inner join CUSTOMER_GROUP on CUSTOMER.Cus_Group_Code = CUSTOMER_GROUP.Cus_Group_Code where CUSTOMER.Cus_Code = @cusCode Order By CUSTOMER.Cus_Name asc";
            return this.ExecuteReader(this.SQL);
        }
        public void insertCusData(string cusGroup, string cusCode, string cusName, string cusBilling, string cusContact, string cusTel, string cusVat, string cusTerm, string cusType, string cusStatus, string userCreate)
        {
            SQL.Parameters.Add("@cusGroup", SqlDbType.NVarChar).Value = cusGroup;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@cusName", SqlDbType.NVarChar).Value = cusName;
            SQL.Parameters.Add("@cusBilling", SqlDbType.NVarChar).Value = cusBilling;
            SQL.Parameters.Add("@cusContact", SqlDbType.NVarChar).Value = cusContact;
            SQL.Parameters.Add("@cusTel", SqlDbType.NVarChar).Value = cusTel;
            SQL.Parameters.Add("@cusVat", SqlDbType.Decimal).Value = cusVat;
            SQL.Parameters.Add("@cusTerm", SqlDbType.NVarChar).Value = cusTerm;
            SQL.Parameters.Add("@cusType", SqlDbType.NVarChar).Value = cusType;
            SQL.Parameters.Add("@cusStatus", SqlDbType.NVarChar).Value = cusStatus;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Customer (Cus_Group_Code, Cus_Code, Cus_Name, Cus_Billing, Cus_Contact, Cus_Tel, Cus_Vat, Cus_Term, Cus_Type, Cus_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@cusGroup, @cusCode, @cusName, @cusBilling, @cusContact, @cusTel, @cusVat, @cusTerm, @cusType, @cusStatus, @userCreate, @userCreate, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateCusData(string cusCode, string cusName, string cusBilling, string cusContact, string cusTel, string cusTerm, string cusVat, string cusType, string cusStatus, string userUpdate)
        {
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@cusName", SqlDbType.NVarChar).Value = cusName;
            SQL.Parameters.Add("@cusBilling", SqlDbType.NVarChar).Value = cusBilling;
            SQL.Parameters.Add("@cusContact", SqlDbType.NVarChar).Value = cusContact;
            SQL.Parameters.Add("@cusTel", SqlDbType.NVarChar).Value = cusTel;
            SQL.Parameters.Add("@cusTerm", SqlDbType.NVarChar).Value = cusTerm;
            SQL.Parameters.Add("@cusType", SqlDbType.NVarChar).Value = cusType;
            SQL.Parameters.Add("@cusVat", SqlDbType.Decimal).Value = cusVat;
            SQL.Parameters.Add("@cusStatus", SqlDbType.NVarChar).Value = cusStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Customer Set Cus_Name = @cusName, Cus_Billing = @cusBilling, Cus_Contact = @cusContact, Cus_Tel = @cusTel, Cus_Term = @cusTerm, Cus_Vat = @cusVat, Cus_Type = @cusType, Cus_Status = @cusStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Cus_Code = @cusCode";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}