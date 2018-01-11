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
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        EQUIPMENT_Service oEQUIPMENT_Service = new EQUIPMENT_Service();
        SALE_Service oSALE_Service = new SALE_Service();
        DELIVERY_ROUTE_Service oDELIVERY_ROUTE_Service = new DELIVERY_ROUTE_Service();
        WAREHOUSE_Service oWAREHOUSE_Service = new WAREHOUSE_Service();

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
            DataTable reqData = oEQUIPMENT_Service.invoiceEditData(id);

            DataTable dtSale = oUtility.getActive(oSALE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditSale, dtSale, "Sale_Name", "Sale_Code", "Please select Sale");

            DataTable dtDeliveryRoute = oUtility.getActive(oDELIVERY_ROUTE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditDeliveryRoute, dtDeliveryRoute, "Delivery_Route_Name", "Delivery_Route_Code", "Please select Delivery Route");

            DataTable dtWarehouse = oUtility.getActive(oWAREHOUSE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditWarehouse, dtWarehouse, "Warehouse_Name", "Warehouse_Code", "Please select Warehouse");

            lblEditId.Text = reqData.Rows[0]["Id"].ToString();
            lblEditCustomerGroup.Text = reqData.Rows[0]["Cus_Group_Name"].ToString();
            lblEditCustomer.Text = reqData.Rows[0]["Cus_Name"].ToString();
            lblEditDepartment.Text = reqData.Rows[0]["Dept_Name"].ToString();
            lblEditConsumtionNumber.Text = reqData.Rows[0]["Consumtion_Number"].ToString();
            txtEditPONumber.Text = reqData.Rows[0]["PO_Number"].ToString();
            lblEditShipping.Text = reqData.Rows[0]["Cus_Shipping_Detail"].ToString();
            DDLEditSale.SelectedValue = reqData.Rows[0]["Sale_Code"].ToString();
            DDLEditDeliveryRoute.SelectedValue = reqData.Rows[0]["Delivery_Route_Code"].ToString();
            DDLEditWarehouse.SelectedValue = reqData.Rows[0]["Warehouse_Code"].ToString();
            txtEditRemark.Text = reqData.Rows[0]["Invoice_Remark"].ToString();
            txtEditDiscount.Text = reqData.Rows[0]["Invoice_Discount"].ToString();

            ViewState["reqData"] = reqData;

            string total = calculateTotal(reqData);
            lblEditTotalAmount.Text = total;
            lblEditVat.Text = ((Convert.ToDecimal(total) * Convert.ToDecimal(reqData.Rows[0]["Cus_Vat"])) / 100).ToString();
            lblEditTotalAmountIncVat.Text = (Convert.ToDecimal(total) + ((Convert.ToDecimal(total) * (Convert.ToDecimal(reqData.Rows[0]["Cus_Vat"]) / 100)))).ToString();

            GridView.DataSource = reqData;
            GridView.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DDLEditSale.SelectedValue != "-1" && DDLEditDeliveryRoute.SelectedValue != "-1"
                && DDLEditWarehouse.SelectedValue != "-1" && !string.IsNullOrEmpty(txtEditPONumber.Text))
            {
                if (RdInvoiceStatus.SelectedValue == "C")
                {
                    oEQUIPMENT_Service.updateInvoiceData(lblEditId.Text, txtEditPONumber.Text, DDLEditSale.SelectedValue, DDLEditSale.SelectedItem.ToString(), DDLEditDeliveryRoute.SelectedValue, DDLEditDeliveryRoute.SelectedItem.ToString(), DDLEditWarehouse.SelectedValue, DDLEditWarehouse.SelectedItem.ToString(), txtEditRemark.Text, txtEditDiscount.Text.Trim(), RdInvoiceStatus.SelectedValue, ViewState["user"].ToString());
                    updateData(RdInvoiceStatus.SelectedValue);
                    Response.Redirect("/Views/Equipment/Detail.aspx?id=" + lblEditId.Text);
                }
                else
                {
                    oEQUIPMENT_Service.updateInvoiceData(lblEditId.Text, txtEditPONumber.Text, DDLEditSale.SelectedValue, DDLEditSale.SelectedItem.ToString(), DDLEditDeliveryRoute.SelectedValue, DDLEditDeliveryRoute.SelectedItem.ToString(), DDLEditWarehouse.SelectedValue, DDLEditWarehouse.SelectedItem.ToString(), txtEditRemark.Text, txtEditDiscount.Text.Trim(), RdInvoiceStatus.SelectedValue, ViewState["user"].ToString());
                    updateData(RdInvoiceStatus.SelectedValue);
                    BindData(lblEditId.Text);
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please select all fields");
            }
        }
        protected void updateData(string status)
        {
            DataTable dtDetail = (DataTable)ViewState["reqData"];
            if (GridView.Rows.Count > 0)
            {
                int index = 0;
                foreach (GridViewRow row in GridView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label gvlblId = GridView.Rows[index].FindControl("gvlblId") as Label;
                        TextBox gvtxtItemName = GridView.Rows[index].FindControl("gvtxtItemName") as TextBox;
                        TextBox gvtxtUnitPrice = GridView.Rows[index].FindControl("gvtxtUnitPrice") as TextBox;
                        oEQUIPMENT_Service.updateInvoiceDetailData(gvlblId.Text, gvtxtItemName.Text, gvtxtUnitPrice.Text, status, ViewState["user"].ToString());
                    }
                    index++;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Equipment/Main.aspx");
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
                TextBox gvtxtItemName = (TextBox)(e.Row.FindControl("gvtxtItemName"));
                gvtxtItemName.Text = (string)DataBinder.Eval(e.Row.DataItem, "Item_Name");

                //Set Item Qty
                Label gvlblItemQty = (Label)(e.Row.FindControl("gvlblItemQty"));
                gvlblItemQty.Text = DataBinder.Eval(e.Row.DataItem, "Item_Qty").ToString();

                //Set Item Unit Price
                TextBox gvtxtUnitPrice = (TextBox)(e.Row.FindControl("gvtxtUnitPrice"));
                gvtxtUnitPrice.Text = DataBinder.Eval(e.Row.DataItem, "Item_Price").ToString();

                //Set Item UOM
                Label gvlblUOM = (Label)(e.Row.FindControl("gvlblUOM"));
                gvlblUOM.Text = DataBinder.Eval(e.Row.DataItem, "uom").ToString();

                //Set Amount
                Label gvlblAmount = (Label)(e.Row.FindControl("gvlblAmount"));
                gvlblAmount.Text = DataBinder.Eval(e.Row.DataItem, "Amount").ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            oEQUIPMENT_Service.deleteInvoiceData(lblEditId.Text);
            oEQUIPMENT_Service.deleteInvoiceDetailData(lblEditId.Text);
            Response.Redirect("/Views/Inventory/Main.aspx");
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
                    double bb = Convert.ToDouble(txtEditDiscount.Text);
                    total -= bb;
                }
            }
            return total.ToString();
        }
        protected void txtEditDiscount_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEditDiscount.Text.Trim()))
            {
                lblEditTotalAmount.Text = calculateTotal((DataTable)ViewState["reqData"]);
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data in discount");
            }
        }
    }
}