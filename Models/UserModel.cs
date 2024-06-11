using System.ComponentModel;

namespace SecondApp.Models
{
    public class UserModel : BaseModel, IEditableObject
    {
        #region Editable Object
        private struct UserData
        {
            public string firstName, lastName;
        }

        private UserData currentData;
        private UserData backUpData;

        public UserModel()
        {
            currentData = new UserData();
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
        public string FirstName
        {
            get => currentData.firstName;
            set
            {
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
                if (currentData.lastName != value)
                {
                    currentData.lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool edited = false;
        public bool IsEdited
        {
            get => edited;
            set
            {
                if (edited != value)
                {
                    edited = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
