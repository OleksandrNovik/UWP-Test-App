using SecondApp.Common;
using SecondApp.DTOs;
using SecondApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

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
            var data = new SaveDataDTO
            {
                Users = Users,
                Inputs = new UserModel
                {
                    FirstName = CurrentFirstName,
                    LastName = CurrentLastName
                },
            };
            await FileOperator.SaveUsersToFileAsync(data);
        }

        /// <summary>
        /// Gets initial data from json file
        /// </summary>
        /// <returns> Status of operation </returns>
        public async Task GetUsersOnStartUpAsync()
        {
            var data = await FileOperator.GetUsersFromFileAsync();

            // No data provided (file does not exist)
            if (data is null)
            {
                Users = new ObservableCollection<UserModel>();
            }
            else
            {
                Users = new ObservableCollection<UserModel>(data.Users);
                CurrentFirstName = data.Inputs.FirstName;
                CurrentLastName = data.Inputs.LastName;
            }
        }

        #region Commands

        /// <summary>
        /// Adds user to the list
        /// </summary>
        public ICommand AddUserCommand => new RelayCommand<object>(async o =>
        {
            // Check if inputed last and first name are not empty
            if (!string.IsNullOrWhiteSpace(CurrentFirstName)
                && !string.IsNullOrWhiteSpace(CurrentLastName))
            {
                var user = new UserModel { FirstName = CurrentFirstName, LastName = CurrentLastName };
                Users.Add(user);
                CurrentFirstName = CurrentLastName = string.Empty;
            }
            else
            {
                await Dialog.OkDialog("User's name is empty", "Cannot add user with empty first or last name.");
            }
        });

        /// <summary>
        /// Deletes user from the list
        /// </summary>
        public ICommand DeleteUserCommand => new RelayCommand<UserModel>(async user =>
        {
            if (user is null)
                throw new ArgumentNullException("Null reference while deleting user.");

            var selection = await Dialog.YesNoDialog("Deleting user data", $"Are you sure you want to delete \"{user}\"");

            if (selection == ContentDialogResult.Primary)
            {
                Users.Remove(user);
            }
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
