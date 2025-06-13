using NPVCalculator.Objects;

namespace NPV_Calculator_UI.ViewModels
{
    public class NcfViewModel : PropertyChangedBase
    {
        private decimal _ncfValue;
        private NpvAndDiscountFactor? _npvAndDiscountFactor;

        public decimal NCFValue
        {
            get => _ncfValue;
            set => SetField(ref _ncfValue, value);
        }

        public string NPV => _npvAndDiscountFactor?.Npv.ToString() ?? "-";

        public string DiscountFactor => _npvAndDiscountFactor?.DiscountFactor?.ToString() ?? "-";

        public NcfViewModel() { }

        public NcfViewModel(decimal ncfValue)
        {
            NCFValue = ncfValue;
        }

        public void Update(NpvAndDiscountFactor? npvAndDiscountFactor)
        {
            _npvAndDiscountFactor = npvAndDiscountFactor;
            OnPropertyChanged(nameof(DiscountFactor));
            OnPropertyChanged(nameof(NPV));
        }
    }
}
