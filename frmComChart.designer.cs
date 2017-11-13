namespace ComReader
{
    partial class frmComChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComChart));
            this.picChart = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblSPO2 = new System.Windows.Forms.Label();
            this.lblHR = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picChart
            // 
            this.picChart.BackColor = System.Drawing.Color.Transparent;
            this.picChart.Cursor = System.Windows.Forms.Cursors.Default;
            this.picChart.Location = new System.Drawing.Point(218, 30);
            this.picChart.Name = "picChart";
            this.picChart.Size = new System.Drawing.Size(358, 60);
            this.picChart.TabIndex = 0;
            this.picChart.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar.Location = new System.Drawing.Point(77, 40);
            this.progressBar.Maximum = 15;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(130, 25);
            this.progressBar.TabIndex = 1;
            // 
            // lblSPO2
            // 
            this.lblSPO2.AutoSize = true;
            this.lblSPO2.BackColor = System.Drawing.Color.Transparent;
            this.lblSPO2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSPO2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblSPO2.Location = new System.Drawing.Point(67, 68);
            this.lblSPO2.Name = "lblSPO2";
            this.lblSPO2.Size = new System.Drawing.Size(0, 22);
            this.lblSPO2.TabIndex = 2;
            // 
            // lblHR
            // 
            this.lblHR.AutoSize = true;
            this.lblHR.BackColor = System.Drawing.Color.Transparent;
            this.lblHR.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblHR.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblHR.Location = new System.Drawing.Point(162, 68);
            this.lblHR.Name = "lblHR";
            this.lblHR.Size = new System.Drawing.Size(0, 22);
            this.lblHR.TabIndex = 3;
            // 
            // trackBar
            // 
            this.trackBar.AutoSize = false;
            this.trackBar.BackColor = System.Drawing.Color.White;
            this.trackBar.Location = new System.Drawing.Point(77, 16);
            this.trackBar.Maximum = 100;
            this.trackBar.Minimum = 40;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(130, 14);
            this.trackBar.TabIndex = 4;
            this.trackBar.Value = 70;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picIcon.Image = global::simri.Properties.Resources.redStatus;
            this.picIcon.Location = new System.Drawing.Point(575, 9);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(18, 20);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 5;
            this.picIcon.TabStop = false;
            // 
            // frmComChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::simri.Properties.Resources.chart;
            this.ClientSize = new System.Drawing.Size(600, 104);
            this.ControlBox = false;
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.lblHR);
            this.Controls.Add(this.lblSPO2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.picChart);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmComChart";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "曲线图";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Red;
            ((System.ComponentModel.ISupportInitialize)(this.picChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picChart;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblSPO2;
        private System.Windows.Forms.Label lblHR;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox picIcon;

    }
}