using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class CREDITNOTE_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select A.Id, B.Consumtion_Number, A.Invoice_Number, A.CreditNote_Number, PO_Number, B.Cus_Name, A.CreditNote_Remark, CONVERT(VARCHAR(10), A.CreditNote_Date,103) as CreditNote_Date, A.CreditNote_Status, case when A.CreditNote_Status = 'C' then 'Complete' else 'On Process' end as Status_Name from CreditNote A inner join Invoice B on A.Invoice_Number = B.Invoice_Number Order By A.Id desc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataBySaleOrder(string saleOrderNumber)
        {
            SQL.Parameters.Add("@saleOrderNumber", SqlDbType.NVarChar).Value = saleOrderNumber;
            SQL.CommandText = "Select A.Id, B.Consumtion_Number, A.Invoice_Number, A.CreditNote_Number, PO_Number, B.Cus_Name, A.CreditNote_Remark, CONVERT(VARCHAR(10), A.CreditNote_Date,103) as CreditNote_Date, A.CreditNote_Status, case when A.CreditNote_Status = 'C' then 'Complete' else 'On Process' end as Status_Name from CreditNote A inner join Invoice B on A.Invoice_Number = B.Invoice_Number Where A.SALE_ORDER_Number = @saleOrderNumber Order By A.Id desc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable creditNoteDetail(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "Select * from NH_CREDITNOTE where id = @id";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkInvoiceCreditNote(string invoiceNumber, string status)
        {
            SQL.Parameters.Add("@invoiceNumber", SqlDbType.NVarChar).Value = invoiceNumber;
            SQL.CommandText = "Select * from CreditNote where Invoice_Number = @invoiceNumber and CreditNote_Status in ('" + status + "')";
            return this.ExecuteReader(this.SQL);
        }
        public void creditNoteInvoice(string id, string userUpdate)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "Update Invoice Set Invoice_Status = 'N', User_Update = @userUpdate, Update_Date = GETDATE() where Id = @id";
            this.ExecuteNonQuery(this.SQL);
        }
        public void creditNoteInvoiceDetail(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "Update Invoice_Detail Set Status = 'N' where Invoice_Id = @id";
            this.ExecuteNonQuery(this.SQL);
        }
        public DataTable insertCreditNoteData(string invoiceNumber, string saleOrderNumber, string creditRemark, string creditStatus, string invoiceType, string creditType, string userCreate, string userUpdate)
        {
            SQL.Parameters.Add("@invoiceNumber", SqlDbType.NVarChar).Value = invoiceNumber;
            SQL.Parameters.Add("@saleOrderNumber", SqlDbType.NVarChar).Value = saleOrderNumber;
            SQL.Parameters.Add("@creditRemark", SqlDbType.NVarChar).Value = creditRemark;
            SQL.Parameters.Add("@creditStatus", SqlDbType.NVarChar).Value = creditStatus;
            SQL.Parameters.Add("@invoiceType", SqlDbType.NVarChar).Value = invoiceType;
            SQL.Parameters.Add("@creditType", SqlDbType.NVarChar).Value = creditType;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Insert Into CreditNote (Invoice_Number, SALE_ORDER_Number, CreditNote_Remark, CreditNote_Status, Invoice_Type, CreditNote_Type, User_Create, User_Update, Create_Date, Update_Date) Values (@invoiceNumber, @saleOrderNumber, @creditRemark, @creditStatus, @invoiceType, @creditType, @userCreate, @userUpdate, GETDATE(), GETDATE());SELECT SCOPE_IDENTITY() AS insertId;";
            return this.ExecuteReader(this.SQL);
        }
        public void insertCreditNoteDetailData(string creditNoteId, string itemCode, string itemName, string itemLocation, string batch, string expireDate, string itemCost, string itemPrice, string itemQty, string uom, string costCenter, string status, string userCreate)
        {
            SQL.Parameters.Add("@creditNoteId", SqlDbType.BigInt).Value = creditNoteId;
            SQL.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
            SQL.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = itemName;
            SQL.Parameters.Add("@itemLocation", SqlDbType.NVarChar).Value = itemLocation;
            SQL.Parameters.Add("@batch", SqlDbType.NVarChar).Value = batch;
            SQL.Parameters.Add("@expireDate", SqlDbType.NVarChar).Value = expireDate;
            SQL.Parameters.Add("@itemCost", SqlDbType.Float).Value = itemCost;
            SQL.Parameters.Add("@itemPrice", SqlDbType.Float).Value = itemPrice;
            SQL.Parameters.Add("@itemQty", SqlDbType.Int).Value = itemQty;
            SQL.Parameters.Add("@uom", SqlDbType.NVarChar).Value = uom;
            SQL.Parameters.Add("@costCenter", SqlDbType.NVarChar).Value = costCenter;
            SQL.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into CreditNote_Detail (CreditNote_Id, Item_Code, Item_Name, Item_Location, Batch, Expire_Date, Item_Cost, Item_Price, Item_Qty, UOM, Cost_Center, Status, User_Create, Create_Date) Values (@creditNoteId, @itemCode, @itemName, @itemLocation, @batch, @expireDate, @itemCost, @itemPrice, @itemQty, @uom, @costCenter, @status, @userCreate, GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateCreditNoteData(string id, string creditStatus, string userUpdate)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.Parameters.Add("@creditStatus", SqlDbType.NVarChar).Value = creditStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update CreditNote Set CreditNote_Status = @creditStatus, CreditNote_Date = GETDATE(), User_Update = @userUpdate, Update_Date = GETDATE() where Id = @id";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateCreditNoteDetailData(string id, string creditStatus, string userUpdate)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.Parameters.Add("@creditStatus", SqlDbType.NVarChar).Value = creditStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update CreditNote_Detail Set Status = @creditStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Id = @id";
            this.ExecuteNonQuery(this.SQL);
        }
        public void deleteCreditNoteData(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "Delete CreditNote where Id = @Id";
            this.ExecuteNonQuery(this.SQL);
        }
        public void deleteCreditNoteDetailData(string creditNoteId)
        {
            SQL.Parameters.Add("@creditNoteId", SqlDbType.BigInt).Value = creditNoteId;
            SQL.CommandText = "Delete CreditNote_Detail where CreditNote_Id = @creditNoteId";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}