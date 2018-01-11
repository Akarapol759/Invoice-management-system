using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Equipment
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
        EQUIPMENT_Service oEQUIPMENT_Service = new EQUIPMENT_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();
        PRICE_Service oPRICE_Service = new PRICE_Service();
        RUNNING_Service oRUNNING_Service = new RUNNING_Service();
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
                            BindData(null, ticket.Version);
                        }
                        else
                        {
                            BindData(ticket.UserData.ToString(), ticket.Version);
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
        protected void BindData(string buCode, int role)
        {
            DataTable dtCustomerGroup = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCustomerGroup, dtCustomerGroup, "Cus_Group_Name", "Cus_Group_Code", "Please select Customer Group");

            DataTable dtCustomer = oUtility.getActive(oCUSTOMER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCustomer, dtCustomer, "Cus_Name", "Cus_Code", "Please select Customer");

            //DataTable dtDepartment = oUtility.getActive(oCUSTOMER_DEPARTMENT_Service.getData(), "Status_Name", "Active");
            //oUtility.DDL(DDLAddDepartment, dtDepartment, "Dept_Name", "Dept_Code", "Please select Customer Department");

            //DataTable dtCustomerShipping = oUtility.getActive(oCUSTOMER_SHIPPING_Service.getData(), "Status_Name", "Active");
            //oUtility.DDL(DDLAddShipping, dtCustomerShipping, "Cus_Shipping_Detail", "Cus_Shipping_Code", "Please select Shipping Address");

            DataTable dtSale = oUtility.getActive(oSALE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddSale, dtSale, "Sale_Name", "Sale_Code", "Please select Sale");

            DataTable dtDeliveryRoute = oUtility.getActive(oDELIVERY_ROUTE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddDeliveryRoute, dtDeliveryRoute, "Delivery_Route_Name", "Delivery_Route_Code", "Please select Delivery Route");

            DataTable dtWarehouse = oUtility.getActive(oWAREHOUSE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddWarehouse, dtWarehouse, "Warehouse_Name", "Warehouse_Code", "Please select Warehouse");

            DataTable dtItem = oUtility.getActive(oITEM_Service.getData("E"), "Status_Name", "Active");
            oUtility.DDL(DDLAddItem, dtItem, "Item_Name", "Item_Code", "Please select Item");

            ViewState["dtDetail"] = oEQUIPMENT_Service.invoiceDetailData("0");
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
                && !string.IsNullOrEmpty(txtAddPONumber.Text.Trim())
                && DDLAddShipping.SelectedValue != "-1"
                && DDLAddSale.SelectedValue != "-1"
                && DDLAddDeliveryRoute.SelectedValue != "-1"
                && DDLAddWarehouse.SelectedValue != "-1")
            {
                //Get ViewState Detail
                DataTable dtDetail = (DataTable)ViewState["dtDetail"];
                if (dtDetail.Rows.Count > 0)
                {
                    string equipmentNo = oRUNNING_Service.generateEquipmentNumber();
                    DataTable id = oEQUIPMENT_Service.insertInvoiceData(DDLAddCustomerGroup.SelectedValue, DDLAddCustomerGroup.SelectedItem.Text, DDLAddCustomer.SelectedValue, DDLAddCustomer.SelectedItem.Text, DDLAddDepartment.SelectedValue, DDLAddDepartment.SelectedItem.Text, equipmentNo, txtAddPONumber.Text, DDLAddShipping.SelectedValue, DDLAddShipping.SelectedItem.Text, DDLAddSale.SelectedValue, DDLAddSale.SelectedItem.Text, DDLAddDeliveryRoute.SelectedValue, DDLAddDeliveryRoute.SelectedItem.Text, DDLAddWarehouse.SelectedValue, DDLAddWarehouse.SelectedItem.Text, txtAddRemark.Text, txtAddDiscount.Text, "O", "E", ViewState["user"].ToString(), ViewState["user"].ToString());
                    for (int j = 0; j < dtDetail.Rows.Count; j++)
                    {
                        oEQUIPMENT_Service.insertInvoiceDetailData(id.Rows[0]["insertId"].ToString(), equipmentNo, dtDetail.Rows[j]["Item_Code"].ToString(), dtDetail.Rows[j]["Item_Name"].ToString(), dtDetail.Rows[j]["Item_Description"].ToString(), dtDetail.Rows[j]["Cost_Center"].ToString(), dtDetail.Rows[j]["Item_Qty"].ToString(), dtDetail.Rows[j]["Item_Price"].ToString(), dtDetail.Rows[j]["UOM"].ToString(), dtDetail.Rows[j]["Status"].ToString(), ViewState["user"].ToString());
                    }
                    Response.Redirect("/Views/Equipment/Edit.aspx?id=" + id.Rows[0]["insertId"].ToString());
                }
                else
                {
                    oUtility.MsgAlert(this, "Not data in detail");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please select all fields");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Equipment/Main.aspx");
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if(DDLAddItem.SelectedValue != "-1" && !string.IsNullOrEmpty(txtAddItemCostCenter.Text.Trim())
                && !string.IsNullOrEmpty(txtAddItemPrice.Text.Trim()) && !string.IsNullOrEmpty(txtAddItemQty.Text.Trim())
                && !string.IsNullOrEmpty(txtAddItemUOM.Text.Trim()))
            {
                DataTable dtDetail = (DataTable)ViewState["dtDetail"];
                dtDetail.Rows.Add(dtDetail.NewRow());
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Item_Code"] = DDLAddItem.SelectedValue;
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Item_Name"] = DDLAddItem.SelectedItem.ToString();
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Item_Description"] = txtAddItemDescription.Text;
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Item_Price"] = txtAddItemPrice.Text;
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Item_Qty"] = txtAddItemQty.Text;
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Cost_Center"] = txtAddItemCostCenter.Text;
                dtDetail.Rows[dtDetail.Rows.Count - 1]["UOM"] = txtAddItemUOM.Text;
                dtDetail.Rows[dtDetail.Rows.Count - 1]["Status"] = "O";
                ViewState["dtDetail"] = dtDetail;
                GridView.DataSource = dtDetail;
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Please select all fields");
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Set Item Code
                Label gvlblItemCode = (Label)(e.Row.FindControl("gvlblItemCode"));
                gvlblItemCode.Text = (string)DataBinder.Eval(e.Row.DataItem, "Item_Code");

                //Set Item Name
                Label gvlblItemName = (Label)(e.Row.FindControl("gvlblItemName"));
                gvlblItemName.Text = (string)DataBinder.Eval(e.Row.DataItem, "Item_Name");

                //Set Item Description
                Label gvlblItemDescription = (Label)(e.Row.FindControl("gvlblItemDescription"));
                gvlblItemDescription.Text = (string)DataBinder.Eval(e.Row.DataItem, "Item_Description");

                //Set Item Cost Center
                Label gvlblCostCenter = (Label)(e.Row.FindControl("gvlblCostCenter"));
                gvlblCostCenter.Text = DataBinder.Eval(e.Row.DataItem, "Cost_Center").ToString();

                //Set Item Qty
                Label gvlblItemQty = (Label)(e.Row.FindControl("gvlblItemQty"));
                gvlblItemQty.Text = DataBinder.Eval(e.Row.DataItem, "Item_Qty").ToString();

                //Set Item Unit Price
                Label gvlblUnitPrice = (Label)(e.Row.FindControl("gvlblUnitPrice"));
                gvlblUnitPrice.Text = DataBinder.Eval(e.Row.DataItem, "Item_Price").ToString();

                //Set Item UOM
                Label gvlblUOM = (Label)(e.Row.FindControl("gvlblUOM"));
                gvlblUOM.Text = DataBinder.Eval(e.Row.DataItem, "uom").ToString();
            }
        }

        protected void DDLAddItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDLAddItem.SelectedValue != "-1")
            {
                DataTable dt = oITEM_Service.getDataByItemCode(DDLAddItem.SelectedValue);
                txtAddItemDescription.Text = dt.Rows[0]["Item_Description"].ToString();
            }
            else
            {
                oUtility.MsgAlert(this, "Please select item");
            }
        }

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //public static List<string> GetListofItem(string prefixText)
        //{
        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT Item_Name FROM Item WHERE Item_Type = 'E'and Item_Name like @SearchText + '%' Order by Item_Name ASC", conn);
        //        cmd.Parameters.AddWithValue("@SearchText", prefixText);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        List<string> ItemName = new List<string>();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            ItemName.Add(dt.Rows[i]["Item_Name"].ToString());
        //        }
        //        return ItemName;
        //    }
        //}
    }
}