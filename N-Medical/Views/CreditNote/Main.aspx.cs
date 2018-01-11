using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.CreditNote
{
    public partial class Main : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CREDITNOTE_Service oCREDITNOTE_Service = new CREDITNOTE_Service();
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
            ViewState["ReqData"] = oCREDITNOTE_Service.getData();
            GridView.DataSource = (DataTable)ViewState["ReqData"];
            GridView.DataBind();
        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Inventory/Add.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCREDITNOTE_Service.getData().AsEnumerable()
                                         select i;
            if (query.Any())
            {
                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Invoice_Number").Contains(txtInvoiceNumber.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtConsumtionNumber.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Consumtion_Number").Contains(txtConsumtionNumber.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtPONumber.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("PO_Number").Contains(txtPONumber.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtCNNumber.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("CreditNote_Number").Contains(txtCNNumber.Text.Trim())
                            select i;
                }
                if(DDLCREStatus.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("CreditNote_Status").Equals(DDLCREStatus.SelectedValue)
                            select i;
                }
                if(query.Any())
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
                string status = row.Cells[8].Text;
                if (status == "Complete")
                {
                    Response.Redirect("/Views/CreditNote/Detail.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
                else
                {
                    Response.Redirect("/Views/CreditNote/Edit.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
            }
        }
    }
}