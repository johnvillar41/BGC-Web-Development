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
                //if (Request.QueryString["orderid"] != null)
                //{
                //    string id = Request.QueryString["orderid"].ToString();
                //    LoadOrders(id);
                //}
                //else
                //{
                //    string id = Request.QueryString["onsiteID"].ToString();
                //    LoadOnsites(id);
                //}
                if (Session["id"]!=null)
                {
                    string id = Session["id"].ToString();
                    //LoadOrders(id);
                    LoadOnsites(id);
                }                
            }           
           
        }
        private async void LoadOrders(string id)
        {
            var orderList = await SpecificOrdersRepository.GetInstance().FetchSpecificOrders(int.Parse(id));
            if (orderList != null)
            {
                SpecificOrdersList = orderList;
            }            
        }
        private async void LoadOnsites(string id)
        {
            var onsiteProductsList = await OnsiteProductsTransactionRepository.GetInstance().FetchTransactionsGivenByID(int.Parse(id));
            if (onsiteProductsList != null)
            {
                OnSiteProducts = onsiteProductsList;
            }            
        }
    }
}