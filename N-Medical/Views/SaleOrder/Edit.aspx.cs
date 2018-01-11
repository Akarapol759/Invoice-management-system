using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.SaleOrder
{
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        SALE_Service oSALE_Service = new SALE_Service();
        SALE_ORDER_Service oSALE_ORDER_Service = new SALE_ORDER_Service();
        COSTCENTER_Service oCOSTCENTER_Service = new COSTCENTER_Service();
        VENDER_Service oVENDER_Service = new VENDER_Service();
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
            DataTable reqData = oSALE_ORDER_Service.getDataById(id);

            DataTable dtCustomerGroup = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditCustomerGroup, dtCustomerGroup, "Cus_Group_Name", "Cus_Group_Code", "Please select Customer Group");
            DataTable dtCustomer = oUtility.getActive(oCUSTOMER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditCustomer, dtCustomer, "Cus_Name", "Cus_Code", "Please select Customer");
            DataTable dtSale = oUtility.getActive(oSALE_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditSale, dtSale, "Sale_Name", "Sale_Code", "Please select Sale");
            DataTable dtCostCenter = oUtility.getActive(oCOSTCENTER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditCostCenter, dtCostCenter, "COST_CENTER_NAME", "COST_CENTER_CODE", "Please select Sale");
            DataTable dtVender = oUtility.getActive(oVENDER_Service.getData(), "Status_Name", "Active");
            oUtility.DDL(DDLEditVender, dtVender, "VENDER_FULL_NAME", "VENDER_CODE", "Please select Customer");

            if (reqData.Rows[0]["SALE_ORDER_TYPE"].ToString() == "MS")
            {
                DDLEditVender.Enabled = false;
                //txtEditSaleOrderAmount.Enabled = false;
                //DDLEditCostCenter.Enabled = false;
            }
            else
            {
                DDLEditVender.Enabled = true;
                //txtEditSaleOrderAmount.Enabled = true;
                //DDLEditCostCenter.Enabled = true;
            }

            lblEditId.Text = reqData.Rows[0]["SALE_ORDER_ID"].ToString();
            DDLEditCustomerGroup.SelectedValue = reqData.Rows[0]["Cus_Group_Code"].ToString();
            DDLEditCustomer.SelectedValue = reqData.Rows[0]["Cus_Code"].ToString();
            DDLEditSaleOrderType.SelectedValue = reqData.Rows[0]["SALE_ORDER_TYPE"].ToString();
            DDLEditVender.SelectedValue = reqData.Rows[0]["VENDER_CODE"].ToString();
            lblEditSaleOrderNumber.Text = reqData.Rows[0]["SALE_ORDER_Number"].ToString();
            txtEditPONumber.Text = reqData.Rows[0]["PO_Number"].ToString();

            txtEditSaleOrderShippingDate.Text = reqData.Rows[0]["SALE_ORDER_Shipping_Date"].ToString();

            txtEditSaleOrderAmount.Text = reqData.Rows[0]["SALE_ORDER_Amount"].ToString();
            DDLEditSale.SelectedValue = reqData.Rows[0]["Sale_Code"].ToString();
            DDLEditCostCenter.SelectedValue = reqData.Rows[0]["Cost_Center"].ToString();
            txtEditRemark.Text = reqData.Rows[0]["SALE_ORDER_Remark"].ToString();

            ViewState["reqDataDetail"] = oSALE_ORDER_Service.getDataDetail(id);
            GridView.DataSource = (DataTable)ViewState["reqDataDetail"];
            GridView.DataBind();

            ViewState["reqDataComment"] = oSALE_ORDER_Service.getDataCommentById(id);
            GridViewComment.DataSource = (DataTable)ViewState["reqDataComment"];
            GridViewComment.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DDLEditSaleOrderType.SelectedValue == "MS")
            {
                if (DDLEditCustomerGroup.SelectedValue != "-1" && DDLEditCustomer.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtEditPONumber.Text.Trim()) && !string.IsNullOrEmpty(txtEditSaleOrderAmount.Text.Trim())
                    && DDLEditSale.SelectedValue != "-1" && DDLEditCostCenter.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtEditSaleOrderShippingDate.Text))
                {
                    //check po number
                    DataTable purchase = oSALE_ORDER_Service.checkPODuplicate(txtEditPONumber.Text.Trim(), lblEditSaleOrderNumber.Text, DDLEditCustomer.SelectedValue);
                    if (purchase.Rows.Count > 0)
                    {
                        oUtility.MsgAlert(this, "Duplicate Purchase Order Number");
                    }
                    else
                    {
                        DateTime date = DateTime.ParseExact(txtEditSaleOrderShippingDate.Text, "dd/MM/yyyy", null);
                        string dateConvert = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        if (RdSaleOrderStatus.SelectedValue == "C")
                        {
                            oSALE_ORDER_Service.updateSaleOrderData(lblEditId.Text, DDLEditCustomerGroup.SelectedValue, DDLEditCustomerGroup.SelectedItem.Text, DDLEditCustomer.SelectedValue, DDLEditCustomer.SelectedItem.Text, DDLEditSaleOrderType.SelectedValue, "", "", txtEditPONumber.Text, dateConvert, txtEditSaleOrderAmount.Text, DDLEditSale.SelectedValue, DDLEditSale.SelectedItem.ToString(), DDLEditCostCenter.SelectedValue, txtEditRemark.Text, RdSaleOrderStatus.SelectedValue, ViewState["user"].ToString());
                            Response.Redirect("/Views/SaleOrder/Detail.aspx?id=" + lblEditId.Text);
                        }
                        else
                        {
                            oSALE_ORDER_Service.updateSaleOrderData(lblEditId.Text, DDLEditCustomerGroup.SelectedValue, DDLEditCustomerGroup.SelectedItem.Text, DDLEditCustomer.SelectedValue, DDLEditCustomer.SelectedItem.Text, DDLEditSaleOrderType.SelectedValue, "", "", txtEditPONumber.Text, dateConvert, txtEditSaleOrderAmount.Text, DDLEditSale.SelectedValue, DDLEditSale.SelectedItem.ToString(), DDLEditCostCenter.SelectedValue, txtEditRemark.Text, RdSaleOrderStatus.SelectedValue, ViewState["user"].ToString());
                            BindData(lblEditId.Text);
                        }
                    }
                }
            }
            else
            {
                if (DDLEditCustomerGroup.SelectedValue != "-1" && DDLEditCustomer.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtEditPONumber.Text.Trim()) && !string.IsNullOrEmpty(txtEditSaleOrderAmount.Text.Trim())
                    && DDLEditSale.SelectedValue != "-1" && DDLEditCostCenter.SelectedValue != "-1" && DDLEditVender.SelectedValue != "-1"
                    && !string.IsNullOrEmpty(txtEditSaleOrderShippingDate.Text))
                {
                    //check po number
                    DataTable purchase = oSALE_ORDER_Service.checkPODuplicate(txtEditPONumber.Text.Trim(), lblEditSaleOrderNumber.Text, DDLEditCustomer.SelectedValue);
                    if (purchase.Rows.Count > 0)
                    {
                        oUtility.MsgAlert(this, "Duplicate Purchase Order Number");
                    }
                    else
                    {
                        DateTime date = DateTime.ParseExact(txtEditSaleOrderShippingDate.Text, "dd/MM/yyyy", null);
                        string dateConvert = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        if (RdSaleOrderStatus.SelectedValue == "C")
                        {
                            oSALE_ORDER_Service.updateSaleOrderData(lblEditId.Text, DDLEditCustomerGroup.SelectedValue, DDLEditCustomerGroup.SelectedItem.Text, DDLEditCustomer.SelectedValue, DDLEditCustomer.SelectedItem.Text, DDLEditSaleOrderType.SelectedValue, DDLEditVender.SelectedValue, DDLEditVender.SelectedItem.ToString(), txtEditPONumber.Text, dateConvert, txtEditSaleOrderAmount.Text, DDLEditSale.SelectedValue, DDLEditSale.SelectedItem.ToString(), DDLEditCostCenter.SelectedValue, txtEditRemark.Text, RdSaleOrderStatus.SelectedValue, ViewState["user"].ToString());
                            Response.Redirect("/Views/SaleOrder/Detail.aspx?id=" + lblEditId.Text);
                        }
                        else
                        {
                            oSALE_ORDER_Service.updateSaleOrderData(lblEditId.Text, DDLEditCustomerGroup.SelectedValue, DDLEditCustomerGroup.SelectedItem.Text, DDLEditCustomer.SelectedValue, DDLEditCustomer.SelectedItem.Text, DDLEditSaleOrderType.SelectedValue, DDLEditVender.SelectedValue, DDLEditVender.SelectedItem.ToString(), txtEditPONumber.Text, dateConvert, txtEditSaleOrderAmount.Text, DDLEditSale.SelectedValue, DDLEditSale.SelectedItem.ToString(), DDLEditCostCenter.SelectedValue, txtEditRemark.Text, RdSaleOrderStatus.SelectedValue, ViewState["user"].ToString());
                            BindData(lblEditId.Text);
                        }
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Please select all fields");
                }
            }
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

                //Set Button Download
                Button btnLinkDownload = (Button)(e.Row.FindControl("btnLinkDownload"));
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "SALE_ORDER_FILE_NAME").ToString()))
                {
                    btnLinkDownload.Visible = true;
                }
                else
                {
                    btnLinkDownload.Visible = false;
                }
            }
        }
        //protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow row = GridView.Rows[e.RowIndex] as GridViewRow;
        //    Label gvlblId = (Label)GridView.Rows[e.RowIndex].FindControl("gvlblId");
        //    FileUpload gvfupload = (FileUpload)GridView.Rows[e.RowIndex].FindControl("gvfupload");
        //    string path = null;
        //    if (gvfupload != null && gvfupload.HasFile)
        //    {
        //        path = Path.GetFileName(gvfupload.PostedFile.FileName);
        //    }
        //    Byte[] bytes = File.ReadAllBytes(path);
        //    String file = Convert.ToBase64String(bytes);
        //    oSALE_ORDER_Service.updateSaleOrderDetailData(gvlblId.Text, file, ViewState["user"].ToString());
        //}
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (fuAttach.HasFile)
            {
                string id = oSALE_ORDER_Service.insertSaleOrderDetailData(lblEditId.Text, DDLAddType.SelectedValue.ToString(), DDLAddType.SelectedValue + "_" + lblEditSaleOrderNumber.Text + Path.GetExtension(fuAttach.FileName.ToString()), ViewState["user"].ToString());
                fuAttach.SaveAs(Server.MapPath("~/Uploads/" + id + "_" + DDLAddType.SelectedValue + "_" + lblEditSaleOrderNumber.Text + Path.GetExtension(fuAttach.FileName.ToString())));
                //fuAttach.SaveAs(Server.MapPath("~/Uploads/" + DDLAddType.SelectedValue.ToString() + Path.GetExtension(fuAttach.FileName.ToString())));
                //oSALE_ORDER_Service.updateSaleOrderDetailData(DDLAddType.SelectedValue.ToString(), Path.GetExtension(fuAttach.FileName.ToString()), ViewState["user"].ToString());
                BindData(lblEditId.Text);
            }
            else
            {
                oUtility.MsgAlert(this, "Please select file");
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gridViewRow.RowIndex;
            string id = GridView.DataKeys[index].Value.ToString();

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
        protected void btnLinkDownload_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gridViewRow.RowIndex;
            //lblsaleOrderDetailId.Text = GridView.DataKeys[index].Value.ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#downloadModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
        }
        protected void DDLEditSaleOrderType_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oVENDER_Service.getData().AsEnumerable()
                                         where i.Field<string>("VENDER_GROUP_CODE").Equals(DDLEditSaleOrderType.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLEditVender, result, "VENDER_FULL_NAME", "VENDER_CODE", "Please select Customer");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
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

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtComment.Text))
            {
                oSALE_ORDER_Service.insertSaleOrderComment(lblEditId.Text, txtComment.Text, ViewState["user"].ToString());
                BindData(lblEditId.Text);
            }
            else
            {
                oUtility.MsgAlert(this, "Please insert comment");
            }
        }

        protected void DDLEditCustomerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Group_Code").Equals(DDLEditCustomerGroup.SelectedValue)
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                oUtility.DDL(DDLEditCustomer, result, "Cus_Name", "Cus_Code", "Please select Customer");
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }

        protected void txtEditCustomerCode_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData().AsEnumerable()
                                         where i.Field<string>("Cus_Code").Equals(txtEditCustomerCode.Text.Trim())
                                         && i.Field<string>("Status_Name").Equals("Active")
                                         select i;
            if (query.Any())
            {
                DataTable result = query.CopyToDataTable<DataRow>();
                DDLEditCustomerGroup.SelectedValue = result.Rows[0]["Cus_Group_Code"].ToString();
                DDLEditCustomer.SelectedValue = txtEditCustomerCode.Text.Trim();
            }
            else
            {
                oUtility.MsgAlert(this, "Not found data");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            oSALE_ORDER_Service.cancelSaleOrder(lblEditId.Text, "N", ViewState["user"].ToString());
            Response.Redirect("/Views/SaleOrder/Cancel.aspx?id=" + lblEditId.Text);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gridViewRow.RowIndex;
            string id = GridView.DataKeys[index].Value.ToString();
            oSALE_ORDER_Service.deleteSaleOrderDetail(id);
            BindData(lblEditId.Text);
        }
    }
}