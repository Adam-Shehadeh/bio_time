namespace bio_time {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnBeginSession = new System.Windows.Forms.Button();
            this.btnEndSession = new System.Windows.Forms.Button();
            this.txtLogContent = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContracts = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblSessionStatus = new System.Windows.Forms.Label();
            this.lblApplicationOutput = new System.Windows.Forms.Label();
            this.btnAddLog = new System.Windows.Forms.Button();
            this.btnOpenFileLocation = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBeginSession
            // 
            this.btnBeginSession.Enabled = false;
            this.btnBeginSession.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginSession.Location = new System.Drawing.Point(480, 86);
            this.btnBeginSession.Name = "btnBeginSession";
            this.btnBeginSession.Size = new System.Drawing.Size(142, 23);
            this.btnBeginSession.TabIndex = 2;
            this.btnBeginSession.Text = "Begin work session";
            this.btnBeginSession.UseVisualStyleBackColor = true;
            this.btnBeginSession.Click += new System.EventHandler(this.btnBeginSession_Click);
            // 
            // btnEndSession
            // 
            this.btnEndSession.Enabled = false;
            this.btnEndSession.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndSession.Location = new System.Drawing.Point(480, 115);
            this.btnEndSession.Name = "btnEndSession";
            this.btnEndSession.Size = new System.Drawing.Size(142, 23);
            this.btnEndSession.TabIndex = 3;
            this.btnEndSession.Text = "End work session";
            this.btnEndSession.UseVisualStyleBackColor = true;
            this.btnEndSession.Click += new System.EventHandler(this.btnEndSession_Click);
            // 
            // txtLogContent
            // 
            this.txtLogContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogContent.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogContent.Location = new System.Drawing.Point(12, 86);
            this.txtLogContent.Name = "txtLogContent";
            this.txtLogContent.Size = new System.Drawing.Size(462, 250);
            this.txtLogContent.TabIndex = 1;
            this.txtLogContent.Text = "";
            this.txtLogContent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtLogContent_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(385, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "Adam\'s Time Logger";
            // 
            // txtContracts
            // 
            this.txtContracts.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContracts.FormattingEnabled = true;
            this.txtContracts.Location = new System.Drawing.Point(12, 25);
            this.txtContracts.Name = "txtContracts";
            this.txtContracts.Size = new System.Drawing.Size(310, 21);
            this.txtContracts.TabIndex = 7;
            this.txtContracts.SelectedIndexChanged += new System.EventHandler(this.TxtContracts_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Client - Contract";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Log content: ";
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(479, 175);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(0, 19);
            this.lblTimer.TabIndex = 11;
            // 
            // lblSessionStatus
            // 
            this.lblSessionStatus.AutoSize = true;
            this.lblSessionStatus.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionStatus.Location = new System.Drawing.Point(480, 162);
            this.lblSessionStatus.Name = "lblSessionStatus";
            this.lblSessionStatus.Size = new System.Drawing.Size(0, 13);
            this.lblSessionStatus.TabIndex = 12;
            // 
            // lblApplicationOutput
            // 
            this.lblApplicationOutput.AutoSize = true;
            this.lblApplicationOutput.Location = new System.Drawing.Point(12, 339);
            this.lblApplicationOutput.Name = "lblApplicationOutput";
            this.lblApplicationOutput.Size = new System.Drawing.Size(0, 13);
            this.lblApplicationOutput.TabIndex = 13;
            // 
            // btnAddLog
            // 
            this.btnAddLog.Enabled = false;
            this.btnAddLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLog.Location = new System.Drawing.Point(480, 226);
            this.btnAddLog.Name = "btnAddLog";
            this.btnAddLog.Size = new System.Drawing.Size(142, 23);
            this.btnAddLog.TabIndex = 4;
            this.btnAddLog.Text = "Add log to file";
            this.btnAddLog.UseVisualStyleBackColor = true;
            this.btnAddLog.Click += new System.EventHandler(this.BtnAddLog_Click);
            // 
            // btnOpenFileLocation
            // 
            this.btnOpenFileLocation.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFileLocation.Location = new System.Drawing.Point(480, 255);
            this.btnOpenFileLocation.Name = "btnOpenFileLocation";
            this.btnOpenFileLocation.Size = new System.Drawing.Size(142, 23);
            this.btnOpenFileLocation.TabIndex = 5;
            this.btnOpenFileLocation.Text = "Open file location";
            this.btnOpenFileLocation.UseVisualStyleBackColor = true;
            this.btnOpenFileLocation.Click += new System.EventHandler(this.BtnOpenFileLocation_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLog.Location = new System.Drawing.Point(480, 284);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(142, 23);
            this.btnClearLog.TabIndex = 6;
            this.btnClearLog.Text = "Clear log file";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.BtnClearLog_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(480, 313);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(142, 23);
            this.btnSendEmail.TabIndex = 14;
            this.btnSendEmail.Text = "Email log to client";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.BtnSendEmail_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 361);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.lblApplicationOutput);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.lblSessionStatus);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContracts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLogContent);
            this.Controls.Add(this.btnEndSession);
            this.Controls.Add(this.btnBeginSession);
            this.Controls.Add(this.btnOpenFileLocation);
            this.Controls.Add(this.btnAddLog);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBeginSession;
        private System.Windows.Forms.Button btnEndSession;
        private System.Windows.Forms.RichTextBox txtLogContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtContracts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblSessionStatus;
        private System.Windows.Forms.Label lblApplicationOutput;
        private System.Windows.Forms.Button btnAddLog;
        private System.Windows.Forms.Button btnOpenFileLocation;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnSendEmail;
    }
}

