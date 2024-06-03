using System;
using System.Windows.Input;

namespace SecondApp.Common
{
    /// <summary>
    /// Generic type for a relay command
    /// </summary>
    /// <typeparam name="T"> Anything can be passed as type parameter </typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// ICommand implementation event 
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Delegate that holds executable method for current command
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Delegate that decides if command can be run or not 
        /// </summary>
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Constructor for only execute parameter that is required
        /// </summary>
        /// <param name="execute"> Executable method for current command </param>
        public RelayCommand(Action<T> execute) : this(execute, null) { }

        /// <summary>
        /// Constructor with all parameters from command 
        /// </summary>
        /// <param name="execute"> Executable method for current command </param>
        /// <param name="canExecute"> Delegate that decides if command can be run  </param>
        /// <exception cref="ArgumentNullException"> if execute parameter is null </exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute is null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Can execute realization for ICommand interface
        /// </summary>
        /// <param name="parameter"> Passed parameter for method </param>
        /// <returns> true if command can be executed depending on execution conditions provided </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        /// <summary>
        /// Executes method that is provided to command
        /// </summary>
        /// <param name="parameter"> Any parameter needed </param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
