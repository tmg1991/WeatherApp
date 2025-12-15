using Weather.Interfaces;

namespace Weather.Models
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlertAsync(string title, string message, string cancel)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current?
                    .MainPage?
                    .DisplayAlertAsync(title, message, cancel);
            });
        }

        public async Task<bool> ShowConfirmAsync(string title, string message, string accept, string cancel)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current?
                    .MainPage?
                    .DisplayAlertAsync(title, message, accept, cancel);
            });
        }

        public async Task<string> ShowPromptAsync(string title, string message)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current?
                    .MainPage?
                    .DisplayPromptAsync(title, message);
            });
        }
    }
}
