using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<InformationModel> values = new List<InformationModel>();

                values.Add(new InformationModel()
                {
                    Product_ID = 1,
                    ProductInformation = "Sample"
                });
                values.Add(new InformationModel()
                {
                    Product_ID = 1,
                    ProductInformation = "HEHE"
                });
                values.Add(new InformationModel()
                {
                    Product_ID = 1,
                    ProductInformation = "SampWAHAHAle"
                });


                InformationRepeater.DataSource = values;
                InformationRepeater.DataBind();

            }
        }      

        protected void InformationRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "InformationCommand")
            {
                string id = e.CommandArgument.ToString();
                Label1.Text = id;
            }
        }

      
    }
}