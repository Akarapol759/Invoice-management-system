using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class RUNNING_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public string generateEquipmentNumber()
        {
            string equipmentNo = "1";
            DataTable result = runningData();
            IEnumerable<DataRow> query = from i in result.AsEnumerable()
                                         where i.Field<String>("Running_Name").Equals("Equipment")
                                         && i.Field<String>("Running_Year").Equals(DateTime.Now.Year.ToString())
                                         select i;
            if (query.Any())
            {
                query = from i in query.AsEnumerable()
                        where i.Field<String>("Running_Month").Equals(DateTime.Now.Month.ToString().PadLeft(2, '0'))
                        select i;
                if (query.Any())
                {
                    DataTable equipment = query.CopyToDataTable<DataRow>();
                    equipmentNo = "EQP" + equipment.Rows[0]["Running_Year"].ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + equipment.Rows[0]["Running_No"].ToString().PadLeft(5, '0');
                    updateRunningData((Convert.ToInt32(equipment.Rows[0]["Running_No"]) + 1).ToString(), "Equipment", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString().PadLeft(2, '0'));
                }
                else
                {
                    insertEquipmentRunning();
                    equipmentNo = "EQP" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + equipmentNo.PadLeft(5, '0');
                }
            }
            else
            {
                insertEquipmentRunning();
                equipmentNo = "EQP" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + equipmentNo.PadLeft(5, '0');
            }
            return equipmentNo;
        }
        public string generateCustomerNumber()
        {
            string customerCode = "1";
            DataTable result = runningData();
            IEnumerable<DataRow> query = from i in result.AsEnumerable()
                                         where i.Field<String>("Running_Name").Equals("Customer")
                                         select i;
            if (query.Any())
            {
                DataTable customer = query.CopyToDataTable<DataRow>();
                customerCode = "10B" + customer.Rows[0]["Running_No"].ToString().PadLeft(4, '0');
                updateRunningData((Convert.ToInt32(customer.Rows[0]["Running_No"]) + 1).ToString(), "Customer", "", "");
            }
            return customerCode;
        }
        protected DataTable runningData()
        {
            SQL.CommandText = "Select * from Running";
            return this.ExecuteReader(this.SQL);
        }
        protected void updateRunningData(string newInvoiceNo, string runningName, string year, string month)
        {
            SQL.Parameters.Add("@newInvoiceNo", SqlDbType.BigInt).Value = newInvoiceNo;
            SQL.Parameters.Add("@runningName", SqlDbType.NVarChar).Value = runningName;
            SQL.Parameters.Add("@year", SqlDbType.NVarChar).Value = year;
            SQL.Parameters.Add("@month", SqlDbType.NVarChar).Value = month;
            SQL.CommandText = "Update Running set Running_No = @newInvoiceNo where Running_Name = @runningName and Running_Year = @year and  Running_Month = @month";
            this.ExecuteNonQuery(this.SQL);
        }
        protected void insertEquipmentRunning()
        {
            SQL.CommandText = "Insert Into Running (Running_Name, Running_No, Running_Year, Running_Month) values('Equipment', '2', '" + DateTime.Now.Year.ToString() + "', '" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "')";
            this.ExecuteNonQuery(this.SQL);
        }
        public string generateCustomerDepartmentNumber()
        {
            string customerDeptCode = "1";
            DataTable result = runningData();
            IEnumerable<DataRow> query = from i in result.AsEnumerable()
                                         where i.Field<String>("Running_Name").Equals("CustomerDepartment")
                                         select i;
            if (query.Any())
            {
                DataTable customerDepartment = query.CopyToDataTable<DataRow>();
                customerDeptCode = "dept" + customerDepartment.Rows[0]["Running_No"].ToString().PadLeft(4, '0');
                updateRunningData((Convert.ToInt32(customerDepartment.Rows[0]["Running_No"]) + 1).ToString(), "CustomerDepartment", "", "");
            }
            return customerDeptCode;
        }
        public string generateCustomerShippingNumber()
        {
            string customerShippingCode = "1";
            DataTable result = runningData();
            IEnumerable<DataRow> query = from i in result.AsEnumerable()
                                         where i.Field<String>("Running_Name").Equals("CustomerShipping")
                                         select i;
            if (query.Any())
            {
                DataTable customerShipping = query.CopyToDataTable<DataRow>();
                customerShippingCode = "SHP" + customerShipping.Rows[0]["Running_No"].ToString().PadLeft(4, '0');
                updateRunningData((Convert.ToInt32(customerShipping.Rows[0]["Running_No"]) + 1).ToString(), "CustomerShipping", "", "");
            }
            return customerShippingCode;
        }
    }
}