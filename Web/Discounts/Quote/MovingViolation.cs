using System.Linq;

namespace Web.Discounts.Quote
{
    using Quote = Web.Quote;

    /// <summary>
    /// Class for discount rule: Customer has no moving violations in past 5 years
    /// </summary>
    public class MovingViolation: IDiscount
    {
        private int discountId;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of discount rule as listed in Discount table</param>
        public MovingViolation(int id)
        {
            this.discountId = id;
        }
        public void Run(Quote quote)
        {
            var db = new QuotesDBEntities();

            if (quote.HasMovingViolations)
            {
                var stateId = (int) db.Drivers.First().DLState;
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