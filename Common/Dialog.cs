using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SecondApp.Common
{
    public static class Dialog
    {
        /// <summary>
        /// Shows <see cref="ContentDialog"/> with "Ok" button option
        /// </summary>
        /// <param name="title"> Dialog title </param>
        /// <param name="content"> Dialog content </param>
        /// <returns> User's decision from dialog buttons </returns>
        public async static Task<ContentDialogResult> OkDialog(string title, string content)
        {
            var dialog = new ContentDialog
            {
                Content = content,
                Title = title,
                CloseButtonText = "Ok"
            };
            return await dialog.ShowAsync();
        }

        /// <summary>
        /// Shows <see cref="ContentDialog"/> with "Yes" and "No" options
        /// </summary>
        /// <param name="title"> Dialog title </param>
        /// <param name="content"> Dialog content </param>
        /// <returns> User's decision from dialog buttons </returns>
        public async static Task<ContentDialogResult> YesNoDialog(string title, string content)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };
            return await dialog.ShowAsync();
        }
    }
}
