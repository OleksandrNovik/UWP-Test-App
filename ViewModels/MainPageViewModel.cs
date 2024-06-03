using SecondApp.Common;
using SecondApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

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

        /// <summary>
        /// Current button content to show the functionality of button
        /// </summary>
        private string currentButtonName = ButtonStates.Add;

        /// <summary>
        /// Fires property changed event for UI to update button content
        /// </summary>
        public string ButtonFunctionality
        {
            get => currentButtonName;
            set
            {
                if (currentButtonName != value)
                {
                    currentButtonName = value;
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
            ButtonClickCommand = AddUserCommand;
            // Subscribing to event when application is exited
            Application.Current.Suspending += SaveOnExitAsync;
        }

        private async void SaveOnExitAsync(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
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
        private ICommand AddUserCommand => new RelayCommand<object>(o =>
        {
            // Check if inputed last and first name are not empty
            if (CurrentFirstName != string.Empty && CurrentLastName != string.Empty)
            {
                var user = new UserModel { FirstName = CurrentFirstName, LastName = CurrentLastName };
                Users.Add(user);
                CurrentFirstName = CurrentLastName = string.Empty;
            }
        });

        /// <summary>
        /// Gets data from input fields and alters user data in the list
        /// </summary>
        private ICommand EditUserCommand => new RelayCommand<object>(o =>
        {
            if (selectedUser != null)
            {
                selectedUser.FirstName = CurrentFirstName;
                selectedUser.LastName = CurrentLastName;

                // Clearing input fields
                CurrentFirstName = CurrentLastName = string.Empty;
                // Changing back to add user mode
                ButtonFunctionality = ButtonStates.Add;
                ButtonClickCommand = AddUserCommand;
            }
        });

        /// <summary>
        /// Selects data about user and gets UI ready for editing process
        /// </summary>
        public ICommand SelectEditedUser => new RelayCommand<UserModel>(user =>
        {
            if (user is null)
                throw new System.ArgumentNullException("Null reference while editing user.");
            // Selecting current editable user
            selectedUser = user;
            // Setting values for input fields
            CurrentFirstName = user.FirstName;
            CurrentLastName = user.LastName;
            // Changing mode to edit mode
            ButtonClickCommand = EditUserCommand;
            ButtonFunctionality = ButtonStates.Apply;
        });

        /// <summary>
        /// Field for current command to identify if user if we add user or edit existing
        /// </summary>
        private ICommand currentButtonCommand;

        /// <summary>
        /// Prop to run on property changed event for UI
        /// </summary>
        public ICommand ButtonClickCommand
        {
            get => currentButtonCommand;
            set
            {
                if (currentButtonCommand != value)
                {
                    currentButtonCommand = value;
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

        /// <summary>
        /// Static class to hold states of button name depending on it's current functionality
        /// </summary>
        private static class ButtonStates
        {
            public const string Add = "Add";
            public const string Apply = "Apply";
        }
    }
}
