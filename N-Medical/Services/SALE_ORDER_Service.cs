using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class SALE_ORDER_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData(string status)
        {
            SQL.CommandText = "Select SALE_ORDER_ID, Cus_Group_Code, Cus_Code, Cus_Name, Cost_Center, SALE_ORDER_TYPE, VENDER_CODE, VENDER_NAME, SALE_ORDER_Number, PO_Number,  CONVERT(VARCHAR(10), Create_Date, 105) as Create_Date, SALE_ORDER_Status, case when SALE_ORDER_Status = 'O' then 'On Process' when SALE_ORDER_Status = 'N' then 'Cancel' else 'Complete' end as Sale_Status_Name, case when SALE_ORDER_Status = 'A' then 'Complete' when SALE_ORDER_Status = 'N' then 'Cancel' else 'On Process' end as Warehouse_Status_Name from Sale_Order Where SALE_ORDER_Status in ('" + status + "') order by SALE_ORDER_Number desc;";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataById(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "SELECT  A.SALE_ORDER_ID, A.Cus_Group_Code, A.Cus_Group_Name, A.Cus_Code, A.Cus_Name, A.SALE_ORDER_TYPE, A.VENDER_CODE, A.VENDER_NAME, A.SALE_ORDER_Number, A.PO_Number, CONVERT(VARCHAR(10),A.SALE_ORDER_Shipping_Date,103) as SALE_ORDER_Shipping_Date, A.SALE_ORDER_Amount, A.SALE_ORDER_Balance, A.Sale_Code, A.Sale_Name, A.Cost_Center, B.COST_CENTER_NAME, A.SALE_ORDER_Remark, A.SALE_ORDER_Status, A.User_Create, A.User_Update, A.Create_Date, A.Update_Date FROM Sale_Order A left join CostCenter B on A.Cost_Center = B.COST_CENTER_CODE where A.SALE_ORDER_ID = @id";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataCommentById(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "SELECT A.SALE_ORDER_Comment_Id, A.Comment, B.Name as User_Create, CONVERT(VARCHAR(10), A.Create_Date, 105) as Create_Date FROM SALE_ORDER_Comment A left join Login B on A.User_Create = B.Username where A.SALE_ORDER_ID = @id ORDER BY A.SALE_ORDER_Comment_Id desc";
            return this.ExecuteReader(this.SQL);
        }

        public DataTable getDataDetail(string id)
        {
            SQL.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            SQL.CommandText = "Select SALE_ORDER_DETAIL_ID, SALE_ORDER_ID, case when SALE_ORDER_TYPE = 'S' then 'Sell' else 'Buy' end as SALE_ORDER_TYPE, SALE_ORDER_FILE_NAME, Status from Sale_Order_Detail where SALE_ORDER_ID = @id";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkSaleOrderBalance(string saleOrderNumber)
        {
            SQL.Parameters.Add("@saleOrderNumber", SqlDbType.NVarChar).Value = saleOrderNumber;
            SQL.CommandText = "Select SALE_ORDER_Balance, SALE_ORDER_TYPE from Sale_Order where SALE_ORDER_NUMBER = @saleOrderNumber";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkPODuplicate(string poNumber, string cusCode)
        {
            SQL.Parameters.Add("@poNumber", SqlDbType.NVarChar).Value = poNumber;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.CommandText = "Select * from Sale_Order where PO_Number = @poNumber and Cus_Code = @cusCode";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkPODuplicate(string poNumber, string saleOrderNumber, string cusCode)
        {
            SQL.Parameters.Add("@poNumber", SqlDbType.NVarChar).Value = poNumber;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.CommandText = "Select * from Sale_Order where PO_Number = @poNumber and Cus_Code = @cusCode and SALE_ORDER_NUMBER not in ('" + saleOrderNumber + "');";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable insertSaleOrderData(string cusGroupCode, string cusGroupName, string cusCode, string cusName, string saleOrderType, string venderCode, string venderName, string poNumber, string shippingDate, string saleOrderAmount, string saleCode, string saleName, string costCenter, string saleOrderRemark, string userCreate, string userUpdate)
        {
            SQL.Parameters.Add("@cusGroupCode", SqlDbType.NVarChar).Value = cusGroupCode;
            SQL.Parameters.Add("@cusGroupName", SqlDbType.NVarChar).Value = cusGroupName;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@cusName", SqlDbType.NVarChar).Value = cusName;
            SQL.Parameters.Add("@saleOrderType", SqlDbType.NVarChar).Value = saleOrderType;
            SQL.Parameters.Add("@venderCode", SqlDbType.NVarChar).Value = venderCode;
            SQL.Parameters.Add("@venderName", SqlDbType.NVarChar).Value = venderName;
            SQL.Parameters.Add("@poNumber", SqlDbType.NVarChar).Value = poNumber;
            SQL.Parameters.Add("@shippingDate", SqlDbType.Date).Value = shippingDate;
            SQL.Parameters.Add("@saleOrderAmount", SqlDbType.Decimal).Value = saleOrderAmount;
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@saleName", SqlDbType.NVarChar).Value = saleName;
            SQL.Parameters.Add("@costCenter", SqlDbType.NVarChar).Value = costCenter;
            SQL.Parameters.Add("@saleOrderRemark", SqlDbType.NVarChar).Value = saleOrderRemark;
            SQL.Parameters.Add("@saleOrderStatus", SqlDbType.NVarChar).Value = "O";
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Insert Into Sale_Order (Cus_Group_Code, Cus_Group_Name, Cus_Code, Cus_Name, SALE_ORDER_TYPE, VENDER_CODE, VENDER_NAME, PO_Number, SALE_ORDER_Shipping_Date, SALE_ORDER_Amount, SALE_ORDER_Balance, Sale_Code, Sale_Name, Cost_Center, SALE_ORDER_Remark, SALE_ORDER_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@cusGroupCode, @cusGroupName, @cusCode, @cusName, @saleOrderType, @venderCode, @venderName, @poNumber, @shippingDate, @saleOrderAmount, @saleOrderAmount, @saleCode, @saleName, @costCenter, @saleOrderRemark, @saleOrderStatus, @userCreate, @userUpdate, GETDATE(), GETDATE());SELECT SCOPE_IDENTITY() AS insertId;";
            return this.ExecuteReader(this.SQL);
        }
        public void updateSaleOrderData(string saleOrderId, string cusGroupCode, string cusGroupName, string cusCode, string cusName, string saleOrderType, string venderCode, string venderName, string poNumber, string shippingDate, string saleOrderAmount, string saleCode, string saleName, string costCenter, string saleOrderRemark, string saleOrderStatus, string userUpdate)
        {
            SQL.Parameters.Add("@cusGroupCode", SqlDbType.NVarChar).Value = cusGroupCode;
            SQL.Parameters.Add("@cusGroupName", SqlDbType.NVarChar).Value = cusGroupName;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@cusName", SqlDbType.NVarChar).Value = cusName;
            SQL.Parameters.Add("@saleOrderId", SqlDbType.BigInt).Value = saleOrderId;
            SQL.Parameters.Add("@saleOrderType", SqlDbType.NVarChar).Value = saleOrderType;
            SQL.Parameters.Add("@venderCode", SqlDbType.NVarChar).Value = venderCode;
            SQL.Parameters.Add("@venderName", SqlDbType.NVarChar).Value = venderName;
            SQL.Parameters.Add("@poNumber", SqlDbType.NVarChar).Value = poNumber;
            SQL.Parameters.Add("@shippingDate", SqlDbType.Date).Value = shippingDate;
            SQL.Parameters.Add("@saleOrderAmount", SqlDbType.NVarChar).Value = saleOrderAmount;
            SQL.Parameters.Add("@saleCode", SqlDbType.NVarChar).Value = saleCode;
            SQL.Parameters.Add("@saleName", SqlDbType.NVarChar).Value = saleName;
            SQL.Parameters.Add("@costCenter", SqlDbType.NVarChar).Value = costCenter;
            SQL.Parameters.Add("@saleOrderRemark", SqlDbType.NVarChar).Value = saleOrderRemark;
            SQL.Parameters.Add("@saleOrderStatus", SqlDbType.NVarChar).Value = saleOrderStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Sale_Order Set CUS_GROUP_CODE = @cusGroupCode, CUS_GROUP_NAME = @cusGroupName, CUS_CODE = @cusCode, CUS_NAME = @cusName, SALE_ORDER_TYPE = @saleOrderType, VENDER_CODE = @venderCode, VENDER_NAME = @venderName, PO_Number = @poNumber, SALE_ORDER_Shipping_Date = @shippingDate, SALE_ORDER_Amount = @saleOrderAmount, SALE_ORDER_Balance = @saleOrderAmount, Sale_Code = @saleCode, Sale_Name= @saleName, Cost_Center = @costCenter, SALE_ORDER_Remark = @saleOrderRemark, SALE_ORDER_Status = @saleOrderStatus, User_Update = @userUpdate, Update_Date = GETDATE() where SALE_ORDER_ID = @saleOrderId;";
            this.ExecuteNonQuery(this.SQL);
        }
        public string insertSaleOrderDetailData(string saleOrderId, string type, string filename, string user)
        {
            SQL.Parameters.Add("@saleOrderId", SqlDbType.BigInt).Value = saleOrderId;
            SQL.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
            SQL.Parameters.Add("@filename", SqlDbType.NVarChar).Value = filename;
            SQL.Parameters.Add("@user", SqlDbType.NVarChar).Value = user;
            SQL.CommandText = "insert into Sale_Order_Detail Values (@saleOrderId, @type, @filename, 'W', @user, @user, GETDATE(), GETDATE());SELECT SCOPE_IDENTITY() AS insertId;";
            DataTable dt = this.ExecuteReader(this.SQL);
            SQL.Parameters.Add("@saleOrderDetailId", SqlDbType.BigInt).Value = dt.Rows[0]["insertId"].ToString();
            SQL.Parameters.Add("@updatefilename", SqlDbType.NVarChar).Value = dt.Rows[0]["insertId"].ToString() + "_" + filename;
            SQL.Parameters.Add("@user", SqlDbType.NVarChar).Value = user;
            SQL.CommandText = "Update Sale_Order_Detail Set SALE_ORDER_FILE_NAME = @updatefilename, User_Update = @user, Update_Date = GETDATE() where SALE_ORDER_DETAIL_ID = @saleOrderDetailId;";
            this.ExecuteNonQuery(this.SQL);
            return dt.Rows[0]["insertId"].ToString();
        }
        public void updateSaleOrderDetailData(string saleOrderDetailId, string status, string user)
        {
            SQL.Parameters.Add("@saleOrderDetailId", SqlDbType.BigInt).Value = saleOrderDetailId;
            SQL.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
            SQL.Parameters.Add("@user", SqlDbType.NVarChar).Value = user;
            SQL.CommandText = "Update Sale_Order_Detail Set Status = @status, User_Update = @user, Update_Date = GETDATE() where SALE_ORDER_DETAIL_ID = @saleOrderDetailId;";
            this.ExecuteNonQuery(this.SQL);
        }
        public void insertSaleOrderComment(string saleOrderId, string comment, string user)
        {
            SQL.Parameters.Add("@saleOrderId", SqlDbType.BigInt).Value = saleOrderId;
            SQL.Parameters.Add("@comment", SqlDbType.NVarChar).Value = comment;
            SQL.Parameters.Add("@user", SqlDbType.NVarChar).Value = user;
            SQL.CommandText = "Insert Into Sale_Order_Comment values (@saleOrderId, @comment, @user, @user, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }

        public void deleteSaleOrderDetail(string saleOrderDetailId)
        {
            SQL.Parameters.Add("@saleOrderDetailId", SqlDbType.BigInt).Value = saleOrderDetailId;
            SQL.CommandText = "Delete Sale_Order_Detail where SALE_ORDER_DETAIL_ID = @saleOrderDetailId";
            this.ExecuteNonQuery(this.SQL);
        }
        public void cancelSaleOrder(string saleOrderId, string status, string user)
        {
            SQL.Parameters.Add("@saleOrderId", SqlDbType.BigInt).Value = saleOrderId;
            SQL.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
            SQL.Parameters.Add("@user", SqlDbType.NVarChar).Value = user;
            SQL.CommandText = "Update Sale_Order Set SALE_ORDER_Status = @status, User_Update = @user, Update_Date = GETDATE() where SALE_ORDER_ID = @saleOrderId;";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}