using NPVCalculator.Interfaces;
using System.Collections.Generic;

namespace NPVCalculator.Objects
{
    public class NPV : INPV
    {
        /// <inheritdoc/>
        public IList<NpvAndDiscountFactor> IndividualNPVsAndDiscountFactors { get; } = [];

        /// <inheritdoc/>
        public decimal TotalNPV
        {
            get
            {
                decimal total = 0;

                foreach (var npv in IndividualNPVsAndDiscountFactors)
                    total += npv.Npv;

                return total;
            }
        }

        /// <inheritdoc/>
        public void Add(decimal npv, decimal? discountFactor) => IndividualNPVsAndDiscountFactors.Add(new NpvAndDiscountFactor(npv, discountFactor));
    }
}
