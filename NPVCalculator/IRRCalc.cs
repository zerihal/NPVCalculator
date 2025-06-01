using NPVCalculator.Interfaces;
using System;
using System.Collections.Generic;

namespace NPVCalculator
{
    public static class IRRCalc
    {
        public static double GetIRR(IList<decimal> ncfs, decimal initialInvestment, double minDiscountRate, 
            double maxDiscountRate, double rateIncrement, out INPV? irrNpv, bool useRoundedDiscountFactor = false)
        {
            // Create a new NPC calculator with default settings
            var npvCalc = new NpvCalc(ncfs, minDiscountRate) { UseRoundedDiscountingFactor = useRoundedDiscountFactor };

            // Iterate through rates to find the NPV closest to zero
            var rate = minDiscountRate;
            INPV? lastNpv = null;

            while (rate <= maxDiscountRate)
            {
                npvCalc.DiscountRate = rate;
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
