using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Orders : System.Web.UI.Page
    {
        private List<OrdersModel> OrdersList = new List<OrdersModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        public List<OrdersModel> DisplayOrders()
        {
            return OrdersList;
        }

        private async void LoadOrders()
        {
            var listOfOrders = await OrdersRepository.GetInstance().FetchAllOrders();
            OrdersList = (List<OrdersModel>)listOfOrders;
        }

        protected void ButtonSaveChangesCancel_Click(object sender, EventArgs e)
        {
            if (OrderIDCancel.Text != null)
            {
                OrdersRepository.GetInstance().ChangeStatusOfOrderToCancelled(int.Parse(OrderIDCancel.Text));
            }
        }

        protected void ButtonSaveChangesFinish_Click(object sender, EventArgs e)
        {
            if (OrderIDFinish.Text != null)
            {
                OrdersRepository.GetInstance().ChangeStatusOfOrderToFinished(int.Parse(OrderIDFinish.Text));
            }

        }

    }
}