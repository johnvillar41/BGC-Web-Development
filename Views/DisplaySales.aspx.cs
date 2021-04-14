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
            //TODO FIX ERROR HERE
            if (!IsPostBack)
            {
                if (Request.QueryString["orderid"] != null)
                {
                    string id = Request.QueryString["orderid"].ToString();
                    LoadOrders(id);
                }
                else
                {
                    string id = Request.QueryString["onsiteid"].ToString();
                    LoadOnsites(id);
                }
            }           
           
        }
        private async void LoadOrders(string id)
        {
            var orderList = await SpecificOrdersRepository.GetInstance().FetchSpecificOrders(int.Parse(id));
            SpecificOrdersList = orderList;
        }
        private async void LoadOnsites(string id)
        {
            var onsiteProductsList = await OnsiteProductsTransactionRepository.GetInstance().FetchTransactionsGivenByID(int.Parse(id));
            OnSiteProducts = onsiteProductsList;
        }
    }
}