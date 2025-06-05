using NPVCalculator.Objects;
using System.Collections.Generic;

namespace NPVCalculator.Interfaces
{
    public interface INPV
    {
        /// <summary>
        /// List of individual NPVs and disount factors for the overall NPV.
        /// </summary>
        IList<NpvAndDiscountFactor> IndividualNPVsAndDiscountFactors { get; }

        /// <summary>
        /// Total NET Present Value (NPV).
        /// </summary>
        decimal TotalNPV { get; }

        /// <summary>
        /// Add an individual NPV and its discount factor to the NPV and discount factor list.
        /// </summary>
        /// <param name="npv">NPV value.</param>
        /// <param name="discountFactor">Discount factor (null if zero / not applicable).</param>
        void Add(decimal npv, decimal? discountFactor);
    }
}
