using NPVCalculator.Objects;
using System.Collections.Generic;

namespace NPVCalculator.Interfaces
{
    public interface INPV
    {
        /// <summary>
        /// 
        /// </summary>
        IList<NpvAndDiscountFactor> IndividualNPVsAndDiscountFactors { get; }

        /// <summary>
        /// 
        /// </summary>
        decimal TotalNPV { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="npvAndDiscountFactor"></param>
        void Add(decimal npv, decimal? discountFactor);
    }
}
