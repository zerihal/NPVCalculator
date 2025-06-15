using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPVCalculator.Interfaces
{
    public interface ICalcSettings
    {
        /// <summary>
        /// Notification that calculator settings have been changed.
        /// </summary>
        event EventHandler CalcSettingsChanged;

        /// <summary>
        /// Discount rate (%) to use for NPV calculation.
        /// </summary>
        double DiscountRate { get; set; }

        /// <summary>
        /// Discounting factor decimal places (for rounding).
        /// </summary>
        int DiscountingFactorDecimalPlaces { get; set; }

        /// <summary>
        /// NPV decimal places (for rounding).
        /// </summary>
        int NPVDecimalPlaces { get; set; }

        /// <summary>
        /// If set to true, the calculation for NPV uses the rounded discounting factor value rather than the raw value.
        /// </summary>
        bool UseRoundedDiscountingFactor { get; set; }
    }
}
