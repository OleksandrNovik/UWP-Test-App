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
            var item = await storage.TryGetItemAsync(fileName);
            List<UserModel> users = null;
            // If file was opened
            if (item != null && item is StorageFile file)
            {
                var json = await FileIO.ReadTextAsync(file);
                users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserModel>>(json);
            }
            return users;
        }

    }
}
