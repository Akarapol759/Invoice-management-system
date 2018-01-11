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
    public partial class Add : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        SALE_Service oSALE_Service = new SALE_Service();
        AREA_Service oAREA_Service = new AREA_Service();
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
            DataTable dtSaleArea = oAREA_Service.getData();
            oUtility.DDL(DDLAddSaleArea, dtSaleArea, "Area_Name", "Area_Code", "All Sale Area");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtAddSaleCode.Text.Trim()) && !string.IsNullOrEmpty(txtAddSaleName.Text)
                && !string.IsNullOrEmpty(txtAddSalePosition.Text) && !string.IsNullOrEmpty(txtAddSaleEmail.Text)
                && !string.IsNullOrEmpty(txtAddSaleTel.Text) && DDLAddSaleArea.SelectedValue != "-1")
            { 
                //check duplicate
                DataTable dt = oSALE_Service.getDataDup(txtAddSaleCode.Text.Trim());
                if(dt.Rows.Count > 0)
                {
                    oUtility.MsgAlert(this, "Data had duplicate");
                }
                else
                {
                    oSALE_Service.insertSaleData(txtAddSaleCode.Text.Trim(), txtAddSaleName.Text.Trim(), txtAddSalePosition.Text, txtAddSaleEmail.Text, txtAddSaleTel.Text, DDLAddSaleArea.SelectedValue, "A", ViewState["user"].ToString());
                    Response.Redirect("/Views/Master/Sale/Edit.aspx?saleCode=" + txtAddSaleCode.Text.Trim());
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all field");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Sale/Main.aspx");
        }
    }
}