// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreviousCarrierIsPervasive.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: previous carrier is Pervasive State Ins
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace Web.Discounts.Quote
{
    using Quote = Web.Quote;

    /// <summary>
    /// Class for discount rule: previous carrier is Pervasive State Ins
    /// </summary>
    public class PreviousCarrierIsPervasive : IDiscount
    {
        private int discountId;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">Discount ID of rule as listed in the Discount table</param>
        public PreviousCarrierIsPervasive(int id)
        {
            this.discountId = id;
        }

        public void Run(Quote quote)
        {
            var db = new QuotesDBEntities();
            if (quote.PreviousCarrier != null)
            if (quote.PreviousCarrier.ToLower().Contains("pervasive"))
            {

                var stateId = quote.StateId;
                var discount = db.DiscountPerStates.Single(d => d.DiscountId == discountId && d.StateId == stateId);
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