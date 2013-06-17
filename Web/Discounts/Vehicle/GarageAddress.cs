// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GarageAddress.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: Garage address is different than residence
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Vehicle
{
    using System.Linq;

    /// <summary>
    /// Class for discount rule: Garage address is different than residence
    /// </summary>
    public class GarageAddress:IDiscount
    {
        private int discountId;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">Discount ID for discount rule as listed in the Discount table</param>
        public GarageAddress(int id)
        {
            this.discountId = id;
        }

        public void Run(Web.Quote quote)
        {
            var db = new QuotesDBEntities();
            
            foreach (var vehicle in quote.Vehicles)
            {
                if (vehicle.DifferentGarageAddress)
                {
                    var stateId = (int) db.Drivers.Single(p => p.ID == vehicle.PrimaryDriver).DLState;
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


