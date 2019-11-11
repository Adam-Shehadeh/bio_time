using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using bio_time.Models;
using System.Text.RegularExpressions;

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
            double curEarnings = ConvertSecondsToEarnings(elapsedSeconds);
            double totalEarnings = GetTotalEarnings(curEarnings);
            using (StreamWriter writer = new StreamWriter("log.txt", true)) {
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -End of session");
                writer.WriteLine("|--------------------------------------------------------------|");
                writer.WriteLine("| _TIME=" + elapsedSeconds+"; _EARNINGS=$" + curEarnings + ";" + " _TOTAL=$" + totalEarnings + "; ");
                writer.WriteLine("|--------------------------------------------------------------|\n");
            }
            //System.Windows.Forms.MessageBox.Show(elapsedSeconds.ToString());
            //write to log file
        }
        private double ConvertSecondsToEarnings(int elapsedSeconds)
        {
            return Math.Round((Convert.ToDouble(elapsedSeconds) / 3600) * configProperties.Rate, 2);
        }
        private double GetTotalEarnings(double currentSessionEarnings)
        {
            double local = currentSessionEarnings;
            string fileContents = File.ReadAllText(Environment.CurrentDirectory + "\\log.txt");
            string searchString = "_EARNINGS=$";
            var earningsOccurrances = Regex.Matches(fileContents, searchString);
            int curStartIndex = 0;
            
            do
            {
                curStartIndex = fileContents.IndexOf(searchString, curStartIndex);
                
                if (curStartIndex != -1)
                {
                    int curEndIndex = fileContents.IndexOf(";", curStartIndex);
                    string occuranceString = fileContents.Substring(curStartIndex, curEndIndex - curStartIndex);
                    local += double.Parse(occuranceString.Replace(searchString, ""));
                    curStartIndex = curEndIndex;
                }
            } while (curStartIndex != -1);
            return local;
        }

        public bool AppendToLogFile(string content) {
            string curTimeStamp = "| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "  ";
            string curBlankString = "|                         ";
            content = content.Replace("\n", "\n" + curBlankString);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine(curTimeStamp + content);
            }
            return true;
        }

        public bool ClearLogFile()
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.Write(string.Empty);
            }
            return true;
        }

    }
}
