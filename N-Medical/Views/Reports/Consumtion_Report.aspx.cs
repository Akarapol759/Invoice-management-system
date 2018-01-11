using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Reporting.WebForms;
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

namespace N_Medical.Views.Reports
{
    public partial class Consumtion_Report : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        REPORT_Service oREPORT_Service = new REPORT_Service();

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
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("~/Reports/NHSStockConsumptionDTL_NMED.rpt"));
            rpt.SetParameterValue("DateFrom", txtSearchStdDate.Text);
            rpt.SetParameterValue("DateTo", txtSearchEndDate.Text);
            rpt.SetParameterValue("Stocklocation", "N Health House-brand Inventory");
            rpt.SetDatabaseLogon("system", "sys");
            //BinaryReader stream = new BinaryReader(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel));
            Response.ClearContent();
            Response.ClearHeaders();
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "StockConsumtionReport");
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "inline; filename=Consumtion_Report");
            //Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            Response.Flush();
            Response.End(); 
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchStdDate.Text = "";
            txtSearchEndDate.Text = "";
        }
    }
}