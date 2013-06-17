// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DriverAge.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: Driver is under 23 years old
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Driver
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class for discount rule: Driver is under 23 years old
    /// </summary>
    public class DriverAge:IDiscount
    {
        private int discountId;

        /// <summary>
        /// Constructor for DriverAge class
        /// </summary>
        /// <param name="id">The ID of the underage driver discount rule as listed in the Discount table</param>
        public DriverAge(int id)
        {
            this.discountId = id;
        }

        public void Run(Web.Quote quote)
        {
            var db = new QuotesDBEntities();
            
            foreach (var driver in quote.Drivers)
            {
               
                if (driver.DateOfBirth.AddYears(23) > DateTime.Now)
                {
                    var discount = db.DiscountPerStates.Single(d => d.DiscountId == discountId && d.StateId == driver.DLState);
                    var appliedDiscountValue = new DriverDiscount()
                        {
                            DiscountId = this.discountId,
                            StateId = driver.DLState,
                            DriverId = driver.ID,
                            AppliedDiscountValue = discount.Amount
                        };
                    db.AddToDriverDiscounts(appliedDiscountValue);
                }
            }

            db.SaveChanges();
        }
    }
}