

select * from deptInfo

select * from employeeInfo

select empNo, empName from employeeInfo where empSalary = (select max(empSalary) from employeeInfo)

---------   Joins Get Data From 2 Or more

-- Types of Joins

-- A. Cross Join or Cartesian Product Join ---

		--eg Table 1 has 5
		  -- Table 2 has 3 rows
		  -- so result will be 5 * 3 = 15 records
	-- cartesian products are used by admins to calculate how much max load will be on the server so they can plan the server capacity accordingly

select * from employeeInfo, deptInfo order by empNo
---------------------------------------------------------------------------------------------------

-- B. Inner Join aka Equi Join aka Join aka Inner Join, only returns matching records (does not show records with missing data)

select empNo, empName, empSalary, deptInfo.deptName, deptInfo.deptLocation
from employeeInfo join deptInfo 
on employeeInfo.empDept = deptInfo.deptNo
where deptLocation like 'N%'
order by empSalary

select count(empNo) as [Total Emp], deptInfo.deptLocation as 'City'
from
employeeInfo join deptInfo
on employeeInfo.empDept = deptInfo.deptNo
group by deptInfo.deptLocation

select count(empNo) from employeeInfo
select count(*) from deptInfo



-- C. Left Join   --- left join will show all matching records FROM LEFT and only EQUAL records from the right

   -- real life example: 


-- D. Right Join

-- E. Full Join

-- F. Null Join

-- G. Self Join

--- INNER Join ----