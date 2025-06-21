using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPV_Calculator_UI.Controls
{
    /// <summary>
    /// Simple textbox that only accepts numeric input and max of 1 decimal point.
    /// </summary>
    public class NumericTextBox : TextBox
    {
        private readonly Regex _regex = new Regex("[^0-9.]+");

        public NumericTextBox()
        {
            DataObject.AddPastingHandler(this, OnPaste);
        }

        private bool IsTextAllowed(string text)
        {
            if (text == "." && (Text?.Contains(".") ?? false))
                return false;

            return !_regex.IsMatch(text);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetData(typeof(string)) is string text)
            {
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
