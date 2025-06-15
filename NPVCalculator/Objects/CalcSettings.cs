using NPVCalculator.Interfaces;
using System;
using System.Collections.Generic;

namespace NPVCalculator.Objects
{
    public class CalcSettings : ICalcSettings
    {
        private double _discountRate;
        private int _discountFactorDecimalPlaces = 2;
        private int _npvDecimalPlaces = 0;
        private bool _useRoundedDiscountingFactor = false;

        /// <inheritdoc/>
        public event EventHandler? CalcSettingsChanged;

        /// <inheritdoc/>
        public double DiscountRate 
        { 
            get => _discountRate; 
            set => SetValue(ref _discountRate, value); 
        }

        /// <inheritdoc/>
        public int DiscountingFactorDecimalPlaces 
        { 
            get => _discountFactorDecimalPlaces; 
            set => SetValue(ref _discountFactorDecimalPlaces, value); 
        } 

        /// <inheritdoc/>
        public int NPVDecimalPlaces 
        { 
            get => _npvDecimalPlaces; 
            set => SetValue(ref _npvDecimalPlaces, value); 
        }

        /// <summary>
        /// If set to true, the calculation for NPV uses the rounded discounting factor value rather than the raw
        /// value (default is <see langword="false"/>).
        /// </summary>
        public bool UseRoundedDiscountingFactor 
        {
            get => _useRoundedDiscountingFactor;
            set => SetValue(ref _useRoundedDiscountingFactor, value); 
        }

        private void SetValue<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnCalcSettingsChanged();
        }

        private void OnCalcSettingsChanged() => CalcSettingsChanged?.Invoke(this, EventArgs.Empty);
    }
}
