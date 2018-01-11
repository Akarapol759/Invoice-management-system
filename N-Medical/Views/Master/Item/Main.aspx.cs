using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Master.Item
{
    public partial class Main : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        ITEM_Service oITEM_Service = new ITEM_Service();
        ITEM_GROUP_Service oITEM_GROUP_Service = new ITEM_GROUP_Service();
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
            DataTable dtItemGroup = oUtility.getActive(oITEM_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLItemGroup, dtItemGroup, "Item_Group_Name", "Id", "All item group");
            //Search All Data
            ViewState["dtItem"] = oITEM_Service.getData(null);
            GridView.DataSource = (DataTable)ViewState["dtItem"];
            GridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oITEM_Service.getData(null).AsEnumerable()
                                         select i;
            if (query.Any())
            {
                if (!string.IsNullOrEmpty(txtItemCode.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Item_Code").Contains(txtItemCode.Text.Trim())
                            select i;
                }
                if (!string.IsNullOrEmpty(txtItemName.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Item_Name").Contains(txtItemName.Text.Trim())
                            select i;
                }
                if (DDLAddItemType.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("Item_Type").Equals(DDLAddItemType.SelectedValue)
                            select i;
                }
                if (DDLItemGroup.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<int>("Item_Group_Id").Equals(DDLItemGroup.SelectedValue)
                            select i;
                }
                if (query.Any())
                {
                    ViewState["dtItem"] = query.CopyToDataTable();
                    GridView.DataSource = (DataTable)ViewState["dtItem"];
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
            Response.Redirect("/Views/Master/Item/Add.aspx");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["dtItem"];
            GridView.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("edit"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                Response.Redirect("/Views/Master/Item/Edit.aspx?itemCode=" + GridView.DataKeys[index].Value.ToString());
            }
        }
    }
}