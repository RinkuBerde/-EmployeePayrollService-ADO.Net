using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService_ADO.Net
{
    //UC-08-Transaction Query
    class TransactionClass
    {
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Payroll;Trusted_Connection=True;";
        SqlConnection SqlConnection = new SqlConnection(connectionString);
        //Transaction Query
        public int InsertIntoTables()
        {
            int result = 0;
            using (SqlConnection)
            {
                SqlConnection.Open();

                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Insert data into Table
                    sqlCommand.CommandText = "Insert into Employee values ('2','Radha Mani','9600035350', 'Chennai', '2017-12-17', 'F')";
                    sqlCommand.CommandText = "Insert into PayrollCalculate(EmployeeIdentity,BasicPay) values('5','650000')";
                    sqlCommand.CommandText = "update PayrollCalculate set Deductions = (BasicPay *20)/100 where EmployeeIdentity = '5'";
                    sqlCommand.CommandText = "update PayrollCalculate set TaxablePay = (BasicPay - Deductions) where EmployeeIdentity = '5'";
                    sqlCommand.CommandText = "update PayrollCalculate set IncomeTax = (TaxablePay * 10) / 100 where EmployeeIdentity = '5'";
                    sqlCommand.CommandText = "update PayrollCalculate set NetPay = (BasicPay - IncomeTax) where EmployeeIdentity = '5'";
                    sqlCommand.CommandText = "Insert into EmployeeDepartment values('3','5')";

                    //Commit 
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                    result = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    result = 1;
                }
            }
            return result;
        }

        //UC-8.1-Refector-Cascading Delete
        public int DeleteUsingCasadeDelete()
        {
            int result = 0;
            using (SqlConnection)
            {
                SqlConnection.Open();
                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "delete from employee where EmployeeID='4'";
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            return result;
        }
    }
}
