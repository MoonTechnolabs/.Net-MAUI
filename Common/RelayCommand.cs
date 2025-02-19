using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLoginDemo.Common
{
    /// <summary>
    /// A generic RelayCommand for command binding in MAUI applications.
    /// </summary>
    /// <typeparam name="T">The type of parameter expected by the command.</typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The action to be executed when the command is invoked.</param>
        /// <param name="canExecute">An optional function that determines if the command can be executed.</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command can be executed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter to be passed to the command.</param>
        /// <returns>True if the command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            try
            {
                return canExecute == null || canExecute((T)parameter);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Defines the command to be executed.
        /// </summary>
        /// <param name="parameter">The parameter to be passed to the command.</param>
        public void Execute(object parameter)
        {
            try
            {
                execute?.Invoke((T)parameter);
            }
            catch
            {
                // Handle exceptions if needed
            }
        }

        /// <summary>
        /// Raises the CanExecuteChanged event to notify the UI that the execution state has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
