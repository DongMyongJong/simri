namespace simri
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmGame = new System.Windows.Forms.Timer(this.components);
            this.axShockwaveView = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.axShockwaveSound = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.tmCollector = new System.Windows.Forms.Timer(this.components);
            this.panelReport = new System.Windows.Forms.Panel();
            this.lnkClose = new System.Windows.Forms.LinkLabel();
            this.lnkPrint = new System.Windows.Forms.LinkLabel();
            this.lnkSave = new System.Windows.Forms.LinkLabel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveSound)).BeginInit();
            this.panelReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // axShockwaveView
            // 
            this.axShockwaveView.Enabled = true;
            this.axShockwaveView.Location = new System.Drawing.Point(12, 12);
            this.axShockwaveView.Name = "axShockwaveView";
            this.axShockwaveView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveView.OcxState")));
            this.axShockwaveView.Size = new System.Drawing.Size(192, 192);
            this.axShockwaveView.TabIndex = 1;
            // 
            // axShockwaveFlash
            // 
            this.axShockwaveFlash.Enabled = true;
            this.axShockwaveFlash.Location = new System.Drawing.Point(70, 34);
            this.axShockwaveFlash.Name = "axShockwaveFlash";
            this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
            this.axShockwaveFlash.Size = new System.Drawing.Size(192, 192);
            this.axShockwaveFlash.TabIndex = 2;
            // 
            // axShockwaveSound
            // 
            this.axShockwaveSound.Enabled = true;
            this.axShockwaveSound.Location = new System.Drawing.Point(150, 79);
            this.axShockwaveSound.Name = "axShockwaveSound";
            this.axShockwaveSound.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveSound.OcxState")));
            this.axShockwaveSound.Size = new System.Drawing.Size(192, 192);
            this.axShockwaveSound.TabIndex = 3;
            // 
            // tmCollector
            // 
            this.tmCollector.Interval = 3000;
            // 
            // panelReport
            // 
            this.panelReport.Controls.Add(this.lnkClose);
            this.panelReport.Controls.Add(this.lnkPrint);
            this.panelReport.Controls.Add(this.lnkSave);
            this.panelReport.Controls.Add(this.webBrowser);
            this.panelReport.Location = new System.Drawing.Point(371, 34);
            this.panelReport.Name = "panelReport";
            this.panelReport.Size = new System.Drawing.Size(294, 184);
            this.panelReport.TabIndex = 4;
            // 
            // lnkClose
            // 
            this.lnkClose.AutoSize = true;
            this.lnkClose.BackColor = System.Drawing.Color.White;
            this.lnkClose.Font = new System.Drawing.Font("NSimSun", 15.75F, System.Drawing.FontStyle.Bold);
            this.lnkClose.Location = new System.Drawing.Point(208, 37);
            this.lnkClose.Name = "lnkClose";
            this.lnkClose.Size = new System.Drawing.Size(56, 21);
            this.lnkClose.TabIndex = 8;
            this.lnkClose.TabStop = true;
            this.lnkClose.Text = "关闭";
            this.lnkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClose_LinkClicked);
            // 
            // lnkPrint
            // 
            this.lnkPrint.AutoSize = true;
            this.lnkPrint.BackColor = System.Drawing.Color.White;
            this.lnkPrint.Font = new System.Drawing.Font("NSimSun", 15.75F, System.Drawing.FontStyle.Bold);
            this.lnkPrint.Location = new System.Drawing.Point(130, 37);
            this.lnkPrint.Name = "lnkPrint";
            this.lnkPrint.Size = new System.Drawing.Size(56, 21);
            this.lnkPrint.TabIndex = 7;
            this.lnkPrint.TabStop = true;
            this.lnkPrint.Text = "打印";
            this.lnkPrint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPrint_LinkClicked);
            // 
            // lnkSave
            // 
            this.lnkSave.AutoSize = true;
            this.lnkSave.BackColor = System.Drawing.Color.White;
            this.lnkSave.Font = new System.Drawing.Font("NSimSun", 15.75F, System.Drawing.FontStyle.Bold);
            this.lnkSave.Location = new System.Drawing.Point(60, 37);
            this.lnkSave.Name = "lnkSave";
            this.lnkSave.Size = new System.Drawing.Size(56, 21);
            this.lnkSave.TabIndex = 6;
            this.lnkSave.TabStop = true;
            this.lnkSave.Text = "导出";
            this.lnkSave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSave_LinkClicked);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(31, 22);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(50);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(250, 148);
            this.webBrowser.TabIndex = 5;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 375);
            this.Controls.Add(this.panelReport);
            this.Controls.Add(this.axShockwaveSound);
            this.Controls.Add(this.axShockwaveFlash);
            this.Controls.Add(this.axShockwaveView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "压力与情绪管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveSound)).EndInit();
            this.panelReport.ResumeLayout(false);
            this.panelReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmGame;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveView;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveSound;
        private System.Windows.Forms.Timer tmCollector;
        private System.Windows.Forms.Panel panelReport;
        private System.Windows.Forms.LinkLabel lnkClose;
        private System.Windows.Forms.LinkLabel lnkPrint;
        private System.Windows.Forms.LinkLabel lnkSave;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

