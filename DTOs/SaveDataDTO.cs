using SecondApp.Models;
using System.Collections.Generic;

namespace SecondApp.DTOs
{
    public class SaveDataDTO
    {
        public ICollection<UserModel> Users { get; set; }
        public UserModel Inputs { get; set; }
    }
}
