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
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        ITEM_Service oITEM_Service = new ITEM_Service();
        ITEM_GROUP_Service oITEM_GROUP_Service = new ITEM_GROUP_Service();
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
                            BindData(Request.QueryString["itemCode"]);
                            //BindData(null, ticket.Version);
                        }
                        else
                        {
                            BindData(Request.QueryString["itemCode"]);
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
        protected void BindData(string itemCode)
        {
            //DataTable dtItemGroup = oUtility.getActive(oITEM_GROUP_Service.getData(), "Status_Name", "Active");
            //oUtility.DDL(DDLAddItemGroup, dtItemGroup, "Item_Group_Name", "Id", "Please select Sale");

            DataTable dt = oITEM_Service.getDataByItemCode(itemCode);
            lblEditItemCode.Text = dt.Rows[0]["Item_Code"].ToString();
            txtEditItemName.Text = dt.Rows[0]["Item_Name"].ToString();
            txtEditItemDescription.Text = dt.Rows[0]["Item_Description"].ToString();
            lblEditItemGroup.Text = dt.Rows[0]["Item_Group_Name"].ToString();
            lblEditItemType.Text = dt.Rows[0]["Item_Type_Name"].ToString();
            RdStatus.Text = dt.Rows[0]["Item_Status"].ToString();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEditItemName.Text))
            {
                oITEM_Service.updateItemData(lblEditItemCode.Text, txtEditItemName.Text, txtEditItemDescription.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
            }
            else
            {
                oUtility.MsgAlert(this, "Please put item name");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Item/Main.aspx");
        }
    }
}