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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<OrdersModel> DisplayOrders()
        {
            var listOfOrders = OrdersRepository.GetInstance().FetchAllOrders();
            return (List<OrdersModel>)listOfOrders;
        }
    }
}