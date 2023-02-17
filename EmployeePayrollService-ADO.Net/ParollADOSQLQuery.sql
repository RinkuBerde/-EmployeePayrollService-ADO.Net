select * from employee_payroll

create or alter  procedure [dbo].[SpAddEmployeeDetails]
(
@EmployeeName varchar(255),
@PhoneNumber varchar(255),
@Address varchar(255),
@Department varchar(255),
@Gender char(1),
@City varchar(100),
@Country varchar(100),
@BasicPay float,
@Deductions float,
@TaxablePay float,
@Tax float,
@NetPay float,
@StartDate Date

)
as
begin
insert into employee_payroll values
(
@EmployeeName,@BasicPay,@StartDate,@Gender,@PhoneNumber,@address,@BasicPay,@department,@Deductions,@TaxablePay,@Tax,@NetPay,@City,@Country 
)
end
GO


create or alter  procedure [dbo].[spUpdateSalary]
(
@EmployeeName varchar(255),
@Salary int,
@PhoneNumber varchar(255),
@Address varchar(255),
@Department varchar(255),
@Gender char(1),
@City varchar(100),
@Country varchar(100),
@BasicPay float,
@Deductions float,
@TaxablePay float,
@Tax float,
@NetPay float,
@StartDate Date

)
as
begin
update SALARY
set EMPSAL=@salary
where name=@EmployeeName;
end
GO
