using SoftEngWebEmployee.Repository;
using System;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSales();
            }
        }
        private async void LoadSales()
        {
            var salesList = await SalesRepository.GetInstance().FetchAllSales();
            if (salesList != null)
            {
                SalesRepeater.DataSource = salesList;
                SalesRepeater.DataBind();
            }           
        }
        

        protected void SalesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "SalesCommand")
            //{
            //    int id = int.Parse(e.CommandArgument.ToString());
            //    Response.Redirect("DisplaySales.aspx?orderid=" + id);
            //    return;
            //}
            //if (e.CommandName == "SalesCommandOnSite")
            //{
            //    int id = int.Parse(e.CommandArgument.ToString());
            //    Response.Redirect("DisplaySales.aspx?onsiteid=" + id);
            //    return;
            //}
           
            //switch (e.CommandName)
            //{                
            //    case "SalesCommand":
            //        int orderID = int.Parse(e.CommandArgument.ToString());
            //        Response.Redirect("DisplaySales.aspx?orderid=" + orderID);
            //        break;              
            //}

        }

        protected void IDS_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] arguments = btn.CommandArgument.Split(';');
            string orderID = arguments[0];
            string onsiteID = arguments[1];
            if (orderID.Length != 0)
            {                
                Session["id"] = orderID;                
            }
            else
            {
                Session["id"] = onsiteID;               
            }
            Response.Redirect("DisplaySales");
        }
    }
}