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
        private int _discountFactorDp;
        private int _npvDp;
        private bool _roundDiscountFactorCalc;
        private decimal _initialInvestment;

        public decimal InitialInvestment
        {
            get => _initialInvestment;
            set
            {
                if (SetField(ref _initialInvestment, value))
                {
                    CalcTable.InitialInvestment = value;
                }
            }
        }

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

        public CalcTableViewModel CalcTable { get; }

        public ICalcSettings CurrentCalcSettings { get; }

        public IList<NcfViewModel> TempNcfs
        {
            get
            {
                var temp = new List<NcfViewModel>();
                temp.Add(new NcfViewModel { NCFValue = 100 });
                temp.Add(new NcfViewModel { NCFValue = 5 });
                temp.Add(new NcfViewModel { NCFValue = 1210 });

                return temp;
            }
        }

        public ObservableCollection<NcfViewModel>? Values { get; set; }

        public ICommand CalculateNPV { get; }

        public ICommand CalculateIRR { get; }

        public CalcUIViewModel()
        {
            CurrentCalcSettings = new CalcSettings();
            CalcTable = new CalcTableViewModel(CurrentCalcSettings);
            CalculateNPV = new RelayCommand(DoNPVCalc, CanExecuteCalculations);
            CalculateIRR = new RelayCommand(DoIRRCalc, CanExecuteCalculations);
        }

        public bool CanExecuteCalculations(object param) => CalcTable.Values.Count > 0;

        private void DoNPVCalc(object param)
        {
            CalcTable.Calculate(CalculationType.NPV);
        }

        private void DoIRRCalc(object param)
        {
            CalcTable.Calculate(CalculationType.IRR);
        }
    }
}
