using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.DAL
{
    public class CategoryGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["StockDB"].ConnectionString;

        internal Category GetExistCategory()
        {
            string query = "SELECT * FROM Categories";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Category existCategory = new Category();

            while (reader.Read())
            {
                existCategory.ID = Convert.ToInt32(reader["CategoryID"]);
                existCategory.Name = reader["CategoryName"].ToString();
            }
            reader.Close();
            connection.Close();

            return existCategory;
        }

        internal Category GetDBCategory(Category aCategory)
        {
            string query = "SELECT CategoryName FROM Categories WHERE CategoryName LIKE '%"+aCategory.Name+"%';";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Category dbCategory = new Category();

            while (reader.Read())
            {
                dbCategory.Name = reader["CategoryName"].ToString();
            }
            reader.Close();
            connection.Close();

            return dbCategory;
        }

        internal int SaveCategory(Category aCategory)
        {
            string query = "INSERT INTO Categories Values(@CategoryID, @CategoryName)";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("CategoryID", SqlDbType.Int);
            command.Parameters["CategoryID"].Value = aCategory.ID;

            command.Parameters.Add("CategoryName", SqlDbType.VarChar);
            command.Parameters["CategoryName"].Value = aCategory.Name;

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
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