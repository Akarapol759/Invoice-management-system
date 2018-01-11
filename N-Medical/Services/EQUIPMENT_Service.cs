using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class EQUIPMENT_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData(string invoiceType)
        {
            SQL.Parameters.Add("@invoiceType", SqlDbType.NVarChar).Value = invoiceType;
            SQL.CommandText = "Select Id, Consumtion_Number, Invoice_Number, Credit_Note, PO_Number, Cus_Group_Code, Cus_Group_Name, Cus_Code, Cus_Name, Dept_Code, Dept_Name, Sale_Code, Sale_Name, Cus_Shipping_Code, Cus_Shipping_Detail, Collector, Delivery_Route_Code, Delivery_Route_Name, Warehouse_Code, Warehouse_Name, Invoice_Remark, Invoice_Discount, Invoice_Date, CONVERT(VARCHAR(10),Invoice_Date,103) as INV_Date, Invoice_Status, case when Invoice_Status = 'C' then 'Complete' when Invoice_Status = 'N' then 'Credit Note' else 'On Process' end as Status_Name, INVOICE.User_Create, INVOICE.User_Update, INVOICE.Create_Date, INVOICE.Update_Date from INVOICE where Invoice_Type = @invoiceType Order By Id desc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataSearch(string invoiceType)
        {
            SQL.Parameters.Add("@invoiceType", SqlDbType.NVarChar).Value = invoiceType;
            SQL.CommandText = "Select Id, case when Consumtion_Number = '' then '0' else Consumtion_Number end as Consumtion_Number, case when Invoice_Number = '' then '0' when Invoice_Number is null then '0' else Invoice_Number end as Invoice_Number, case when Credit_Note = '' then '0' when Credit_Note is null then '0' else Credit_Note end as Credit_Note, PO_Number, Cus_Group_Code, Cus_Group_Name, Cus_Code, Cus_Name, Dept_Code, Dept_Name, Sale_Code, Sale_Name, Cus_Shipping_Code, Cus_Shipping_Detail, Collector, Delivery_Route_Code, Delivery_Route_Name, Warehouse_Code, Warehouse_Name, Invoice_Remark, Invoice_Discount, Invoice_Date, CONVERT(VARCHAR(10),Invoice_Date,103) as INV_Date, Invoice_Status, case when Invoice_Status = 'C' then 'Complete' when Invoice_Status = 'N' then 'Credit Note' else 'On Process' end as Status_Name, INVOICE.User_Create, INVOICE.User_Update, INVOICE.Create_Date, INVOICE.Update_Date from INVOICE where Invoice_Type = @invoiceType Order By Id desc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkConsumtion(string consumtion, string status)
        {
            SQL.Parameters.Add("@consumtion", SqlDbType.NVarChar).Value = consumtion;
            SQL.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
            SQL.CommandText = "Select * from INVOICE Where Consumtion_Number = @consumtion and Invoice_Status = @status";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable invoiceDetailData(string invoiceId)
        {
            SQL.Parameters.Add("@invoiceId", SqlDbType.BigInt).Value = invoiceId;
            SQL.CommandText = "Select * from INVOICE_DETAIL Where Invoice_Id = @invoiceId";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable invoiceEditData(string invoiceId)
        {
            SQL.Parameters.Add("@invoiceId", SqlDbType.BigInt).Value = invoiceId;
            SQL.CommandText = "Select * from NH_INVOICE Where Id = @invoiceId";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable insertInvoiceData(string cusGroupCode, string cusGroupName, string cusCode, string cusName, string deptCode, string deptName, string consumtionNo, string poNo, string cusShippingCode, string cusShippingDetail, string saleCode, string saleName, string deliveryRouteCode, string deliveryRouteName, string warehouseCode, string warehouseName, string invoiceRemark, string invoiceDiscount, string invoiceStatus, string invoiceType, string userCreate, string userUpdate)
        {
            SQL.Parameters.Add("@cusGroupCode", SqlDbType.NVarChar).Value = cusGroupCode;
            SQL.Parameters.Add("@cusGroupName", SqlDbType.NVarChar).Value = cusGroupName;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@cusName", SqlDbType.NVarChar).Value = cusName;
            SQL.Parameters.Add("@deptCode", SqlDbType.NVarChar).Value = deptCode;
            SQL.Parameters.Add("@deptName", SqlDbType.NVarChar).Value = deptName;
            SQL.Parameters.Add("@consumtionNo", SqlDbType.NVarChar).Value = consumtionNo;
            SQL.Parameters.Add("@poNo", SqlDbType.NVarChar).Value = poNo;
            SQL.Parameters.Add("@cusShippingCode", SqlDbType.NVarChar).Value = cusShippingCode;
            SQL.Parameters.Add("@cusShippingDetail", SqlDbType.NVarChar).Value = cusShippingDetail;
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@saleName", SqlDbType.NVarChar).Value = saleName;
            SQL.Parameters.Add("@deliveryRouteCode", SqlDbType.NVarChar).Value = deliveryRouteCode;
            SQL.Parameters.Add("@deliveryRouteName", SqlDbType.NVarChar).Value = deliveryRouteName;
            SQL.Parameters.Add("@warehouseCode", SqlDbType.NVarChar).Value = warehouseCode;
            SQL.Parameters.Add("@warehouseName", SqlDbType.NVarChar).Value = warehouseName;
            SQL.Parameters.Add("@invoiceRemark", SqlDbType.NVarChar).Value = invoiceRemark;
            SQL.Parameters.Add("@invoiceDiscount", SqlDbType.Decimal).Value = invoiceDiscount;
            SQL.Parameters.Add("@invoiceStatus", SqlDbType.NVarChar).Value = invoiceStatus;
            SQL.Parameters.Add("@invoiceType", SqlDbType.NVarChar).Value = invoiceType;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Insert Into Invoice (Cus_Group_Code, Cus_Group_Name, Cus_Code, Cus_Name, Dept_Code, Dept_Name, Consumtion_Number, PO_Number, Cus_Shipping_Code, Cus_Shipping_Detail, Sale_Code, Sale_Name, Delivery_Route_Code, Delivery_Route_Name, Warehouse_Code, Warehouse_Name, Invoice_Remark, Invoice_Discount, Invoice_Status, Invoice_Type, User_Create, User_Update, Create_Date, Update_Date) Values (@cusGroupCode, @cusGroupName, @cusCode, @cusName, @deptCode, @deptName, @consumtionNo, @poNo, @cusShippingCode, @cusShippingDetail, @saleCode, @saleName, @deliveryRouteCode, @deliveryRouteName, @warehouseCode, @warehouseName, @invoiceRemark, @invoiceDiscount, @invoiceStatus, @invoiceType, @userCreate, @userUpdate, GETDATE(), GETDATE());SELECT SCOPE_IDENTITY() AS insertId;";
            return this.ExecuteReader(this.SQL);
        }
        public void insertInvoiceDetailData(string invoiceId, string equipmentNo, string itemCode, string itemName, string itemDescription, string costCenter, string itemQty, string itemPrice, string uom, string status, string userCreate)
        {
            SQL.Parameters.Add("@invoiceId", SqlDbType.BigInt).Value = invoiceId;
            SQL.Parameters.Add("@equipmentNo", SqlDbType.NVarChar).Value = equipmentNo;
            SQL.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
            SQL.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = itemName;
            SQL.Parameters.Add("@itemDescription", SqlDbType.NVarChar).Value = itemDescription;
            SQL.Parameters.Add("@costCenter", SqlDbType.NVarChar).Value = costCenter;
            SQL.Parameters.Add("@itemQty", SqlDbType.Int).Value = itemQty;
            SQL.Parameters.Add("@itemPrice", SqlDbType.Float).Value = itemPrice;
            SQL.Parameters.Add("@uom", SqlDbType.NVarChar).Value = uom;
            SQL.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Invoice_Detail (Invoice_Id, Consumtion_Number, Item_Code, Item_Name, Item_Description, Cost_Center, Item_Qty, Item_Price, UOM, Status, User_Create, Create_Date, User_Update, Update_Date) Values (@invoiceId, @equipmentNo, @itemCode, @itemName, @itemDescription, @costCenter, @itemQty, @itemPrice, @uom, @status, @userCreate, GETDATE(), @userCreate, GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateInvoiceData(string id, string poNo, string saleCode, string saleName, string deliveryRouteCode, string deliveryRouteName, string warehouseCode, string warehouseName, string invoiceRemark, string invoiceDiscount, string invoiceStatus, string userUpdate)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.Parameters.Add("@poNo", SqlDbType.NVarChar).Value = poNo;
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@saleName", SqlDbType.NVarChar).Value = saleName;
            SQL.Parameters.Add("@deliveryRouteCode", SqlDbType.NVarChar).Value = deliveryRouteCode;
            SQL.Parameters.Add("@deliveryRouteName", SqlDbType.NVarChar).Value = deliveryRouteName;
            SQL.Parameters.Add("@warehouseCode", SqlDbType.NVarChar).Value = warehouseCode;
            SQL.Parameters.Add("@warehouseName", SqlDbType.NVarChar).Value = warehouseName;
            SQL.Parameters.Add("@invoiceRemark", SqlDbType.NVarChar).Value = invoiceRemark;
            SQL.Parameters.Add("@invoiceDiscount", SqlDbType.Decimal).Value = invoiceDiscount;
            SQL.Parameters.Add("@invoiceStatus", SqlDbType.NVarChar).Value = invoiceStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            if (invoiceStatus == "C")
            {
                SQL.CommandText = "Update Invoice Set PO_Number = @poNo, Sale_Code = @saleCode, Sale_Name = @saleName, Delivery_Route_Code = @deliveryRouteCode, Delivery_Route_Name = @deliveryRouteName, Warehouse_Code = @warehouseCode, Warehouse_Name = @warehouseName, Invoice_Remark = @invoiceRemark, Invoice_Discount = @invoiceDiscount, Invoice_Status = @invoiceStatus, Invoice_Date = GETDATE(), User_Update = @userUpdate, Update_Date = GETDATE() where Id = @id";
            }
            else
            {
                SQL.CommandText = "Update Invoice Set PO_Number = @poNo, Sale_Code = @saleCode, Sale_Name = @saleName, Delivery_Route_Code = @deliveryRouteCode, Delivery_Route_Name = @deliveryRouteName, Warehouse_Code = @warehouseCode, Warehouse_Name = @warehouseName, Invoice_Remark = @invoiceRemark, Invoice_Discount = @invoiceDiscount, Invoice_Status = @invoiceStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Id = @id";
            }
            this.ExecuteNonQuery(this.SQL);
        }
        public void deleteInvoiceData(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "Delete Invoice where Id = @Id";
            this.ExecuteNonQuery(this.SQL);
        }
        public void deleteInvoiceDetailData(string invoiceId)
        {
            SQL.Parameters.Add("@invoiceId", SqlDbType.BigInt).Value = invoiceId;
            SQL.CommandText = "Delete Invoice_Detail where Invoice_Id = @invoiceId";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateInvoiceDetailData(string id, string itemName, string unitPrice, string status, string userUpdate)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = itemName;
            SQL.Parameters.Add("@unitPrice", SqlDbType.Float).Value = unitPrice;
            SQL.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Invoice_Detail Set Item_Name = @itemName, Item_Price = @unitPrice,  Status = @status, User_Update = @userUpdate, Update_Date = GETDATE() where Id = @id";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}