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
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
        CUSTOMER_TYPE_Service oCUSTOMER_TYPE_Service = new CUSTOMER_TYPE_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnUpdate.UniqueID;
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
                            BindData(Request.QueryString["cusCode"]);
                            //BindData(null, ticket.Version);
                        }
                        else
                        {
                            BindData(Request.QueryString["cusCode"]);
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
        protected void BindData(string cusCode)
        {
            DataTable dtCustomerType = oUtility.getActive(oCUSTOMER_TYPE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditCustomerType, dtCustomerType, "Customer_Type_Name", "Customer_Type_Code", "Please select type");
            DataTable dt = oCUSTOMER_Service.getDataByCusCode(cusCode);
            lblEditCustomerGroupCode.Text = dt.Rows[0]["Cus_Group_Name"].ToString();
            lblEditCustomerCode.Text = dt.Rows[0]["Cus_Code"].ToString();
            txtEditCustomerName.Text = dt.Rows[0]["Cus_Name"].ToString();
            txtEditCustomerBilling.Text = dt.Rows[0]["Cus_Billing"].ToString();
            txtEditCustomerContact.Text = dt.Rows[0]["Cus_Contact"].ToString();
            txtEditCustomerTel.Text = dt.Rows[0]["Cus_Tel"].ToString();
            txtEditCustomerVat.Text = dt.Rows[0]["Cus_Vat"].ToString();
            DDLEditCustomerTerm.SelectedValue = dt.Rows[0]["Cus_Term"].ToString();
            DDLEditCustomerType.SelectedValue = dt.Rows[0]["Cus_Type"].ToString();
            RdStatus.SelectedValue = dt.Rows[0]["Cus_Status"].ToString();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEditCustomerName.Text))
            {
                oCUSTOMER_Service.updateCusData(lblEditCustomerCode.Text, txtEditCustomerName.Text, txtEditCustomerBilling.Text, txtEditCustomerContact.Text, txtEditCustomerTel.Text, DDLEditCustomerTerm.SelectedValue, txtEditCustomerVat.Text, DDLEditCustomerType.SelectedValue, RdStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
            }
            else
            {
                oUtility.MsgAlert(this, "Please put customer name");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Customer/Main.aspx");
        }

        protected void DDLEditCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLEditCustomerType.SelectedValue == "DO")
            {
                txtEditCustomerVat.Text = "7";
            }
            else
            {
                txtEditCustomerVat.Text = "10";
            }
        }
    }
}