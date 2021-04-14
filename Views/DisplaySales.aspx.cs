using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;

namespace SoftEngWebEmployee.Views
{
    public partial class DisplaySales : System.Web.UI.Page
    {
        public List<SpecificOrdersModel> SpecificOrdersList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null && !IsPostBack)
            {
                string id = Request.QueryString["id"].ToString();
                LoadOrders(id);
            }
            //Request for onsiteID
            //if()
        }
        private async void LoadOrders(string id)
        {
            var orderList = await SpecificOrdersRepository.GetInstance().FetchSpecificOrders(int.Parse(id));
            SpecificOrdersList = orderList;
        }
        private async void LoadOnsites(string id)
        {
            //TODO
        }
    }
}