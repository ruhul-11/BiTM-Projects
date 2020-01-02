using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectOneStockManagement.BLL;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.UI
{
    public partial class SearchAndViewItemSummaryLayout : System.Web.UI.Page
    {
        CompanyManager companyManager = new CompanyManager();
        ItemManager itemManager = new ItemManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)                    //If page load for first time then execute this block
            {
                BindAllCompany();               //Get All Company List inside DropDownList
                BindAllCategory();              //Get All Category List inside DropDownList

                ListItem liCompany = new ListItem("Select Company", "-1");
                ddlSearchCompany.Items.Insert(0, liCompany);                 //Send "Select Company" to first position of DropDownList

                ListItem liCategory = new ListItem("Select Category", "-1");
                ddlSearchCategory.Items.Insert(0, liCategory);               //Send "Select Category" to first position of DropDownList
            }
        }

        private void BindAllCompany()
        {
            ddlSearchCompany.DataSource = companyManager.GetCompanyList();  //Get All Company Name From DB to DDL
            ddlSearchCompany.DataBind();
        }

        private void BindAllCategory()
        {
            ddlSearchCategory.DataSource = companyManager.GetCategoryList();  //Get All Category Name From DB to DDL
            ddlSearchCategory.DataBind();
        }

        protected void btnSearchItemSummary_Click(object sender, EventArgs e)
        {
            int companyID = Convert.ToInt32(ddlSearchCompany.SelectedValue);    //Take CompanyID by Selected Value of DDL
            int categoryID = Convert.ToInt32(ddlSearchCategory.SelectedValue);   //Take CategoryID by Selected Value of DDL
            gridViewItemSummary.DataSource = itemManager.GetItemInfoByCompanyAndCategory(companyID, categoryID);  //Send Item Info to GridView
            gridViewItemSummary.DataBind();
        }
    }
}