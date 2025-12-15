using Microsoft.Extensions.Logging;
using Weather.Interfaces;
using Weather.Models;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls.Hosting;
using LiveChartsCore;

namespace Weather
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseLiveCharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<Serializer>();
            return builder.Build();
        }
    }
}
