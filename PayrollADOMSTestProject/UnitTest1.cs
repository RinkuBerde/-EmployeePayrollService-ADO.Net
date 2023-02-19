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

    //Usecase 8: Insert using Transaction
    [TestMethod]
    [TestCategory("Using Transaction Query")]
    public void GivenInsertQuery_usingTransaction_returnOne()
    {
        int expected = 1;
        TransactionClass transactionClass = new TransactionClass();
        int actual = transactionClass.InsertIntoTables();
        Assert.AreEqual(actual, expected);
    }

    //Usecase 8.1: Delete using Cascade Delete alteration
    [TestMethod]
    [TestCategory("Using Transaction Query")]
    public void GivenDeleteQuery_usingTransaction_returnOne()
    {
        int expected = 1;
        TransactionClass transactionClass = new TransactionClass();
        int actual = transactionClass.DeleteUsingCasadeDelete();
        Assert.AreEqual(actual, expected);
    }


    //-----------Usecase 9 and 10: Implement ER Diagram -----------

    //Usecase2: Insert record 
    [TestMethod]
    [TestCategory("Using ER Table Implementation")]
    public void GivenSelectQuery_ReturnCount()
    {
        int expected = 5;
        int actual = eRRepository.RetrieveAllData();
        Assert.AreEqual(actual, expected);
    }
    // //UseCase 4: Update Salary to 3000000
    [TestMethod]
    [TestCategory("Using ER Table Implementation")]
    public void GivenUpdateQuery_ERTable_ReturnOne()
    {
        int expected = 1;
        int actual = eRRepository.UpdateSalaryQuery();
        Assert.AreEqual(actual, expected);
    }
    //Usecase 5: Update basic pay in Sql Server using Stored Procedure
    [TestMethod]
    [TestCategory("Using ER Table Implementation")]
    public void GivenUpdateQuery_ERTable_UsingStoredProcedure_ReturnOne()
    {
        EmployeeDataManager employeeDataManager = new EmployeeDataManager();
        int expected = 1;
        employeeDataManager.EmployeeName = "Nandeeshwar";
        employeeDataManager.BasicPay = 30000000;
        int actual = eRRepository.UpdateSalary(employeeDataManager);
        Assert.AreEqual(actual, expected);
    }
    //Usecase 6: Finds the employees in a given range from start date to current
    [TestMethod]
    [TestCategory("Using ER Table Implementation")]
    public void GivenStartDate_ERTable_UsingStoredProcedure_ReturnStringodName()
    {
        EmployeeDataManager employeeDataManager = new EmployeeDataManager();
        string expected = "Kriti Deshmuk Nandeeshwar ";
        string actual = eRRepository.DataBasedOnDateRange();
        Assert.AreEqual(actual, expected);
    }
    [TestMethod]
    [TestCategory("Using SQL Query for Female")]
    public void GivenGenderFemale_ERTable_GroupBygender_ReturnAggregateFunction()
    {
        EmployeeDataManager employeeDataManager = new EmployeeDataManager();
        string expected = "7500000 3000000 4500000 3750000 2";
        string query = "select sum(PayrollCalculate.BasicPay),min(PayrollCalculate.BasicPay),max(PayrollCalculate.BasicPay),Round(AVG(PayrollCalculate.BasicPay),0),COUNT(*)  from Employee inner join PayrollCalculate on Employee.EmployeeId = PayrollCalculate.EmployeeIdentity where Employee.Gender = 'F' group by Employee.Gender";
        string actual = eRRepository.AggregateFunctionBasedOnGender(query);
        Assert.AreEqual(actual, expected);
    }

    [TestMethod]
    [TestCategory("Using SQL Query for Male")]
    public void GivenGenderMale_ERTable_GroupBygender_ReturnAggregateFunction()
    {
        EmployeeDataManager employeeDataManager = new EmployeeDataManager();
        string expected = "39000000 9000000 30000000 19500000 2";
        string query = "select sum(PayrollCalculate.BasicPay),min(PayrollCalculate.BasicPay),max(PayrollCalculate.BasicPay),Round(AVG(PayrollCalculate.BasicPay),0),COUNT(*)  from Employee inner join PayrollCalculate on Employee.EmployeeId = PayrollCalculate.EmployeeIdentity where Employee.Gender = 'M' group by Employee.Gender";
        string actual = eRRepository.AggregateFunctionBasedOnGender(query);
        Assert.AreEqual(actual, expected);
    }

    //Usecase 12: Add IsActive Field
    [TestMethod]
    [TestCategory("Using Transaction Query")]
    public void AlterTablewithIsActive_usingTransaction_returnOne()
    {
        string expected = "Updated";
        TransactionClass transactionClass = new TransactionClass();
        string actual = transactionClass.AddIsActiveColumn();
        Assert.AreEqual(actual, expected);
    }

    //Usecase 12:Delete user from List and set IsActive as 0
    [TestMethod]
    [TestCategory("Using Transaction Query")]
    public void GivenUpdateQuery_usingTransaction_returnOne()
    {
        int expected = 1;
        TransactionClass transactionClass = new TransactionClass();
        int actual = transactionClas.MaintainListforAudit(6);
        transactionClass.RetrieveAllData();
        Assert.AreEqual(actual, expected);
    }
}