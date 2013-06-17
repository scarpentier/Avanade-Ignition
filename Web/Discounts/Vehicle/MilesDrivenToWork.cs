// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MilesDrivenToWork.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: Miles driven to work less than or equal to 25
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Vehicle
{
    using System.Linq;

    /// <summary>
    /// Class for discount rule: Miles driven to work less than or equal to 25
    /// </summary>
    public class MilesDrivenToWork : IDiscount
    {
        private int discountId;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">Discount ID for discount rule as listed in the Discount table</param>
        public MilesDrivenToWork(int id)
        {
            this.discountId = id;
        }

        public void Run(Web.Quote quote)
        {
            var db = new QuotesDBEntities();
            
            foreach (var vehicle in quote.Vehicles)
            {
                if (vehicle.MilesDrivenToWork < 25) //this is as in old application. doesn't correspond to excel document. 
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