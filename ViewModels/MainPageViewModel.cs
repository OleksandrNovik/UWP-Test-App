using SecondApp.Common;
using SecondApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecondApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Fields for inputed first and last name for a new or editable user
        /// </summary>
        private string currentFirstname, currentLastName;

        /// <summary>
        /// Prop for a property changed event to update UI
        /// </summary>
        public string CurrentFirstName
        {
            get => currentFirstname;
            set
            {
                if (currentFirstname != value)
                {
                    currentFirstname = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Prop for a property changed event to update UI
        /// </summary>
        public string CurrentLastName
        {
            get => currentLastName;
            set
            {
                if (currentLastName != value)
                {
                    currentLastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<UserModel> users;

        /// <summary>
        /// Collection of users in the table 
        /// </summary>
        public ObservableCollection<UserModel> Users
        {
            get => users;
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Saving main data from application to a json file
        /// </summary>
        /// <returns> Status of operation </returns>
        public async Task SaveOnExitAsync()
        {
            await FileOperator.SaveUsersToFileAsync(Users);
        }

        /// <summary>
        /// Gets initial data from json file
        /// </summary>
        /// <returns> Status of operation </returns>
        public async Task GetUsersOnStartUpAsync()
        {
            var list = await FileOperator.GetUsersFromFileAsync();
            Users = new ObservableCollection<UserModel>(list ?? new List<UserModel>());
        }

        #region Commands

        /// <summary>
        /// Adds user to the list
        /// </summary>
        public ICommand AddUserCommand => new RelayCommand<object>(o =>
        {
            // Check if inputed last and first name are not empty
            if (!string.IsNullOrWhiteSpace(CurrentFirstName)
                && !string.IsNullOrWhiteSpace(CurrentLastName))
            {
                var user = new UserModel { FirstName = CurrentFirstName, LastName = CurrentLastName };
                Users.Add(user);
                CurrentFirstName = CurrentLastName = string.Empty;
            }
        });

        /// <summary>
        /// Command that switches mode for a user from edit to read-only
        /// </summary>
        public ICommand EditUserCommand => new RelayCommand<UserModel>(user =>
        {
            if (user is null)
                throw new System.ArgumentNullException("Null reference while editing user.");

            // Switches editable mode for user 
            user.IsEdited = !user.IsEdited;
        });

        /// <summary>
        /// Deletes user from the list
        /// </summary>
        public ICommand DeleteUserCommand => new RelayCommand<UserModel>(user =>
        {
            if (user is null)
                throw new System.ArgumentNullException("Null reference while deleting user.");

            Users.Remove(user);
        });

        #endregion

        #region INotifyProperyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
