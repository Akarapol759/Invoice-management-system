using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.SaleOrder
{
    public partial class Main : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        SALE_ORDER_Service oSALE_ORDER_Service = new SALE_ORDER_Service();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        COSTCENTER_Service oCOSTCENTER_Service = new COSTCENTER_Service();
        VENDER_Service oVENDER_Service = new VENDER_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Form.DefaultButton = btnSearch.UniqueID;
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
                            ViewState["bu"] = "0";
                            BindData(null, ticket.Version);
                        }
                        else
                        {
                            ViewState["bu"] = ticket.UserData.ToString();
                            BindData(ViewState["bu"].ToString(), ticket.Version);
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
            oUtility.DDL(DDLCustomerGroup, dtCustomerGroup, "Cus_Group_Name", "Cus_Group_Code", "Please select Customer Group");

            DataTable dtCustomer = oUtility.getActive(oCUSTOMER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLCustomer, dtCustomer, "Cus_Name", "Cus_Code", "Please select Customer");

            DataTable dtCostCenter = oUtility.getActive(oCOSTCENTER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLCostCenter, dtCostCenter, "COST_CENTER_NAME", "COST_CENTER_CODE", "Please select Sale");

            DataTable dtVender = oUtility.getActive(oVENDER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLVender, dtVender, "VENDER_FULL_NAME", "VENDER_CODE", "Please select Customer");

            ViewState["ReqData"] = oSALE_ORDER_Service.getData("N','O','C','A");
            GridView.DataSource = (DataTable)ViewState["ReqData"];
            GridView.DataBind();
        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SaleOrder/Add.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oSALE_ORDER_Service.getData("N','O','C','A").AsEnumerable()
                                         select i;
            if (query.Any())
            {
                if (DDLSaleOrderType.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("SALE_ORDER_TYPE").Equals(DDLSaleOrderType.SelectedValue)
                            select i;
                }
                if (!string.IsNullOrEmpty(txtSaleOrderNumber.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("SALE_ORDER_Number").Contains(txtSaleOrderNumber.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtPONumber.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("PO_Number").Contains(txtPONumber.Text.Trim())
                            select i;
                }
                if (DDLCustomerGroup.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cus_Group_Code").Equals(DDLCustomerGroup.SelectedValue)
                            select i;
                }
                if (DDLCustomer.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cus_Code").Equals(DDLCustomer.SelectedValue)
                            select i;
                }
                if (DDLVender.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Vender_Code").Equals(DDLVender.SelectedValue)
                            select i;
                }
                if (DDLCostCenter.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cost_Center").Equals(DDLCostCenter.SelectedValue)
                            select i;
                }
                if (DDLSaleStatus.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("SALE_ORDER_Status").Equals(DDLSaleStatus.SelectedValue)
                            select i;
                }
                if (DDLWarehouseStatus.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("SALE_ORDER_Status").Equals(DDLWarehouseStatus.SelectedValue)
                            select i;
                }
                if (query.Any())
                {
                    ViewState["ReqData"] = query.CopyToDataTable();
                    GridView.DataSource = (DataTable)ViewState["ReqData"];
                    GridView.DataBind();
                }
                else
                {
                    oUtility.MsgAlert(this, "Not found data");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["ReqData"];
            GridView.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                string status = row.Cells[7].Text;
                if (status == "Complete")
                {
                    Response.Redirect("/Views/SaleOrder/Detail.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
                else if(status == "Cancel")
                {
                    Response.Redirect("/Views/SaleOrder/Cancel.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
                else
                {
                    Response.Redirect("/Views/SaleOrder/Edit.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
            }
        }
        protected void DDLCustomerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Group_Code").Equals(DDLCustomerGroup.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLCustomer, result, "Cus_Name", "Cus_Code", "Please select Customer");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }
        protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Code").Equals(txtCustomerCode.Text.Trim())
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                DDLCustomerGroup.SelectedValue = result.Rows[0]["Cus_Group_Code"].ToString();
                DDLCustomer.SelectedValue = txtCustomerCode.Text.Trim();
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }
        protected void DDLSaleOrderType_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oVENDER_Service.getData().AsEnumerable()
                                         where i.Field<string>("VENDER_GROUP_CODE").Equals(DDLSaleOrderType.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            IEnumerable<DataRow> query2 = from i in oCOSTCENTER_Service.getData().AsEnumerable()
                                          where i.Field<string>("COST_CENTER_TYPE").Equals(DDLSaleOrderType.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                          select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLVender, result, "VENDER_FULL_NAME", "VENDER_CODE", "Please select Customer");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
            if (query2.Any())
            {
                DataTable result = query2.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLCostCenter, result, "COST_CENTER_NAME", "COST_CENTER_CODE", "Please select Sale");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }
    }
}