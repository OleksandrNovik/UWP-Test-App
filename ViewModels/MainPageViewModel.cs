using SecondApp.Common;
using SecondApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SecondApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string currentFirstname, currentLastName;
        public string CurrentFirstName
        {
            get => currentFirstname;
            set
            {
                currentFirstname = value;
                OnPropertyChanged();
            }
        }

        public string CurrentLastName
        {
            get => currentLastName;
            set
            {
                currentLastName = value;
                OnPropertyChanged();
            }
        }

        private string currentButtonName = ButtonStates.Add;
        public string ButtonFunctionality
        {
            get => currentButtonName;
            set
            {
                currentButtonName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserModel> Users { get; set; }

        private UserModel selectedUser;

        public MainPageViewModel()
        {
            Users = new ObservableCollection<UserModel>()
            {
                new UserModel { FirstName = "FirstName", LastName = "LastName" }
            };
            // For start we can add user not edit
            ButtonClickCommand = AddUserCommand;
        }

        #region Commands

        private ICommand AddUserCommand => new RelayCommand<object>(o =>
        {
            var user = new UserModel { FirstName = CurrentFirstName, LastName = CurrentLastName };
            Users.Add(user);
            CurrentFirstName = CurrentLastName = string.Empty;
        });

        private ICommand EditUserCommand => new RelayCommand<object>(o =>
        {
            if (selectedUser is null)
                return;
            selectedUser.FirstName = CurrentFirstName;
            selectedUser.LastName = CurrentLastName;

            CurrentFirstName = CurrentLastName = string.Empty;
            ButtonFunctionality = ButtonStates.Add;
            ButtonClickCommand = AddUserCommand;
        });

        public ICommand SelectEditedUser => new RelayCommand<object>(o =>
        {
            if (o is UserModel user)
            {
                selectedUser = user;
                CurrentFirstName = user.FirstName;
                CurrentLastName = user.LastName;

                ButtonClickCommand = EditUserCommand;
                ButtonFunctionality = ButtonStates.Apply;
            }
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
                currentButtonCommand = value;
                OnPropertyChanged();
            }
        }

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
