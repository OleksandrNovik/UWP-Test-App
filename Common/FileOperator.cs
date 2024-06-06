using SecondApp.DTOs;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace SecondApp.Common
{
    /// <summary>
    /// File operations class
    /// </summary>
    public static class FileOperator
    {
        private static readonly StorageFolder storage = ApplicationData.Current.LocalFolder;

        private const string fileName = "data.json";

        /// <summary>
        /// Saves application state to a json storage file
        /// </summary>
        /// <param name="data"> Data of application current state </param>
        /// <returns> Status of operation </returns>
        public static async Task SaveUsersToFileAsync(SaveDataDTO data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var file = await storage.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, json);
        }

        /// <summary>
        /// Gets application state from json file
        /// </summary>
        /// <returns> Resulting application data if operation was successful </returns>
        public static async Task<SaveDataDTO> GetUsersFromFileAsync()
        {
            var item = await storage.TryGetItemAsync(fileName);
            SaveDataDTO data = null;

            // If file was opened
            if (item != null && item is StorageFile file)
            {
                var json = await FileIO.ReadTextAsync(file);
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveDataDTO>(json);
            }
            return data;
        }

    }
}
