using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using bio_time.Models;

namespace bio_time.Helpers {
    public class LogHelper {
        ConfigModel configProperties;
        int selectedContractIndex;
        public LogHelper(ConfigModel configProperties, int selectedContractIndex)
        {
            this.configProperties = configProperties;
            this.selectedContractIndex = selectedContractIndex;
        }
        public void BeginSession() {
            ContractModel selectedContract = configProperties.Contracts.Where(c => c.Index == selectedContractIndex).FirstOrDefault();
            using (StreamWriter writer = new StreamWriter("log.txt", true)) {
                writer.WriteLine("|--------------------------------------------------------------|");
                writer.WriteLine("| Client=" + selectedContract.ClientName + "; | " + "Contract=" + selectedContract.ContractTitle + "; |");
                //writer.WriteLine("| Developer: " + configProperties.Developer + " | " + "Rate: " + configProperties.Rate + " |");
                writer.WriteLine("|--------------------------------------------------------------|");
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -Beginning of session");
            }
        }
        public void EndSession(int elapsedSeconds) {
            double earnings = Math.Round((Convert.ToDouble(elapsedSeconds) / 3600) * configProperties.Rate, 2);
            using (StreamWriter writer = new StreamWriter("log.txt", true)) {
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -End of session");
                writer.WriteLine("|--------------------------------------------------------------|");
                writer.WriteLine("| _TIME=" + elapsedSeconds+"; _EARNINGS=" + earnings + ";" + " _TOTAL=" + GetTotalEarnings() + "; ");
                writer.WriteLine("|--------------------------------------------------------------|\n");
            }
            //System.Windows.Forms.MessageBox.Show(elapsedSeconds.ToString());
            //write to log file
        }

        private double GetTotalEarnings()
        {
            return -1;
        }

        public bool AppendToLogFile(string content) {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "  " + content);
            }
            return true;
        }


    }
}
