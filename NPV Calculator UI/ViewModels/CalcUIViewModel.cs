using NPV_Calculator_UI.Commands;
using NPVCalculator.Interfaces;
using NPVCalculator.Objects;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NPV_Calculator_UI.ViewModels
{
    public class CalcUIViewModel : PropertyChangedBase
    {
        private double _discountRate;
        private int _discountFactorDp = 2;
        private int _npvDp;
        private bool _roundDiscountFactorCalc;
        private string _initialInvestment = string.Empty;

        /// <summary>
        /// Initial investment value.
        /// </summary>
        public string InitialInvestment
        {
            get => _initialInvestment;
            set => SetField(ref _initialInvestment, value, onAfterPropertyChanged: InitInvestmentUpdated);
        }

        /// <summary>
        /// Discount rate (%).
        /// </summary>
        public double DiscountRate
        {
            get => _discountRate;
            set
            {
                if (SetField(ref _discountRate, value))
                {
                    CurrentCalcSettings.DiscountRate = value;
                    OnPropertyChanged(nameof(CurrentCalcSettings)); 
                }
            }
        }

        /// <summary>
        /// Discount factor decimal places.
        /// </summary>
        public int DiscountFactorDp
        {
            get => _discountFactorDp;
            set
            {
                if (SetField(ref _discountFactorDp, value))
                {
                    CurrentCalcSettings.DiscountingFactorDecimalPlaces = value;
                    OnPropertyChanged(nameof(CurrentCalcSettings));
                }
            }
        }

        /// <summary>
        /// NPV decimal places.
        /// </summary>
        public int NpvDp
        {
            get => _npvDp;
            set
            {
                if (SetField(ref _npvDp, value))
                {
                    CurrentCalcSettings.NPVDecimalPlaces = value;
                    OnPropertyChanged(nameof(CurrentCalcSettings));
                }
            }
        }

        /// <summary>
        /// Flag to indicate whether values should be rounded in discount factor calculation.
        /// </summary>
        public bool RoundDiscountFactorCalc
        {
            get => _roundDiscountFactorCalc;
            set
            {
                if (SetField(ref _roundDiscountFactorCalc, value))
                {
                    CurrentCalcSettings.UseRoundedDiscountingFactor = value;
                    OnPropertyChanged(nameof(CurrentCalcSettings));
                }
            }
        }

        /// <summary>
        /// Calculation table view model.
        /// </summary>
        public CalcTableViewModel CalcTable { get; }

        /// <summary>
        /// Current calculator settings.
        /// </summary>
        public ICalcSettings CurrentCalcSettings { get; }

        /// <summary>
        /// NCF values (to use for calculations).
        /// </summary>
        public ObservableCollection<NcfViewModel>? Values { get; set; }

        /// <summary>
        /// Update calculator command.
        /// </summary>
        public ICommand UpdateCalculator { get; }

        public CalcUIViewModel()
        {
            CurrentCalcSettings = new CalcSettings();
            CalcTable = new CalcTableViewModel(CurrentCalcSettings);
            UpdateCalculator = new RelayCommand(DoRecalc, CanExecuteCalculations);
        }

        /// <summary>
        /// Can execute calculations delegate.
        /// </summary>
        /// <param name="param">Command parameter.</param>
        /// <returns><see langword="True"/> if calculation table contains any NCF values, otherwise <see langword="false"/>.</returns>
        public bool CanExecuteCalculations(object param) => CalcTable.Values.Count > 0;

        private void InitInvestmentUpdated()
        {
            if (decimal.TryParse(InitialInvestment, out var initInvestmentValue))
                CalcTable.InitialInvestment = initInvestmentValue;
        }

        private void DoRecalc(object param)
        {
            if (param is CalculationType calcType)
                CalcTable.Calculate(calcType);
        }
    }
}
