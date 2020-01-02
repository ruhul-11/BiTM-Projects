using System;
using System.Collections;
using System.Collections.Generic;
using ProjectOneStockManagement.DAL;
using ProjectOneStockManagement.Models;


namespace ProjectOneStockManagement.BLL
{
    public class ItemManager
    {
        ItemGateway gateway = new ItemGateway();

        internal string SaveItem(Item aItem)
        {
            
            Item dbItem = gateway.GetDbItem(aItem);
            if(dbItem.Name != aItem.Name)
            {
                int rowAffected = gateway.SaveItem(aItem);

                if (rowAffected > 0)
                {
                    dbItem = gateway.GetItemIDByItemName(aItem);
                    gateway.SetInitialValueToItemStatus(dbItem, aItem);
                    return "Item Saved Successfully";
                }
                
                return "Item Saved Failed";

            }

            return "Item already Exist";
        }

        internal List<Item> GetItemListByCompany(Company aCompany)
        {
            List<Item> dbItems = gateway.GetItemListByCompany(aCompany);
            return dbItems;
        }

        internal Item GetItemInfoByID(Item stockOutItem)
        {
            return gateway.GetItemInfoByID(stockOutItem);
        }

        internal string AddQuantity(Item newItem)
        {
            Item dbItem = new Item();
            if (newItem.StockInQuantity > 0)
            {
                dbItem = gateway.GetItemInfoByID(newItem);
                dbItem.AvailableQuantity += newItem.StockInQuantity;

                int rowAffected = gateway.AddQuantityToItems(dbItem);
                if (rowAffected > 0)
                    return "Quantity added successfully";
                return "Quantity added Failed";

            }
            return "Only positive amount is acceptable.";
        }

        internal string AddStockOutQuantity(Item stockOutItem)
        {
            Item dbItem = gateway.GetItemInfoByID(stockOutItem); // Inside dbItem ItemID, AvailableQuantity, ReorderLevel
            if (stockOutItem.AvailableQuantity>=0)
            {
                if (stockOutItem.AvailableQuantity <= dbItem.AvailableQuantity)
                    {
                        dbItem.AvailableQuantity -= stockOutItem.AvailableQuantity;

                        StockOutItem aStockOutItem = new StockOutItem();
                        aStockOutItem = gateway.GetStockOutItemInfo(stockOutItem);
                        aStockOutItem.Quantity = stockOutItem.AvailableQuantity;

                        gateway.InsertToStockOutTable(aStockOutItem);  // Insert ItemID, ItemName, Company, Quantity

                        int rowAffected = gateway.AddQuantityToItems(dbItem);
                        if (rowAffected > 0)
                            return "Stock Out Successfully";
                        return "Stock Out Failed";
                    }
                    return "Not Enough Stock";
            }
            return "Please give some positive number";
            
        }
                
        internal object GetStockOutItemListByID(Item newItem)
        {
            StockOutItem aStockOutItem = gateway.GetStockOutItemInfo(newItem);
            List<StockOutItem> itemList = gateway.GetStockOutItemList();
            return itemList;
        }

        internal void ClearStockOutTable()
        {
            gateway.ClearStockOutTable();
        }

        internal string AddSellItems()
        {
            List<StockOutItem> itemList =gateway.GetStockOutItemList();
            
            foreach (StockOutItem aItem in itemList)  //Inside aItem = (ItemID, Name, Company, Given-Quantity)
            {
                gateway.UpdateSaleItem(aItem); // Insert Given-SaleQuantity to SaleItems
                StockOutItem dbItem = new StockOutItem();
                dbItem = gateway.GetPreviousSellItem(aItem);  //Get Previous-Sale Quantity Only
                dbItem.ID = aItem.ID;
                dbItem.Quantity += aItem.Quantity;
                gateway.UpdateItemStatusTable(dbItem);
                
            }
            return "Update Successfull";
        }

        internal string AddDamageItems()
        {
            List<StockOutItem> itemList = gateway.GetStockOutItemList();

            foreach (StockOutItem aItem in itemList) //Inside aItem = (ItemID, Name, Company, Given-Quantity)
            {
                StockOutItem dbItem = new StockOutItem();
                dbItem = gateway.GetPreviousDamageItem(aItem); //Get Previous-DamageQuantity Quantity Only
                dbItem.ID = aItem.ID;
                dbItem.Quantity += aItem.Quantity;
                gateway.InsertDamageQuantity(dbItem);
            }
            return "Update Successfull";
        }

        internal string AddLostItems()
        {
            List<StockOutItem> itemList = gateway.GetStockOutItemList();

            foreach (StockOutItem aItem in itemList)
            {
                StockOutItem dbItem = new StockOutItem();
                dbItem = gateway.GetPreviousLostItem(aItem);
                dbItem.ID = aItem.ID;
                dbItem.Quantity += aItem.Quantity;
                gateway.InsertLostQuantity(dbItem);
            }
            return "Update Successfull";
        }

        internal List<ItemSummary> GetItemInfoByCompanyAndCategory(int companyID, int categoryID)
        {
            return gateway.GetItemInfoByCompanyAndCategory(companyID, categoryID);
        }


        internal List<SaleSummary> GetSaleSummaryBetweenDates(string fromDate, string toDate)
        {
            return gateway.GetSaleSummaryBetweenDates(fromDate, toDate);
        }

    }
}