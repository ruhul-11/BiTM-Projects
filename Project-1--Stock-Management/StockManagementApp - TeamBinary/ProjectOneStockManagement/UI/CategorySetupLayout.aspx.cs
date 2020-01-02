using System;
using ProjectOneStockManagement.Models;
using ProjectOneStockManagement.BLL;

namespace ProjectOneStockManagement.UI
{
    public partial class CategorySetupLayout : System.Web.UI.Page
    {
        Category aCategory = new Category();
        CategoryManager aCategoryManager = new CategoryManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCategoryListInsideGrid();  //Show Category List Name at Page Load time.
            
        }

        protected void btnSaveCategoryName_Click(object sender, EventArgs e)
        {
            string categoryName = textBoxAddCategory.Text;  //Take Category Name from User

            aCategory.ID = aCategoryManager.IncrementCategorySL();  //Make an Manual Increment into Category Table to maintain sequential serial.
            aCategory.Name = categoryName;   //Pass the Category Name to the Category Instance.

            string msg = aCategoryManager.SaveCategory(aCategory);  //Save New CategoryName and CategoryID using
            lblCategorySaveStatus.Text = msg;

            ShowCategoryListInsideGrid();   // Show Updated Category List inside GridView
        }

        private void ShowCategoryListInsideGrid()
        {
            gridViewCategoryList.DataSource = aCategoryManager.GetCategoryList();  //Get All Category List from DB
            gridViewCategoryList.DataBind();
        }

        
    }
}