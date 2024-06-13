using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SecondApp.Common
{
    /// <summary>
    /// Class used to separate error creating and setting logic from models
    /// Models can use composition to use all needed information nad logic
    /// </summary>
    public class Validator : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, string> errors = new Dictionary<string, string>();
        public bool HasErrors => errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void UpdateError(string propertyName, string errorMessage)
        {
            if (!errors.ContainsKey(propertyName))
            {
                errors.Add(propertyName, errorMessage);
            }
            errors[propertyName] = errorMessage;
            OnErrorsChanged(propertyName);
        }
        public void RemoveError(string propertyName)
        {
            if (errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errors.GetValueOrDefault(propertyName ?? "");
        }
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
