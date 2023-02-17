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
}