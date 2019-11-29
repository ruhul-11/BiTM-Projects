using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagement.Models;

namespace UniversityManagement.DAL
{
    public class DepartmentGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        internal string SaveDepartment(Department aDepartment)
        {
            string query = "INSERT INTO Departments Values (@Code, @Name)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("Code", SqlDbType.VarChar);
            command.Parameters["Code"].Value = aDepartment.Code;

            command.Parameters.Add("Name", SqlDbType.VarChar);
            command.Parameters["Name"].Value = aDepartment.Name;

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
                return "Saved";
            return "Failed";
        }

        internal List<Department> GetAllDepartment()
        {
            string query = "SELECT * FROM Departments";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Department> departmentList = new List<Department>();
            while (reader.Read())
            {
                departmentList.Add(new Department
                {
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString()
                });
            }
            reader.Close();
            connection.Close();
            return departmentList;
        }
    }
}