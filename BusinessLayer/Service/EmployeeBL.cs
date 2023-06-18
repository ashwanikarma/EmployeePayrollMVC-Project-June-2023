using CommonLayer.Model;
using Microsoft.EntityFrameworkCore.Storage;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class EmployeeBL:IEmployeeBL
    {
        private readonly IEmployeeRL iemployeeRL;

        public EmployeeBL(IEmployeeRL iemployeeRL)
        {
            this.iemployeeRL = iemployeeRL;
        }

        public void AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                iemployeeRL.AddEmployee(employeeModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                iemployeeRL.UpdateEmployee(employeeModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteEmployeeById(int? empId)
        {
            try
            {
                iemployeeRL.DeleteEmployeeById(empId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            try
            {
                return iemployeeRL.GetAllEmployees();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public EmployeeModel GetEmployeeById(int? id)
        {
            try
            {
                return iemployeeRL.GetEmployeeById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public LoginModel GetUser(LoginModel loginModel)
        {
            try
            {
                return iemployeeRL.GetUser(loginModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
