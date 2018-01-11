using Microsoft.Reporting.WebForms;
using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Equipment
{
    public partial class Detail : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        EQUIPMENT_Service oEQUIPMENT_Service = new EQUIPMENT_Service();
        CREDITNOTE_Service oCREDITNOTE_Service = new CREDITNOTE_Service();

        //Variable
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Form.DefaultButton = btnUpdate.UniqueID;
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
            DataTable reqData = oEQUIPMENT_Service.invoiceEditData(id);
            lblDetailId.Text = reqData.Rows[0]["Id"].ToString();
            lblDetailCustomerGroup.Text = reqData.Rows[0]["Cus_Group_Name"].ToString();
            lblDetailCustomer.Text = reqData.Rows[0]["Cus_Name"].ToString();
            lblDetailDepartment.Text = reqData.Rows[0]["Dept_Name"].ToString();
            lblDetailInvoiceNumber.Text = reqData.Rows[0]["Invoice_Number"].ToString();
            lblDetailInvoiceDate.Text = reqData.Rows[0]["Invoice_Date"].ToString();
            lblDetailConsumtionNumber.Text = reqData.Rows[0]["Consumtion_Number"].ToString();
            lblDetailPONumber.Text = reqData.Rows[0]["PO_Number"].ToString();
            lblDetailShipping.Text = reqData.Rows[0]["Cus_Shipping_Detail"].ToString();
            lblDetailSale.Text = reqData.Rows[0]["Sale_Name"].ToString();
            lblDetailDeliveryRoute.Text = reqData.Rows[0]["Delivery_Route_Name"].ToString();
            lblDetailWarehouse.Text = reqData.Rows[0]["Warehouse_Name"].ToString();
            txtDetailRemark.Text = reqData.Rows[0]["Invoice_Remark"].ToString();
            lblDetailDiscount.Text = reqData.Rows[0]["Invoice_Discount"].ToString();

            string total = calculateTotal(reqData);
            lblDetailTotalAmount.Text = total;
            lblDetailVat.Text = ((Convert.ToDecimal(total) * Convert.ToDecimal(reqData.Rows[0]["Cus_Vat"])) / 100).ToString();
            lblDetailTotalAmountIncVat.Text = (Convert.ToDecimal(total) + ((Convert.ToDecimal(total) * (Convert.ToDecimal(reqData.Rows[0]["Cus_Vat"]) / 100)))).ToString();
            chkNoVat.Checked = true;

            GridView.DataSource = reqData;
            GridView.DataBind();
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

                //Set AMount
                Label gvlblAmount = (Label)(e.Row.FindControl("gvlblAmount"));
                gvlblAmount.Text = DataBinder.Eval(e.Row.DataItem, "Amount").ToString();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable data = oEQUIPMENT_Service.invoiceEditData(lblDetailId.Text);
            ReportViewer1.Reset();
            ReportDataSource dt = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dt);
            if (chkNoDate.Checked && chkNoVat.Checked)
            {
                ReportViewer1.LocalReport.ReportPath = @"Reports\Equipment\Invoice\Invoice_ExcDateExcVat.rdlc";
            }
            else if (!chkNoDate.Checked && chkNoVat.Checked)
            {
                ReportViewer1.LocalReport.ReportPath = @"Reports\Equipment\Invoice\Invoice_ExcVat.rdlc";
            }
            else if (chkNoDate.Checked && !chkNoVat.Checked)
            {
                ReportViewer1.LocalReport.ReportPath = @"Reports\Equipment\Invoice\Invoice_ExcDate.rdlc";
            }
            else
            {
                ReportViewer1.LocalReport.ReportPath = @"Reports\Equipment\Invoice\Invoice.rdlc"; ;
            }

            //Calculate
            double total = 0;
            if (data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    double aa = Convert.ToDouble(data.Rows[i]["Amount"]);
                    total += aa;
                }
                if (!string.IsNullOrEmpty(data.Rows[0]["Invoice_Discount"].ToString()))
                {
                    double bb = Convert.ToDouble(data.Rows[0]["Invoice_Discount"]);
                    total -= bb;
                }
            }
            ReportParameter totalExVat = new ReportParameter("TotalExVat", total.ToString());
            ReportParameter Vat = new ReportParameter("Vat", ((Convert.ToDecimal(total) * Convert.ToDecimal(data.Rows[0]["Cus_Vat"])) / 100).ToString());
            ReportParameter totalIncVat = new ReportParameter("TotalIncVat", (Convert.ToDecimal(total) + ((Convert.ToDecimal(total) * (Convert.ToDecimal(data.Rows[0]["Cus_Vat"]) / 100)))).ToString());
            ReportParameter thaiBahtIncVat = new ReportParameter("ThaiBahtIncVat", oUtility.ThaiBaht((Convert.ToDecimal(total) + ((Convert.ToDecimal(total) * (Convert.ToDecimal(data.Rows[0]["Cus_Vat"]) / 100)))).ToString()));
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalExVat, Vat, totalIncVat, thaiBahtIncVat });
            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            Response.Buffer = true;
            Response.ClearHeaders();
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "inline; filename= " + lblDetailInvoiceNumber.Text + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }

        protected void btnCreditNote_Click(object sender, EventArgs e)
        {
            DataTable dt = oCREDITNOTE_Service.checkInvoiceCreditNote(lblDetailInvoiceNumber.Text, "C','O");
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("/Views/CreditNote/Add.aspx?id=" + lblDetailId.Text);
            }
            else
            {
                if (dt.Rows[0]["CreditNote_Status"].ToString() == "C")
                {
                    Response.Redirect("/Views/CreditNote/Detail.aspx?id=" + dt.Rows[0]["Id"].ToString());
                }
                else
                {
                    Response.Redirect("/Views/CreditNote/Edit.aspx?id=" + dt.Rows[0]["Id"].ToString());
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Equipment/Main.aspx");
        }
        protected string calculateTotal(DataTable data)
        {
            //Calculate
            double total = 0;
            if (data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    double aa = Convert.ToDouble(data.Rows[i]["Amount"]);
                    total += aa;
                }
                if (!string.IsNullOrEmpty(data.Rows[0]["Invoice_Discount"].ToString()))
                {
                    double bb = Convert.ToDouble(data.Rows[0]["Invoice_Discount"].ToString());
                    total -= bb;
                }
            }
            return total.ToString();
        }
    }
}