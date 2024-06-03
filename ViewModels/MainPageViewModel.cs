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
        /// Selected user for editing his data
        /// </summary>
        private UserModel selectedUser;

        public MainPageViewModel()
        {
            // For start we can add user not edit
            EditUserCommand = SelectEditedUser;
        }

        public async Task SaveOnExitAsync()
        {
            await FileOperator.SaveUsersToFileAsync(Users);
        }

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
        /// Saves changes for a user when its done being edited
        /// </summary>
        private ICommand SaveChangesCommand => new RelayCommand<object>(o =>
        {
            if (selectedUser != null)
            {
                // We finished editing user
                selectedUser.IsEdited = false;
                // Now button changes mode for user again
                EditUserCommand = SelectEditedUser;
            }
        });

        /// <summary>
        /// Changes state of selected user from reading to editing
        /// </summary>
        public ICommand SelectEditedUser => new RelayCommand<UserModel>(user =>
        {
            if (user is null)
                throw new System.ArgumentNullException("Null reference while editing user.");
            // Selecting current editable user
            selectedUser = user;
            // Started editing this user
            selectedUser.IsEdited = true;
            // Now button is saving changes for a user, not changing mode from read to write 
            EditUserCommand = SaveChangesCommand;
        });

        /// <summary>
        /// Field for current command to identify if user is edited or selected
        /// </summary>
        private ICommand editUserCommand;

        /// <summary>
        /// Prop to run on property changed event for UI
        /// </summary>
        public ICommand EditUserCommand
        {
            get => editUserCommand;
            set
            {
                if (editUserCommand != value)
                {
                    editUserCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Deletes user from the list
        /// </summary>
        public ICommand DeleteUserCommand => new RelayCommand<UserModel>((user) =>
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
