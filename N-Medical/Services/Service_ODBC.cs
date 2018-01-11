using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class Service_ODBC
    {
        OdbcCommand Com = new OdbcCommand();
        OdbcConnection Con = new OdbcConnection();
        string SQL;

        public Service_ODBC()
        {
            this.Con = new OdbcConnection();
            this.Com = new OdbcCommand();
            this.Con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringMedTrak"].ConnectionString;
        }
        public void ExecuteNonQuery(string SQL)
        {
            try
            {
                this.Con.Open();
                this.Com.Connection = this.Con;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = SQL;
                this.Com.ExecuteNonQuery();
            }
            catch (OdbcException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
            }
            this.Con.Close();
            this.Com.Dispose();
        }

        public DataTable ExecuteReader(string SQL)
        {
            DataTable table = new DataTable();
            try
            {
                this.Con.Open();
                OdbcDataAdapter dAdapter = new OdbcDataAdapter(SQL, Con);
                DataSet ds = new DataSet();
                dAdapter.Fill(ds);
                table = ds.Tables[0];
            }
            catch (OdbcException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
                return table;
            }
            this.Con.Close();
            this.Com.Dispose();
            return table;
        }
        public DataTable consumtionData(string consumtionNo)
        {
            this.SQL = string.Concat(new string[] { "SELECT DEP_Code as Cost_Center, INDS_Date, INDS_Time, INDS_No, INCI_Code as Item_Code, INCI_Desc, CTLOC_Code, CTLOC_Desc, INDSI_Qty as Item_Qty, INDSI_UCost as Item_Cost, CTUOM_Code, CTUOM_Desc as UOM, INCIB_No as Batch, INCIB_ExpDate as Expire_Date From VS_StockDisp Where INDS_No = '" + consumtionNo + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable adjustData(string adjustNo)
        {
            this.SQL = string.Concat(new string[] { "SELECT INAD_Date as AdjDate, INAD_No as AdjNo, INAD_RowId, INAD_Time as AdjTime, ADJ_Desc AdjDesc, SSUSR_Name as UserAdj, INADI_Qty as AdjQty, INADI_UCost as ADJUnitCost, CTUOM_Desc as ADJUOM, CTLOC_Desc as AdjLoc, INCI_Code as StockItemCode, INCI_Desc StockItemDesc, INCIB_No as Batch, INCIB_ExpDate as ExpDate, ADJDEP_CODE FROM VS_StockAdjust Where AdjNo = '" + adjustNo + "'" });
            return this.ExecuteReader(this.SQL);
        }
    }
}