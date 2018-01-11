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

namespace N_Medical.Views.Reports
{
    public partial class Compare_Consumtion : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        REPORT_Service oREPORT_Service = new REPORT_Service();
        //Variable
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnSearch.UniqueID;
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
                        }
                        else
                        {
                            ViewState["bu"] = ticket.UserData.ToString();
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchStdDate.Text) && !string.IsNullOrEmpty(txtSearchEndDate.Text))
                {
                    DataTable result = new DataTable();
                    DateTime stdDate = DateTime.ParseExact(txtSearchStdDate.Text, "dd/MM/yyyy", null);
                    DateTime endDate = DateTime.ParseExact(txtSearchEndDate.Text, "dd/MM/yyyy", null);
                    if (stdDate <= endDate)
                    {
                        result = oREPORT_Service.bBraunReportData(stdDate.ToString("dd/MM/yyyy"), endDate.ToString("dd/MM/yyyy"));
                        if (result.Rows.Count > 0)
                        {

                            ReportViewer1.Reset();
                            ReportViewer1.Visible = true;
                            ReportDataSource dt = new ReportDataSource("DataSet1", result);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(dt);
                            ReportViewer1.LocalReport.ReportPath = @"Reports\compareNmedMedtrak.rpt";
                            byte[] bytes = ReportViewer1.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                            Response.Buffer = true;
                            Response.ClearHeaders();
                            Response.Clear();
                            Response.ContentType = mimeType;
                            Response.AddHeader("content-disposition", "inline; filename= BBraunReport." + extension);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                        }
                        else
                        {
                            oUtility.MsgAlert(this, "Not found data");
                        }
                    }
                    else
                    {
                        oUtility.MsgAlert(this, "Please selected start date less than end date");
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Please selected all criteria");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchStdDate.Text = "";
            txtSearchEndDate.Text = "";
        }
    }
}