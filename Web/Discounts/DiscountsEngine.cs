// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscountsEngine.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the DiscountsEngine type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Web.Discounts.Driver;
    using Web.Discounts.Quote;
    using Web.Discounts.Vehicle;

    /// <summary>
    /// Global discount class to execute discount rules
    /// </summary>
    public class DiscountsEngine
    {
        /// <summary>
        /// List of discounts to apply
        /// </summary>
        private readonly List<IDiscount> discounts = new List<IDiscount>();

        #region Discount ID declarations

        private const int DayTimeRunningLightsId = 1;

        private const int AntiLockBrakesId = 2;

        private const int AnnualMileageId = 3;

        private const int PassiveRestraintsId = 4;

        private const int AntiTheftId = 5;

        private const int DaysDriverPerWeekId = 6;

        private const int MilesDrivenToWorkId = 7;

        private const int ReduceUseDiscountId = 8;

        private const int GarageAddressId = 9;

        private const int CustomerDrivingExperienceId = 10;

        private const int PreviousCarrierIsLizardId = 11;

        private const int PreviousCarrierIsPervasiveId = 12;

        private const int MovingViolationId = 13;

        private const int ClaimId = 14;

        private const int MultiCarId = 15;

        private const int DriverAgeId = 16;

        private const int AttendedSafeDrivingSchoolId = 17;

        #endregion

        #region Config item ID declarations

        private const int VehicleBasePriceMultiplierId = 3;

        private const int QuoteBasePriceId = 1;

        private const int DriverBasePriceId = 2;

        #endregion

        private QuotesDBEntities Db = new QuotesDBEntities();

        public decimal BasePolicyPrice { get; private set; }

        public decimal BaseDriverPrice { get; private set; }

        public decimal BaseVehicleMultiplier { get; private set; }

        public global::Web.Quote Quote { get; private set; }

        public DiscountsEngine(global::Web.Quote quote)
        {
            this.Quote = quote;
            this.GetDiscounts();

            BasePolicyPrice = decimal.Parse(Db.Configs.Single(conf => conf.ID == QuoteBasePriceId).Value, new CultureInfo("en-US"));
            BaseDriverPrice = decimal.Parse(Db.Configs.Single(conf => conf.ID == DriverBasePriceId).Value, new CultureInfo("en-US"));
            BaseVehicleMultiplier = decimal.Parse(Db.Configs.Single(conf => conf.ID == VehicleBasePriceMultiplierId).Value, new CultureInfo("en-US"));

            if (Quote.Incomplete)
            {
                this.ProcessDiscounts();

                var db = new QuotesDBEntities();
                var q = db.Quotes.Single(i => i.ID == Quote.ID);
                q.Incomplete = false;
                q.Price = this.GetTotalPrice();
                db.SaveChanges();

                Quote = q;

                // Add to sharepoint
                try
                {
                    this.SendQuoteToSharePoint();
                }
                catch (Exception ex)
                {
                    // TODO: Handle that
                }
            }
        }

        /// <summary>
        /// Adds all the discounts to the list of discount to be processed
        /// </summary>
        private void GetDiscounts()
        {
            // Vehicle scope
            this.discounts.Add(new DayTimeRunningLights(DayTimeRunningLightsId));
            this.discounts.Add(new AntiLockBrakes(AntiLockBrakesId));
            this.discounts.Add(new AnnualMileage(AnnualMileageId));
            this.discounts.Add(new PassiveRestraints(PassiveRestraintsId));
            this.discounts.Add(new AntiTheft(AntiTheftId));
            this.discounts.Add(new DaysDrivenPerWeek(DaysDriverPerWeekId));
            this.discounts.Add(new MilesDrivenToWork(MilesDrivenToWorkId));
            this.discounts.Add(new ReduceUseDiscount(ReduceUseDiscountId));
            this.discounts.Add(new GarageAddress(GarageAddressId));

            // Quote scope
            this.discounts.Add(new CustomerDrivingExperience(CustomerDrivingExperienceId));
            this.discounts.Add(new PreviousCarrierIsLizard(PreviousCarrierIsLizardId));
            this.discounts.Add(new PreviousCarrierIsPervasive(PreviousCarrierIsPervasiveId));
            this.discounts.Add(new MovingViolation(MovingViolationId));
            this.discounts.Add(new Claim(ClaimId));
            this.discounts.Add(new MultiCar(MultiCarId));

            // Person scope
            this.discounts.Add(new DriverAge(DriverAgeId));
            this.discounts.Add(new AttendedSafeDrivingSchool(AttendedSafeDrivingSchoolId));
        }

        /// <summary>
        /// Apply the discounts to the quote
        /// </summary>
        /// <remarks>Only do this for incomplete quotes</remarks>
        private void ProcessDiscounts()
        {
            foreach (var discount in this.discounts)
            {
                discount.Run(Quote);
            }
        }

        /// <summary>
        /// Returns the price for a driver
        /// </summary>
        /// <param name="driver">The driver to quote</param>
        /// <returns>Amount of money</returns>
        public decimal PriceDriver(global::Web.Driver driver)
        {
            decimal discountMultiplier = 1;
            var driverDiscountList = Db.DriverDiscounts.Where(d => d.DriverId == driver.ID);

            foreach (var driverDiscount in driverDiscountList)
            {
                discountMultiplier *= (1 + driverDiscount.AppliedDiscountValue / 100);
            }

            return BaseDriverPrice * discountMultiplier;
        }

        /// <summary>
        /// Returns the price for a vehicle
        /// </summary>
        /// <param name="vehicle">The vechicle to quote</param>
        /// <returns>Amount of money</returns>
        public decimal PriceVehicle(global::Web.Vehicle vehicle)
        {
            decimal discountMultiplier = 1;
            var vehicleDiscountList = Db.VehicleDiscounts.Where(v => v.VehicleId == vehicle.ID);
            foreach (var vehicleDiscount in vehicleDiscountList)
            {
                discountMultiplier *= (1 + vehicleDiscount.AppliedDiscountValue / 100);
            }
           
            // add the primary driver multiplier
            var primaryDriver = Db.Drivers.Single(d => d.ID == vehicle.PrimaryDriver);
            discountMultiplier *= this.PriceDriver(primaryDriver) / BaseDriverPrice;
            
            // Vehicle current value may be null
            if (vehicle.CurrentValue != null)
            {
                var vehicleBasePrice = vehicle.CurrentValue * this.BaseVehicleMultiplier / 100;

                return (decimal)vehicleBasePrice * discountMultiplier; 
                
            }

            return 0;
        }

        public decimal GetQuotePriceBeforeAdjustement()
        {
            var total = BasePolicyPrice;

            total += this.Quote.Drivers.Sum(driver => this.PriceDriver(driver));

            total += this.Quote.Vehicles.Sum(vehicle => this.PriceVehicle(vehicle));

            return total;
        }

        /// <summary>
        /// Gets the total price of the quote
        /// </summary>
        /// <returns>Amount of money</returns>
        public decimal GetTotalPrice()
        {
            var total = this.GetQuotePriceBeforeAdjustement();
            decimal quoteMultiplier = 1;
            foreach (var quoteDiscount in this.Quote.QuoteDiscounts)
            {
                quoteMultiplier *= (1 + quoteDiscount.AppliedDiscountValue / 100);
            }

            return total * quoteMultiplier;
        }

        public decimal GetQuoteAdjustment()
        {
            return this.GetTotalPrice() - this.GetQuotePriceBeforeAdjustement();
        }

        /// <summary>
        /// Saves the quote to SharePoint
        /// </summary>
        /// <remarks>Uses the Client Object Model</remarks>
        private void SendQuoteToSharePoint()
        {
            // This was used to send the quote to a SharePoint list. We have disabled that part to avoid the need of a full SharePoint server on the demo machine

            //var db = new QuotesDBEntities();

            //var clientContext = new ClientContext(db.Configs.Single(c => c.Name == "SharePointURL").Value);
            //var list = clientContext.Web.Lists.GetByTitle("Quotes");

            //var item = list.AddItem(new ListItemCreationInformation());

            //item["Title"] = Quote.ID;
            //item["FirstName"] = Quote.CustomerFirstName;
            //item["LastName"] = Quote.CustomerLastName;
            //item["StateCode"] = Quote.State.StateCode;
            //item["Price"] = Quote.Price.Value.ToString(new CultureInfo("en-US"));
            //item["DateCreated"] = Quote.DateCreated;
            //item["Drivers"] = Quote.Drivers.Count;
            //item["Vehicles"] = Quote.Vehicles.Count;
            //item["AgentName"] = Quote.aspnet_Users.UserName;

            //item.Update();

            //clientContext.ExecuteQuery();
        }
    }
}