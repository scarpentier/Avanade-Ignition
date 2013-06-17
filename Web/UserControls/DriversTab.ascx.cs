// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DriversTab.ascx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   UserControl shown inside the "Drivers" tab in Create.aspx
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    /// <summary>
    /// UserControl shown inside the "Drivers" tab in Create.aspx
    /// </summary>
    public partial class DriversTab : System.Web.UI.UserControl
    {
        /// <summary>
        /// Enum that determines the view to show
        /// </summary>
        private enum DriversView
        {
            /// <summary>
            /// Grid
            /// </summary>
            Grid,

            /// <summary>
            /// Details
            /// </summary>
            Details
        }

        /// <summary>
        /// Gets the quote object used to fill the datagrid
        /// </summary>
        public Quote Quote { get; private set; }

        /// <summary>
        /// Public method used to initialize the UserControl
        /// </summary>
        /// <param name="quote">Quote object linked to the drivers</param>
        public void LoadQuote(Quote quote)
        {
            this.Quote = quote;
            this.RefreshGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucDriverInfo.SaveComplete += new EventHandler(ucDriverInfo_SaveComplete);
       //     gvDrivers.Visible = true;
        }

        void ucDriverInfo_SaveComplete(object sender, EventArgs e)
        {
            this.ShowView(DriversView.Grid);
            this.RefreshGrid();
        }

        /// <summary>
        /// Refresh the content of the grid
        /// </summary>
        private void RefreshGrid()
        {
            var db = new QuotesDBEntities();

            Quote = db.Quotes.Single(i => i.ID == Quote.ID);

            var drivers = from d in db.vw_Drivers where d.QuoteID == Quote.ID select d;

            gvDrivers.DataSource = drivers;
            gvDrivers.DataBind();
        }

        /// <summary>
        /// Switches the view between master/detail
        /// </summary>
        /// <param name="view">Enum value of the view</param>
        private void ShowView(DriversView view)
        {
            switch (view)
            {
                case DriversView.Grid:
                    // Show grid
                    gvDrivers.Visible = true;
                    Button1.Visible = true;

                    // Refresh grid
                    this.RefreshGrid();
                    
                    // Hide details
                    ucDriverInfo.Visible = false;
                    break;

                case DriversView.Details:
                    // Hide grid
                    gvDrivers.Visible = false;

                    // Show details
                    Button1.Visible = false;
                    ucDriverInfo.Visible = true;
                    break;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.ShowView(DriversView.Details);
            ucDriverInfo.Create(Quote);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var driverId = (int)gvDrivers.DataKeys[index].Value;

            var db = new QuotesDBEntities();
            var driver = db.Drivers.Single(i => i.ID == driverId);

            if (e.CommandName == "EditDriver")
            {
                Button1.Visible = false;
                gvDrivers.Visible = false;
                ucDriverInfo.Read(driver);
                ucDriverInfo.Visible = true;
            }
            else if (e.CommandName == "DeleteDriver")
            {
                db.Drivers.DeleteObject(driver);
                db.SaveChanges();

                this.RefreshGrid();
            }
        }
    }
}