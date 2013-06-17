// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Claim.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Class for discount rule: Customer has claims in the past 5 years
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts.Quote
{
    using System.Linq;

    using Quote = Web.Quote;

    /// <summary>
    /// Class for discount rule: Customer has claims in the past 5 years
    /// </summary>
    public class Claim:IDiscount
    {

        private int discountId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">discount id of the claim rule as listed in Discount table</param>
        public Claim(int id)
        {
            this.discountId = id;
        }

        public void Run(Quote quote)
        {
            var db = new QuotesDBEntities();
            if (quote.ClaimsInPast5Yrs)
            {
                var stateId = quote.StateId;
                var discount =
                    db.DiscountPerStates.Single(
                        d => d.DiscountId == discountId && d.StateId == stateId);

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