using NPV_Calculator_UI.Commands;
using NPV_Calculator_UI.EventArguments;
using NPVCalculator;
using NPVCalculator.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NPV_Calculator_UI.ViewModels
{
    public class CalcTableViewModel : PropertyChangedBase
    {
        private ICalcSettings _calcSettings;
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

        public ICommand AddNcf { get; }

        public ICommand RemoveNcf { get; }

        public CalcTableViewModel(ICalcSettings settings) 
        {
            _calcSettings = settings;
            Values = new ObservableCollection<NcfViewModel>();
            AddNcf = new RelayCommand(OnAddNcf);
            RemoveNcf = new RelayCommand(OnRemoveNcf);
        }

        public void Calculate(CalculationType calcType)
        {
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

                    // ToDo - Update total

                    break;

                case CalculationType.IRR:
                    // ToDo ...
                    break;

                case CalculationType.Clear:
                    // ToDo ...
                    break;
            }
        }

        public void UpdateCalcSettings(ICalcSettings newSettings)
        {
            _calcSettings = newSettings;
            ClearPreviousCalcs();
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

        private void ClearPreviousCalcs()
        {
            // ToDo: Put in something here to check whether already cleared - no need to do again if so

            foreach (var value in Values)
            {
                value.Update(null);
            }
        }

        private void OnNcfValuesChanged(NcfValuesChangedEventArgs e) => NcfValuesChanged?.Invoke(this, e);
    }
}
