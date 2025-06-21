using NPVCalculator.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace NPV_Calculator_UI.Controls
{
    /// <summary>
    /// Interaction logic for CalcTable.xaml
    /// </summary>
    public partial class CalcTable : UserControl
    {
        public static readonly DependencyProperty CalcSettingsProperty = DependencyProperty.Register(nameof(CalcSettings), 
            typeof(ICalcSettings), typeof(CalcTable), new PropertyMetadata(null));

        /// <summary>
        /// Calculator settings.
        /// </summary>
        public ICalcSettings CalcSettings
        {
            get => (ICalcSettings)GetValue(CalcSettingsProperty);
            set => SetValue(CalcSettingsProperty, value);
        }

        public CalcTable()
        {
            InitializeComponent();
        }
    }
}
