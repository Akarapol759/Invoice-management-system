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
    public partial class Add : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        ITEM_Service oITEM_Service = new ITEM_Service();
        ITEM_GROUP_Service oITEM_GROUP_Service = new ITEM_GROUP_Service();
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
            DataTable dtItemGroup = oUtility.getActive(oITEM_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddItemGroup, dtItemGroup, "Item_Group_Name", "Id", "Please select item group");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtAddItemCode.Text.Trim()) && !string.IsNullOrEmpty(txtAddItemName.Text)
                && !string.IsNullOrEmpty(txtAddItemDescription.Text) && DDLAddItemType.SelectedValue != "-1"
                && DDLAddItemGroup.SelectedValue != "-1")
            { 
                //check duplicate
                DataTable dt = oITEM_Service.getDataDup(txtAddItemCode.Text.Trim(), DDLAddItemType.SelectedValue);
                if(dt.Rows.Count > 0)
                {
                    oUtility.MsgAlert(this, "Data had duplicate");
                }
                else
                {
                    oITEM_Service.insertItemData(txtAddItemCode.Text.Trim(), txtAddItemName.Text.Trim(), txtAddItemDescription.Text, DDLAddItemType.SelectedValue, DDLAddItemGroup.SelectedValue, "A", ViewState["user"].ToString());
                    Response.Redirect("/Views/Master/Item/Edit.aspx?itemCode=" + txtAddItemCode.Text.Trim());
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all field");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Item/Main.aspx");
        }
    }
}