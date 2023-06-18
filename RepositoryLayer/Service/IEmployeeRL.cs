using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public interface IEmployeeRL
    {
        public void AddEmployee(EmployeeModel employeeModel);
        public IEnumerable<EmployeeModel> GetAllEmployees();
        public void UpdateEmployee(EmployeeModel employeeModel);
        public void DeleteEmployeeById(int? empId);

        public EmployeeModel GetEmployeeById(int? id);
        public LoginModel GetUser(LoginModel loginModel);



    }
}
