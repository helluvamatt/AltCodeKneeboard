using AltCodeKneeboard.Models;
using AltCodeKneeboard.Utils;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard
{
    public static class Program
    {
        private static readonly Mutex mutex = new Mutex(true, "{07F3E6F1-F365-45C3-A74A-0F5444565E96}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool showOptions = true;
            bool showHelp = false;
            var opts = new Mono.Options.OptionSet
            {
                { "h|help", "Show this help message and exit", n => showHelp = n != null },
                { "s|startup", "Startup mode, don't show the configuration editor", n => showOptions = n == null }
            };
            opts.Parse(args);

            if (showHelp)
            {
                opts.WriteOptionDescriptions(Console.Out);
                return;
            }
            
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var appDataPath = FormUtils.GetApplicationDirectory();

                AltCodeData altCodes;
                try
                {
                    altCodes = AltCodesXmlParser.LoadFromFile(Path.Combine(appDataPath, "alt_codes.xml"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(R.ErrorFailedToLoadAltCodes + ex.Message, R.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

#if DEBUG
                Data.UnicodeAltCode.DebugPrintMissing(altCodes.AltCodes);
#endif

                Application.Run(new KneeboardApplication(showOptions, altCodes));
            }
            else
            {
                Console.WriteLine("Instance already running. Exiting...");
            }
        }
    }
}
