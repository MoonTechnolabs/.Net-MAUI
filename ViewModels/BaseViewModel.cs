using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiLoginDemo.ViewModels
{
    /// <summary>
    /// Serves as a base class for all ViewModels, providing property change notification.
    /// Implements <see cref="INotifyPropertyChanged"/> to support data binding.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// This event is used to notify the UI about property updates.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify the UI 
        /// that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that changed. Automatically detected when called from a property setter.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
