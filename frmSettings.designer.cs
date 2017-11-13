namespace simri
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnClose = new System.Windows.Forms.Button();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsUser = new System.Windows.Forms.ToolStripDropDownButton();
            this.addUser = new System.Windows.Forms.ToolStripMenuItem();
            this.delUser = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyUser = new System.Windows.Forms.ToolStripMenuItem();
            this.searchUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMusicEmotion = new System.Windows.Forms.ToolStripButton();
            this.tsMusicFree = new System.Windows.Forms.ToolStripButton();
            this.tsMusicSelf = new System.Windows.Forms.ToolStripButton();
            this.tsHistory = new System.Windows.Forms.ToolStripButton();
            this.tsImagination = new System.Windows.Forms.ToolStripButton();
            this.tsRelaxmethod = new System.Windows.Forms.ToolStripButton();
            this.tsChangeAdmin = new System.Windows.Forms.ToolStripButton();
            this.dgUserMan = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.btnDelFolder = new System.Windows.Forms.Button();
            this.btnUpFolder = new System.Windows.Forms.Button();
            this.btnDownFolder = new System.Windows.Forms.Button();
            this.btnDownFile = new System.Windows.Forms.Button();
            this.btnUpFile = new System.Windows.Forms.Button();
            this.btnDelFile = new System.Windows.Forms.Button();
            this.btnChangeFile = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.dgHistory = new System.Windows.Forms.DataGridView();
            this.mark = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelHistory = new System.Windows.Forms.Button();
            this.panelUser = new System.Windows.Forms.Panel();
            this.panelSound = new System.Windows.Forms.Panel();
            this.panelHistory = new System.Windows.Forms.Panel();
            this.panelFlash = new System.Windows.Forms.Panel();
            this.dgFlash = new System.Windows.Forms.DataGridView();
            this.flashno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flashtitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soundfile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDownFlash = new System.Windows.Forms.Button();
            this.btnUpFlash = new System.Windows.Forms.Button();
            this.btnChangeFlash = new System.Windows.Forms.Button();
            this.btnDelFlash = new System.Windows.Forms.Button();
            this.btnAddFlash = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFlash)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(654, 353);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUser,
            this.tsMusicEmotion,
            this.tsMusicFree,
            this.tsMusicSelf,
            this.tsHistory,
            this.tsImagination,
            this.tsRelaxmethod,
            this.tsChangeAdmin});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(741, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsUser
            // 
            this.tsUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUser,
            this.delUser,
            this.modifyUser,
            this.searchUser});
            this.tsUser.Image = ((System.Drawing.Image)(resources.GetObject("tsUser.Image")));
            this.tsUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUser.Name = "tsUser";
            this.tsUser.Size = new System.Drawing.Size(84, 22);
            this.tsUser.Text = "用户管理";
            this.tsUser.Click += new System.EventHandler(this.user_Click);
            // 
            // addUser
            // 
            this.addUser.Image = global::simri.Properties.Resources.add;
            this.addUser.Name = "addUser";
            this.addUser.Size = new System.Drawing.Size(146, 22);
            this.addUser.Text = "添加用户";
            this.addUser.Click += new System.EventHandler(this.addUser_Click);
            // 
            // delUser
            // 
            this.delUser.Image = global::simri.Properties.Resources.minus;
            this.delUser.Name = "delUser";
            this.delUser.Size = new System.Drawing.Size(146, 22);
            this.delUser.Text = "删除用户";
            this.delUser.Click += new System.EventHandler(this.delUser_Click);
            // 
            // modifyUser
            // 
            this.modifyUser.Image = global::simri.Properties.Resources.modify;
            this.modifyUser.Name = "modifyUser";
            this.modifyUser.Size = new System.Drawing.Size(146, 22);
            this.modifyUser.Text = "修改用户信息";
            this.modifyUser.Click += new System.EventHandler(this.modifyUser_Click);
            // 
            // searchUser
            // 
            this.searchUser.Image = global::simri.Properties.Resources.search;
            this.searchUser.Name = "searchUser";
            this.searchUser.Size = new System.Drawing.Size(146, 22);
            this.searchUser.Text = "查询用户";
            this.searchUser.Click += new System.EventHandler(this.searchUser_Click);
            // 
            // tsMusicEmotion
            // 
            this.tsMusicEmotion.CheckOnClick = true;
            this.tsMusicEmotion.Image = global::simri.Properties.Resources.music;
            this.tsMusicEmotion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMusicEmotion.Name = "tsMusicEmotion";
            this.tsMusicEmotion.Size = new System.Drawing.Size(75, 22);
            this.tsMusicEmotion.Text = "情绪音乐";
            this.tsMusicEmotion.Click += new System.EventHandler(this.musicEmotion_Click);
            // 
            // tsMusicFree
            // 
            this.tsMusicFree.CheckOnClick = true;
            this.tsMusicFree.Image = global::simri.Properties.Resources.music;
            this.tsMusicFree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMusicFree.Name = "tsMusicFree";
            this.tsMusicFree.Size = new System.Drawing.Size(75, 22);
            this.tsMusicFree.Text = "个性音乐";
            this.tsMusicFree.Click += new System.EventHandler(this.musicFree_Click);
            // 
            // tsMusicSelf
            // 
            this.tsMusicSelf.CheckOnClick = true;
            this.tsMusicSelf.Image = global::simri.Properties.Resources.music;
            this.tsMusicSelf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMusicSelf.Name = "tsMusicSelf";
            this.tsMusicSelf.Size = new System.Drawing.Size(75, 22);
            this.tsMusicSelf.Text = "自选音乐";
            this.tsMusicSelf.Click += new System.EventHandler(this.musicSelf_Click);
            // 
            // tsHistory
            // 
            this.tsHistory.Image = global::simri.Properties.Resources.history;
            this.tsHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsHistory.Name = "tsHistory";
            this.tsHistory.Size = new System.Drawing.Size(75, 22);
            this.tsHistory.Text = "历史管理";
            this.tsHistory.Click += new System.EventHandler(this.tsHistory_Click);
            // 
            // tsImagination
            // 
            this.tsImagination.Image = global::simri.Properties.Resources.imagination;
            this.tsImagination.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsImagination.Name = "tsImagination";
            this.tsImagination.Size = new System.Drawing.Size(75, 22);
            this.tsImagination.Text = "想象放松";
            this.tsImagination.Click += new System.EventHandler(this.tsImagination_Click);
            // 
            // tsRelaxmethod
            // 
            this.tsRelaxmethod.Image = global::simri.Properties.Resources.relaxmethod;
            this.tsRelaxmethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRelaxmethod.Name = "tsRelaxmethod";
            this.tsRelaxmethod.Size = new System.Drawing.Size(75, 22);
            this.tsRelaxmethod.Text = "放松方法";
            this.tsRelaxmethod.Click += new System.EventHandler(this.tsRelaxmethod_Click);
            // 
            // tsChangeAdmin
            // 
            this.tsChangeAdmin.Image = global::simri.Properties.Resources.key;
            this.tsChangeAdmin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsChangeAdmin.Name = "tsChangeAdmin";
            this.tsChangeAdmin.Size = new System.Drawing.Size(111, 22);
            this.tsChangeAdmin.Text = "修改管理员密码";
            this.tsChangeAdmin.Click += new System.EventHandler(this.changeAdmin_Click);
            // 
            // dgUserMan
            // 
            this.dgUserMan.AllowUserToAddRows = false;
            this.dgUserMan.AllowUserToDeleteRows = false;
            this.dgUserMan.AllowUserToResizeRows = false;
            this.dgUserMan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgUserMan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUserMan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.username,
            this.password,
            this.gender,
            this.birthday,
            this.job});
            this.dgUserMan.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dgUserMan.Location = new System.Drawing.Point(12, 28);
            this.dgUserMan.MultiSelect = false;
            this.dgUserMan.Name = "dgUserMan";
            this.dgUserMan.RowHeadersVisible = false;
            this.dgUserMan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUserMan.Size = new System.Drawing.Size(616, 150);
            this.dgUserMan.TabIndex = 3;
            // 
            // no
            // 
            this.no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.no.HeaderText = "顺序";
            this.no.Name = "no";
            this.no.ReadOnly = true;
            this.no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.no.Width = 80;
            // 
            // username
            // 
            this.username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.username.HeaderText = "用户名";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.username.Width = 120;
            // 
            // password
            // 
            this.password.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.password.HeaderText = "密码";
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.password.Width = 80;
            // 
            // gender
            // 
            this.gender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gender.HeaderText = "性别";
            this.gender.Name = "gender";
            this.gender.ReadOnly = true;
            this.gender.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gender.Width = 80;
            // 
            // birthday
            // 
            this.birthday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.birthday.HeaderText = "出生日期";
            this.birthday.Name = "birthday";
            this.birthday.ReadOnly = true;
            this.birthday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.birthday.Width = 200;
            // 
            // job
            // 
            this.job.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.job.HeaderText = "职业";
            this.job.Name = "job";
            this.job.ReadOnly = true;
            this.job.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.job.Width = 80;
            // 
            // treeView
            // 
            this.treeView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.treeView.Location = new System.Drawing.Point(-16, 62);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(393, 301);
            this.treeView.TabIndex = 4;
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFolder.Location = new System.Drawing.Point(497, 40);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(75, 23);
            this.btnAddFolder.TabIndex = 5;
            this.btnAddFolder.Text = "添加文件夹";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // btnChangeFolder
            // 
            this.btnChangeFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeFolder.Location = new System.Drawing.Point(497, 84);
            this.btnChangeFolder.Name = "btnChangeFolder";
            this.btnChangeFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFolder.TabIndex = 6;
            this.btnChangeFolder.Text = "修改文件夹";
            this.btnChangeFolder.UseVisualStyleBackColor = true;
            this.btnChangeFolder.Click += new System.EventHandler(this.btnChangeFolder_Click);
            // 
            // btnDelFolder
            // 
            this.btnDelFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelFolder.Location = new System.Drawing.Point(497, 128);
            this.btnDelFolder.Name = "btnDelFolder";
            this.btnDelFolder.Size = new System.Drawing.Size(75, 23);
            this.btnDelFolder.TabIndex = 7;
            this.btnDelFolder.Text = "删除文件夹";
            this.btnDelFolder.UseVisualStyleBackColor = true;
            this.btnDelFolder.Click += new System.EventHandler(this.btnDelFolder_Click);
            // 
            // btnUpFolder
            // 
            this.btnUpFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpFolder.Location = new System.Drawing.Point(497, 172);
            this.btnUpFolder.Name = "btnUpFolder";
            this.btnUpFolder.Size = new System.Drawing.Size(75, 23);
            this.btnUpFolder.TabIndex = 8;
            this.btnUpFolder.Text = "上移文件夹";
            this.btnUpFolder.UseVisualStyleBackColor = true;
            this.btnUpFolder.Click += new System.EventHandler(this.btnUpFolder_Click);
            // 
            // btnDownFolder
            // 
            this.btnDownFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownFolder.Location = new System.Drawing.Point(497, 216);
            this.btnDownFolder.Name = "btnDownFolder";
            this.btnDownFolder.Size = new System.Drawing.Size(75, 23);
            this.btnDownFolder.TabIndex = 9;
            this.btnDownFolder.Text = "下移文件夹";
            this.btnDownFolder.UseVisualStyleBackColor = true;
            this.btnDownFolder.Click += new System.EventHandler(this.btnDownFolder_Click);
            // 
            // btnDownFile
            // 
            this.btnDownFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownFile.Location = new System.Drawing.Point(621, 216);
            this.btnDownFile.Name = "btnDownFile";
            this.btnDownFile.Size = new System.Drawing.Size(75, 23);
            this.btnDownFile.TabIndex = 14;
            this.btnDownFile.Text = "下移文件";
            this.btnDownFile.UseVisualStyleBackColor = true;
            this.btnDownFile.Click += new System.EventHandler(this.btnDownFile_Click);
            // 
            // btnUpFile
            // 
            this.btnUpFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpFile.Location = new System.Drawing.Point(621, 172);
            this.btnUpFile.Name = "btnUpFile";
            this.btnUpFile.Size = new System.Drawing.Size(75, 23);
            this.btnUpFile.TabIndex = 13;
            this.btnUpFile.Text = "上移文件";
            this.btnUpFile.UseVisualStyleBackColor = true;
            this.btnUpFile.Click += new System.EventHandler(this.btnUpFile_Click);
            // 
            // btnDelFile
            // 
            this.btnDelFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelFile.Location = new System.Drawing.Point(621, 128);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(75, 23);
            this.btnDelFile.TabIndex = 12;
            this.btnDelFile.Text = "删除文件";
            this.btnDelFile.UseVisualStyleBackColor = true;
            this.btnDelFile.Click += new System.EventHandler(this.btnDelFile_Click);
            // 
            // btnChangeFile
            // 
            this.btnChangeFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeFile.Location = new System.Drawing.Point(621, 84);
            this.btnChangeFile.Name = "btnChangeFile";
            this.btnChangeFile.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFile.TabIndex = 11;
            this.btnChangeFile.Text = "修改文件";
            this.btnChangeFile.UseVisualStyleBackColor = true;
            this.btnChangeFile.Click += new System.EventHandler(this.btnChangeFile_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFile.Location = new System.Drawing.Point(621, 40);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 10;
            this.btnAddFile.Text = "添加文件";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // dgHistory
            // 
            this.dgHistory.AllowUserToAddRows = false;
            this.dgHistory.AllowUserToDeleteRows = false;
            this.dgHistory.AllowUserToResizeRows = false;
            this.dgHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mark,
            this.name,
            this.moment,
            this.title});
            this.dgHistory.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dgHistory.Location = new System.Drawing.Point(277, 216);
            this.dgHistory.MultiSelect = false;
            this.dgHistory.Name = "dgHistory";
            this.dgHistory.RowHeadersVisible = false;
            this.dgHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistory.Size = new System.Drawing.Size(419, 150);
            this.dgHistory.TabIndex = 15;
            // 
            // mark
            // 
            this.mark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.mark.HeaderText = "选择";
            this.mark.Name = "mark";
            this.mark.ReadOnly = true;
            this.mark.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.mark.Width = 80;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.name.HeaderText = "用户名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.name.Width = 120;
            // 
            // moment
            // 
            this.moment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.moment.HeaderText = "时间";
            this.moment.Name = "moment";
            this.moment.ReadOnly = true;
            this.moment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.moment.Width = 80;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.title.HeaderText = "内容";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.title.Width = 80;
            // 
            // btnDelHistory
            // 
            this.btnDelHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelHistory.Location = new System.Drawing.Point(16, 309);
            this.btnDelHistory.Name = "btnDelHistory";
            this.btnDelHistory.Size = new System.Drawing.Size(615, 23);
            this.btnDelHistory.TabIndex = 16;
            this.btnDelHistory.Text = "删除";
            this.btnDelHistory.UseVisualStyleBackColor = true;
            this.btnDelHistory.Click += new System.EventHandler(this.btnDelHistory_Click);
            // 
            // panelUser
            // 
            this.panelUser.Location = new System.Drawing.Point(47, 92);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(200, 100);
            this.panelUser.TabIndex = 17;
            // 
            // panelSound
            // 
            this.panelSound.Location = new System.Drawing.Point(272, 57);
            this.panelSound.Name = "panelSound";
            this.panelSound.Size = new System.Drawing.Size(200, 100);
            this.panelSound.TabIndex = 18;
            // 
            // panelHistory
            // 
            this.panelHistory.Location = new System.Drawing.Point(16, 198);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Size = new System.Drawing.Size(200, 100);
            this.panelHistory.TabIndex = 19;
            // 
            // panelFlash
            // 
            this.panelFlash.Location = new System.Drawing.Point(291, 164);
            this.panelFlash.Name = "panelFlash";
            this.panelFlash.Size = new System.Drawing.Size(200, 100);
            this.panelFlash.TabIndex = 20;
            // 
            // dgFlash
            // 
            this.dgFlash.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.dgFlash.AllowUserToAddRows = false;
            this.dgFlash.AllowUserToDeleteRows = false;
            this.dgFlash.AllowUserToResizeRows = false;
            this.dgFlash.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFlash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFlash.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flashno,
            this.flashtitle,
            this.soundfile});
            this.dgFlash.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dgFlash.Location = new System.Drawing.Point(0, 299);
            this.dgFlash.MultiSelect = false;
            this.dgFlash.Name = "dgFlash";
            this.dgFlash.RowHeadersVisible = false;
            this.dgFlash.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFlash.Size = new System.Drawing.Size(616, 77);
            this.dgFlash.TabIndex = 21;
            // 
            // flashno
            // 
            this.flashno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.flashno.HeaderText = "序号";
            this.flashno.MinimumWidth = 10;
            this.flashno.Name = "flashno";
            this.flashno.ReadOnly = true;
            this.flashno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.flashno.Width = 60;
            // 
            // flashtitle
            // 
            this.flashtitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.flashtitle.HeaderText = "主题";
            this.flashtitle.Name = "flashtitle";
            this.flashtitle.ReadOnly = true;
            this.flashtitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.flashtitle.Width = 80;
            // 
            // soundfile
            // 
            this.soundfile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.soundfile.HeaderText = "音乐文件";
            this.soundfile.Name = "soundfile";
            this.soundfile.ReadOnly = true;
            this.soundfile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.soundfile.Width = 200;
            // 
            // btnDownFlash
            // 
            this.btnDownFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownFlash.Location = new System.Drawing.Point(642, 209);
            this.btnDownFlash.Name = "btnDownFlash";
            this.btnDownFlash.Size = new System.Drawing.Size(75, 23);
            this.btnDownFlash.TabIndex = 26;
            this.btnDownFlash.Text = "下移项目";
            this.btnDownFlash.UseVisualStyleBackColor = true;
            this.btnDownFlash.Click += new System.EventHandler(this.btnDownFlash_Click);
            // 
            // btnUpFlash
            // 
            this.btnUpFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpFlash.Location = new System.Drawing.Point(642, 165);
            this.btnUpFlash.Name = "btnUpFlash";
            this.btnUpFlash.Size = new System.Drawing.Size(75, 23);
            this.btnUpFlash.TabIndex = 25;
            this.btnUpFlash.Text = "上移项目";
            this.btnUpFlash.UseVisualStyleBackColor = true;
            this.btnUpFlash.Click += new System.EventHandler(this.btnUpFlash_Click);
            // 
            // btnChangeFlash
            // 
            this.btnChangeFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeFlash.Location = new System.Drawing.Point(642, 121);
            this.btnChangeFlash.Name = "btnChangeFlash";
            this.btnChangeFlash.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFlash.TabIndex = 24;
            this.btnChangeFlash.Text = "修改";
            this.btnChangeFlash.UseVisualStyleBackColor = true;
            this.btnChangeFlash.Click += new System.EventHandler(this.btnChangeFlash_Click);
            // 
            // btnDelFlash
            // 
            this.btnDelFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelFlash.Location = new System.Drawing.Point(642, 77);
            this.btnDelFlash.Name = "btnDelFlash";
            this.btnDelFlash.Size = new System.Drawing.Size(75, 23);
            this.btnDelFlash.TabIndex = 23;
            this.btnDelFlash.Text = "删除";
            this.btnDelFlash.UseVisualStyleBackColor = true;
            this.btnDelFlash.Click += new System.EventHandler(this.btnDelFlash_Click);
            // 
            // btnAddFlash
            // 
            this.btnAddFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFlash.Location = new System.Drawing.Point(642, 33);
            this.btnAddFlash.Name = "btnAddFlash";
            this.btnAddFlash.Size = new System.Drawing.Size(75, 23);
            this.btnAddFlash.TabIndex = 22;
            this.btnAddFlash.Text = "添加";
            this.btnAddFlash.UseVisualStyleBackColor = true;
            this.btnAddFlash.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 388);
            this.Controls.Add(this.dgHistory);
            this.Controls.Add(this.btnDownFlash);
            this.Controls.Add(this.btnUpFlash);
            this.Controls.Add(this.btnChangeFlash);
            this.Controls.Add(this.btnDelFlash);
            this.Controls.Add(this.btnAddFlash);
            this.Controls.Add(this.panelFlash);
            this.Controls.Add(this.panelSound);
            this.Controls.Add(this.panelHistory);
            this.Controls.Add(this.dgFlash);
            this.Controls.Add(this.panelUser);
            this.Controls.Add(this.btnDelHistory);
            this.Controls.Add(this.btnDownFile);
            this.Controls.Add(this.btnUpFile);
            this.Controls.Add(this.btnDelFile);
            this.Controls.Add(this.btnChangeFile);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.btnDownFolder);
            this.Controls.Add(this.btnUpFolder);
            this.Controls.Add(this.btnDelFolder);
            this.Controls.Add(this.btnChangeFolder);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.dgUserMan);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFlash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripDropDownButton tsUser;
        private System.Windows.Forms.ToolStripMenuItem addUser;
        private System.Windows.Forms.ToolStripMenuItem delUser;
        private System.Windows.Forms.ToolStripMenuItem modifyUser;
        private System.Windows.Forms.ToolStripMenuItem searchUser;
        private System.Windows.Forms.ToolStripButton tsChangeAdmin;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.DataGridView dgUserMan;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.Button btnDelFolder;
        private System.Windows.Forms.Button btnUpFolder;
        private System.Windows.Forms.Button btnDownFolder;
        private System.Windows.Forms.ToolStripButton tsMusicEmotion;
        private System.Windows.Forms.Button btnDownFile;
        private System.Windows.Forms.Button btnUpFile;
        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.Button btnChangeFile;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.ToolStripButton tsMusicFree;
        private System.Windows.Forms.ToolStripButton tsMusicSelf;
        private System.Windows.Forms.ToolStripButton tsHistory;
        private System.Windows.Forms.DataGridView dgHistory;
        private System.Windows.Forms.Button btnDelHistory;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Panel panelSound;
        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.ToolStripButton tsImagination;
        private System.Windows.Forms.ToolStripButton tsRelaxmethod;
        private System.Windows.Forms.Panel panelFlash;
        private System.Windows.Forms.DataGridView dgFlash;
        private System.Windows.Forms.Button btnDownFlash;
        private System.Windows.Forms.Button btnUpFlash;
        private System.Windows.Forms.Button btnChangeFlash;
        private System.Windows.Forms.Button btnDelFlash;
        private System.Windows.Forms.Button btnAddFlash;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn job;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mark;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn moment;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn flashno;
        private System.Windows.Forms.DataGridViewTextBoxColumn flashtitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn soundfile;
    }
}