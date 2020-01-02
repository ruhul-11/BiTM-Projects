using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.DAL
{
    public class ItemGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["StockDB"].ConnectionString;
        string query;
        
        internal Item GetDbItem(Item aItem)
        {
            query = "SELECT * FROM Items";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Item existingItem = new Item();

            while (reader.Read())
            {
                existingItem.Name = reader["ItemName"].ToString();
            }
            reader.Close();
            connection.Close();

            return existingItem;
        }

        internal void SetInitialValueToItemStatus(Item dbItem, Item aItem)
        {
            string query = "INSERT INTO ItemStatus VALUES ("+dbItem.ID+",'"+aItem.Name+"', 0, 0, 0)";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal Item GetItemIDByItemName(Item aItem)
        {
            query = "SELECT ItemID FROM Items WHERE ItemName='"+aItem.Name+"';";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Item existingItem = new Item();

            while (reader.Read())
            {
                existingItem.ID = Convert.ToInt32(reader["ItemID"].ToString());
            }
            reader.Close();
            connection.Close();

            return existingItem;
        }


        internal int SaveItem(Item aItem)
        {
            query = "INSERT INTO Items (CategoryID, CompanyID, ItemName, ReorderLevel, AvailableQuantity, Date, Time) Values (@CategoryID, @CompanyID, @ItemName, @ReorderLevel, @AvailableQuantity, (SELECT CONVERT(date,SYSDATETIME())), (SELECT CONVERT(time,SYSDATETIME())))";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("CategoryID", SqlDbType.Int);
            command.Parameters["CategoryID"].Value = aItem.Category;

            command.Parameters.Add("CompanyID", SqlDbType.Int);
            command.Parameters["CompanyID"].Value = aItem.Company;

            command.Parameters.Add("ItemName", SqlDbType.VarChar);
            command.Parameters["ItemName"].Value = aItem.Name;

            command.Parameters.Add("ReorderLevel", SqlDbType.Int);
            command.Parameters["ReorderLevel"].Value = aItem.ReorderLevel;

            command.Parameters.Add("AvailableQuantity", SqlDbType.Int);
            command.Parameters["AvailableQuantity"].Value = aItem.AvailableQuantity;
                        
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        internal List<Item> GetItemListByCompany(Company aCompany)
        {
            string query = "SELECT ItemID, ItemName, AvailableQuantity, ReorderLevel FROM Items AS IT INNER JOIN Companies AS CM ON IT.CompanyID = CM.CompanyID WHERE CM.CompanyID = " + aCompany.ID+";";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Item> dbItem = new List<Item>();
            while (reader.Read())
            {
                Item aItem = new Item();
                aItem.ID = Convert.ToInt32(reader["ItemID"].ToString());
                aItem.Name = reader["ItemName"].ToString();
                aItem.AvailableQuantity = Convert.ToInt32(reader["AvailableQuantity"].ToString());
                aItem.ReorderLevel = Convert.ToInt32(reader["ReorderLevel"].ToString());

                dbItem.Add(aItem);
            }
            reader.Close();
            connection.Close();

            return dbItem;
        }

        internal int UpdateSaleItem(StockOutItem aItem)
        {
            query = "INSERT INTO SaleItems (ItemID, ItemName, SaleQty, Date) Values (@ItemID, @ItemName, @SaleQty, (SELECT CONVERT(date,SYSDATETIME())))";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("ItemID", SqlDbType.Int);
            command.Parameters["ItemID"].Value = aItem.ID;

            command.Parameters.Add("ItemName", SqlDbType.VarChar);
            command.Parameters["ItemName"].Value = aItem.Name;

            command.Parameters.Add("SaleQty", SqlDbType.Int);
            command.Parameters["SaleQty"].Value = aItem.Quantity;

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        
        internal void ClearStockOutTable()
        {
            query = "DELETE FROM StockOutItems";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal Item GetItemInfoByID(Item aItem)
        {
            string query = "SELECT ItemID, AvailableQuantity, ReorderLevel FROM Items WHERE ItemID = "+aItem.ID+";";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Item existingItem = new Item();
            while (reader.Read())
            {
                existingItem.ID = Convert.ToInt32(reader["ItemID"].ToString());
                existingItem.AvailableQuantity = Convert.ToInt32(reader["AvailableQuantity"].ToString());
                existingItem.ReorderLevel = Convert.ToInt32(reader["ReorderLevel"].ToString());
            }
            reader.Close();
            connection.Close();
            return existingItem;
        }

        internal int AddQuantityToItems(Item aItem)
        {
            string query = "UPDATE Items SET AvailableQuantity = '"+aItem.AvailableQuantity+"' WHERE ItemID = "+aItem.ID+";";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        internal StockOutItem GetStockOutItemInfo(Item newItem)
        {
            query = "SELECT ItemID, ItemName, CompanyName, AvailableQuantity FROM Items AS IT INNER JOIN Companies AS CM ON IT.CompanyID = CM.CompanyID WHERE IT.ItemID =" + newItem.ID + ";";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            StockOutItem aStockOutItem = new StockOutItem();

            while (reader.Read())
            {
                aStockOutItem.ID = Convert.ToInt32(reader["ItemID"].ToString());
                aStockOutItem.Name = reader["ItemName"].ToString();
                aStockOutItem.CompanyName = reader["CompanyName"].ToString();
                aStockOutItem.Quantity = newItem.AvailableQuantity;
            }
            reader.Close();
            connection.Close();
            return aStockOutItem;
        }

        internal void InsertToStockOutTable(StockOutItem aStockOutItem)
        {
            query = "INSERT INTO StockOutItems Values(@ItemID, @ItemName, @CompanyName, @Quantity);";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("ItemID", SqlDbType.Int);
            command.Parameters["ItemID"].Value = aStockOutItem.ID;

            command.Parameters.Add("ItemName", SqlDbType.VarChar);
            command.Parameters["ItemName"].Value = aStockOutItem.Name;

            command.Parameters.Add("CompanyName", SqlDbType.VarChar);
            command.Parameters["CompanyName"].Value = aStockOutItem.CompanyName;

            command.Parameters.Add("Quantity", SqlDbType.Int);
            command.Parameters["Quantity"].Value = aStockOutItem.Quantity;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        internal List<StockOutItem> GetStockOutItemList()
        {
            query = "SELECT * FROM StockOutItems";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            
            List<StockOutItem> itemList = new List<StockOutItem>();

            while (reader.Read())
            {
                StockOutItem aStockOutItem = new StockOutItem();
                aStockOutItem.ID = Convert.ToInt32(reader["ItemID"].ToString());
                aStockOutItem.Name = reader["ItemName"].ToString();
                aStockOutItem.CompanyName = reader["CompanyName"].ToString();
                aStockOutItem.Quantity = Convert.ToInt32(reader["Quantity"].ToString());

                itemList.Add(aStockOutItem);
            }
            reader.Close();
            connection.Close();

            return itemList;
        }
        
        internal StockOutItem GetPreviousSellItem(StockOutItem aItem)
        {
            query = "SELECT SaleQuantity FROM ItemStatus WHERE ItemID = "+aItem.ID+"";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            StockOutItem aStockOutItem = new StockOutItem();

            while (reader.Read())
            {
                
                aStockOutItem.Quantity = Convert.ToInt32(reader["SaleQuantity"].ToString());
            }
            reader.Close();
            connection.Close();
            return aStockOutItem;
        }


        internal bool UpdateItemStatusTable(StockOutItem dbItem)
        {
            query = "UPDATE ItemStatus SET SaleQuantity = "+dbItem.Quantity+ " WHERE ItemID = " + dbItem.ID+"";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
                return true;
            return false;
        }


        internal StockOutItem GetPreviousDamageItem(StockOutItem aItem)
        {
            query = "SELECT DamageQuantity FROM ItemStatus WHERE ItemID = " + aItem.ID + "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            StockOutItem aStockOutItem = new StockOutItem();

            while (reader.Read())
            {
                aStockOutItem.Quantity = Convert.ToInt32(reader["DamageQuantity"].ToString());
            }
            reader.Close();
            connection.Close();
            return aStockOutItem;
        }


        internal void InsertDamageQuantity(StockOutItem dbItem)
        {
            query = "UPDATE ItemStatus SET DamageQuantity = " + dbItem.Quantity + " WHERE ItemID = " + dbItem.ID + "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }


        internal StockOutItem GetPreviousLostItem(StockOutItem aItem)
        {
            query = "SELECT LostQuantity FROM ItemStatus WHERE ItemID = " + aItem.ID + "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            StockOutItem aStockOutItem = new StockOutItem();

            while (reader.Read())
            {
                aStockOutItem.Quantity = Convert.ToInt32(reader["LostQuantity"].ToString());
            }
            reader.Close();
            connection.Close();
            return aStockOutItem;
        }


        internal void InsertLostQuantity(StockOutItem dbItem)
        {
            query = "UPDATE ItemStatus SET LostQuantity = " + dbItem.Quantity + " WHERE ItemID = " + dbItem.ID + "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        internal List<ItemSummary> GetItemInfoByCompanyAndCategory(int companyID, int categoryID)
        {
            query = "SELECT ItemID, ItemName, CompanyName, CategoryName, AvailableQuantity, ReorderLevel FROM ((Items INNER JOIN Companies ON Items.CompanyID=Companies.CompanyID) INNER JOIN Categories ON Items.CategoryID = Categories.CategoryID) WHERE (Categories.CategoryID = " + categoryID + " AND Companies.CompanyID = " + companyID + ")";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<ItemSummary> itemSummaryList = new List<ItemSummary>();
            while (reader.Read())
            {
                ItemSummary itemSummary = new ItemSummary();
                itemSummary.SL = Convert.ToInt32(reader["ItemID"].ToString());
                itemSummary.Item = reader["ItemName"].ToString();
                itemSummary.Company = reader["CompanyName"].ToString();
                itemSummary.Category = reader["CategoryName"].ToString();
                itemSummary.AvailableQuantity = Convert.ToInt32(reader["AvailableQuantity"].ToString());
                itemSummary.ReorderLevel = Convert.ToInt32(reader["ReorderLevel"].ToString());

                itemSummaryList.Add(itemSummary);
            }
            reader.Close();
            connection.Close();
            return itemSummaryList;
        }


        internal List<SaleSummary> GetSaleSummaryBetweenDates(string fromDate, string toDate)
        {
            query = "SELECT SaleID, ItemName, SaleQty FROM SaleItems WHERE Date BETWEEN '"+fromDate+ "' AND '" + toDate + "';";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<SaleSummary> saleSummaryList = new List<SaleSummary>();
            while (reader.Read())
            {
                SaleSummary saleSummary = new SaleSummary();
                saleSummary.SL = Convert.ToInt32(reader["SaleID"].ToString());
                saleSummary.Item = reader["ItemName"].ToString();
                saleSummary.Quantity = Convert.ToInt32(reader["SaleID"].ToString());
                                                
                saleSummaryList.Add(saleSummary);
            }
            reader.Close();
            connection.Close();
            return saleSummaryList;
        }
    }
}