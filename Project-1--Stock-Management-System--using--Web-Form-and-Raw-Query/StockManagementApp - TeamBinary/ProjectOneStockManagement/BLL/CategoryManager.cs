using System.Collections.Generic;
using ProjectOneStockManagement.DAL;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.BLL
{
    public class CategoryManager
    {
        CategoryGateway gateway = new CategoryGateway();
        Category existCategory = new Category();
        Category dbCategory = new Category();

        internal int IncrementCategorySL()
        {
            existCategory = gateway.GetExistCategory();
            existCategory.ID++;

            return existCategory.ID;
        }

        internal string SaveCategory(Category aCategory)
        {
            if(aCategory.Name != null)
            {
                dbCategory = gateway.GetDBCategory(aCategory);   //Get all DB Categories
                if(dbCategory.Name != aCategory.Name)            //Check if the New Category Name is Exist or Not.
                {
                    int rowAffected = gateway.SaveCategory(aCategory);  //Save CategoryID and CategoryName to Categories Table.
                    if (rowAffected > 0)
                        return "Save Successfull";
                    return "Save Failed";
                }
                else
                {
                    return "Category Name already Exist";
                }
            }
            return "Please insert a Category Name";
        }

        //Get all Category List from DB
        internal object GetCategoryList()
        {
            return gateway.GetCategoryList();  //Get All Category Names
        }
    }
}