using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class Service_SQL
    {
        SqlCommand Com = new SqlCommand();
        SqlConnection Con = new SqlConnection();
        //string SQL;

        public Service_SQL()
        {
            this.Con = new SqlConnection();
            this.Com = new SqlCommand();
            this.Con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public DataTable ExecuteReader(SqlCommand SQL)
        {
            Com = SQL;
            DataTable table = new DataTable();
            try
            {
                this.Con.Open();
                Com.Connection = Con;
                SqlDataAdapter dAdapter = new SqlDataAdapter(SQL);
                DataSet ds = new DataSet();
                dAdapter.Fill(ds);
                table = ds.Tables[0];
                SQL.Parameters.Clear();
            }
            catch (SqlException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
                SQL.Parameters.Clear();
                return table;
            }
            this.Con.Close();
            this.Com.Dispose();
            return table;
        }
        public void ExecuteNonQuery(SqlCommand SQL)
        {
            try
            {
                this.Con.Open();
                SQL.Connection = Con;
                //this.Com.CommandType = CommandType.Text;
                //this.Com.CommandText = SQL.CommandText;
                SQL.ExecuteNonQuery();
                SQL.Parameters.Clear();
            }
            catch (SqlException exception1)
            {
                this.Con.Close();
                SQL.Dispose();
                SQL.Parameters.Clear();
            }
            this.Con.Close();
            SQL.Dispose();
        }
        public DataSet ExecuteReaderDataSet(SqlCommand SQL)
        {
            DataSet ds = new DataSet();
            try
            {
                this.Con.Open();
                Com.Connection = Con;
                SqlDataAdapter dAdapter = new SqlDataAdapter(SQL);
                dAdapter.Fill(ds);
                SQL.Parameters.Clear();
            }
            catch (SqlException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
                SQL.Parameters.Clear();
                return ds;
            }
            this.Con.Close();
            this.Com.Dispose();
            return ds;
        }
    }
}