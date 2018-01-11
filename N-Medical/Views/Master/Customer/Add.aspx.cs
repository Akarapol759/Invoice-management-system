using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Master.Customer
{
    public partial class Add : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
        RUNNING_Service oRUNNING_Service = new RUNNING_Service();
        CUSTOMER_TYPE_Service oCUSTOMER_TYPE_Service = new CUSTOMER_TYPE_Service();
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
                            ViewState["bu"] = "0";
                            BindData();
                            //BindData(null, ticket.Version);
                        }
                        else
                        {
                            ViewState["bu"] = ticket.UserData.ToString();
                            BindData();
                            //BindData(ViewState["bu"].ToString(), ticket.Version);
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
        public void BindData()
        {
            DataTable dtCustomerGroup = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCustomerGroup, dtCustomerGroup, "Cus_Group_Name", "Cus_Group_Code", "Please select group");
            DataTable dtCustomerType = oUtility.getActive(oCUSTOMER_TYPE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddCustomerType, dtCustomerType, "Customer_Type_Name", "Customer_Type_Code", "Please select type");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtAddCustomerName.Text) && !string.IsNullOrEmpty(txtAddCustomerBilling.Text) 
                && !string.IsNullOrEmpty(txtAddCustomerVat.Text.Trim()))
            {
                if (DDLAddCustomerGroup.SelectedValue == "BDMS" || DDLAddCustomerGroup.SelectedValue == "NH")
                {
                    if(!string.IsNullOrEmpty(txtAddCustomerCode.Text.Trim()))
                    {
                        //check duplicate
                        DataTable dt = oCUSTOMER_Service.checkCustomerDup(txtAddCustomerCode.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            oUtility.MsgAlert(this, "Data had duplicate");
                        }
                        else
                        {
                            oCUSTOMER_Service.insertCusData(DDLAddCustomerGroup.SelectedValue, txtAddCustomerCode.Text.Trim(), txtAddCustomerName.Text.Trim(), txtAddCustomerBilling.Text, txtAddCustomerContact.Text, txtAddCustomerTel.Text, txtAddCustomerVat.Text.Trim(), DDLAddCustomerTerm.SelectedValue, DDLAddCustomerType.SelectedValue, "A", ViewState["user"].ToString());
                            Response.Redirect("/Views/Master/Customer/Edit.aspx?cusCode=" + txtAddCustomerCode.Text.Trim());
                        }
                    }
                    else
                    {
                        oUtility.MsgAlert(this, "Please put customer code");
                    }
                }
                else
                {
                    string customerCode = oRUNNING_Service.generateCustomerNumber();
                    oCUSTOMER_Service.insertCusData(DDLAddCustomerGroup.SelectedValue, customerCode, txtAddCustomerName.Text.Trim(), txtAddCustomerBilling.Text, txtAddCustomerContact.Text, txtAddCustomerTel.Text, txtAddCustomerVat.Text.Trim(), DDLAddCustomerTerm.SelectedValue, DDLAddCustomerType.SelectedValue, "A", ViewState["user"].ToString());
                    Response.Redirect("/Views/Master/Customer/Edit.aspx?cusCode=" + customerCode);
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all field");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Customer/Main.aspx");
        }

        protected void DDLAddCustomerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLAddCustomerGroup.SelectedValue == "BDMS" || DDLAddCustomerGroup.SelectedValue == "NH")
            {
                txtAddCustomerCode.Enabled = true;
            }
            else
            {
                txtAddCustomerCode.Enabled = false;
            }
        }
    }
}