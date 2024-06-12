using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SecondApp.Common
{
    public static class Dialog
    {
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
