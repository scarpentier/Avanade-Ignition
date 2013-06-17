// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReduceUseDiscount.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: Vehicle is not used often.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Vehicle
{
    using System.Linq;

    using Quote = Web.Quote;

    /// <summary>
    /// Class for discount rule: Vehicle is not used often. 
    /// </summary>
    public class ReduceUseDiscount : IDiscount
    {
        private readonly int discountId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReduceUseDiscount"/> class. 
        /// </summary>
        /// <param name="id">
        /// The ID of this rule as listed in the Discount table
        /// </param>
        public ReduceUseDiscount(int id)
        {
            this.discountId = id;
        }

        /// <summary>
        /// This function applies the rule to a given quote.
        /// </summary>
        /// <param name="quote">The quote object that the rule should be applied to</param>
        public void Run(Quote quote)
        {
            var db = new QuotesDBEntities();
            
            foreach (var vehicle in quote.Vehicles)
            {
                if (vehicle.ReducedUsedDiscount)
                {
                    var vehicleOwner = db.Drivers.Single(p => p.ID == vehicle.PrimaryDriver);
                    var discount = db.DiscountPerStates.Single(d => d.DiscountId == this.discountId && d.StateId == vehicleOwner.DLState);
                    var appliedDiscount = new VehicleDiscount
                                              {
                                                  DiscountId = discount.DiscountId,
                                                  StateId = vehicleOwner.DLState,
                                                  VehicleId = vehicle.ID,
                                                  AppliedDiscountValue = discount.Amount
                                              };

                    db.AddToVehicleDiscounts(appliedDiscount);
                }
            }

            db.SaveChanges();
        }
    }
}