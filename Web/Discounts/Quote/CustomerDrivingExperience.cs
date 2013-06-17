// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerDrivingExperience.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: "Customer has under 3 years driving experience
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Quote
{
    using System.Linq;

    using Quote = Web.Quote;

    /// <summary>
    /// Class for discount rule: "Customer has under 3 years driving experience
    /// </summary>
    public class CustomerDrivingExperience:IDiscount
    {
        private int discountId;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">ID for the discount rule as listed in the Discount table</param>
        public CustomerDrivingExperience(int id)
        {
            this.discountId = id;
        }


        public void Run(Quote quote)
        {
            var db = new QuotesDBEntities();

            if (quote.HasLessThan3YrsDriving)
            {
                var stateId = quote.StateId;
                var discount = db.DiscountPerStates.Single(d => d.DiscountId == this.discountId && d.StateId == stateId);
                var appliedDiscountValue = new QuoteDiscount()
                    {
                        DiscountId = this.discountId,
                        StateId = stateId,
                        QuoteId = quote.ID,
                        AppliedDiscountValue = discount.Amount
                    };
                db.AddToQuoteDiscounts(appliedDiscountValue);
            }

            db.SaveChanges();

        }


    }
}