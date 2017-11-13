namespace simri
{
    partial class frmAddModifyFlash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddModifyFlash));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSoundfile = new System.Windows.Forms.TextBox();
            this.btnOpensound = new System.Windows.Forms.Button();
            this.dgImage = new System.Windows.Forms.DataGridView();
            this.imageno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imagefile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imagelife = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.btnDelImage = new System.Windows.Forms.Button();
            this.btnChangeImage = new System.Windows.Forms.Button();
            this.btnUpImage = new System.Windows.Forms.Button();
            this.btnDownImage = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "主题";
            // 
            // txtTitle
            // 
            this.txtTitle.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtTitle.Location = new System.Drawing.Point(70, 17);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(150, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "音乐文件";
            // 
            // txtSoundfile
            // 
            this.txtSoundfile.BackColor = System.Drawing.Color.White;
            this.txtSoundfile.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSoundfile.Location = new System.Drawing.Point(70, 58);
            this.txtSoundfile.Name = "txtSoundfile";
            this.txtSoundfile.ReadOnly = true;
            this.txtSoundfile.Size = new System.Drawing.Size(356, 20);
            this.txtSoundfile.TabIndex = 3;
            // 
            // btnOpensound
            // 
            this.btnOpensound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpensound.Location = new System.Drawing.Point(432, 57);
            this.btnOpensound.Name = "btnOpensound";
            this.btnOpensound.Size = new System.Drawing.Size(75, 23);
            this.btnOpensound.TabIndex = 4;
            this.btnOpensound.Text = "打开";
            this.btnOpensound.UseVisualStyleBackColor = true;
            this.btnOpensound.Click += new System.EventHandler(this.btnOpensound_Click);
            // 
            // dgImage
            // 
            this.dgImage.AllowUserToAddRows = false;
            this.dgImage.AllowUserToDeleteRows = false;
            this.dgImage.AllowUserToResizeRows = false;
            this.dgImage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgImage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgImage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageno,
            this.imagefile,
            this.imagelife});
            this.dgImage.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dgImage.Location = new System.Drawing.Point(15, 99);
            this.dgImage.MultiSelect = false;
            this.dgImage.Name = "dgImage";
            this.dgImage.RowHeadersVisible = false;
            this.dgImage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgImage.Size = new System.Drawing.Size(492, 154);
            this.dgImage.TabIndex = 5;
            // 
            // imageno
            // 
            this.imageno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageno.HeaderText = "序号";
            this.imageno.Name = "imageno";
            this.imageno.ReadOnly = true;
            this.imageno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.imageno.Width = 60;
            // 
            // imagefile
            // 
            this.imagefile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imagefile.HeaderText = "文件名";
            this.imagefile.Name = "imagefile";
            this.imagefile.ReadOnly = true;
            this.imagefile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.imagefile.Width = 80;
            // 
            // imagelife
            // 
            this.imagelife.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imagelife.HeaderText = "显示时间(秒)";
            this.imagelife.Name = "imagelife";
            this.imagelife.ReadOnly = true;
            this.imagelife.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.imagelife.Width = 200;
            // 
            // btnAddImage
            // 
            this.btnAddImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddImage.Location = new System.Drawing.Point(15, 260);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(75, 23);
            this.btnAddImage.TabIndex = 6;
            this.btnAddImage.Text = "添加";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // btnDelImage
            // 
            this.btnDelImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelImage.Location = new System.Drawing.Point(119, 260);
            this.btnDelImage.Name = "btnDelImage";
            this.btnDelImage.Size = new System.Drawing.Size(75, 23);
            this.btnDelImage.TabIndex = 7;
            this.btnDelImage.Text = "删除";
            this.btnDelImage.UseVisualStyleBackColor = true;
            this.btnDelImage.Click += new System.EventHandler(this.btnDelImage_Click);
            // 
            // btnChangeImage
            // 
            this.btnChangeImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeImage.Location = new System.Drawing.Point(223, 260);
            this.btnChangeImage.Name = "btnChangeImage";
            this.btnChangeImage.Size = new System.Drawing.Size(75, 23);
            this.btnChangeImage.TabIndex = 8;
            this.btnChangeImage.Text = "修改";
            this.btnChangeImage.UseVisualStyleBackColor = true;
            this.btnChangeImage.Click += new System.EventHandler(this.btnChangeImage_Click);
            // 
            // btnUpImage
            // 
            this.btnUpImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpImage.Location = new System.Drawing.Point(327, 260);
            this.btnUpImage.Name = "btnUpImage";
            this.btnUpImage.Size = new System.Drawing.Size(75, 23);
            this.btnUpImage.TabIndex = 9;
            this.btnUpImage.Text = "上移项目";
            this.btnUpImage.UseVisualStyleBackColor = true;
            this.btnUpImage.Click += new System.EventHandler(this.btnUpImage_Click);
            // 
            // btnDownImage
            // 
            this.btnDownImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownImage.Location = new System.Drawing.Point(431, 260);
            this.btnDownImage.Name = "btnDownImage";
            this.btnDownImage.Size = new System.Drawing.Size(75, 23);
            this.btnDownImage.TabIndex = 10;
            this.btnDownImage.Text = "下移项目";
            this.btnDownImage.UseVisualStyleBackColor = true;
            this.btnDownImage.Click += new System.EventHandler(this.btnDownImage_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(431, 297);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // frmAddModifyFlash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 332);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDownImage);
            this.Controls.Add(this.btnUpImage);
            this.Controls.Add(this.btnChangeImage);
            this.Controls.Add(this.btnDelImage);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.dgImage);
            this.Controls.Add(this.btnOpensound);
            this.Controls.Add(this.txtSoundfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddModifyFlash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAddModifyFlash";
            this.Load += new System.EventHandler(this.frmAddModifyFlash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSoundfile;
        private System.Windows.Forms.Button btnOpensound;
        private System.Windows.Forms.DataGridView dgImage;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.Button btnDelImage;
        private System.Windows.Forms.Button btnChangeImage;
        private System.Windows.Forms.Button btnUpImage;
        private System.Windows.Forms.Button btnDownImage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn imageno;
        private System.Windows.Forms.DataGridViewTextBoxColumn imagefile;
        private System.Windows.Forms.DataGridViewTextBoxColumn imagelife;
    }
}