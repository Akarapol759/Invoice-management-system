using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.SaleOrder
{
    public partial class Detail : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        SALE_Service oSALE_Service = new SALE_Service();
        SALE_ORDER_Service oSALE_ORDER_Service = new SALE_ORDER_Service();
        INVENTORY_Service oINVENTORY_Service = new INVENTORY_Service();
        CREDITNOTE_Service oCREDITNOTE_Service = new CREDITNOTE_Service();
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
            DataTable reqData = oSALE_ORDER_Service.getDataById(id);
            ViewState["reqData"] = reqData;

            lblDetailId.Text = reqData.Rows[0]["SALE_ORDER_ID"].ToString();
            lblDetailCustomerGroup.Text = reqData.Rows[0]["Cus_Group_Name"].ToString();
            lblDetailCustomer.Text = reqData.Rows[0]["Cus_Name"].ToString();
            lblDetailSaleOrderType.Text = reqData.Rows[0]["SALE_ORDER_TYPE"].ToString();
            lblDetailVenderName.Text = reqData.Rows[0]["VENDER_NAME"].ToString();
            lblDetailSaleOrderNumber.Text = reqData.Rows[0]["SALE_ORDER_Number"].ToString();
            lblDetailSaleOrderBalance.Text = reqData.Rows[0]["SALE_ORDER_Balance"].ToString();
            lblDetailPONumber.Text = reqData.Rows[0]["PO_Number"].ToString();
            lblDetailSaleOrderShippingDate.Text = reqData.Rows[0]["SALE_ORDER_Shipping_Date"].ToString();
            double amount = Double.Parse(reqData.Rows[0]["SALE_ORDER_Amount"].ToString());
            lblDetailSaleOrderAmount.Text = string.Format("{0:C2}", amount);
            double balance = Double.Parse(reqData.Rows[0]["SALE_ORDER_Balance"].ToString());
            lblDetailSaleOrderBalance.Text = string.Format("{0:C2}", balance);
            lblDetailSale.Text = reqData.Rows[0]["Sale_Name"].ToString();
            lblDetailCostCenter.Text = reqData.Rows[0]["COST_CENTER_NAME"].ToString();
            txtEditRemark.Text = reqData.Rows[0]["SALE_ORDER_Remark"].ToString();
            RdWarehouseStatus.SelectedValue = reqData.Rows[0]["SALE_ORDER_Status"].ToString();

            DataTable reqDataDetail = oSALE_ORDER_Service.getDataDetail(id);
            ViewState["reqDataDetail"] = reqDataDetail;
            GridView.DataSource = reqDataDetail;
            GridView.DataBind();

            ViewState["reqDataInvoice"] = oINVENTORY_Service.getDataBySaleOrder(lblDetailSaleOrderNumber.Text);
            GridViewInvoice.DataSource = (DataTable)ViewState["reqDataInvoice"];
            GridViewInvoice.DataBind();

            ViewState["reqDataCreditNote"] = oCREDITNOTE_Service.getDataBySaleOrder(lblDetailSaleOrderNumber.Text);
            GridViewCreditNote.DataSource = (DataTable)ViewState["reqDataCreditNote"];
            GridViewCreditNote.DataBind();

            ViewState["reqDataComment"] = oSALE_ORDER_Service.getDataCommentById(id);
            GridViewComment.DataSource = (DataTable)ViewState["reqDataComment"];
            GridViewComment.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SaleOrder/Main.aspx");
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Set Id
                Label gvlblId = (Label)(e.Row.FindControl("gvlblId"));
                gvlblId.Text = DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_DETAIL_ID").ToString();

                //Set Type Attach
                Label gvlblTypeAttach = (Label)(e.Row.FindControl("gvlblTypeAttach"));
                gvlblTypeAttach.Text = (string)DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_TYPE").ToString();

                //Set Extension
                Label gvlblFileName = (Label)(e.Row.FindControl("gvlblFileName"));
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_FILE_NAME").ToString()))
                {
                    gvlblFileName.Text = (string)DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_FILE_NAME").ToString();
                }

                //Set Visible Download
                Button btnDownload = (Button)(e.Row.FindControl("btnDownload"));
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_FILE_NAME").ToString()))
                {
                    btnDownload.Visible = true;
                }
                else
                {
                    btnDownload.Visible = false;
                }

                //Set Status
                DropDownList DDLDetailStatus = (DropDownList)(e.Row.FindControl("DDLDetailStatus"));
                DDLDetailStatus.SelectedValue = (string)DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_TYPE").ToString() == "Buy")
                {
                    DDLDetailStatus.Visible = true;
                }
                else
                {
                    DDLDetailStatus.Visible = false;
                }
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gridViewRow.RowIndex;
            string id = GridView.DataKeys[index].Value.ToString();
            //string aa = GridView.Rows[index].Cells[2].Text;
            //get extension

            IEnumerable<DataRow> query = from i in ((DataTable)ViewState["reqDataDetail"]).AsEnumerable()
                                         select i;
            DataTable dtExtension = null;
            if (query.Any())
            {
                query = from i in query.AsEnumerable()
                        where i.Field<Int64>("SALE_ORDER_DETAIL_ID").Equals(Convert.ToInt64(id))
                        select i;
                dtExtension = query.CopyToDataTable();
            }

            string filename = dtExtension.Rows[0]["SALE_ORDER_FILE_NAME"].ToString();
            string path = @"http://" + HttpContext.Current.Request.Url.Host + "/Uploads/" + filename; //Server
            Response.Expires = 1500;
            Response.Redirect(path);

            //Show Edit Par
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(@"<script type='text/javascript'>");
            //sb.Append("$('#addModal').modal('show');");
            //sb.Append(@"</script>");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addModalScript", sb.ToString(), false);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            string a = (lblDetailSaleOrderBalance.Text).Substring(1);
            if (lblDetailSaleOrderType.Text == "MS")
            {
                Response.Redirect("/Views/Inventory/Add.aspx?id=" + lblDetailId.Text);
            }
            else
            {
                if (Convert.ToDecimal((lblDetailSaleOrderBalance.Text).Substring(1)) > 0)
                {
                    Response.Redirect("/Views/Inventory/Add.aspx?id=" + lblDetailId.Text);
                }
                else
                {
                    oUtility.MsgAlert(this, "Balance is 0.");
                }
            }
        }

        protected void GridViewInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewInvoice.Rows[index];
                string status = row.Cells[8].Text;
                if (status == "Complete" || status == "Credit Note")
                {
                    Response.Redirect("/Views/Inventory/Detail.aspx?id=" + GridViewInvoice.DataKeys[index].Value.ToString());
                }
                else
                {
                    Response.Redirect("/Views/Inventory/Edit.aspx?id=" + GridViewInvoice.DataKeys[index].Value.ToString());
                }
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (fuAttach.HasFile)
            {
                string id = oSALE_ORDER_Service.insertSaleOrderDetailData(lblDetailId.Text, DDLAddType.SelectedValue.ToString(), DDLAddType.SelectedValue + "_" + lblDetailSaleOrderNumber.Text + Path.GetExtension(fuAttach.FileName.ToString()), ViewState["user"].ToString());
                fuAttach.SaveAs(Server.MapPath("~/Uploads/" + id + "_" + DDLAddType.SelectedValue + "_" + lblDetailSaleOrderNumber.Text + Path.GetExtension(fuAttach.FileName.ToString())));
                //fuAttach.SaveAs(Server.MapPath("~/Uploads/" + DDLAddType.SelectedValue.ToString() + Path.GetExtension(fuAttach.FileName.ToString())));
                //oSALE_ORDER_Service.updateSaleOrderDetailData(DDLAddType.SelectedValue.ToString(), Path.GetExtension(fuAttach.FileName.ToString()), ViewState["user"].ToString());
                BindData(lblDetailId.Text);
            }
            else
            {
                oUtility.MsgAlert(this, "Please select file");
            }
        }
        protected void DDLDetailStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gridViewRow.RowIndex;
                string id = GridView.DataKeys[index].Value.ToString();

                var ddl = GridView.Rows[index].FindControl("DDLDetailStatus") as DropDownList;
                string value = ddl.SelectedItem.Value;

                //Update Status Detail
                oSALE_ORDER_Service.updateSaleOrderDetailData(id, value, ViewState["user"].ToString());
            }
            catch (Exception ex)
            {
                oUtility.MsgAlert(this, ex.Message);
            }
        }
        protected void GridViewCreditNote_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewCreditNote.Rows[index];
                string status = row.Cells[8].Text;
                if (status == "Complete")
                {
                    Response.Redirect("/Views/CreditNote/Detail.aspx?id=" + GridViewCreditNote.DataKeys[index].Value.ToString());
                }
                else
                {
                    Response.Redirect("/Views/CreditNote/Edit.aspx?id=" + GridViewCreditNote.DataKeys[index].Value.ToString());
                }
            }
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComment.Text))
            {
                oSALE_ORDER_Service.insertSaleOrderComment(lblDetailId.Text, txtComment.Text, ViewState["user"].ToString());
                BindData(lblDetailId.Text);
            }
            else
            {
                oUtility.MsgAlert(this, "Please insert comment");
            }
        }

        protected void GridViewComment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Set Id
                Label gvlblId = (Label)(e.Row.FindControl("gvlblId"));
                gvlblId.Text = DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_Comment_Id").ToString();

                //Set Comment
                Label gvlblComment = (Label)(e.Row.FindControl("gvlblComment"));
                gvlblComment.Text = (string)DataBinder.Eval(e.Row.DataItem, "Comment").ToString();

                //Set By Whom
                Label gvlblByWhom = (Label)(e.Row.FindControl("gvlblByWhom"));
                gvlblByWhom.Text = (string)DataBinder.Eval(e.Row.DataItem, "User_Create").ToString();

                //Set Date
                Label gvlblDate = (Label)(e.Row.FindControl("gvlblDate"));
                gvlblDate.Text = (string)DataBinder.Eval(e.Row.DataItem, "Create_Date").ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //check invoice
            //DataTable dt = oINVENTORY_Service.getDataBySaleOrder(lblDetailSaleOrderNumber.Text);
            DataTable reqData = (DataTable)ViewState["reqData"];
            double amount = Double.Parse(reqData.Rows[0]["SALE_ORDER_Amount"].ToString());
            double balance = Double.Parse(reqData.Rows[0]["SALE_ORDER_Balance"].ToString());
            if (amount == balance)
            {
                oSALE_ORDER_Service.cancelSaleOrder(lblDetailId.Text, "N", ViewState["user"].ToString());
                Response.Redirect("/Views/SaleOrder/Cancel.aspx?id=" + lblDetailId.Text);
            }
            else
            {
                oUtility.MsgAlert(this, "Cannot cancel");
            }
        }
    }
}