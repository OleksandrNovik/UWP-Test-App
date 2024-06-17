﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SecondApp.Common
{
    /// <summary>
    /// Class used to separate error creating and setting logic from models
    /// Models can use composition to use all needed information had logic
    /// </summary>
    public class Validator : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void UpdateError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, errorMessage);
            }
            _errors[propertyName] = errorMessage;
            OnErrorsChanged(propertyName);
        }
        public void RemoveError(string propertyName)
        {
            if (_errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.GetValueOrDefault(propertyName ?? "");
        }
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
