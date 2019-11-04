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

        private void Form1_Load(object sender, EventArgs e) {
            foreach(var c in config.Contracts) {
                txtContracts.Items.Add(c.ClientName + " - " + c.ContractTitle);
            }
        }

        private void btnBeginSession_Click(object sender, EventArgs e) {
            timer.Start();
            lblSessionStatus.Text = "Session started...";
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            lblTimer.Text = time.ToString(@"hh\:mm\:ss");
            LogHelper.BeginSession();
        }

        private void btnEndSession_Click(object sender, EventArgs e) {
            timer.Stop();
            lblSessionStatus.Text = "Session ended...";
            LogHelper.EndSession(elapsedSeconds);
            elapsedSeconds = 0;
        }
    }
}
