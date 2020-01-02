using System;
using ProjectOneStockManagement.BLL;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.UI
{
    public partial class ViewSalesBetweenTwoDatesLayout : System.Web.UI.Page
    {
        ItemManager itemManager = new ItemManager();
        SaleSummary saleSummary = new SaleSummary();

        protected void btnSearchSellStatus_Click(object sender, EventArgs e)
        {
            DateTime fromDate = Convert.ToDateTime(textBoxFromDate.Text);
            DateTime toDate = Convert.ToDateTime(textBoxToDate.Text);
            
            if(fromDate < toDate)
            {
                gridViewSellStatus.DataSource = itemManager.GetSaleSummaryBetweenDates(textBoxFromDate.Text, textBoxToDate.Text);
                gridViewSellStatus.DataBind();
            }
            else
            {
                lblSearchStatus.Text = "To Date can not be past date of From Date";
            }
        }
    }
}