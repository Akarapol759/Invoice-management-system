using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N_Medical.Services
{
    public class CUSTOMER_DEPARTMENT_Service : Service_SQL
    {
        SqlCommand SQL = new SqlCommand();
        public DataTable getData()
        {
            SQL.CommandText = "Select Customer.Cus_Group_Code, Customer_Department.Cus_Code, Customer.Cus_Name, Customer_Department.Dept_Code, Customer_Department.Dept_Name, Customer_Department.Dept_Status, case when Customer_Department.Dept_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Customer_Department left join Customer on Customer_Department.Cus_Code = Customer.Cus_Code  Order By Dept_Name asc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataByCusCode(string cusCode)
        {
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.CommandText = "Select Customer.Cus_Group_Code, Customer_Department.Cus_Code, Customer.Cus_Name, Customer_Department.Dept_Code, Customer_Department.Dept_Name, Customer_Department.Dept_Status, case when Customer_Department.Dept_Status = 'A' then 'Active' else 'Inactive' end as Status_Name from Customer_Department left join Customer on Customer_Department.Cus_Code = Customer.Cus_Code where Customer_Department.Cus_Code = @cusCode Order By Dept_Name asc";
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataDup(string deptCode, string cusCode)
        {
            SQL.Parameters.Add("@deptCode", SqlDbType.VarChar).Value = deptCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.VarChar).Value = cusCode;
            SQL.CommandText = "Select * from Customer_Department Where Dept_Code = @deptCode and Cus_Code = @cusCode";
            return this.ExecuteReader(this.SQL);
        }
        public void insertDepartmentData(string deptCode, string cusCode, string deptName, string deptStatus, string userCreate)
        {
            SQL.Parameters.Add("@deptCode", SqlDbType.NVarChar).Value = deptCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@deptName", SqlDbType.NVarChar).Value = deptName;
            SQL.Parameters.Add("@deptStatus", SqlDbType.NVarChar).Value = deptStatus;
            SQL.Parameters.Add("@userCreate", SqlDbType.NVarChar).Value = userCreate;
            SQL.CommandText = "Insert Into Customer_Department (Dept_Code, Cus_Code, Dept_Name, Dept_Status, User_Create, User_Update, Create_Date, Update_Date) Values (@deptCode, @cusCode, @deptName, @deptStatus, @userCreate, @userCreate, GETDATE(), GETDATE())";
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateDepartmentData(string deptCode, string cusCode, string deptName, string deptStatus, string userUpdate)
        {
            SQL.Parameters.Add("@deptCode", SqlDbType.NVarChar).Value = deptCode;
            SQL.Parameters.Add("@cusCode", SqlDbType.NVarChar).Value = cusCode;
            SQL.Parameters.Add("@deptName", SqlDbType.Float).Value = deptName;
            SQL.Parameters.Add("@deptStatus", SqlDbType.NVarChar).Value = deptStatus;
            SQL.Parameters.Add("@userUpdate", SqlDbType.NVarChar).Value = userUpdate;
            SQL.CommandText = "Update Customer_Department Set Dept_Name = @deptName, Dept_Status = @deptStatus, User_Update = @userUpdate, Update_Date = GETDATE() where Dept_Code = @deptCode and Cus_Code = @cusCode";
            this.ExecuteNonQuery(this.SQL);
        }
    }
}