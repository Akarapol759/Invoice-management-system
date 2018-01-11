using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class REPORT_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable johnsonReportData(string stdDate, string endDate)
        {
            SQL.CommandText = "Select * from NH_JohnsonJohnsonReport where Convert(datetime,Invoice_Date,105) between Convert(datetime,'" + stdDate + "',105) and Convert(datetime,'" + endDate + "',105)";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable bBraunReportData(string stdDate, string endDate)
        {
            SQL.CommandText = "Select * from NH_BBraunReport where Convert(datetime,Invoice_Date,105) between Convert(datetime,'" + stdDate + "',105) and Convert(datetime,'" + endDate + "',105) and Item_Group_Id = '1'";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable saleReportData(string stdDate, string endDate)
        {
            SQL.CommandText = "Select * from NH_SaleReport_New where Convert(datetime,Invoice_Date,105) between Convert(datetime,'" + stdDate + "',105) and Convert(datetime,'" + endDate + "',105)";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable financeReportNewData(string stdDate, string endDate)
        {
            SQL.CommandText = "Select * from NH_FinanceReport_New where Convert(datetime,Invoice_Date,105) between Convert(datetime,'" + stdDate + "',105) and Convert(datetime,'" + endDate + "',105)";
            return this.ExecuteReader(this.SQL);
        }
    }
}