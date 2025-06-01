using NPVCalculator.Interfaces;
using NPVCalculator.Objects;
using System;
using System.Collections.Generic;

namespace NPVCalculator
{
    public class NpvCalc
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<decimal> NCFs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double DiscountRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DiscountingFactorDecimalPlaces { get; set; } = 2;

        /// <summary>
        /// 
        /// </summary>
        public int NPVDecimalPlaces { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public bool UseRoundedDiscountingFactor { get; set; } = false;

        public NpvCalc()
        {
            NCFs = new List<decimal>();
        }

        public NpvCalc(IList<decimal> nCFs, double discountRate) : this()
        {
            NCFs = nCFs;
            DiscountRate = discountRate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialInvestment"></param>
        /// <returns></returns>
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
    }
}
