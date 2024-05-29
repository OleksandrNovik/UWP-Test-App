using SecondApp.Models;
using System.Collections.ObjectModel;

namespace SecondApp.ViewModels
{
    public class MainPageViewModel
    {
        public string CurrentFirstName { get; set; }

        public string CurrentLastName { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }

        public MainPageViewModel()
        {
            Users = new ObservableCollection<UserModel>()
            {
                new UserModel { FirstName = "FirstName", LastName = "LastName" }
            };
        }
    }
}
