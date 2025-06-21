using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NPV_Calculator_UI.ViewModels
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event.
        /// </summary>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Standard property changed handler.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets backing field in the view model if new value is different, also firing property changed event for the property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="field">Backing field.</param>
        /// <param name="value">New property value.</param>
        /// <param name="propertyName">Public oroperty name.</param>
        /// <param name="onAfterPropertyChanged">Optional action to perform after property changed.</param>
        /// <returns><see langword="True"/> if property was changed, otherwise <see langword="false"/>.</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null, Action? onAfterPropertyChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) 
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            onAfterPropertyChanged?.Invoke();

            return true;
        }
    }
}
