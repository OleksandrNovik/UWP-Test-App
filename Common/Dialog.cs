using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SecondApp.Common
{
    public static class Dialog
    {
        /// <summary>
        /// Creates content dialog with title and content
        /// </summary>
        /// <param name="title"> Title of dialog </param>
        /// <param name="content"> Content of dialog </param>
        /// <returns> Created dialog with provided title and content </returns>
        private static ContentDialog DefaultDialog(string title, string content)
        {
            return new ContentDialog
            {
                Title = title,
                Content = content,
            };
        }
        /// <summary>
        /// Shows <see cref="ContentDialog"/> with "Ok" button option
        /// </summary>
        /// <param name="title"> Dialog title </param>
        /// <param name="content"> Dialog content </param>
        /// <returns> User's decision from dialog buttons </returns>
        public async static Task<ContentDialogResult> OkDialog(string title, string content)
        {
            var dialog = DefaultDialog(title, content);
            dialog.CloseButtonText = "Ok";
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
            var dialog = DefaultDialog(title, content);
            dialog.PrimaryButtonText = "Yes";
            dialog.SecondaryButtonText = "No";
            return await dialog.ShowAsync();
        }
    }
}
