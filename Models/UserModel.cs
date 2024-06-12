using SecondApp.Common;
using System;
using System.Collections;
using System.ComponentModel;

namespace SecondApp.Models
{
    public class UserModel : BaseModel, IEditableObject, INotifyDataErrorInfo
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
            errorsValidator.ErrorsChanged += OnError;
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
                currentData = backUpData;
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

        private readonly Validator errorsValidator = new Validator();
        public bool HasErrors => errorsValidator.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnError(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(sender, e);
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorsValidator.GetErrors(propertyName);
        }

        #endregion
        public string FirstName
        {
            get => currentData.firstName;
            set
            {
                errorsValidator.RemoveError(nameof(FirstName));

                if (string.IsNullOrWhiteSpace(value))
                {
                    errorsValidator.UpdateError(nameof(FirstName), $"First name cannot be empty.");
                }
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
                errorsValidator.RemoveError(nameof(LastName));

                if (string.IsNullOrWhiteSpace(value))
                {
                    errorsValidator.UpdateError(nameof(LastName), $"Last name cannot be empty.");
                }

                if (currentData.lastName != value)
                {
                    currentData.lastName = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
