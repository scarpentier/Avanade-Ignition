// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the _Default type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.WebControls;

    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           this.RefreshIncompleteGrid();
           this.RefreshCompleteGrid();
        }

        protected void IncompleteQuoteGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var quoteId = IncompleteQuoteGridView.DataKeys[index].Value;

            if (e.CommandName == "EditIncomplete")
            {
                Response.Redirect(string.Format("Create.aspx?id={0}", quoteId.ToString()));
            
            }
            else if (e.CommandName == "DeleteIncomplete")
            {
                var db = new QuotesDBEntities();
                var quote = db.Quotes.Single(q => q.ID == (int)quoteId);
                db.DeleteObject(quote);
                db.SaveChanges();
                this.RefreshIncompleteGrid();
            }
        }

        protected void CompletedQuoteGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var quoteId = CompletedQuotesGridView.DataKeys[index].Value;

            if (e.CommandName == "CopyQuote")
            {
                copyExistingQuote((int)quoteId);
                Response.Redirect(string.Format("Create.aspx?id={0}", quoteId));
            }
            else if (e.CommandName == "OpenQuote")
            {
                Response.Redirect(string.Format("Finish.aspx?id={0}", quoteId));
            }
        }

        private void RefreshIncompleteGrid()
        {
            var userId = (Guid)Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey;
            var db = new QuotesDBEntities();

            var quotes = from u in db.vw_Quotes where u.Incomplete && u.UserId == userId select u;

            IncompleteQuoteGridView.DataSource = quotes;
            IncompleteQuoteGridView.DataBind();
        }

        private void RefreshCompleteGrid()
        {
            var userId = (Guid)Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey;
            var db = new QuotesDBEntities();

            var quotes = from u in db.vw_Quotes where !u.Incomplete && u.UserId == userId select u;

            CompletedQuotesGridView.DataSource = quotes;
            CompletedQuotesGridView.DataBind();
        }

        private void copyExistingQuote(int id)
        {
            var db = new QuotesDBEntities();
            var oldQuote = db.Quotes.Single(q => q.ID == id);

            var newQuoteId = CopyQuoteInfo(oldQuote);
            CopyDrivers(oldQuote, newQuoteId);
            CopyVehicles(oldQuote, newQuoteId);

            Response.Redirect(string.Format("Create.aspx?id={0}", newQuoteId));
        }

        private void CopyDrivers(Quote oldQuote, int newQuoteId)
        {
            var db = new QuotesDBEntities();

            var driverList = db.Drivers.Select(d => d).Where(d => d.QuoteID == oldQuote.ID);
            foreach (var oldDriver in driverList)
            {
                var newDriver = new Driver
                {
                    FirstName = oldDriver.FirstName,
                    LastName = oldDriver.LastName,
                    Ssn = oldDriver.Ssn,
                    DateOfBirth = oldDriver.DateOfBirth,
                    DriverLicenseNumber = oldDriver.DriverLicenseNumber,
                    DLState = oldDriver.DLState,
                    SafeDrivingSchool = oldDriver.SafeDrivingSchool,
                    QuoteID = newQuoteId
                };

                db.Drivers.AddObject(newDriver);
            }

            db.SaveChanges();
        }

        private void CopyVehicles(Quote oldQuote, int newQuoteId)
        {
            var db = new QuotesDBEntities();
            var vehicleList = db.Vehicles.Select(d => d).Where(v => v.QuoteId == oldQuote.ID);

            foreach (var vehicle in vehicleList)
            {
                // TODO: Find a better way to copy the primary driver info than compare the driver license number

                // Find the driver's licence number of the primary driver of the original vehicle
                var a = db.Drivers.Single(d => d.ID == vehicle.PrimaryDriver).DriverLicenseNumber;

                // Find the ID of the driver in the new quote that has the same licence number as the original one
                var b = db.Quotes.Single(q => q.ID == newQuoteId).Drivers.Single(d => d.DriverLicenseNumber == a).ID;

                var newVehicle = new Vehicle
                    {
                        Vin = vehicle.Vin,
                        Make = vehicle.Make,
                        Model = vehicle.Model,
                        Year = vehicle.Year,
                        CurrentValue = vehicle.CurrentValue,
                        PrimaryDriver = b,
                        AnnualMileage = vehicle.AnnualMileage,
                        DaysDrivenPerWeek = vehicle.DaysDrivenPerWeek,
                        MilesDrivenToWork = vehicle.MilesDrivenToWork,
                        AntiTheft = vehicle.AntiTheft,
                        AntiLock = vehicle.AntiLock,
                        DaytimeRunningLights = vehicle.DaytimeRunningLights,
                        DifferentGarageAddress = vehicle.DifferentGarageAddress,
                        PassiveRestraints = vehicle.PassiveRestraints,
                        ReducedUsedDiscount = vehicle.ReducedUsedDiscount,
                        QuoteId = newQuoteId
                    };
                db.Vehicles.AddObject(newVehicle);
            }

            db.SaveChanges();
        }

        private int CopyQuoteInfo(Quote oldQuote)
        {
            var db = new QuotesDBEntities();
            var newQuote = new Quote
            {
                UserId = (Guid)Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey,
                CustomerFirstName = oldQuote.CustomerFirstName,
                CustomerLastName = oldQuote.CustomerLastName,
                Address = oldQuote.Address,
                City = oldQuote.City,
                StateId = oldQuote.StateId,
                Ssn = oldQuote.Ssn,
                Zip = oldQuote.Zip,
                DateOfBirth = oldQuote.DateOfBirth,
                DateCreated = DateTime.Now,
                Incomplete = true,
                ClaimsInPast5Yrs = oldQuote.ClaimsInPast5Yrs,
                ForceMultiCarDiscount = oldQuote.ForceMultiCarDiscount,
                HasLessThan3YrsDriving = oldQuote.HasLessThan3YrsDriving,
                HasMovingViolations = oldQuote.HasMovingViolations
            };
            db.Quotes.AddObject(newQuote);
            db.SaveChanges();
            return newQuote.ID;

        }

    }
}
