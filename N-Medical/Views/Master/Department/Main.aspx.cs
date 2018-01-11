using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Master.Department
{
    public partial class Main : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
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
            DataTable dtCustomerGroup = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLCustomerGroup, dtCustomerGroup, "Cus_Group_Name", "Cus_Group_Code", "Please select group");
            //Search All Data
            ViewState["dtCustomer"] = oCUSTOMER_Service.getData();
            GridView.DataSource = (DataTable)ViewState["dtCustomer"];
            GridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         select i;
            if (query.Any())
            {
                if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cus_Code").Contains(txtCustomerCode.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cus_Name").Contains(txtCustomerName.Text.Trim())
                            select i;
                }
                if (DDLCustomerGroup.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cus_Group_Code").Equals(DDLCustomerGroup.SelectedValue)
                            select i;
                }
                if (DDLCustomerTerm.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Cus_Term").Equals(DDLCustomerTerm.SelectedValue)
                            select i;
                }
                if (query.Any())
                {
                    ViewState["dtCustomer"] = query.CopyToDataTable();
                    GridView.DataSource = (DataTable)ViewState["dtCustomer"];
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
            GridView.DataSource = (DataTable)ViewState["dtCustomer"];
            GridView.DataBind();
        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("edit"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                Response.Redirect("/Views/Master/Department/Edit.aspx?cusCode=" + GridView.DataKeys[index].Value.ToString());
            }
        }
    }
}