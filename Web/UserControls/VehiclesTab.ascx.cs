// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VehiclesTab.ascx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the VehiclesTab type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.UserControls
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class VehiclesTab : System.Web.UI.UserControl
    {
        public Quote Quote { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucVehicleInfo.SaveComplete += new EventHandler(ucVehicleInfo_SaveComplete);
        }

        private enum VehicleView
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


        void ucVehicleInfo_SaveComplete(object sender, EventArgs e)
        {
            this.ShowView(VehicleView.Grid);
            this.gvVehicles.Visible = true;
        }


        public void LoadQuote(Quote quote)
        {
            this.Quote = quote;
            this.RefreshGrid();
        }

        /// <summary>
        /// Refreshes the grid with previously entered vehicles
        /// </summary>
        private void RefreshGrid()
        {
            var db = new QuotesDBEntities();
            Quote = db.Quotes.Single(i => i.ID == Quote.ID);

            gvVehicles.DataSource = Quote.Vehicles;
            gvVehicles.DataBind();
        }

        /// <summary>
        /// Edits or deletes vehicle data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var vehicleId = (int)gvVehicles.DataKeys[index].Value;

            var db = new QuotesDBEntities();
            var vehicle = db.Vehicles.Single(i => i.ID == vehicleId);

            if (e.CommandName == "EditVehicle")
            {
                Button1.Visible = false;
                gvVehicles.Visible = false;
                ucVehicleInfo.Read(vehicle);
                ucVehicleInfo.Visible = true;
                ucVehicleInfo.PrimaryDriver();
            }
            else if (e.CommandName == "DeleteVehicle")
            {
                db.Vehicles.DeleteObject(vehicle);
                db.SaveChanges();

                this.RefreshGrid();

            }
        }

        /// <summary>
        /// Switches the view between master/detail
        /// </summary>
        /// <param name="view">Enum value of the view</param>
        private void ShowView(VehicleView view)
        {
            switch (view)
            {
                case VehicleView.Grid:
                    // Show grid
                    gvVehicles.Visible = true;
                    Button1.Visible = true;

                    // Refresh grid
                    this.RefreshGrid();
                    
                    // Hide details
                    ucVehicleInfo.Visible = false;
                    break;

                case VehicleView.Details:
                    // Hide grid
                    gvVehicles.Visible = false;

                    // Show details
                    Button1.Visible = false;
                    ucVehicleInfo.Visible = true;
                    break;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.ShowView(VehicleView.Details);
            ucVehicleInfo.Create(Quote);
            ucVehicleInfo.PrimaryDriver();
        }

        
    }
}