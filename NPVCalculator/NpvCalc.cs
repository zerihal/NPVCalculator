using NPVCalculator.Interfaces;
using NPVCalculator.Objects;
using System;
using System.Collections.Generic;

namespace NPVCalculator
{
    public class NpvCalc
    {
        #region Properties

        /// <summary>
        /// List of NET Cash Flow (NCF) values.
        /// </summary>
        public IList<decimal> NCFs { get; set; }

        /// <summary>
        /// Discount rate (%) to use for NPV calculation.
        /// </summary>
        public double DiscountRate { get; set; }

        /// <summary>
        /// Discounting factor decimal places (for rounding).
        /// </summary>
        public int DiscountingFactorDecimalPlaces { get; set; } = 2;

        /// <summary>
        /// NPV decimal places (for rounding).
        /// </summary>
        public int NPVDecimalPlaces { get; set; } = 0;

        /// <summary>
        /// If set to true, the calculation for NPV uses the rounded discounting factor value rather than the raw
        /// value (default is <see langword="false"/>).
        /// </summary>
        public bool UseRoundedDiscountingFactor { get; set; } = false;

        #endregion

        #region Construction

        public NpvCalc()
        {
            NCFs = new List<decimal>();
        }

        public NpvCalc(IList<decimal> nCFs, double discountRate) : this()
        {
            NCFs = nCFs;
            DiscountRate = discountRate;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the overall NPV (comprised of individual NPVs for each NCF) based on the NCFs and discount rate
        /// that has been set in this instance of calculator against the intial investment amount.
        /// </summary>
        /// <param name="initialInvestment">Initial investment amount (use positive number).</param>
        /// <returns><see cref="INPV"/> instance with overall and individual NPVs.</returns>
        public INPV GetNPVs(decimal initialInvestment)
        {
            var discountRatioDivider = 1 + (DiscountRate / 100);

            var npvs = new NPV();
            npvs.Add(-initialInvestment, null);

            for (var i = 0; i < NCFs.Count; i++)
            {
                var discountingFactor = 1 / (decimal)Math.Pow(discountRatioDivider, i + 1);
                var roundedDiscountingFactor = decimal.Round(discountingFactor, DiscountingFactorDecimalPlaces, MidpointRounding.AwayFromZero);
                
                var npv = decimal.Round(NCFs[i] * (UseRoundedDiscountingFactor ? roundedDiscountingFactor : discountingFactor), 
                    NPVDecimalPlaces, MidpointRounding.AwayFromZero);

                npvs.Add(npv, roundedDiscountingFactor);
            }

            return npvs;
        }

        #endregion
    }
}
