using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace bio_time.Helpers {
    public static class LogHelper {

        public static void BeginSession() {
            using (StreamWriter writer = new StreamWriter("log.txt", true)) {
                writer.WriteLine("------------------------------------------------------------");
                writer.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -Beginning of session");
            }
        }
        public static void EndSession(int elapsedSeconds) {
            using (StreamWriter writer = new StreamWriter("log.txt", true)) {
                writer.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -End of session");
                writer.WriteLine("_TIME=" + elapsedSeconds+";");
                writer.WriteLine("------------------------------------------------------------");
            }
            //System.Windows.Forms.MessageBox.Show(elapsedSeconds.ToString());
            //write to log file
        }

        public static bool AppendToLogFile(string content) {
            return true;
        }
    }
}
