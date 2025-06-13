using NPVCalculator.Interfaces;
using System;
using System.Collections.Generic;

namespace NPVCalculator
{
    public static class IRRCalc
    {
        /// <summary>
        /// Internal Rate of Return (IRR) calculator for a list of NCFs against a given initial investment amount. The IRR will
        /// be calculated between the min and max discount rate passed and incremented by the amount specified until the IRR is
        /// found (i.e. NPV closest to zero).
        /// </summary>
        /// <param name="ncfs">NET Present Value (NCF) list.</param>
        /// <param name="initialInvestment">Initial investment amount (use positive number).</param>
        /// <param name="minDiscountRate">Minimum discount rate.</param>
        /// <param name="maxDiscountRate">Maximum discount rate.</param>
        /// <param name="rateIncrement">Increment amount from min to max discount rate.</param>
        /// <param name="irrNpv"><see cref="INPV"/> instance for the IRR.</param>
        /// <param name="useRoundedDiscountFactor">Use rounded discount factor in NPV calculations (default is <see langword="false"/></param>
        /// <returns>IRR value (discount rate at which NPV is closes to zero).</returns>
        public static double GetIRR(IList<decimal> ncfs, decimal initialInvestment, double minDiscountRate, 
            double maxDiscountRate, double rateIncrement, out INPV? irrNpv, bool useRoundedDiscountFactor = false)
        {
            // Create a new NPC calculator with default settings
            var npvCalc = new NpvCalc(ncfs, minDiscountRate);
            npvCalc.Settings.UseRoundedDiscountingFactor = useRoundedDiscountFactor;

            // Iterate through rates to find the NPV closest to zero
            var rate = minDiscountRate;
            INPV? lastNpv = null;

            while (rate <= maxDiscountRate)
            {
                npvCalc.Settings.DiscountRate = rate;
                var currentNpv = npvCalc.GetNPVs(initialInvestment);

                if (lastNpv != null)
                {
                    if (Math.Abs(currentNpv.TotalNPV) > Math.Abs(lastNpv.TotalNPV))
                    {
                        // The current NPV is further from zero than the last, so the last one calculated will give the IRR
                        break;
                    }
                }

                lastNpv = currentNpv;
                rate += rateIncrement;
            }

            irrNpv = lastNpv;
            return rate - rateIncrement;
        }
    }
}
