using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bio_time.Helpers;
using bio_time.Models;

namespace bio_time {
    public partial class frmMain : Form {

        private Timer timer;
        private int elapsedSeconds;
        private bool sessionInProgress = false;
        LogHelper logger;

        ConfigModel config;
        public frmMain() {
            InitializeComponent();
            config = ConfigHelper.GetConfigProperties();
            
            elapsedSeconds = 0;
            timer = new Timer();
            timer.Interval = 1000;
            //timer.Enabled = true;
            timer.Tick += (o, e) => {
                elapsedSeconds++; //global tracker variable
                TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
                lblTimer.Text = time.ToString(@"hh\:mm\:ss");
            };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (sessionInProgress)
            {
                btnEndSession.PerformClick();
            }
            base.OnClosing(e);
        }

        private void btnBeginSession_Click(object sender, EventArgs e) {

            logger = new LogHelper(config, txtContracts.SelectedIndex);
            sessionInProgress = true;
            btnBeginSession.Enabled = false;
            btnEndSession.Enabled = true;
            btnAddLog.Enabled = true;
            txtContracts.Enabled = false;
            timer.Start();
            lblSessionStatus.Text = "Session started...";
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            lblTimer.Text = time.ToString(@"hh\:mm\:ss");
            logger.BeginSession();
            SaveLogMessage();
        }

        private void btnEndSession_Click(object sender, EventArgs e) {
            btnBeginSession.Enabled = true;
            btnEndSession.Enabled = false;
            btnAddLog.Enabled = false;
            txtContracts.Enabled = true;
            SaveLogMessage();
            timer.Stop();
            lblSessionStatus.Text = "Session ended...";
            //logger.EndSession(elapsedSeconds);
            logger.EndSession(7469); //Test value
            elapsedSeconds = 0;
        }

        private void TxtContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger = new LogHelper(config, txtContracts.SelectedIndex);
            int ind = (sender as ComboBox).SelectedIndex;
            var configModel = ConfigHelper.GetConfigProperties();
            configModel.LastSelectedContractIndex = ind;
            ConfigHelper.UpdateConfig(configModel);
        }

        private void TxtLogContent_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtLogContent.Text != string.Empty && !sessionInProgress)
            {
                btnBeginSession.Enabled = true;
            } else
            {
                btnBeginSession.Enabled = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            foreach (var c in config.Contracts)
            {
                txtContracts.Items.Add(c.ClientName + " - " + c.ContractTitle);
            }
            txtContracts.SelectedIndex = config.LastSelectedContractIndex;
        }

        //Saves current content of txtLogContent and clears it.
        private void SaveLogMessage()
        {
            if (logger.AppendToLogFile(txtLogContent.Text))
            {
                WriteToApplicationOutput("Successfully logged entry to file. Woo!");
            } else
            {
                WriteToApplicationOutput("Failed to add entry to file. Darn...");
            }
            txtLogContent.Text = string.Empty;
        }
        private async void WriteToApplicationOutput(string message)
        {
            lblApplicationOutput.Text = message;
            await Task.Delay(5000);
            lblApplicationOutput.Text = string.Empty;
        }

        private void BtnAddLog_Click(object sender, EventArgs e)
        {
            SaveLogMessage();
        }
    }
}
