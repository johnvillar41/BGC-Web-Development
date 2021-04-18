using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SoftEngWebEmployee.Helpers
{
    public class SweetAlertBuilder
    {
        public static void BuildMessage(Page page, Constants.AlertStatus alertStatus, string title, string message)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "Swal.fire( '" + title + "','" + message + "', '" + alertStatus.ToString() + "')", true);
        }
        public static void BuildAlertGivenPosition(Page page, Constants.AlertStatus alertStatus, string alertPositions, string title, int timer)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "Swal.fire({position: '" + alertPositions + "', icon: '" + alertStatus.ToString() + "',title: '" + title + "',showConfirmButton: false, timer: " + timer + "})", true);
        }
        public static void BuildBasicAlert(Page page, string message)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "Swal.fire('" + message + "')", true);
        }
    }
}