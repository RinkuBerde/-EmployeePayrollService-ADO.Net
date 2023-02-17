using EmployeePayrollService_ADO.Net;

namespace PayrollADOMSTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //UC-02-Insert record into table
        public void TestMethod1()
        {
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
            employee.EmployeeName = "Raj";
            employee.Department = "Tech1";
            employee.PhoneNumber = "6302907678";
            employee.Address = "02-Patna";
            employee.Gender = 'M';
            employee.City = "Chennai";
            employee.BasicPay = 10000.00M;
            employee.Deductions = 1500.00;
            employee.StartDate = Convert.ToDateTime("2020-11-03");
            employee.Country = "India";
            //Mock<EmployeeRepo> mockObj = new Mock<EmployeeRepo>();
            //mockObj.Setup(t=>t.AddEmployee(It.IsIn<EmployeeModel>)).return (true);
            var result = repo.AddEmployee(employee);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetAllEmployeeShouldReturnListOfRecords()
        {
            EmployeeRepo repo = new EmployeeRepo();
            var result = repo.GetAllEmployee();
            Assert.IsTrue(result);
        }

        //Usecase 5: Update basic pay in Sql Server using Stored Procedure
        [TestMethod]
        [TestCategory("Using Stored Procedure")]
        public void GivenUpdateQuery_UsingStoredProcedure_ReturnOne()
        {
            EmployeeModel employeeDataManager = new EmployeeModel();
            int expected = 1;
            employeeDataManager.EmployeeID = 1;
            employeeDataManager.EmployeeName = "Bill";
            employeeDataManager.BasicPay = 30000000;
            int actual = employeeRepository.UpdateSalary(employeeDataModel);
            Assert.AreEqual(actual, expected);
        }
    }
    //Usecase 7: Aggregate Functions
    [TestMethod]
    [TestCategory("Using SQL Query for Female")]
    public void GivenGenderFemale_GroupBygender_ReturnAggregateFunction()
    {
        EmployeeModel employeeDataModel = new EmployeeModel();
        string expected = "3069000 19000 3000000 1023000 3";
        string query = "select sum(BasicPay) as TotalSalary,min(BasicPay) as MinimumSalary,max(BasicPay) as MaximumSalary,Round(avg(BasicPay), 0) as AverageSalary,Count(BasicPay) as Count from employee_payroll where Gender = 'F' group by Gender";
        string actual = EmployeeRepo.AggregateFunctionBasedOnGender(query);
        Assert.AreEqual(actual, expected);
    }
    [TestMethod]
    [TestCategory("Using SQL Query for Male")]
    public void GivenGenderMale_GroupBygender_ReturnAggregateFunction()
    {
        EmployeeModel employeeDataManager = new EmployeeModel();
        string expected = "30250000 250000 30000000 15125000 2";
        string query = "select sum(BasicPay) as TotalSalary,min(BasicPay) as MinimumSalary,max(BasicPay) as MaximumSalary,Round(avg(BasicPay), 0) as AverageSalary,Count(BasicPay) as Count from employee_payroll where Gender = 'M' group by Gender";
        string actual = EmployeeRepo.AggregateFunctionBasedOnGender(query);
        Assert.AreEqual(actual, expected);
    }
}