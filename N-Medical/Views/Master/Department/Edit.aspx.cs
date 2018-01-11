using N_Medical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Medical.Views.Master.Department
{
    public partial class Edit : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_DEPARTMENT_Service oCUSTOMER_DEPARTMENT_Service = new CUSTOMER_DEPARTMENT_Service();
        RUNNING_Service oRUNNING_Service = new RUNNING_Service();
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
            //DataTable dtItem = oUtility.getActive(oITEM_Service.getData("I"), "Status_Name", "Active");
            //oUtility.DDL(DDLAddItem, dtItem, "Item_Name", "Item_Code", "Please select Item");

            DataTable dt = oCUSTOMER_Service.getDataByCusCode(cusCode);
            lblEditCustomerGroup.Text = dt.Rows[0]["Cus_Group_Name"].ToString();
            lblEditCustomerCode.Text = dt.Rows[0]["Cus_Code"].ToString();
            lblEditCustomerName.Text = dt.Rows[0]["Cus_Name"].ToString();

            if (dt.Rows[0]["Cus_Group_Code"].ToString() == "BDMS" || dt.Rows[0]["Cus_Group_Code"].ToString() == "NH")
            {
                txtAddDepartmentCode.Enabled = true;
            }
            else
            {
                txtAddDepartmentCode.Enabled = false;
            }

            ViewState["dtDepartment"] = oCUSTOMER_DEPARTMENT_Service.getDataByCusCode(cusCode);
            GridView.DataSource = (DataTable)ViewState["dtDepartment"];
            GridView.DataBind();
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Master/Department/Main.aspx");
        }

        protected void btnAddDepartment_Click(object sender, EventArgs e)
        {
            if (lblEditCustomerGroup.Text == "BDMS" || lblEditCustomerGroup.Text == "NH")
            {
                if (!string.IsNullOrEmpty(txtAddDepartmentCode.Text) && !string.IsNullOrEmpty(txtAddDepartmentName.Text))
                {
                    DataTable dt = oCUSTOMER_DEPARTMENT_Service.getDataDup(txtAddDepartmentCode.Text, lblEditCustomerCode.Text);
                    if (dt.Rows.Count > 0)
                    {
                        oUtility.MsgAlert(this, "This item had price");
                    }
                    else
                    {
                        oCUSTOMER_DEPARTMENT_Service.insertDepartmentData(txtAddDepartmentCode.Text, lblEditCustomerCode.Text, txtAddDepartmentName.Text, "A", ViewState["user"].ToString());
                        oUtility.MsgAlert(this, "Success");
                        BindData(lblEditCustomerCode.Text);
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Please put data all field");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtAddDepartmentName.Text))
                {
                    string deptCode = oRUNNING_Service.generateCustomerDepartmentNumber();
                    oCUSTOMER_DEPARTMENT_Service.insertDepartmentData(deptCode, lblEditCustomerCode.Text, txtAddDepartmentName.Text, "A", ViewState["user"].ToString());
                    oUtility.MsgAlert(this, "Success");
                    BindData(lblEditCustomerCode.Text);
                }
                else
                {
                    oUtility.MsgAlert(this, "Please put department name");
                }
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRow"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                IEnumerable<DataRow> query = from i in ((DataTable)ViewState["dtDepartment"]).AsEnumerable()
                                             where i.Field<String>("Dept_Code").Equals(GridView.DataKeys[index].Value.ToString())
                                             && i.Field<String>("Cus_Code").Equals(lblEditCustomerCode.Text)
                                             select i;
                if (query.Any())
                {
                    DataTable price = query.CopyToDataTable<DataRow>();
                    lblEditDepartmentCode.Text = price.Rows[0]["Dept_Code"].ToString();
                    txtEditDepartmentName.Text = price.Rows[0]["Dept_Name"].ToString();
                    RdStatus.SelectedValue = price.Rows[0]["Dept_Status"].ToString();
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
            GridView.DataSource = (DataTable)ViewState["dtDepartment"];
            GridView.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEditDepartmentName.Text))
            {
                oCUSTOMER_DEPARTMENT_Service.updateDepartmentData(lblEditDepartmentCode.Text, lblEditCustomerCode.Text, txtEditDepartmentName.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                BindData(lblEditCustomerCode.Text);
                oUtility.MsgAlert(this, "Success");
            }
            else
            {
                oUtility.MsgAlert(this, "Please insert department name");
            }
        }
    }
}