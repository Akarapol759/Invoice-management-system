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
    public partial class Add : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        INVENTORY_Service oINVENTORY_Service = new INVENTORY_Service();
        CREDITNOTE_Service oCREDITNOTE_Service = new CREDITNOTE_Service();
        REMARK_Service oREMARK_Service = new REMARK_Service();
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
            DataTable reqData = oINVENTORY_Service.invoiceEditData(id);
            lblAddInvoiceNumber.Text = reqData.Rows[0]["Invoice_Number"].ToString();
            lblAddSaleOrderNumber.Text = reqData.Rows[0]["SALE_ORDER_Number"].ToString();
            lblAddInvocieType.Text = reqData.Rows[0]["Invoice_Type"].ToString();

            DataTable dtRemark = oUtility.getActive(oREMARK_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLAddRemark, dtRemark, "Remark", "Id", "Please select Remark");

            ViewState["reqData"] = reqData;
            GridView.DataSource = reqData;
            GridView.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(DDLAddRemark.SelectedValue != "-1")
            {
                //check invoice in credit note again
                DataTable dt = oCREDITNOTE_Service.checkInvoiceCreditNote(lblAddInvoiceNumber.Text, "C','O");
                if(dt.Rows.Count == 0)
                {
                    DataTable id = oCREDITNOTE_Service.insertCreditNoteData(lblAddInvoiceNumber.Text, lblAddSaleOrderNumber.Text, DDLAddRemark.SelectedItem.ToString(), "O", lblAddInvocieType.Text, "0", ViewState["user"].ToString(), ViewState["user"].ToString());
                    DataTable dtDetail = (DataTable)ViewState["reqData"];
                    //Insert CreditNote Detail
                    for (int j = 0; j < dtDetail.Rows.Count; j++)
                    {
                        string itemcost = "0";
                        if(!string.IsNullOrEmpty(dtDetail.Rows[j]["Item_Cost"].ToString()))
                        {
                            itemcost = dtDetail.Rows[j]["Item_Cost"].ToString();
                        }
                        oCREDITNOTE_Service.insertCreditNoteDetailData(id.Rows[0]["insertId"].ToString(), dtDetail.Rows[j]["Item_Code"].ToString(), dtDetail.Rows[j]["Item_Name"].ToString(), dtDetail.Rows[j]["Item_Location"].ToString(), dtDetail.Rows[j]["Batch"].ToString(), dtDetail.Rows[j]["Expire_Date"].ToString(), itemcost, dtDetail.Rows[j]["Item_Price"].ToString(), dtDetail.Rows[j]["Item_Qty"].ToString(), dtDetail.Rows[j]["UOM"].ToString(), dtDetail.Rows[j]["Cost_Center"].ToString(), "O", ViewState["user"].ToString());
                    }
                    Response.Redirect("/Views/CreditNote/Edit.aspx?id=" + id.Rows[0]["insertId"].ToString());
                }
                else
                {
                    oUtility.MsgAlert(this, "Please select remark");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please select remark");
            }
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
                gvlblId.Text = DataBinder.Eval(e.Row.DataItem, "Detail_Id").ToString();

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
    }
}