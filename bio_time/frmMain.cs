﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private SessionState sessionState = SessionState.Session_Not_Recording;
        LogHelper logger;
        ConfigModel config;
        ContractModel selectedContract;

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
            this.selectedContract = config.Contracts.Where(c => c.Index == txtContracts.SelectedIndex).FirstOrDefault();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (sessionState != SessionState.Session_Not_Recording)
            {
                btnEndSession.PerformClick();
            }
            base.OnClosing(e);
        }

        private void btnBeginSession_Click(object sender, EventArgs e) {
            logger = new LogHelper(config, txtContracts.SelectedIndex);

            switch (sessionState)
            {
                case SessionState.Session_Not_Recording:
                    sessionState = SessionState.Session_Recording;
                    btnBeginSession.Text = "Pause Session";
                    btnBeginSession.Enabled = true;
                    btnEndSession.Enabled = true;
                    btnAddLog.Enabled = true;
                    btnDownloadFile.Enabled = false;
                    btnClearLog.Enabled = false;
                    btnSendEmail.Enabled = false;
                    txtContracts.Enabled = false;
                    timer.Start();
                    lblSessionStatus.Text = "Session started...";
                    TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
                    lblTimer.Text = time.ToString(@"hh\:mm\:ss");
                    logger.BeginSession();
                    SaveLogMessage();
                    break;
                case SessionState.Session_Recording:
                    sessionState = SessionState.Session_Paused;
                    btnBeginSession.Text = "Resume Session";
                    btnEndSession.Enabled = true;
                    btnAddLog.Enabled = false;
                    btnDownloadFile.Enabled = false;
                    btnClearLog.Enabled = false;
                    btnSendEmail.Enabled = false;
                    txtContracts.Enabled = false;
                    timer.Stop();
                    lblSessionStatus.Text = "Session paused...";
                    logger.PauseSession();
                    break;
                case SessionState.Session_Paused:
                    sessionState = SessionState.Session_Recording;
                    btnBeginSession.Text = "Pause Session";
                    btnBeginSession.Enabled = true;
                    btnEndSession.Enabled = true;
                    btnAddLog.Enabled = true;
                    btnDownloadFile.Enabled = false;
                    btnClearLog.Enabled = false;
                    btnSendEmail.Enabled = false;
                    txtContracts.Enabled = false;
                    timer.Start();
                    lblSessionStatus.Text = "Session resumed...";
                    logger.ResumeSession();
                    break;
            }
        }

        private void btnEndSession_Click(object sender, EventArgs e) {
            sessionState = SessionState.Session_Not_Recording;
            btnBeginSession.Text = "Begin Session";
            btnBeginSession.Enabled = true;
            btnEndSession.Enabled = false;
            btnAddLog.Enabled = false;
            btnDownloadFile.Enabled = true;
            btnClearLog.Enabled = true;
            btnSendEmail.Enabled = true;
            txtContracts.Enabled = true;
            SaveLogMessage();
            timer.Stop();
            lblSessionStatus.Text = "Session ended...";
            logger.EndSession(elapsedSeconds);
            //logger.EndSession(7469); //Test value
            elapsedSeconds = 0;
        }

        private void TxtContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger = new LogHelper(config, txtContracts.SelectedIndex);
            int ind = (sender as ComboBox).SelectedIndex;
            var configModel = ConfigHelper.GetConfigProperties();
            configModel.LastSelectedContractIndex = ind;
            ConfigHelper.UpdateConfig(configModel);
            this.selectedContract = config.Contracts.Where(c => c.Index == txtContracts.SelectedIndex).FirstOrDefault();
        }

        private void TxtLogContent_KeyUp(object sender, KeyEventArgs e)
        {

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

        private void BtnDownloadFile_Click(object sender, EventArgs e)
        {
            
            try
            {
                string filepath = Environment.CurrentDirectory + "\\" + selectedContract.LogFileName;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                var timeLog = SQLHelper.GetLogFiles().Where(r=>r.FileName == selectedContract.LogFileName).FirstOrDefault();
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    if (timeLog != null)
                        writer.Write(timeLog.FileContent);
                }
                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filepath));
            } catch (Exception ex)
            {
                MessageBox.Show("Could not find log file ("+selectedContract.LogFileName+"). " + ex.Message);

            }
        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to E-mail the log file to " + config.Contracts[txtContracts.SelectedIndex].ClientEmail + "?", "E-mail ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (File.Exists(selectedContract.LogFileName))
                {
                    START_EMAIL:
                    if (EmailHelper.SendEmail(config.DeveloperEmail,
                    "xrnhynpyabefpawf",
                    config.Contracts[txtContracts.SelectedIndex].ClientEmail,
                    "Worklog for " + config.Contracts[txtContracts.SelectedIndex].ContractTitle + " has been updated.",
                    File.ReadAllText(selectedContract.LogFileName)))
                    {
                        MessageBox.Show("Successfully emailed log file to " + config.Contracts[txtContracts.SelectedIndex].ClientName + "!", "Email sender thing woot woot", MessageBoxButtons.OK);
                    } else
                    {
                        var result = MessageBox.Show("Failed to send email to " + config.Contracts[txtContracts.SelectedIndex].ClientEmail + "... :(", "Email sender thing failed fk", MessageBoxButtons.RetryCancel);
                        if (result == DialogResult.Retry)
                        {
                            goto START_EMAIL;
                        }
                    }
                } else
                {
                    MessageBox.Show("Could not find " + selectedContract.LogFileName, "fkkkkk", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the log file?", "Clear log file", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                logger.ClearLogFile();
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            txtLogContent.Width = this.Width - 185;
            txtLogContent.Height = this.Height - 155;
        }
    }
}
