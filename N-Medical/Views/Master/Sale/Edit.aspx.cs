using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Master.Sale
{
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        SALE_Service oSALE_Service = new SALE_Service();
        AREA_Service oAREA_Service = new AREA_Service();
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
                            BindData(Request.QueryString["saleCode"]);
                            //BindData(null, ticket.Version);
                        }
                        else
                        {
                            BindData(Request.QueryString["saleCode"]);
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
        protected void BindData(string saleCode)
        {
            DataTable dtSaleArea = oAREA_Service.getData();
            oUtility.DDL(DDLEditSaleArea, dtSaleArea, "Area_Name", "Area_Code", "All Sale Area");

            DataTable dt = oSALE_Service.getDataBySaleCode(saleCode);
            lblEditSaleCode.Text = dt.Rows[0]["Sale_Code"].ToString();
            txtEditSaleName.Text = dt.Rows[0]["Sale_Name"].ToString();
            txtEditSalePosition.Text = dt.Rows[0]["Sale_Position"].ToString();
            txtEditSaleEmail.Text = dt.Rows[0]["Sale_Email"].ToString();
            txtEditSaleTel.Text = dt.Rows[0]["Sale_Tel"].ToString();
            DDLEditSaleArea.Text = dt.Rows[0]["Sale_Area"].ToString();
            RdStatus.Text = dt.Rows[0]["Sale_Status"].ToString();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEditSaleName.Text))
            {
                oSALE_Service.updateSaleData(lblEditSaleCode.Text, txtEditSaleName.Text, txtEditSalePosition.Text, txtEditSaleEmail.Text, txtEditSaleTel.Text, DDLEditSaleArea.SelectedValue, RdStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
            }
            else
            {
                oUtility.MsgAlert(this, "Please put item name");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Sale/Main.aspx");
        }
    }
}