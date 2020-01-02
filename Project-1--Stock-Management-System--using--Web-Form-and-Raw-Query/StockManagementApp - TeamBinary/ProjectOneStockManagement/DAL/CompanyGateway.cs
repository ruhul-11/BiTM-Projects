using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.DAL
{
    public class CompanyGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["StockDB"].ConnectionString;

        internal Company GetExistCompany()
        {
            string query = "SELECT * FROM Companies";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Company existCompany = new Company();

            while (reader.Read())
            {
                existCompany.ID = Convert.ToInt32(reader["CompanyID"]);
            }
            reader.Close();
            connection.Close();

            return existCompany;
        }

        internal Company GetDBCompany(Company aCompany)
        {
            string query = "SELECT CompanyName FROM Companies WHERE CompanyName LIKE '%" + aCompany.Name + "%';";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Company dbCompany = new Company();

            while (reader.Read())
            {
                dbCompany.Name = reader["CompanyName"].ToString();
            }
            reader.Close();
            connection.Close();

            return dbCompany;
        }
                
        internal int SaveCompany(Company aCompany)
        {
            string query = "INSERT INTO Companies Values(@CompanyID, @CompanyName)";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("CompanyID", SqlDbType.Int);
            command.Parameters["CompanyID"].Value = aCompany.ID;

            command.Parameters.Add("CompanyName", SqlDbType.VarChar);
            command.Parameters["CompanyName"].Value = aCompany.Name;

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        internal List<Company> GetCompanyList()
        {
            string query = "SELECT * FROM Companies";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Company> companyList = new List<Company>();

            while (reader.Read())
            {
                Company aCompany = new Company();
                aCompany.ID = Convert.ToInt32(reader["CompanyID"]);
                aCompany.Name = reader["CompanyName"].ToString();

                companyList.Add(aCompany);
            }

            reader.Close();
            connection.Close();

            return companyList;
        }

        internal List<Category> GetCategoryList()
        {
            string query = "SELECT * FROM Categories";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Category> categoryList = new List<Category>();

            while (reader.Read())
            {
                Category aCategory = new Category();
                aCategory.ID = Convert.ToInt32(reader["CategoryID"]);
                aCategory.Name = reader["CategoryName"].ToString();

                categoryList.Add(aCategory);
            }

            reader.Close();
            connection.Close();

            return categoryList;
        }
    }
}