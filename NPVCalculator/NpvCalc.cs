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
        public INPVs GetNPVs(decimal initialInvestment)
        {
            var discountRatioDivider = 1 + (DiscountRate / 100);

            var npvs = new NPVs();
            npvs.Add(new KeyValuePair<decimal, decimal?>(-initialInvestment, null));

            for (var i = 0; i < NCFs.Count; i++)
            {
                var discountingFactor = 1 / (decimal)Math.Pow(discountRatioDivider, i + 1);
                var npv = decimal.Round(NCFs[i] * discountingFactor, NPVDecimalPlaces, MidpointRounding.AwayFromZero);

                npvs.Add(new KeyValuePair<decimal, decimal?>(npv, decimal.Round(discountingFactor,
                    DiscountingFactorDecimalPlaces, MidpointRounding.AwayFromZero)));
            }

            return npvs;
        }
    }
}
