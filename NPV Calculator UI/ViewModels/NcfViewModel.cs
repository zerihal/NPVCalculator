using NPVCalculator.Objects;

namespace NPV_Calculator_UI.ViewModels
{
    public class NcfViewModel : PropertyChangedBase
    {
        private decimal _ncfValue;
        private NpvAndDiscountFactor? _npvAndDiscountFactor;

        /// <summary>
        /// NCF value.
        /// </summary>
        public decimal NCFValue
        {
            get => _ncfValue;
            set => SetField(ref _ncfValue, value);
        }

        /// <summary>
        /// Calculated NPV for this NCF.
        /// </summary>
        public string NPV => _npvAndDiscountFactor?.Npv.ToString() ?? "-";

        /// <summary>
        /// Calculated discount factor for this NCF.
        /// </summary>
        public string DiscountFactor => _npvAndDiscountFactor?.DiscountFactor?.ToString() ?? "-";

        public NcfViewModel() { }

        public NcfViewModel(decimal ncfValue)
        {
            NCFValue = ncfValue;
        }

        /// <summary>
        /// Updates the NCF with calculated NPV and discount factor.
        /// </summary>
        /// <param name="npvAndDiscountFactor">NPV and discount factor to update this NCF with.</param>
        public void Update(NpvAndDiscountFactor? npvAndDiscountFactor)
        {
            _npvAndDiscountFactor = npvAndDiscountFactor;
            OnPropertyChanged(nameof(DiscountFactor));
            OnPropertyChanged(nameof(NPV));
        }
    }
}
