using System.Collections.Generic;

namespace NPVCalculator.Interfaces
{
    public interface INPVs
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<decimal, decimal?> NPVsAndDiscountFactors { get; }

        /// <summary>
        /// 
        /// </summary>
        decimal TotalNPV { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="npvAndDiscountFactor"></param>
        void Add(KeyValuePair<decimal, decimal?> npvAndDiscountFactor);
    }
}
