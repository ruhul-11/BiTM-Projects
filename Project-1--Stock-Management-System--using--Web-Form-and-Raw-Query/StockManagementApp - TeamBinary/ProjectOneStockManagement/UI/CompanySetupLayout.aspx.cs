using System;
using ProjectOneStockManagement.Models;
using ProjectOneStockManagement.BLL;

namespace ProjectOneStockManagement.UI
{
    public partial class CompanySetupLayout : System.Web.UI.Page
    {
        Company aCompany = new Company();
        CompanyManager aCompanyManager = new CompanyManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCompanyListInsideGrid();  //Show Company List Name at Page Load time.
        }

        protected void btnSaveCompanyName_Click(object sender, EventArgs e)
        {
            string companyName = textBoxCompanyName.Text;    //Take Company Name from User

            aCompany.ID = aCompanyManager.IncrementCompanySL();  //Make an Manual Increment into Company Table to maintain sequential serial.
            aCompany.Name = companyName;   //Pass the Category Name to the Company Instance.

            string msg = aCompanyManager.SaveCompany(aCompany);   //Save New CompanyName and CompanyID using
            lblCompanySaveStatus.Text = msg;

            ShowCompanyListInsideGrid();   // Show Updated Company List inside GridView
        }

        private void ShowCompanyListInsideGrid()
        {
            gridViewCompanyList.DataSource = aCompanyManager.GetCompanyList();   //Get All Company List from DB
            gridViewCompanyList.DataBind();
        }
    }
}