using NPVCalculator.Interfaces;

namespace NPVCalculator.Objects
{
    public class CalcSettings : ICalcSettings
    {
        /// <inheritdoc/>
        public double DiscountRate { get; set; }

        /// <inheritdoc/>
        public int DiscountingFactorDecimalPlaces { get; set; } = 2;

        /// <inheritdoc/>
        public int NPVDecimalPlaces { get; set; } = 0;

        /// <summary>
        /// If set to true, the calculation for NPV uses the rounded discounting factor value rather than the raw
        /// value (default is <see langword="false"/>).
        /// </summary>
        public bool UseRoundedDiscountingFactor { get; set; } = false;
    }
}
