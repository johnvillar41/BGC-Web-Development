using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SoftEngWebEmployee.Helpers
{
    public class SweetAlertBuilder
    {
        public string HexaBackgroundColor { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Constants.AlertStatus AlertIcons { get; set; }
        public string AlertPositions { get; set; }
        public int? Timer { get; set; }
        public bool ShowConfirmationDialog { get; set; }
        public string FooterMessage { get; set; }
        public bool ShowCloseButton { get; set; }

        public void BuildSweetAlert(Page page)
        {
            if (Timer != null)
            {
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "Swal.fire({" +
                   "background:'" + HexaBackgroundColor +
                   "',title:'" + Title + "'," +
                   "text:'" + Message + "'," +
                   "showCloseButton:'" + ShowCloseButton + "'," +
                   "footer:'" + FooterMessage + "'," +
                   "timer:'" + Timer + "'," +
                   "position:'" + AlertPositions + "'," +
                   "icon:'" + AlertIcons.ToString() +
                   "'})", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "Swal.fire({" +
                  "background:'" + HexaBackgroundColor +
                  "',title:'" + Title + "'," +
                  "showCloseButton:'" + ShowCloseButton + "'," +
                  "text:'" + Message + "'," +
                  "position:'" + AlertPositions + "'," +
                  "footer:'" + FooterMessage + "'," +
                  "icon:'" + AlertIcons.ToString() +
                  "'})", true);
            }

        }
    }
}