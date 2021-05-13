using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;

namespace SoftEngWebEmployee.Views
{
    public partial class DisplaySales : System.Web.UI.Page
    {
        public List<SpecificOrdersModel> SpecificOrdersList { get; set; }
        public List<OnsiteProductsTransactionModel> OnSiteProducts { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {                
                if (Session["orderID"] !=null)
                {
                    string id = Session["orderID"].ToString();
                    LoadOrders(id);                    
                } 
                else if(Session["onsiteID"] != null)
                {
                    string id = Session["onsiteID"].ToString();
                    LoadOnsites(id);
                }
                Session.Clear();
            }           
        }
        private async void LoadOrders(string id)
        {
            var orderList = await SpecificOrdersRepository.SingleInstance.FetchSpecificOrdersAsync(int.Parse(id));
            if (orderList != null)
            {
                SpecificOrdersList = orderList;
            }            
        }
        private async void LoadOnsites(string id)
        {
            var onsiteProductsList = await OnsiteProductsTransactionRepository.SingleInstance.FetchTransactionsGivenByIDAsync(int.Parse(id));
            if (onsiteProductsList != null)
            {
                OnSiteProducts = onsiteProductsList;
            }            
        }
    }
}