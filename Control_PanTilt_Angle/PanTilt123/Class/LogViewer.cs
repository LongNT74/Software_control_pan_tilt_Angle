using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanTilt123.Class
{
    public static class LogViewer
    {
        private static int index = 0;

        public static void ClearAll()
        {
            if (System.Windows.Application.Current != null)
            {

                System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() => frmLogView.GetInstance().LogEntries.Clear()));
            }
        }

        public static void Add(string logMessage, LogType colortext)
        {
            string codecolor = "";
            string backgroundcolor = "";
            if (colortext == LogType.Error)
            {
                codecolor = "Red";
                backgroundcolor = "White";
            }
            else if (colortext == LogType.Info)
            {
                codecolor = "Green";
                backgroundcolor = "White";

            }
            else if (colortext == LogType.Critical)
            {
                codecolor = "White";
                backgroundcolor = "Red";

            }
            else if (colortext == LogType.Warning)
            {
                codecolor = "Goldenrod";
                backgroundcolor = "White";

            }
            else if (colortext == LogType.Verbose)
            {
                codecolor = "Blue";
                backgroundcolor = "White";

            }
            else if (colortext == LogType.Default)
            {
                codecolor = "Black";
                backgroundcolor = "White";

            }

            if (logMessage != String.Empty)
            {
                new System.Windows.Application();
                if (System.Windows.Application.Current != null)
                {

                    System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() => frmLogView.GetInstance().LogEntries.Add(
                        new LogEntry()
                        {
                            Index = index++,
                            DateTime = DateTime.Now,
                            Message = logMessage,
                            MessageColor = codecolor,
                            BackColor = backgroundcolor

                        }
                    )));
                }
            }
        }
    }
}
