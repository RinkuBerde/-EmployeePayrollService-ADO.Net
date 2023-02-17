using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService_ADO.Net
{
    public class EmployeeRepo
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PayrollService;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //employeeModel.EmployeeID = dr.GetInt32(0);

                            employeeModel.EmployeeName = dr.GetString(0);
                            employeeModel.BasicPay = dr.GetDecimal(1);
                            employeeModel.StartDate = dr.GetDateTime(2);
                            employeeModel.Gender = Convert.ToChar(dr.GetString(3));
                            //employeeModel.PhoneNumber = dr.GetString(4);
                            employeeModel.Address = dr.GetString(5);
                            //employeeModel.Department = dr.GetString(6);
                            //employeeModel.Deductions = dr.GetDouble(7);
                            //employeeModel.TaxablePay = dr.GetDouble(8);
                            //employeeModel.Tax = dr.GetDouble(9);
                            //employeeModel.NetPay = dr.GetDouble(10);
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        //UC_02-Insert record 
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    //var qury=values()
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@Country", model.Country);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }

        //UseCase 4: Update Salary to 3000000
        public void UpdateSalaryQuery()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)

                {
                    //sqlConnection.Open();
                    string query = "update employee_payroll set BasicPay=3000000 where EmployeeName= 'Ashaya Sivakumar'";
                    this.connection.Open();
                    //Pass query to TSql
                    SqlCommand sqlCommand = new SqlCommand(query, this.connection);
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated!");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated!");
                    }
                    //Close Connection
                    this.connection.Close();
                    GetAllEmployee();
                }
            }
        }

        //Usecase 5: Update basic pay in Sql Server using Stored Procedure
        public int UpdateSalary(EmployeeModel employeeDataModel)
        {
            int result = 0;
            try
            {
                using (this.connection)
                {
                    //Give stored Procedure
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUpdateSalary", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@salary", employeeDataModel.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@EmpName", employeeDataModel.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@EmpId", employeeDataModel.EmployeeID);
                    //Open Connection
                    connection.Open();
                    //Return Number of Rows affected
                    result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        //Usecase 6: Finds the employees in a given range from start date to current
        public string DataBasedOnDateRange()
        {
            string nameList = "";
            try
            {
                using (this.connection)
                {
                    //query execution
                    string query = @"select * from employee_payroll where StartDate BETWEEN Cast('2019-12-13' as Date) and GetDate();";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    //open sql connection
                    connection.Open();

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            GetAllEmployee(sqlDataReader);
                            nameList += sqlDataReader["EmployeeName"].ToString() + " ";
                        }
                    }
                    //close reader
                    sqlDataReader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }
            //returns the count of employee in the list between the given range
            return nameList;

        }
    }

    //Usecase 7: Aggregate Function
    public string AggregateFunctionBasedOnGender(string query)
    {
        string nameList = "";
        try
        {
            using (this.connection)
            {
                ////query execution
                SqlCommand command = new SqlCommand(query, this.connection);
                //open sql connection
                connection.Open();

                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine("TotalSalary: {0} \t MinimumSalary: {1} \t MaximumSalary: {2}AverageSalary: {3} \t Count: {4}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2], sqlDataReader[3], sqlDataReader[4]);
                        nameList += sqlDataReader[0] + " " + sqlDataReader[1] + " " + sqlDataReader[2] + " " + sqlDataReader[3] + " " + sqlDataReader[4];
                    }
                }
                //close reader
                sqlDataReader.Close();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {

           connection.Close();
        }
        //returns the count of employee in the list between the given range
        return nameList;

    }
}

