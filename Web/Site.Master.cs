// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Site.Master.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the SiteMaster type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("Manager"))
            {
                var db = new QuotesDBEntities();
                var url = db.Configs.Single(c => c.Name == "SharePointListURL").Value;

                var manageQuotes = new MenuItem("Manage Quotes", null, null, url);
                var manageUsers = new MenuItem("Manage Users", null, null, "~/Admin/ManageUsers.aspx");
                NavigationMenu.Items.Add(manageQuotes);
                NavigationMenu.Items.Add(manageUsers);
            }
        }
    }
}
