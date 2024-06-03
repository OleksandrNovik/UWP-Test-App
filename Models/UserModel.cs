using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SecondApp.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        private string firstName;

        private string lastName;

        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Decides if user is currently edited
        /// </summary>
        private bool edited = false;

        /// <summary>
        /// Full prop for property changed event
        /// </summary>
        [JsonIgnore]
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


        #region INotifyProperyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
