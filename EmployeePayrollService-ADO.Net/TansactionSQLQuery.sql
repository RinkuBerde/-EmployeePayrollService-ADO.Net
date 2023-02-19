select * from Employee
select * from PayrollCalculate
select * from EmployeeDepartment

create  procedure [dbo].[Employee]
(
@CompanyIdentity int ,
@EmployeeName varchar(255),
@EmployeePhoneNumber varchar(255),
@EmployeeAddress varchar(255),
@StartDate Date,
@Gender char

)
as
begin
insert into employee_payroll values
(
 @CompanyIdentity ,@EmployeeName,@EmployeePhoneNumber,@EmployeeAddress,@StartDate,@Gender
)
end
GO


create or alter  procedure [dbo].[PayrollCalculate]
(
@BasicPay float,
@Deductions float,
@TaxablePay float,
@IncomeTax float,
@NetPay float,
@EmployeeIdentity Date

)
as
begin
insert into employee_payroll values
(
  @Deductions,@TaxablePay,@IncomeTax,@NetPay,@EmployeeIdentity
)
end
GO


create  procedure [dbo].[EmployeeDepartment]
(
@DepartmentIdentity int ,
@EmployeeIdentity int


)
as
begin
insert into employee_payroll values
(
 @DepartmentIdentity ,@EmployeeIdentity 
)
end
GO