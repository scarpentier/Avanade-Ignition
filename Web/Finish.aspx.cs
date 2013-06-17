// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Finish.aspx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Page that processes the Quotes and shows the final price breakdown
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
//    using Microsoft.SharePoint.Client;
//    using ListItem = Microsoft.SharePoint.Client.ListItem;

    /// <summary>
    /// Page that processes the Quotes and shows the final price breakdown
    /// </summary>
    public partial class Finish : System.Web.UI.Page
    {
        /// <summary>
        /// Gets the quote.
        /// </summary>
        public Quote Quote { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                var id = int.Parse(Request.QueryString["id"]);
                this.LoadExistingQuote(id);
            }
        }

        /// <summary>
        /// Processes a quote
        /// </summary>
        /// <param name="id">The ID of the quote to process</param>
        private void LoadExistingQuote(int id)
        {
            var db = new QuotesDBEntities();
            this.Quote = db.Quotes.Single(i => i.ID == id);

            this.ShowCustomerInformation();
            this.ShowPriceSummary();
        }

        private void ShowCustomerInformation()
        {
            lblClientName.Text = string.Format("{0} {1}", Quote.CustomerFirstName, Quote.CustomerLastName);
            lblCity.Text = Quote.City;
            lblState.Text = Quote.State.StateName;
        }

        private void ShowPriceSummary()
        {
            var engine = new Discounts.DiscountsEngine(Quote);

            var output = new StringBuilder();

            output.Append(
                string.Format("<table cellpadding=3 cellspacing=3><tr><td>Policy base price: </td><td>${0:####0.00}</td></tr>", engine.BasePolicyPrice));

            foreach (var driver in Quote.Drivers)
            {
                var discountString = string.Empty;
                foreach (var discount in driver.DriverDiscounts)
                {
                    discountString += string.Format("{0} ({1}%)&#013;", discount.DiscountPerState.Discount.DiscountName, discount.AppliedDiscountValue);
                }
                discountString += string.Format("TOTAL: {0}%", driver.DriverDiscounts.Sum(d => d.AppliedDiscountValue));

                output.Append(
                    string.Format(
                        "<tr><td>Driver: {0} {1}:</td><td>${2:####0.00}</td><td><img valign='bottom' src='Styles/help.png' title='{3}' /></td></tr>", driver.FirstName, driver.LastName, engine.PriceDriver(driver), discountString));
            }

            foreach (var vehicle in Quote.Vehicles)
            {
                var discountString = string.Empty;
                foreach (var discount in vehicle.VehicleDiscounts)
                {
                    discountString += string.Format("{0} ({1}%)&#013;", discount.DiscountPerState.Discount.DiscountName, discount.AppliedDiscountValue);
                }
                discountString += string.Format("TOTAL: {0}%", vehicle.VehicleDiscounts.Sum(d => d.AppliedDiscountValue));

                output.Append(
                    string.Format(
                        "<tr><td>Vehicle: {0} {1} {2}:</td><td>${3:####0.00}</td><td><img valign='bottom' src='Styles/help.png' title='{4}' /></td></tr>",
                        vehicle.Make,
                        vehicle.Model,
                        vehicle.Year,
                        engine.PriceVehicle(vehicle),
                        discountString));
            }

            var quoteDiscountString = string.Empty;
            foreach (var discount in Quote.QuoteDiscounts)
            {
                quoteDiscountString += string.Format("{0} ({1}%)&#013;", discount.DiscountPerState.Discount.DiscountName, discount.AppliedDiscountValue);
            }
            quoteDiscountString += string.Format("TOTAL: {0}%", Quote.QuoteDiscounts.Sum(d => d.AppliedDiscountValue));

            output.Append(
                string.Format("<tr><td>Quote adjustments:</td><td>${0:####0.00}</td><td><img valign='bottom' src='Styles/help.png' title='{1}' /></td></tr>", engine.GetQuoteAdjustment(), quoteDiscountString));

            output.Append("</table>");

            Literal1.Text = output.ToString();

            lblTotal.Text = string.Format("Total policy price: ${0:####0.00}", engine.GetTotalPrice());
        }
    }
}