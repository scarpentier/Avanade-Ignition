// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDiscount.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Discount Interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Discounts
{
    /// <summary>
    /// Discount Interface
    /// </summary>
    public interface IDiscount
    {
        void Run(Web.Quote quote);
    }
}
