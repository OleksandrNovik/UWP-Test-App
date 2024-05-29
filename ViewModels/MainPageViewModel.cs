using SecondApp.Common;
using SecondApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Popups;

namespace SecondApp.ViewModels
{
    public class MainPageViewModel
    {
        public string CurrentFirstName { get; set; }

        public string CurrentLastName { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }

        public ICommand Click => new RelayCommand<object>(async (o) =>
        {
            var box = new MessageDialog(CurrentFirstName + CurrentLastName);
            await box.ShowAsync();
        });

        public MainPageViewModel()
        {
            Users = new ObservableCollection<UserModel>()
            {
                new UserModel { FirstName = "FirstName", LastName = "LastName" }
            };
        }
    }
}
