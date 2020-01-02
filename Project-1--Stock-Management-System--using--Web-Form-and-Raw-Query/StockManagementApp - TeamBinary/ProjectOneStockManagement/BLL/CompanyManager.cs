using System;
using System.Collections.Generic;
using ProjectOneStockManagement.DAL;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.BLL
{
    public class CompanyManager
    {
        CompanyGateway gateway = new CompanyGateway();
        Company existCompany = new Company();
        Company dbCompany = new Company();

        internal int IncrementCompanySL()
        {
            existCompany = gateway.GetExistCompany();  //Get the Last ID from Company Table
            existCompany.ID++;

            return existCompany.ID;
        }

        internal string SaveCompany(Company aCompany)
        {
            
                dbCompany = gateway.GetDBCompany(aCompany);    //Get Company Name by using Given Company Name
                if (dbCompany.Name != aCompany.Name)           //If New Company doesn't exist then Save.
                {
                    int rowAffected = gateway.SaveCompany(aCompany);  //Save CompanyID and CompanyName
                    if (rowAffected > 0)
                        return "Save Successfull";
                    return "Save Failed";
                }
                else
                {
                    return "Company Name is already Exist in the Database";
                }
        }

        internal object GetCategoryList()
        {
            return gateway.GetCategoryList();
        }

        internal object GetCompanyList()
        {
            return gateway.GetCompanyList();
        }
    }
}
