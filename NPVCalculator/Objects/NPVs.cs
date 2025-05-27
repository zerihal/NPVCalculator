using NPVCalculator.Interfaces;
using System.Collections.Generic;

namespace NPVCalculator.Objects
{
    public class NPVs : INPVs
    {
        /// <inheritdoc/>
        public IDictionary<decimal, decimal?> NPVsAndDiscountFactors { get; } = new Dictionary<decimal, decimal?>();

        /// <inheritdoc/>
        public decimal TotalNPV
        {
            get
            {
                decimal total = 0;

                foreach (var npv in NPVsAndDiscountFactors.Keys)
                    total += npv;

                return total;
            }
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<decimal, decimal?> npvAndDiscountFactor) => NPVsAndDiscountFactors.Add(npvAndDiscountFactor);
    }
}
