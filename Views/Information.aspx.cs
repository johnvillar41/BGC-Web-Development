using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sample code for the use of repeater
            if (!IsPostBack)
            {
                DisplayInformationTable();
            }
        }

        protected void InformationRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "InformationCommand")
            {
                //TODO
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("AddInformation.aspx?id=" + id);
            }
        }

        private async void DisplayInformationTable()
        {
            var informations = await InformationRepository.GetInstance().FetchInformations();

            InformationRepeater.DataSource = informations;
            InformationRepeater.DataBind();
        }


    }
}