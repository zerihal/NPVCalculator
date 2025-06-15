using NPV_Calculator_UI.Commands;
using NPV_Calculator_UI.EventArguments;
using NPV_Calculator_UI.Strings;
using NPVCalculator;
using NPVCalculator.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NPV_Calculator_UI.ViewModels
{
    public class CalcTableViewModel : PropertyChangedBase
    {
        private ICalcSettings _calcSettings;
        private string _totalNpvIrr = SR.NoTotalNpvIrr;
        private string _totalNpvIrrLabel = SR.EmptyTotalNpvIrrLabel;
        private decimal _initialInvestment;

        public event EventHandler<NcfValuesChangedEventArgs>? NcfValuesChanged;

        public ObservableCollection<NcfViewModel> Values { get; set; }

        public decimal InitialInvestment 
        { 
            get => _initialInvestment; 
            set
            {
                if (_initialInvestment == value) return;
                _initialInvestment = value;
                ClearPreviousCalcs();
            }
        }

        public string TotalNpvIrr
        {
            get => _totalNpvIrr;
            set => SetField(ref _totalNpvIrr, value);
        }

        public string TotalNpvIrrLabel
        {
            get => _totalNpvIrrLabel;
            set => SetField(ref _totalNpvIrrLabel, value);
        }

        public ICommand AddNcf { get; }

        public ICommand RemoveNcf { get; }

        public CalcTableViewModel(ICalcSettings settings) 
        {
            _calcSettings = settings;
            Values = new ObservableCollection<NcfViewModel>();
            AddNcf = new RelayCommand(OnAddNcf);
            RemoveNcf = new RelayCommand(OnRemoveNcf);

            _calcSettings.CalcSettingsChanged += OnCalcSettingsChanged;
        }

        public void Calculate(CalculationType calcType)
        {
            if (!Values.Any()) return;

            switch (calcType)
            {
                case CalculationType.NPV:
                    var calc = new NpvCalc() { Settings = _calcSettings, NCFs = Values.Select(v => v.NCFValue).ToList() };
                    var NPVs = calc.GetNPVs(InitialInvestment);

                    for (var i = 0; i < Values.Count(); i++)
                    {
                        // ToDo - Maybe add a row for year 0?

                        Values[i].Update(NPVs.IndividualNPVsAndDiscountFactors[i + 1]);
                    }

                    TotalNpvIrrLabel = SR.TotalNpvLabel;
                    TotalNpvIrr = NPVs.TotalNPV.ToString();

                    break;

                case CalculationType.IRR:
                    var irr = IRRCalc.GetIRR(Values.Select(v => v.NCFValue).ToList(), InitialInvestment, 1, 99, 1, out _);
                    TotalNpvIrrLabel = SR.IrrLabel;
                    TotalNpvIrr = $"{irr}%";
                    break;

                case CalculationType.Clear:
                    ClearPreviousCalcs();
                    Values.Clear();
                    break;
            }
        }

        private void OnRemoveNcf(object obj)
        {
            if (obj is NcfViewModel ncfVm)
            {
                Values.Remove(ncfVm);
                OnNcfValuesChanged(new NcfValuesChangedEventArgs(NcfCollectionChangeType.Remove, Values.Count()));
                ClearPreviousCalcs();
            }
        }

        private void OnAddNcf(object obj)
        {
            if (obj is string ncfStr && decimal.TryParse(ncfStr, out var ncf))
            {
                Values.Add(new NcfViewModel(ncf));
                OnNcfValuesChanged(new NcfValuesChangedEventArgs(NcfCollectionChangeType.Add, Values.Count()));
            }
        }

        private void OnCalcSettingsChanged(object? sender, EventArgs e)
        {
            ClearPreviousCalcs();
        }

        private void ClearPreviousCalcs()
        {
            foreach (var value in Values)
                value.Update(null);

            TotalNpvIrrLabel = SR.EmptyTotalNpvIrrLabel;
            TotalNpvIrr = SR.NoTotalNpvIrr;
        }

        private void OnNcfValuesChanged(NcfValuesChangedEventArgs e) => NcfValuesChanged?.Invoke(this, e);
    }
}
