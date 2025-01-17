using Microsoft.Extensions.Logging;
using ExpenseWise2.Components;

namespace ExpenseWise2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();


            //File Check

            if (!Directory.Exists(Util.ROOTFOLDER))
            {
                Directory.CreateDirectory(Util.ROOTFOLDER);
            }

            if (!File.Exists(Util.TAG))
            {
                File.Create(Util.TAG).Close();
            }

            if (!File.Exists(Util.TRANSACTION))
            {
                File.Create(Util.TRANSACTION).Close();
            }


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
