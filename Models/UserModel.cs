using SecondApp.Common;
using System;
using System.Collections;
using System.ComponentModel;

namespace SecondApp.Models
{
    public class UserModel : PropertyChangedModel, IEditableObject, INotifyDataErrorInfo
    {
        #region Editable Object
        private struct UserData
        {
            public string firstName, lastName;
        }

        private UserData currentData;

        private UserData backUpData;

        private bool edited = false;

        public UserModel()
        {
            currentData = new UserData();
            _errorsValidator.ErrorsChanged += OnErrorChanged;
        }

        public void BeginEdit()
        {
            if (!edited)
            {
                backUpData = currentData;
                edited = true;
            }
        }

        public void CancelEdit()
        {
            if (edited)
            {
                FirstName = backUpData.firstName;
                LastName = backUpData.lastName;
                edited = false;
            }
        }

        public void EndEdit()
        {
            if (edited)
            {
                backUpData = new UserData();
                edited = false;
            }
        }
        #endregion

        #region Error Validation

        private readonly Validator _errorsValidator = new Validator();
        public bool HasErrors => _errorsValidator.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasErrors));
            ErrorsChanged?.Invoke(sender, e);
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsValidator.GetErrors(propertyName);
        }

        /// <summary>
        /// Checks if provided string value is empty
        /// </summary>
        /// <param name="value"> Value we are checking </param>
        /// <param name="propertyName"> Name of property that is checked </param>
        private void ValidateEmptyString(string value, string propertyName)
        {
            _errorsValidator.RemoveError(propertyName);

            if (string.IsNullOrWhiteSpace(value))
            {
                _errorsValidator.UpdateError(propertyName, $"{propertyName} cannot be empty");
            }
        }

        #endregion
        public string FirstName
        {
            get => currentData.firstName;
            set
            {
                ValidateEmptyString(value, nameof(FirstName));

                if (currentData.firstName != value)
                {
                    currentData.firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => currentData.lastName;
            set
            {
                ValidateEmptyString(value, nameof(LastName));

                if (currentData.lastName != value)
                {
                    currentData.lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }

    }
}
