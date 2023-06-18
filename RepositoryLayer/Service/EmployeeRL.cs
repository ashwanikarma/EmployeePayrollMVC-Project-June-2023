using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace RepositoryLayer.Service
{
    public class EmployeeRL : IEmployeeRL
    {
        private readonly IConfiguration Configuration; //passing connection in a appsetting.json- can be access by IConfiguration

        public EmployeeRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        //mvc layer -> presentation layer
        //repository layer -> data access layer
        //business layer -> business logic layer - call the method from repo to business(interface b/w mvc and repo)
        //model layer or common layer -> hold all the models
        //ado.net is a database first approach(CREATE a database table procedure first then call those procedure inside a method)
        // To Add new employee record
        public void AddEmployee(EmployeeModel employeeModel)
        {   //SqlConnection Class helps to connect to the database
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))//EmployeePayRoll = key
            {
                //sqlcommand will hold query or storedprocedure
                SqlCommand cmd = new SqlCommand("uspAddEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure; //specifying 
                cmd.Parameters.AddWithValue("@Name", employeeModel.Name);
                cmd.Parameters.AddWithValue("@E_image", employeeModel.Image);
                cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                cmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employeeModel.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employeeModel.Notes);
                con.Open(); //connection open 
                cmd.ExecuteNonQuery(); //add update delete 
                con.Close(); // connection close
            }
        }


        //  To Update the records of a particluar employee
        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("uspUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", employeeModel.EmpId);
                cmd.Parameters.AddWithValue("@Name", employeeModel.Name);
                cmd.Parameters.AddWithValue("@E_image", employeeModel.Image);
                cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                cmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                cmd.Parameters.AddWithValue("@StartDate", employeeModel.StartDate);
                cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                cmd.Parameters.AddWithValue("@Notes", employeeModel.Notes);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteEmployeeById(int? empId)
        {

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("uspDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", empId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public EmployeeModel GetEmployeeById(int? id)
        {
            EmployeeModel employeeModel = new EmployeeModel();

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
            {
                string sqlQuery = "SELECT * FROM tblEmployees WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employeeModel.EmpId = Convert.ToInt32(rdr["EmployeeID"]);
                    employeeModel.Name = rdr["Name"].ToString();
                    employeeModel.Image = rdr["E_image"].ToString();
                    employeeModel.Gender = rdr["Gender"].ToString();
                    employeeModel.Department = rdr["Department"].ToString();
                    employeeModel.Salary = Convert.ToInt32(rdr["Salary"]);
                    employeeModel.StartDate = (DateTime)rdr["StartDate"];
                    employeeModel.Notes = rdr["Notes"].ToString();
                }
            }
            return employeeModel;
        }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> listemployee = new List<EmployeeModel>();
            //Represents a connection to Sql Server Database
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
            {
                // Creating SqlCommand object 
                SqlCommand cmd = new SqlCommand("uspGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open(); //opening connection

                // Executing the SQL query
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeModel employeeModel = new EmployeeModel();

                    employeeModel.EmpId = Convert.ToInt32(rdr["EmployeeID"]);
                    employeeModel.Name = rdr["Name"].ToString();
                    employeeModel.Image = rdr["E_image"].ToString();
                    employeeModel.Gender = rdr["Gender"].ToString();
                    employeeModel.Department = rdr["Department"].ToString();
                    employeeModel.Salary = Convert.ToInt32(rdr["Salary"]);
                    employeeModel.StartDate = (DateTime)rdr["StartDate"];
                    employeeModel.Notes = rdr["Notes"].ToString();
                    listemployee.Add(employeeModel);
                }
                con.Close();
            }
            return listemployee;
        }
        public LoginModel GetUser(LoginModel loginModel)
        {
            try
            {
                LoginModel model = new LoginModel();
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
                {
                    SqlCommand cmd = new SqlCommand("uspAdminLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@email", loginModel.email);
                    cmd.Parameters.AddWithValue("@u_password", loginModel.password);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        model.employeeID = Convert.ToInt32(rdr["EmployeeID"]);
                        model.Name = rdr["Name"].ToString();
                        model.email = rdr["email"].ToString();
                        model.password = rdr["u_password"].ToString();
                        model.roleID = Convert.ToInt32(rdr["roleID"]);
                        //if (loginModel.email == rdr["email"].ToString() && loginModel.password == rdr["u_password"].ToString() )
                        //    return true;
                        //else
                        //    throw new Exception("Not an admin!!!");
                    }
                    con.Close();
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
