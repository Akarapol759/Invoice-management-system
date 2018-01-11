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
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CREDITNOTE_Service oCREDITNOTE_Service = new CREDITNOTE_Service();
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
                            BindData(Request.QueryString["id"]);
                        }
                        else
                        {
                            BindData(Request.QueryString["id"]);
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
        protected void BindData(string id)
        {
            DataTable reqData = oCREDITNOTE_Service.creditNoteDetail(id);

            lblEditId.Text = reqData.Rows[0]["Id"].ToString();
            lblEditInvoiceNumber.Text = reqData.Rows[0]["Invoice_Number"].ToString();
            lblEditRemark.Text = reqData.Rows[0]["CreditNote_Remark"].ToString();

            GridView.DataSource = reqData;
            GridView.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            oCREDITNOTE_Service.updateCreditNoteData(lblEditId.Text, RdCreditNoteStatus.SelectedValue, ViewState["user"].ToString());
            Response.Redirect("/Views/CreditNote/Detail.aspx?id=" + lblEditId.Text);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/CreditNote/Main.aspx");
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Set Id
                Label gvlblId = (Label)(e.Row.FindControl("gvlblId"));
                gvlblId.Text = DataBinder.Eval(e.Row.DataItem, "Id").ToString();

                //Set Item Code
                Label gvlblItemCode = (Label)(e.Row.FindControl("gvlblItemCode"));
                gvlblItemCode.Text = (string)DataBinder.Eval(e.Row.DataItem, "Item_Code");

                //Set Item Name
                Label gvlblItemName = (Label)(e.Row.FindControl("gvlblItemName"));
                gvlblItemName.Text = (string)DataBinder.Eval(e.Row.DataItem, "Item_Name");

                //Set Item Qty
                Label gvlblItemQty = (Label)(e.Row.FindControl("gvlblItemQty"));
                gvlblItemQty.Text = DataBinder.Eval(e.Row.DataItem, "Item_Qty").ToString();

                //Set Item Unit Price
                Label gvlblUnitPrice = (Label)(e.Row.FindControl("gvlblUnitPrice"));
                gvlblUnitPrice.Text = DataBinder.Eval(e.Row.DataItem, "Item_Price").ToString();

                //Set Item UOM
                Label gvlblUOM = (Label)(e.Row.FindControl("gvlblUOM"));
                gvlblUOM.Text = DataBinder.Eval(e.Row.DataItem, "uom").ToString();

                //Set Batch
                Label gvlblBatchNo = (Label)(e.Row.FindControl("gvlblBatchNo"));
                gvlblBatchNo.Text = DataBinder.Eval(e.Row.DataItem, "Batch").ToString();
                //Set AMount
                Label gvlblAmount = (Label)(e.Row.FindControl("gvlblAmount"));
                gvlblAmount.Text = DataBinder.Eval(e.Row.DataItem, "Amount").ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            oCREDITNOTE_Service.deleteCreditNoteData(lblEditId.Text);
            oCREDITNOTE_Service.deleteCreditNoteDetailData(lblEditId.Text);
            Response.Redirect("/Views/CreditNote/Main.aspx");
        }
    }
}