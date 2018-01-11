using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Master.Price
{
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();
        PRICE_Service oPRICE_Service = new PRICE_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            DataTable dtItem = oUtility.getActive(oITEM_Service.getData("I"), "Status_Name", "Active");
            oUtility.DDL(DDLAddItem, dtItem, "Item_Name", "Item_Code", "Please select Item");

            DataTable dt = oCUSTOMER_Service.getDataByCusCode(cusCode);
            lblEditCustomerGroupCode.Text = dt.Rows[0]["Cus_Group_Name"].ToString();
            lblEditCustomerCode.Text = dt.Rows[0]["Cus_Code"].ToString();
            lblEditCustomerName.Text = dt.Rows[0]["Cus_Name"].ToString();
            lblEditCustomerVat.Text = dt.Rows[0]["Cus_Vat"].ToString();
            lblCustomerTerm.Text = dt.Rows[0]["Cus_Term_Name"].ToString();

            ViewState["dtPrice"] = oPRICE_Service.getDataByCusCode(cusCode);
            GridView.DataSource = (DataTable)ViewState["dtPrice"];
            GridView.DataBind();
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Price/Main.aspx");
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if(DDLAddItem.SelectedValue != "-1" && !string.IsNullOrEmpty(txtAddItemPrice.Text.Trim()))
            {
                DataTable dt = oPRICE_Service.getDataDup(DDLAddItem.SelectedValue, lblEditCustomerCode.Text);
                if(dt.Rows.Count > 0)
                {
                    oUtility.MsgAlert(this, "This item had price");
                }
                else
                {
                    oPRICE_Service.insertPriceData(DDLAddItem.SelectedValue, lblEditCustomerCode.Text, txtAddItemPrice.Text.Trim(), txtAddSaleCode.Text.Trim(), "A", ViewState["user"].ToString());
                    oUtility.MsgAlert(this, "Success");
                    BindData(lblEditCustomerCode.Text);
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all field");
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRow"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                IEnumerable<DataRow> query = from i in ((DataTable)ViewState["dtPrice"]).AsEnumerable()
                                             where i.Field<String>("Item_Code").Equals(GridView.DataKeys[index].Value.ToString())
                                             && i.Field<String>("Cus_Code").Equals(lblEditCustomerCode.Text)
                                             select i;
                if (query.Any())
                {
                    DataTable price = query.CopyToDataTable<DataRow>();
                    lblEditItemCode.Text = price.Rows[0]["Item_Code"].ToString();
                    lblEditItemName.Text = price.Rows[0]["Item_Name"].ToString();
                    txtEditItemPrice.Text = price.Rows[0]["Unit_Price"].ToString();
                    txtEditSaleCode.Text = price.Rows[0]["Sale_Code_BB"].ToString();
                    RdStatus.SelectedValue = price.Rows[0]["Price_Status"].ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#editModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
                else
                {
                    oUtility.MsgAlert(this, "Not found data");
                }
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["dtPrice"];
            GridView.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEditItemPrice.Text.Trim()))
            {
                oPRICE_Service.updatePriceData(lblEditItemCode.Text, lblEditCustomerCode.Text, txtEditItemPrice.Text.Trim(), txtEditSaleCode.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                BindData(lblEditCustomerCode.Text);
                oUtility.MsgAlert(this, "Success");
            }
            else
            {
                oUtility.MsgAlert(this, "Please insert item price");
            }
        }

        protected void txtAddItem_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oITEM_Service.getData("I").AsEnumerable()
                                         where i.Field<string>("Item_Code").Equals(txtAddItem.Text.Trim())
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                DDLAddItem.SelectedValue = result.Rows[0]["Item_Code"].ToString();
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }

        protected void DDLAddItem_TextChanged(object sender, EventArgs e)
        {
            if (DDLAddItem.SelectedValue != "-1")
            {
                txtAddItem.Text = DDLAddItem.SelectedValue;
            }
            else
            {
                txtAddItem.Text = "";
            }
        }
    }
}