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
    public partial class Main : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        SALE_Service oSALE_Service = new SALE_Service();
        AREA_Service oAREA_Service = new AREA_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnSearch.UniqueID;
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
            oUtility.DDL(DDLSaleArea, dtSaleArea, "Area_Name", "Area_Code", "All Sale Area");
            //Search All Data
            ViewState["dtSale"] = oSALE_Service.getData();
            GridView.DataSource = (DataTable)ViewState["dtSale"];
            GridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oSALE_Service.getData().AsEnumerable()
                                         select i;
            if (query.Any())
            {
                if (!string.IsNullOrEmpty(txtSaleCode.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Sale_Code").Contains(txtSaleCode.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtSaleName.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Sale_Name").Contains(txtSaleName.Text.Trim())
                            select i;
                }
                if (DDLSaleArea.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<int>("Sale_Area").Equals(DDLSaleArea.SelectedValue)
                            select i;
                }
                if (query.Any())
                {
                    ViewState["dtSale"] = query.CopyToDataTable();
                    GridView.DataSource = (DataTable)ViewState["dtSale"];
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

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Sale/Add.aspx");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["dtSale"];
            GridView.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("edit"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                Response.Redirect("/Views/Master/Sale/Edit.aspx?saleCode=" + GridView.DataKeys[index].Value.ToString());
            }
        }
    }
}