using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Inventory
{
    public partial class Add : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_DEPARTMENT_Service oCUSTOMER_DEPARTMENT_Service = new CUSTOMER_DEPARTMENT_Service();
        CUSTOMER_SHIPPING_Service oCUSTOMER_SHIPPING_Service = new CUSTOMER_SHIPPING_Service();
        SALE_Service oSALE_Service = new SALE_Service();
        DELIVERY_ROUTE_Service oDELIVERY_ROUTE_Service = new DELIVERY_ROUTE_Service();
        WAREHOUSE_Service oWAREHOUSE_Service = new WAREHOUSE_Service();
        Service_ODBC oService_ODBC = new Service_ODBC();
        INVENTORY_Service oINVENTORY_Service = new INVENTORY_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();
        PRICE_Service oPRICE_Service = new PRICE_Service();
        SALE_ORDER_Service oSALE_ORDER_Service = new SALE_ORDER_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnSave.UniqueID;
            if (!Page.IsPostBack)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    if (!string.IsNullOrEmpty(ticket.Name.ToString()))
                    {
                        ViewState["user"] = ticket.Name.ToString();
                        if (ticket.Version == 1)
                        {
                            BindData(null, Request.QueryString["id"]);
                        }
                        else
                        {
                            BindData(ticket.UserData.ToString(), Request.QueryString["id"]);
                        }
                    }
                    else
                    {
                        Response.Redirect("/Account/Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx");
                }
            }
        }
        protected void BindData(string buCode, string id)
        {
            DataTable dtCustomerGroup = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCustomerGroup, dtCustomerGroup, "Cus_Group_Name", "Cus_Group_Code", "Please select Customer Group");

            DataTable dtCustomer = oUtility.getActive(oCUSTOMER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCustomer, dtCustomer, "Cus_Name", "Cus_Code", "Please select Customer");

            DataTable dtSale = oUtility.getActive(oSALE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddSale, dtSale, "Sale_Name", "Sale_Code", "Please select Sale");

            DataTable dtDeliveryRoute = oUtility.getActive(oDELIVERY_ROUTE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddDeliveryRoute, dtDeliveryRoute, "Delivery_Route_Name", "Delivery_Route_Code", "Please select Delivery Route");

            DataTable dtWarehouse = oUtility.getActive(oWAREHOUSE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddWarehouse, dtWarehouse, "Warehouse_Name", "Warehouse_Code", "Please select Warehouse");

            if (!string.IsNullOrEmpty(id))
            {
                DataTable dt = oSALE_ORDER_Service.getDataById(id);
                DDLAddCustomerGroup.SelectedValue = dt.Rows[0]["Cus_Group_Code"].ToString();
                DDLAddCustomer.SelectedValue = dt.Rows[0]["Cus_Code"].ToString();
                txtAddPONumber.Text = dt.Rows[0]["PO_Number"].ToString();
                DDLAddSale.SelectedValue = dt.Rows[0]["Sale_Code"].ToString();
                lblAddSaleOrderNumber.Text = dt.Rows[0]["SALE_ORDER_Number"].ToString();

                //select Department & Shipping address
                IEnumerable<DataRow> queryDep = from i in oCUSTOMER_DEPARTMENT_Service.getData().AsEnumerable()
                                                where i.Field<string>("Cus_Code").Equals(DDLAddCustomer.SelectedValue)
                                                && i.Field<string>("Status_Name").Equals("Active")
                                                select i;
                IEnumerable<DataRow> queryShip = from i in oCUSTOMER_SHIPPING_Service.getData().AsEnumerable()
                                                 where i.Field<string>("Cus_Code").Equals(DDLAddCustomer.SelectedValue)
                                                 && i.Field<string>("Status_Name").Equals("Active")
                                                 select i;
                if (queryDep.Any())
                {
                    DataTable result = queryDep.CopyToDataTable<DataRow>();
                    oUtility.DDL(DDLAddDepartment, result, "Dept_Name", "Dept_Code", "Please select Customer Department");
                    if (queryShip.Any())
                    {
                        DataTable result2 = queryShip.CopyToDataTable<DataRow>();
                        oUtility.DDL(DDLAddShipping, result2, "Cus_Shipping_Detail", "Cus_Shipping_Code", "Please select Shipping Address");
                    }
                    else
                    {
                        oUtility.MsgAlert(this, "Not find shipping address");
                    }
                }
                else
                {
                    if (queryShip.Any())
                    {
                        DataTable result2 = queryShip.CopyToDataTable<DataRow>();
                        oUtility.DDL(DDLAddShipping, result2, "Cus_Shipping_Detail", "Cus_Shipping_Code", "Please select Shipping Address");
                        oUtility.MsgAlert(this, "Not find department in this customer. Please add customer department");
                    }
                    else
                    {
                        oUtility.MsgAlert(this, "Not find department and shipping address in this customer. Please add customer department and shipping address");
                    }
                }
            }
            else
            {

            }
        }
        protected void DDLAddCustomerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Group_Code").Equals(DDLAddCustomerGroup.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLAddCustomer, result, "Cus_Name", "Cus_Code", "Please select Customer");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }

        protected void DDLAddCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> queryDep = from i in oCUSTOMER_DEPARTMENT_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Code").Equals(DDLAddCustomer.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            IEnumerable<DataRow> queryShip = from i in oCUSTOMER_SHIPPING_Service.getData().AsEnumerable()
                                          where i.Field<string>("Cus_Code").Equals(DDLAddCustomer.SelectedValue)
                                          && i.Field<string>("Status_Name").Equals("Active")
                                          select i;
            if (queryDep.Any())
            {
                DataTable result = queryDep.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLAddDepartment, result, "Dept_Name", "Dept_Code", "Please select Customer Department");
                if (queryShip.Any())
                {
                    DataTable result2 = queryShip.CopyToDataTable<DataRow>();
                    oUtility.DDL(DDLAddShipping, result2, "Cus_Shipping_Detail", "Cus_Shipping_Code", "Please select Shipping Address");
                }
                else
                {
                    oUtility.MsgAlert(this, "Not find shipping address");
                }
            }
            else
            {
                if (queryShip.Any())
                {
                    DataTable result2 = queryShip.CopyToDataTable<DataRow>();
                    oUtility.DDL(DDLAddShipping, result2, "Cus_Shipping_Detail", "Cus_Shipping_Code", "Please select Shipping Address");
                    oUtility.MsgAlert(this, "Not find department in this customer. Please add customer department");
                }
                else
                {
                    oUtility.MsgAlert(this, "Not find department and shipping address in this customer. Please add customer department and shipping address");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLAddCustomerGroup.SelectedValue != "-1" && DDLAddCustomer.SelectedValue != "-1"
                && DDLAddDepartment.SelectedValue != "-1"
                && !string.IsNullOrEmpty(txtAddConsumtionNumber.Text.Trim())
                && !string.IsNullOrEmpty(txtAddPONumber.Text.Trim())
                && DDLAddShipping.SelectedValue != "-1"
                && DDLAddSale.SelectedValue != "-1"
                && DDLAddDeliveryRoute.SelectedValue != "-1"
                && DDLAddWarehouse.SelectedValue != "-1"
                && !string.IsNullOrEmpty(txtAddDiscount.Text.Trim()))
            {
                //Get Consumion Data From Medtrak
                DataTable dtConsum = oService_ODBC.consumtionData(txtAddConsumtionNumber.Text.Trim());
                if (dtConsum.Rows.Count > 0)
                {
                    //check duplicate consumtion in nmed db
                    DataTable check = oINVENTORY_Service.checkAddConsumtion(txtAddConsumtionNumber.Text.Trim(), "C','O");
                    DataTable checkDetail = oINVENTORY_Service.checkAddConsumtionDetail(txtAddConsumtionNumber.Text.Trim(), "C','O");
                    if(check.Rows.Count == 0 && checkDetail.Rows.Count == 0)
                    {
                        DataTable dtDetail = oINVENTORY_Service.invoiceDetailData("0");
                        DataTable dtItem = oITEM_Service.getData("I");
                        DataTable dtPrice = oPRICE_Service.getData();
                        DataTable id = oINVENTORY_Service.insertInvoiceData(DDLAddCustomerGroup.SelectedValue, DDLAddCustomerGroup.SelectedItem.Text, DDLAddCustomer.SelectedValue, DDLAddCustomer.SelectedItem.Text, DDLAddDepartment.SelectedValue, DDLAddDepartment.SelectedItem.Text, txtAddConsumtionNumber.Text.Trim(), txtAddPONumber.Text, DDLAddShipping.SelectedValue, DDLAddShipping.SelectedItem.Text, DDLAddSale.SelectedValue, DDLAddSale.SelectedItem.Text, DDLAddDeliveryRoute.SelectedValue, DDLAddDeliveryRoute.SelectedItem.Text, DDLAddWarehouse.SelectedValue, DDLAddWarehouse.SelectedItem.Text, txtAddRemark.Text, txtAddDiscount.Text, "O", "I", lblAddSaleOrderNumber.Text, ViewState["user"].ToString(), ViewState["user"].ToString());
                        //Match name and price invoice detail
                        for (int j = 0; j < dtConsum.Rows.Count; j++)
                        {
                            dtDetail.Rows.Add(dtDetail.NewRow());
                            dtDetail.Rows[j]["Invoice_Id"] = id.Rows[0]["insertId"].ToString();
                            dtDetail.Rows[j]["Consumtion_Number"] = txtAddConsumtionNumber.Text.Trim();
                            dtDetail.Rows[j]["Item_Code"] = dtConsum.Rows[j]["Item_Code"].ToString();
                            //Macth Item Name
                            IEnumerable<DataRow> item = from i in dtItem.AsEnumerable()
                                                        where i.Field<string>("Item_Code").Contains(dtConsum.Rows[j]["Item_Code"].ToString())
                                                        select i;
                            if (item.Any())
                            {
                                DataTable dt = item.CopyToDataTable<DataRow>();
                                dtDetail.Rows[j]["Item_Name"] = dt.Rows[0]["Item_Name"].ToString();
                            }
                            else
                            {
                                dtDetail.Rows[j]["Item_Name"] = "Item Not Match";
                            }

                            dtDetail.Rows[j]["Item_Location"] = dtConsum.Rows[j]["CTLOC_Code"].ToString();
                            dtDetail.Rows[j]["Batch"] = dtConsum.Rows[j]["Batch"].ToString();
                            if(!string.IsNullOrEmpty(dtConsum.Rows[j]["Expire_Date"].ToString()))
                            {
                                dtDetail.Rows[j]["Expire_Date"] = Convert.ToDateTime(dtConsum.Rows[j]["Expire_Date"]).ToString("dd/MM/yyyy");
                            }
                            dtDetail.Rows[j]["Item_Cost"] = dtConsum.Rows[j]["Item_Cost"].ToString();
                            //Macth Item Price
                            IEnumerable<DataRow> price = from i in dtPrice.AsEnumerable()
                                                         where i.Field<string>("Item_Code").Equals(dtConsum.Rows[j]["Item_Code"].ToString())
                                                         && i.Field<string>("Cus_Code").Equals(DDLAddCustomer.SelectedValue)
                                                         select i;
                            if (price.Any())
                            {
                                DataTable dt = price.CopyToDataTable<DataRow>();
                                dtDetail.Rows[j]["Item_Price"] = dt.Rows[0]["Unit_Price"].ToString();
                            }
                            else
                            {
                                dtDetail.Rows[j]["Item_Price"] = "0";
                            }
                            dtDetail.Rows[j]["Item_Qty"] = dtConsum.Rows[j]["Item_Qty"].ToString();
                            dtDetail.Rows[j]["UOM"] = dtConsum.Rows[j]["UOM"].ToString();
                            dtDetail.Rows[j]["Cost_Center"] = dtConsum.Rows[j]["Cost_Center"].ToString();
                            dtDetail.Rows[j]["Status"] = "O";
                        }
                        //Insert Invoice Detail
                        for (int j = 0; j < dtDetail.Rows.Count; j++)
                        {
                            oINVENTORY_Service.insertInvoiceDetailData(dtDetail.Rows[j]["Invoice_Id"].ToString(), dtDetail.Rows[j]["Consumtion_Number"].ToString(), dtDetail.Rows[j]["Item_Code"].ToString(), dtDetail.Rows[j]["Item_Name"].ToString(), dtDetail.Rows[j]["Item_Location"].ToString(), dtDetail.Rows[j]["Batch"].ToString(), dtDetail.Rows[j]["Expire_Date"].ToString(), dtDetail.Rows[j]["Item_Cost"].ToString(), dtDetail.Rows[j]["Item_Price"].ToString(), dtDetail.Rows[j]["Item_Qty"].ToString(), dtDetail.Rows[j]["UOM"].ToString(), dtDetail.Rows[j]["Cost_Center"].ToString(), dtDetail.Rows[j]["Status"].ToString(), dtDetail.Rows[j]["User_Create"].ToString());
                        }
                        Response.Redirect("/Views/Inventory/Edit.aspx?id=" + id.Rows[0]["insertId"].ToString());
                    }
                    else
                    {
                        oUtility.MsgAlert(this, "This consumtion number had invoice already");
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Not consumtion data");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please select all fields");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Inventory/Main.aspx");
        }
        //public static List<string> GetCompletionList(string prefixText, int count)
        //{
        //    return AutoFillProducts(prefixText);

        //}
        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //private static List<string> AutoFillProducts(string prefixText)
        //{
        //    using (SqlConnection con = new SqlConnection())
        //    {
        //        con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //        using (SqlCommand com = new SqlCommand())
        //        {
        //            com.CommandText = "select Cus_Name from Customer where Cus_Name like @Search + '%'";
        //            com.Parameters.AddWithValue("@Search", prefixText);
        //            SqlDataAdapter da = new SqlDataAdapter(com);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            List<string> CustomerNames = new List<string>();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                CustomerNames.Add(dt.Rows[i]["Cus_Name"].ToString());
        //            }
        //            con.Close();
        //            return CustomerNames;
        //        }
        //    }
        //}  
    }
}