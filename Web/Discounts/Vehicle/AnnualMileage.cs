// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnnualMileage.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: The annual mileage of vehicle is less than 6,000 miles
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Vehicle
{
    using System.Linq;

    /// <summary>
    /// Class for discount rule: The annual mileage of vehicle is less than 6,000 miles
    /// </summary>
    public class AnnualMileage : IDiscount
    {
        private int discountId;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">Discount ID for discount rule as listed in the Discount table</param>
        public AnnualMileage(int id)
        {
            this.discountId = id;
        }

        public void Run(Web.Quote quote)
        {
            var db = new QuotesDBEntities();
            
            foreach (var vehicle in quote.Vehicles)
            {
                if (vehicle.AnnualMileage < 6000)
                {
                    var stateId = db.Drivers.Single(p => p.ID == vehicle.PrimaryDriver).DLState;
                    var discount =
                        db.DiscountPerStates.Single(d => d.DiscountId == this.discountId && d.StateId == stateId);
                    var appliedDiscountValue = new VehicleDiscount()
                        {
                            AppliedDiscountValue = discount.Amount,
                            DiscountId = this.discountId,
                            StateId = stateId,
                            VehicleId = vehicle.ID
                        };

                    db.AddToVehicleDiscounts(appliedDiscountValue);

                }
            }
            db.SaveChanges();

        }
    }
}