using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using bio_time.Models;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bio_time.Helpers {
    public class LogHelper {
        ConfigModel configProperties;
        int selectedContractIndex;
        ContractModel selectedContract;
        public LogHelper(ConfigModel configProperties, int selectedContractIndex)
        {
            this.configProperties = configProperties;
            this.selectedContractIndex = selectedContractIndex;
            this.selectedContract = configProperties.Contracts.Where(c => c.Index == selectedContractIndex).FirstOrDefault();
        }
        public void BeginSession() {

            //Get current file from SQL and update local
            TimeLogFile file = SQLHelper.GetLogFiles().Where(r => r.FileName == selectedContract.LogFileName).FirstOrDefault();
            if (file != null)
            {
                using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, false))
                {
                    writer.Write(file.FileContent);
                }
            }

            //Update file locally. 
            using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, true)) {
                writer.WriteLine("| " + DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                writer.WriteLine("|=========================================================================|");
                writer.WriteLine("| Client=" + selectedContract.ClientName + "; " + "Contract=" + selectedContract.ContractTitle + "; ");
                writer.WriteLine("|=========================================================================|");
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -Beginning of session");
            }
        }
        public void PauseSession()
        {
            using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, true))
            {
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -Session paused.");
            }
        }
        public void ResumeSession()
        {
            using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, true))
            {
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -Session resumed.");
            }
        }
        public void EndSession(int elapsedSeconds) {
            double curEarnings = ConvertSecondsToEarnings(elapsedSeconds);
            double totalEarnings = GetTotalEarnings(curEarnings);
            string duration = TimeSpan.FromSeconds(elapsedSeconds).ToString(@"hh\:mm\:ss");
            using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, true)) {
                writer.WriteLine("| " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -End of session");
                writer.WriteLine("|=========================================================================|");
                writer.WriteLine("| _TIME=" + elapsedSeconds+"; _DURATION="+duration+"; _EARNINGS=$" + curEarnings + ";" + " _TOTAL=$" + totalEarnings + "; ");
                writer.WriteLine("|=========================================================================|\n\n");
            }

            //Sync on SQL server
            if (!SQLHelper.SaveLogFile(new TimeLogFile()
            {
                FileName = selectedContract.LogFileName,
                FileContent = File.ReadAllText(selectedContract.LogFileName)
            }))
            {
                System.Windows.Forms.MessageBox.Show("Could not sync to SQL...fk");
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
            string fileContents = File.ReadAllText(Environment.CurrentDirectory + "\\" + selectedContract.LogFileName);
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
            using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, true))
            {
                writer.WriteLine(curTimeStamp + content);
            }
            return true;
        }

        public bool ClearLogFile()
        {
            using (StreamWriter writer = new StreamWriter(selectedContract.LogFileName, false)){}
            if (SQLHelper.SaveLogFile(new TimeLogFile()
            {
                FileName = selectedContract.LogFileName,
                FileContent = File.ReadAllText(selectedContract.LogFileName)
            }))
            {
                System.Windows.Forms.MessageBox.Show("Successfully cleared log file " + selectedContract.LogFileName + "!", "Worked once again hehe best programmer ever", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            } else {
                return false;
            }
        }
    }
}
