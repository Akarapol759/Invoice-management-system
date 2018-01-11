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
    public partial class Add : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        SALE_Service oSALE_Service = new SALE_Service();
        SALE_ORDER_Service oSALE_ORDER_Service = new SALE_ORDER_Service();
        COSTCENTER_Service oCOSTCENTER_Service = new COSTCENTER_Service();
        VENDER_Service oVENDER_Service = new VENDER_Service();
        EMAIL_Service oEMAIL_Service = new EMAIL_Service();
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

            DataTable dtSale = oUtility.getActive(oSALE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddSale, dtSale, "Sale_Name", "Sale_Code", "Please select Sale");

            DataTable dtCostCenter = oUtility.getActive(oCOSTCENTER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCostCenter, dtCostCenter, "COST_CENTER_NAME", "COST_CENTER_CODE", "Please select Sale");

            DataTable dtVender = oUtility.getActive(oVENDER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddVender, dtVender, "VENDER_FULL_NAME", "VENDER_CODE", "Please select Customer");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLAddSaleOrderType.SelectedValue == "MS")
            {
                if (DDLAddCustomerGroup.SelectedValue != "-1" && DDLAddCustomer.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtAddPONumber.Text.Trim()) && !string.IsNullOrEmpty(txtAddSaleOrderAmount.Text.Trim())
                    && DDLAddSale.SelectedValue != "-1" && DDLAddCostCenter.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtAddSaleOrderShippingDate.Text))
                {
                    //Check PO Number
                    DataTable purchase = oSALE_ORDER_Service.checkPODuplicate(txtAddPONumber.Text.Trim(), DDLAddCustomer.SelectedValue);
                    if (purchase.Rows.Count > 0)
                    {
                        oUtility.MsgAlert(this, "Duplicate Purchase Order Number");
                    }
                    else
                    {
                        DateTime date = DateTime.ParseExact(txtAddSaleOrderShippingDate.Text, "dd/MM/yyyy", null);
                        string dateConvert = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        DataTable insertId = oSALE_ORDER_Service.insertSaleOrderData(DDLAddCustomerGroup.SelectedValue, DDLAddCustomerGroup.SelectedItem.ToString(), DDLAddCustomer.SelectedValue, DDLAddCustomer.SelectedItem.ToString(), DDLAddSaleOrderType.SelectedValue, "", "", txtAddPONumber.Text.Trim(), dateConvert, txtAddSaleOrderAmount.Text.Trim(), DDLAddSale.SelectedValue, DDLAddSale.SelectedItem.ToString(), DDLAddCostCenter.SelectedValue, txtAddRemark.Text, ViewState["user"].ToString(), ViewState["user"].ToString());
                        //oEMAIL_Service.email("Akarapol.Im@nhealth-asia.com", "Akarapol.Im@nhealth-asia.com", "Test Send from N-Med", "Test by y0y0", "Akarapol.Imh");
                        Response.Redirect("/Views/SaleOrder/Edit.aspx?id=" + insertId.Rows[0]["insertId"].ToString());
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Please select all fields");
                }
            }
            else
            {

                if (DDLAddCustomerGroup.SelectedValue != "-1" && DDLAddCustomer.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtAddPONumber.Text.Trim()) && !string.IsNullOrEmpty(txtAddSaleOrderAmount.Text.Trim())
                    && DDLAddSale.SelectedValue != "-1" && DDLAddCostCenter.SelectedValue != "-1" && DDLAddVender.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtAddSaleOrderShippingDate.Text))
                {
                    //Check PO Number
                    DataTable purchase = oSALE_ORDER_Service.checkPODuplicate(txtAddPONumber.Text.Trim(), DDLAddCustomer.SelectedValue);
                    if (purchase.Rows.Count > 0)
                    {
                        oUtility.MsgAlert(this, "Duplicate Purchase Order Number");
                    }
                    else
                    {
                        DateTime date = DateTime.ParseExact(txtAddSaleOrderShippingDate.Text, "dd/MM/yyyy", null);
                        string dateConvert = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        DataTable insertId = oSALE_ORDER_Service.insertSaleOrderData(DDLAddCustomerGroup.SelectedValue, DDLAddCustomerGroup.SelectedItem.ToString(), DDLAddCustomer.SelectedValue, DDLAddCustomer.SelectedItem.ToString(), DDLAddSaleOrderType.SelectedValue, DDLAddVender.SelectedValue, DDLAddVender.SelectedItem.ToString(), txtAddPONumber.Text.Trim(), dateConvert, txtAddSaleOrderAmount.Text.Trim(), DDLAddSale.SelectedValue, DDLAddSale.SelectedItem.ToString(), DDLAddCostCenter.SelectedValue, txtAddRemark.Text, ViewState["user"].ToString(), ViewState["user"].ToString());
                        //oEMAIL_Service.email("Akarapol.Im@nhealth-asia.com", "Akarapol.Im@nhealth-asia.com", "Test Send from N-Med", "Test by y0y0", "Akarapol.Imh");
                        Response.Redirect("/Views/SaleOrder/Edit.aspx?id=" + insertId.Rows[0]["insertId"].ToString());
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Please select all fields");
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SaleOrder/Main.aspx");
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
        protected void txtAddCustomerCode_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Code").Equals(txtAddCustomerCode.Text.Trim())
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                DDLAddCustomerGroup.SelectedValue = result.Rows[0]["Cus_Group_Code"].ToString();
                DDLAddCustomer.SelectedValue = txtAddCustomerCode.Text.Trim();
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }

        protected void DDLAddSaleOrderType_TextChanged(object sender, EventArgs e)
        {
            if (DDLAddSaleOrderType.SelectedValue == "MS")
            {
                DDLAddVender.Enabled = false;
                //txtAddSaleOrderAmount.Enabled = false;
                //DDLAddCostCenter.Enabled = false;
            }
            else
            {
                DDLAddVender.Enabled = true;
                //txtAddSaleOrderAmount.Enabled = true;
                //DDLAddCostCenter.Enabled = true;
            }
            IEnumerable<DataRow> query = from i in oVENDER_Service.getData().AsEnumerable()
                                         where i.Field<string>("VENDER_GROUP_CODE").Equals(DDLAddSaleOrderType.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            IEnumerable<DataRow> query2 = from i in oCOSTCENTER_Service.getData().AsEnumerable()
                                          where i.Field<string>("COST_CENTER_TYPE").Equals(DDLAddSaleOrderType.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                          select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLAddVender, result, "VENDER_FULL_NAME", "VENDER_CODE", "Please select Customer");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
            if (query2.Any())
            {
                DataTable result = query2.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLAddCostCenter, result, "COST_CENTER_NAME", "COST_CENTER_CODE", "Please select Sale");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }
    }
}