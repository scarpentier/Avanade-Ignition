// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttendedSafeDrivingSchool.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: Driver has attended safe driving school
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Driver
{
    using System.Linq;

    /// <summary>
    /// Class for discount rule: Driver has attended safe driving school
    /// </summary>
    public class AttendedSafeDrivingSchool : IDiscount
    {
        private int discountId;

        /// <summary>
        /// Constructor for AttendedSafeDrivingSchool class
        /// </summary>
        /// <param name="id">The id of the rule as listed in the Discount table</param>
        public AttendedSafeDrivingSchool(int id)
        {
            this.discountId = id;
        }


        public void Run(Web.Quote quote)
        {
            var db = new QuotesDBEntities();
           
            foreach (var driver in quote.Drivers)
            {
                if (driver.SafeDrivingSchool)
                {
                    var discount = db.DiscountPerStates.Single(d => d.DiscountId == discountId && d.StateId == driver.DLState);
                    var appliedDiscount = new DriverDiscount()
                        {
                            DriverId = driver.ID,
                            StateId = (int) driver.DLState,
                            DiscountId = discount.DiscountId,
                            AppliedDiscountValue = discount.Amount
                        };
                    db.AddToDriverDiscounts(appliedDiscount);
                }
            }

            db.SaveChanges();
        }
    }
}