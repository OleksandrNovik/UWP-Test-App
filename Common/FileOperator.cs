using SecondApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace SecondApp.Common
{
    public static class FileOperator
    {
        private static readonly StorageFolder storage = ApplicationData.Current.LocalFolder;

        private const string fileName = "users.json";

        public static async Task SaveUsersToFileAsync(ICollection<UserModel> users)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(users);
            var file = await storage.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, json);
        }

        public static async Task<IList<UserModel>> GetUsersFromFileAsync()
        {
            var file = await storage.GetFileAsync(fileName);
            var json = await FileIO.ReadTextAsync(file);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserModel>>(json);
            return users;
        }

    }
}
