using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace simri
{
    public partial class frmSettings : Form
    {
        private float soundTreeWidth = 0.6f;
        private int gaps = 5;
        private int soundType = 0; // 음악버튼을 눌렀을 때(1: 정서음악, 2: 개성음악, 3: 자유음악)
        private ImageList imgList = null;
        private int flashType = 0; // 플래쉬편집버튼을 눌렀을 때(1: 상상완화, 2:완화방법)
        public frmSettings()
        {
            InitializeComponent();
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            // 패널설정
            panelUser.Location = new Point(0, toolStrip.Height);
            panelUser.Size = new Size(this.ClientSize.Width, btnClose.Top - toolStrip.Height - gaps);
            panelSound.Location = panelUser.Location;
            panelSound.Size = panelUser.Size;
            panelHistory.Location = panelUser.Location;
            panelHistory.Size = panelUser.Size;
            panelFlash.Location = panelUser.Location;
            panelFlash.Size = panelUser.Size;
            panelFlash.Location = panelUser.Location;
            panelFlash.Size = panelUser.Size;
            // 리용자관리패널
            panelUser.Controls.Add(dgUserMan);
            dgUserMan.Location = new Point(0, 0);
            dgUserMan.Width = panelUser.Width;
            dgUserMan.Height = panelUser.Height;
            // 사운드관리패널
            panelSound.Controls.Add(treeView);
            panelSound.Controls.Add(btnAddFolder);
            panelSound.Controls.Add(btnAddFile);
            panelSound.Controls.Add(btnChangeFolder);
            panelSound.Controls.Add(btnChangeFile);
            panelSound.Controls.Add(btnDelFolder);
            panelSound.Controls.Add(btnDelFile);
            panelSound.Controls.Add(btnUpFolder);
            panelSound.Controls.Add(btnUpFile);
            panelSound.Controls.Add(btnDownFolder);
            panelSound.Controls.Add(btnDownFile);
            imgList = new ImageList();
            imgList.Images.Add(Bitmap.FromFile(_path.icons + "folder.png"));
            imgList.Images.Add(Bitmap.FromFile(_path.icons + "music.png"));
            treeView.ImageList = imgList;
            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            treeView.Location = new Point(0, 0);
            treeView.Size = new Size((int)(panelSound.Width * soundTreeWidth), panelSound.Height);
            // 리력관리패널
            panelHistory.Controls.Add(dgHistory);
            panelHistory.Controls.Add(btnDelHistory);
            dgHistory.Location = new Point(0, 0);
            dgHistory.Size = new Size(panelHistory.Width, panelHistory.Height - btnDelHistory.Height - gaps);
            dgHistory.CellClick += new DataGridViewCellEventHandler(dgHistory_CellClick);
            btnDelHistory.Location = new Point(0, dgHistory.Bottom + gaps);
            btnDelHistory.Width = dgHistory.Width;
            // 상상완화, 완화방법
            panelFlash.Controls.Add(dgFlash);
            panelFlash.Controls.Add(btnAddFlash);
            panelFlash.Controls.Add(btnDelFlash);
            panelFlash.Controls.Add(btnChangeFlash);
            panelFlash.Controls.Add(btnUpFlash);
            panelFlash.Controls.Add(btnDownFlash);
            dgFlash.Location = new Point(0, 0);
            dgFlash.Height = panelFlash.Height;
            // 초기화
            initGridUser();
            togglePanel(panelUser);
        }
        private void togglePanel(Panel curPanel)
        {
            panelUser.Visible = false;
            panelSound.Visible = false;
            panelHistory.Visible = false;
            panelFlash.Visible = false;
            curPanel.Visible = true;
        }
        private void initGridUser()
        {
            dgUserMan.Rows.Clear();
            List<_row> userList = new List<_row>();
            int count = _dbManager.getUserList(ref userList);
            for (int i = 0; i < count; i++)
            {
                dgUserMan.Rows.Add();
                dgUserMan[0, i].Value = (i + 1).ToString();
                dgUserMan[1, i].Value = userList[i].username;
                dgUserMan[2, i].Value = userList[i].password;
                dgUserMan[3, i].Value = userList[i].gender;
                dgUserMan[4, i].Value = userList[i].birthYear.ToString() + "年" + userList[i].birthMonth.ToString() + "月" + userList[i].birthDay.ToString() + "日";
                dgUserMan[5, i].Value = userList[i].job;
            }
        }
        private void initGridLog()
        {
            dgHistory.Rows.Clear();
            List<_user_log> logList = new List<_user_log>();
            int count = _dbManager.getUserLogList(ref logList);
            for (int i = 0; i < count; i++)
            {
                dgHistory.Rows.Add();
                dgHistory[0, i].Value = false;
                dgHistory[1, i].Value = logList[i].username;
                dgHistory[2, i].Value = logList[i].moment;
                string[] arryInfo;
                switch (logList[i].type)
                {
                    case 1:
                        dgHistory[3, i].Value = "压力测评";
                        break;
                    case 2:
                    case 3:
                        arryInfo = logList[i].report.Split('|');
                        dgHistory[3, i].Value = arryInfo[0];
                        break;
                    case 0:
                    default:
                        break;
                }
            }
        }
        private void initGridFlash()
        {
            dgFlash.Rows.Clear();
            List<_flash> flashList = new List<_flash>();
            int countFlash = _dbManager.getFlashList(flashType, ref flashList);
            for (int i = 0; i < countFlash; i++ )
            {
                dgFlash.Rows.Add();
                dgFlash[0, i].Value = (i + 1).ToString(); // 번호(1-base)
                dgFlash[1, i].Value = flashList[i].title; // 제목
                dgFlash[2, i].Value = flashList[i].music; // 음악화일
            }
        }
        private void initTreeview()
        {
            treeView.Nodes.Clear();
            List<_folder> folderList = new List<_folder>();
            int countFolder = _dbManager.getFolderList(soundType, ref folderList);
            for (int i = 0; i < countFolder; i++ )
            {
                TreeNode node = treeView.Nodes.Add("folder" + i.ToString(), folderList[i].name, 0, 0);
                List<_file> fileList = new List<_file>();
                int countFile = _dbManager.getFolderLeafs(soundType, folderList[i].no, ref fileList);
                if (countFile == 0) continue;
                for (int j = 0; j < countFile; j++)
                {
                    node.Nodes.Add("file" + j.ToString(), fileList[j].name, 1, 1);
                }
            }
            if (treeView.Visible)
            {
                treeView.Focus();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void treeView_AfterSelect(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode.Level == 0)
            {
                toggleFolderBtnGroup(true);
                toggleFileBtnGroup(false);
                btnAddFile.Enabled = true;
            }
            else
            {
                toggleFolderBtnGroup(false);
                toggleFileBtnGroup(true);
            }
        }
        private void addUser_Click(object sender, EventArgs e)
        {
            _record.empty();

            frmAddModifyUser frmAddModify = new frmAddModifyUser();
            if (frmAddModify.ShowDialog() == DialogResult.OK)
            {
                string strError = getErrorRecord();
                if (strError.Length == 0)
                {
                    if (_dbManager.isExist(_record.username))
                    {
                        MessageBox.Show("用户名存在，请重新输入用户名");
                        return;
                    }
                    if (!_dbManager.registUser())
                    {
                        MessageBox.Show("数据库操作时发生了错误。");
                        return;
                    }
                    initGridUser();
                }
                else
                {
                    MessageBox.Show(strError);
                }
            }
        }
        private string getErrorRecord()
        {
            if (!_global.tmp.Equals(_record.password) || _record.password.Length == 0)
            {　
                return "请重新输入密码。";
            }
            if (_record.username.Length == 0)
            {
                return "请输入用户名。";
            }
            if (_record.username.Length == 0)
            {
                return "请输入用户名。";
            }
            if (_record.job.Length == 0)
            {
                return "请输入职业。";
            }
            return "";
        }
        private void delUser_Click(object sender, EventArgs e)
        {
            // 리용자이름 얻기
            int iRow = dgUserMan.SelectedRows[0].Index;
            string strUsername = dgUserMan[1, iRow].Value.ToString();
            if (MessageBox.Show("您确定要删除选定的用户(" + strUsername + ")吗？", "注意", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (!_dbManager.deleteUser(strUsername))
                {
                    MessageBox.Show("数据库操作时发生了错误。");
                    return;
                }
                initGridUser();
            }
        }
        private void modifyUser_Click(object sender, EventArgs e)
        {
            // 리용자이름 얻기
            int iRow = dgUserMan.SelectedRows[0].Index;
            _record.username = dgUserMan[1, iRow].Value.ToString();
            _record.password = dgUserMan[2, iRow].Value.ToString();
            _record.gender = dgUserMan[3, iRow].Value.ToString();
            DateTime date = DateTime.Parse(dgUserMan[4, iRow].Value.ToString());
            _record.birthYear = date.Year;
            _record.birthMonth = date.Month;
            _record.birthDay = date.Day;
            _record.job = dgUserMan[5, iRow].Value.ToString();
            _row row = new _row();
            _record.clone(ref row);
            frmAddModifyUser frmAddModify = new frmAddModifyUser();
            if (frmAddModify.ShowDialog() == DialogResult.OK)
            {
                string strError = getErrorRecord();
                if (strError.Length == 0)
                {
                    int[] iFields = new int[7];
                    bool bEqual = _record.isEqual(ref row, ref iFields);
                    if (bEqual)
                    {
                        MessageBox.Show("没有可修改的内容。");
                        return;
                    }
                    if (iFields[0] == 1)
                    {
                        if (_dbManager.isExist(_record.username))
                        {
                            MessageBox.Show("您要修改的用户已存在。");
                            return;
                        }
                    }
                    // 변경시작
                    // 본래값
                    string strValuesOrigin = "";
                    if (iFields[0] == 1) strValuesOrigin += "'" + row.username + "',";
                    if (iFields[1] == 1) strValuesOrigin += "'" + row.password + "',";
                    if (iFields[2] == 1) strValuesOrigin += "'" + row.gender + "',";
                    if (iFields[3] == 1) strValuesOrigin += row.birthYear + ",";
                    if (iFields[4] == 1) strValuesOrigin += Convert.ToByte(row.birthMonth) + ",";
                    if (iFields[5] == 1) strValuesOrigin += Convert.ToByte(row.birthDay) + ",";
                    if (iFields[6] == 1) strValuesOrigin += "'" + row.job + "'";
                    if (strValuesOrigin.EndsWith(","))
                    {
                        strValuesOrigin = strValuesOrigin.Substring(0, strValuesOrigin.Length - 1);
                    }
                    // 변경할 값목록
                    string strValues = "";
                    if (iFields[0] == 1) strValues += "'" + _record.username + "',";
                    if (iFields[1] == 1) strValues += "'" + _record.password + "',";
                    if (iFields[2] == 1) strValues += "'" + _record.gender + "',";
                    if (iFields[3] == 1) strValues += _record.birthYear + ",";
                    if (iFields[4] == 1) strValues += Convert.ToByte(_record.birthMonth) + ",";
                    if (iFields[5] == 1) strValues += Convert.ToByte(_record.birthDay) + ",";
                    if (iFields[6] == 1) strValues += "'" + _record.job + "'";
                    if (strValues.EndsWith(","))
                    {
                        strValues = strValues.Substring(0, strValues.Length - 1);
                    }
                    if (!_dbManager.updateRecord(ref iFields, strValues, strValuesOrigin, row.username))
                    {
                        MessageBox.Show("修改数据库时发生了错误。");
                        return;
                    }
                    initGridUser();
                }
                else
                {
                    MessageBox.Show(strError);
                }
            }
            
        }
        private void searchUser_Click(object sender, EventArgs e)
        {
            frmSearch search = new frmSearch();
            if (search.ShowDialog() == DialogResult.OK)
            {
                string strPattern = _global.tmp;
                if (strPattern.Length == 0)
                {
                    MessageBox.Show("你没选任何查询条件将得到所有用户信息。");
                    return;
                }
                string[] array = strPattern.Split('|');
                string strString = array[0];
                string strNumber = array[1];
                List<_row> userList = new List<_row>();
                int count = _dbManager.searchUserList(strString, strNumber, ref userList);
                dgUserMan.Rows.Clear();
                for (int i = 0; i < count; i++)
                {
                    dgUserMan.Rows.Add();
                    dgUserMan[0, i].Value = (i + 1).ToString();
                    dgUserMan[1, i].Value = userList[i].username;
                    dgUserMan[2, i].Value = userList[i].password;
                    dgUserMan[3, i].Value = userList[i].gender;
                    dgUserMan[4, i].Value = userList[i].birthYear.ToString() + "年" + userList[i].birthMonth.ToString() + "月" + userList[i].birthDay.ToString() + "日";
                    dgUserMan[5, i].Value = userList[i].job;
                }
            }
        }
        private void user_Click(object sender, EventArgs e)
        {
            togglePanel(panelUser);
            makeToolBtnNormal();
        }
        private void musicEmotion_Click(object sender, EventArgs e)
        {
            if (soundType != 1 || !panelSound.Visible)
            {
                soundType = 1;
                togglePanel(panelSound);
                toggleToolButton(tsMusicEmotion);
                initTreeview();
            }
        }
        private void musicFree_Click(object sender, EventArgs e)
        {
            if (soundType != 2 || !panelSound.Visible)
            {
                soundType = 2;
                togglePanel(panelSound);
                toggleToolButton(tsMusicFree);
                initTreeview();
            }
        }
        private void musicSelf_Click(object sender, EventArgs e)
        {
            if (soundType != 3 || !panelSound.Visible)
            {
                soundType = 3;
                togglePanel(panelSound);
                toggleToolButton(tsMusicSelf);
                initTreeview();
            }
        }
        private void toggleFolderBtnGroup(bool bEnabled)
        {
            btnAddFolder.Enabled = bEnabled;
            btnChangeFolder.Enabled = bEnabled;
            btnDelFolder.Enabled = bEnabled;
            btnUpFolder.Enabled = bEnabled;
            btnDownFolder.Enabled = bEnabled;
        }
        private void toggleFileBtnGroup(bool bEnabled)
        {
            btnAddFile.Enabled = bEnabled;
            btnChangeFile.Enabled = bEnabled;
            btnDelFile.Enabled = bEnabled;
            btnUpFile.Enabled = bEnabled;
            btnDownFile.Enabled = bEnabled;
        }
        private void toggleToolButton(ToolStripButton curButton)
        {
            tsMusicEmotion.Checked = false;
            tsMusicFree.Checked = false;
            tsMusicSelf.Checked = false;
            curButton.Checked = true;
        }
        private void makeToolBtnNormal()
        {
            tsMusicEmotion.Checked = false;
            tsMusicFree.Checked = false;
            tsMusicSelf.Checked = false;
        }
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件夹/文件。");
                return;
            }
            _recordFile.empty();
            frmAddModifyFile input = new frmAddModifyFile();
            if (input.ShowDialog() == DialogResult.OK)
            {
                string strFile = _recordFile.name;
                string strTitle = _recordFile.title;
                if (strFile.Length == 0)
                {
                    MessageBox.Show("请选择文件。");
                    treeView.Focus();
                    return;
                }
                if (strTitle.Length == 0)
                {
                    MessageBox.Show("请输入文件标题。");
                    treeView.Focus();
                    return;
                }
                int folderno = (curNode.Level == 0) ? curNode.Index + 1 : curNode.Parent.Index + 1; // 0-base를 1-base로 변환
                if (_dbManager.isExistFile(soundType, folderno, strFile))
                {
                    MessageBox.Show("文件名义存在。");
                    treeView.Focus();
                    return;
                }
                if (_dbManager.isExistTitle(soundType, folderno, strTitle))
                {
                    MessageBox.Show("文件标题已存在。");
                    treeView.Focus();
                    return;
                }
                TreeNode parentNode = (curNode.Level == 0) ? curNode : curNode.Parent;
                int count = parentNode.Nodes.Count;
                TreeNode newNode = new TreeNode(strFile);
                newNode.SelectedImageIndex = 1;
                newNode.ImageIndex = 1;
                if (curNode.Level == 0) // 홀더를 선택하여 화일추가를 할 때
                {
                    if (!_dbManager.insertFile(count, soundType, folderno, count + 1, strFile, strTitle))
                    {
                        MessageBox.Show("添加文件夹时发生了错误。");
                        treeView.Focus();
                        return;
                    }
                    parentNode.Nodes.Add(newNode);
                }
                else // 홀더안에 화일을 선택하여 화일을 추가할 때
                {
                    int curIndex = curNode.Index + 1; // 0-base를 1-base로 변환
                    if (!_dbManager.insertFile(count, soundType, folderno, curIndex + 1, strFile, strTitle)) // 선택한 항목 다음에 추가
                    {
                        MessageBox.Show("添加文件夹时发生乐错误。");
                        treeView.Focus();
                        return;
                    }
                    if (curIndex == count)
                    {
                        parentNode.Nodes.Add(newNode);
                    }
                    else
                    {
                        parentNode.Nodes.Insert(curIndex, newNode);
                    }
                }
                treeView.SelectedNode = newNode;
                File.Copy(_global.tmp, _path.music + strFile, true);
            }
            treeView.Focus();
        }
        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            _recordfolder.empty();
            frmAddModifyFolder input = new frmAddModifyFolder();
            if (input.ShowDialog() == DialogResult.OK)
            {
                string strFolder = _recordfolder.name;
                string strComment = _recordfolder.comment;
                if (strFolder.Length == 0)
                {
                    MessageBox.Show("请输入文件夹名。");
                    treeView.Focus();
                    return;
                }
                if (strComment.Length == 0)
                {
                    MessageBox.Show("请输入文字。");
                    treeView.Focus();
                    return;
                }
                if (_dbManager.isExistFolder(soundType, strFolder))
                {
                    MessageBox.Show("文件夹名义存在。");
                    treeView.Focus();
                    return;
                }
                if (_dbManager.isExistComment(soundType, strComment))
                {
                    MessageBox.Show("文件夹的文字已存在。");
                    treeView.Focus();
                    return;
                }
                int count = treeView.Nodes.Count;
                TreeNode selNode = treeView.SelectedNode;
                TreeNode newNode = new TreeNode(strFolder);
                newNode.ImageIndex = 0;
                if (selNode == null) // 현재 선택된 홀더가 없을 때
                {
                    if (!_dbManager.insertFolder(soundType, count, count + 1, strFolder, strComment))
                    {
                        MessageBox.Show("添加文件夹时发生了错误。");
                        treeView.Focus();
                        return;
                    }
                    treeView.Nodes.Add(newNode);
                }
                else
                {
                    int selIndex = selNode.Index + 1; // 0-base를 1-base로 변환
                    if (!_dbManager.insertFolder(soundType, count, selIndex + 1, strFolder, strComment)) // 선택한 항목 다음에 추가
                    {
                        MessageBox.Show("添加文件夹时发生了错误。");
                        treeView.Focus();
                        return;
                    }
                    if (selIndex == count)
                    {
                        treeView.Nodes.Add(newNode);
                    }
                    else
                    {
                        treeView.Nodes.Insert(selIndex, newNode);
                    }

                }
                treeView.Focus();
                treeView.SelectedNode = newNode;
            }
        }
        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView.SelectedNode;
            if (selNode == null)
            {
                MessageBox.Show("请选择文件夹。");
                treeView.Focus();
                return;
            }
            string strFolderName = selNode.Text;
            _dbManager.getFolderInfo(soundType, strFolderName);
            _folder folderOrigin = new _folder();
            _recordfolder.clone(ref folderOrigin);
            frmAddModifyFolder change = new frmAddModifyFolder();
            if (change.ShowDialog() == DialogResult.OK)
            {
                if (folderOrigin.name.Equals(_recordfolder.name) && folderOrigin.comment.Equals(_recordfolder.comment))
                {
                    MessageBox.Show("没有可修改的内容。");
                    treeView.Focus();
                    return;
                }
                if (_recordfolder.name.Length == 0)
                {
                    MessageBox.Show("请输入文件夹名。");
                    treeView.Focus();
                    return;
                }
                if (_recordfolder.comment.Length == 0)
                {
                    MessageBox.Show("请输入文字。");
                    treeView.Focus();
                    return;
                }
                if (!folderOrigin.name.Equals(_recordfolder.name) && _dbManager.isExistFolder(soundType, _recordfolder.name)) // 이름을 변경시키려는 경우
                {
                    MessageBox.Show("您要修改的文件夹已存在。");
                    treeView.Focus();
                    return;
                }
                if (!folderOrigin.comment.Equals(_recordfolder.comment) && _dbManager.isExistComment(soundType, _recordfolder.comment)) // 콤멘트를 변경시키려는 경우
                {
                    MessageBox.Show("您要修改的文字已存在。");
                    treeView.Focus();
                    return;
                }
                if (!_dbManager.updateFolderRecord(soundType, ref folderOrigin))
                {
                    MessageBox.Show("数据库操作时发生了错误。");
                    treeView.Focus();
                    return;
                }
                selNode.Text = _recordfolder.name;
            }
            treeView.Focus();
        }
        private void btnDelFolder_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView.SelectedNode;
            if (selNode == null)
            {
                MessageBox.Show("请选择文件夹。");
                treeView.Focus();
                return;
            }
            int selIndex = selNode.Index + 1; // 0-base를 1-base로 변환
            if (!_dbManager.deleteFolder(soundType, selNode.Text, treeView.Nodes.Count, selIndex))
            {
                MessageBox.Show("删除文件夹时发生了错误。");
                treeView.Focus();
                return;
            }
            treeView.Nodes.Remove(selNode);
            treeView.Focus();
        }
        private void btnUpFolder_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件夹。");
                treeView.Focus();
                return;
            }
            TreeNode prevNode = curNode.PrevNode;
            if (prevNode == null) // 올라갈 자리가 없다
            {
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            int curIndex = curNode.Index + 1; // 0-base를 1-base로 변환
            int prevIndex = prevNode.Index + 1; // 0-base를 1-base로 변환
            if (!_dbManager.replaceFolderNo(soundType, curIndex, curNode.Text, prevIndex, prevNode.Text, treeView.Nodes.Count))
            {
                MessageBox.Show("移动时发生了错误。");
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            int n = prevNode.Index + 1; // 0-base를 1-base로 변환
            TreeNode tmp = (TreeNode)prevNode.Clone();
            treeView.Nodes.Remove(prevNode);
            if (treeView.Nodes.Count == n)
            {
                treeView.Nodes.Add(tmp);
            }
            else
            {
                treeView.Nodes.Insert(n, tmp);
            }
            treeView.Focus();
            treeView.SelectedNode = curNode;
        }
        private void btnDownFolder_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件夹。");
                treeView.Focus();
                return;
            }
            TreeNode nextNode = curNode.NextNode;
            if (nextNode == null) // 내려갈 자리가 없다
            {
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            int curIndex = curNode.Index + 1; // 0-base를 1-base로 변환
            int nextIndex = nextNode.Index + 1; // 0-base를 1-base로 변환
            if (!_dbManager.replaceFolderNo(soundType, curIndex, curNode.Text, nextIndex, nextNode.Text, treeView.Nodes.Count))
            {
                MessageBox.Show("移动时发生了错误。");
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            int n = nextNode.Index;
            TreeNode tmp = (TreeNode)nextNode.Clone();
            treeView.Nodes.Remove(nextNode);
            treeView.Nodes.Insert(n - 1, tmp);
            treeView.Focus();
            treeView.SelectedNode = curNode;
        }
        private void btnChangeFile_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件。");
                treeView.Focus();
                return;
            }
            TreeNode parentNode = curNode.Parent;
            int folderno = parentNode.Index + 1; // 0-base를 1-base로 변환
            _dbManager.getFileInfo(soundType, folderno, curNode.Text);
            _file fileOrigin = new _file();
            _recordFile.clone(ref fileOrigin);
            frmAddModifyFile change = new frmAddModifyFile();
            if (change.ShowDialog() == DialogResult.OK )
            {
                if (fileOrigin.name.Equals(_recordFile.name) && fileOrigin.title.Equals(_recordFile.title))
                {
                    MessageBox.Show("没有可修改的内容。");
                    treeView.Focus();
                    return;
                }
                if (_recordFile.name.Length == 0)
                {
                    MessageBox.Show("请选择文件。");
                    treeView.Focus();
                    return;
                }
                if (_recordFile.title.Length == 0)
                {
                    MessageBox.Show("请输入标题。");
                    treeView.Focus();
                    return;
                }
                if (!fileOrigin.name.Equals(_recordFile.name) && _dbManager.isExistFile(soundType, folderno, _recordFile.name)) // 파일이름을 변경시키려는 경우
                {
                    MessageBox.Show("您要修改的文件名已存在。");
                    treeView.Focus();
                    return;
                }
                if (!fileOrigin.title.Equals(_recordFile.title) && _dbManager.isExistTitle(soundType, folderno, _recordFile.title)) // 파일제목을 변경시키려는 경우
                {
                    MessageBox.Show("您要修改的文件标题已存在。");
                    treeView.Focus();
                    return;
                }
                if (!_dbManager.updateFileRecord(soundType, folderno, ref fileOrigin))
                {
                    MessageBox.Show("数据库操作时发生了错误");
                    treeView.Focus();
                    return;
                }
                curNode.Text = _recordFile.name;
                // 파일이름이 변경된 경우
                if (!fileOrigin.name.Equals(_recordFile.name))
                {
                    // 일단 파일을 변경된 이름으로 복사한다.
                    File.Copy(_path.music + fileOrigin.name, _path.music + _recordFile.name, true);
                    if (!_dbManager.isExistFile(fileOrigin.name))// 변경되기전 화일이 다른 홀더에서 참조하지 않으면 지운다.
                    {
                        File.Delete(_path.music + fileOrigin.name);
                    }
                }
            }

            treeView.Focus();
        }
        private void btnDelFile_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件。");
                treeView.Focus();
                return;
            }
            TreeNode parentNode = curNode.Parent;
            int folderno = parentNode.Index + 1; // 0-base를 1-base로 변환
            if (!_dbManager.deleteFile(soundType, folderno, curNode.Text, parentNode.Nodes.Count, curNode.Index + 1))
            {
                MessageBox.Show("删除文件时发生了错误。");
                treeView.Focus();
                return;
            }
            treeView.Nodes.Remove(curNode);
            treeView.Focus();
            if (!_dbManager.isExistFile(curNode.Text)) // 파일이 더는 존재하지 않으므로 화일을 지운다.
            {
                File.Delete(_path.music + curNode.Text);
            }
        }
        private void btnUpFile_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件。");
                return;
            }
            TreeNode prevNode = curNode.PrevNode;
            if (prevNode == null) // 올라갈 자리가 없다
            {
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            int curIndex = curNode.Index + 1; // 0-base를 1-base로 변환
            int prevIndex = prevNode.Index + 1; // 0-base를 1-base로 변환
            int folderno = curNode.Parent.Index + 1; // 0-base를 1-base로 변환
            TreeNode parentNode = curNode.Parent;
            if (!_dbManager.replaceFileNo(soundType, folderno, curIndex, curNode.Text, prevIndex, prevNode.Text, parentNode.Nodes.Count))
            {
                MessageBox.Show("移动时发生了错误。");
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            string tmp = prevNode.Text;
            prevNode.Text = curNode.Text;
            curNode.Text = tmp;
            treeView.Focus();
            treeView.SelectedNode = prevNode;
        }
        private void btnDownFile_Click(object sender, EventArgs e)
        {
            TreeNode curNode = treeView.SelectedNode;
            if (curNode == null)
            {
                MessageBox.Show("请选择文件。");
                return;
            }
            TreeNode nextNode = curNode.NextNode;
            if (nextNode == null) // 내려갈 자리가 없다
            {
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            int curIndex = curNode.Index + 1; // 0-base를 1-base로 변환
            int nextIndex = nextNode.Index + 1; // 0-base를 1-base로 변환
            int folderno = curNode.Parent.Index + 1; // 0-base를 1-base로 변환
            TreeNode parentNode = curNode.Parent;
            if (!_dbManager.replaceFileNo(soundType, folderno, curIndex, curNode.Text, nextIndex, nextNode.Text, parentNode.Nodes.Count))
            {
                MessageBox.Show("移动时发生了错误。");
                treeView.Focus();
                treeView.SelectedNode = curNode;
                return;
            }
            string tmp = nextNode.Text;
            nextNode.Text = curNode.Text;
            curNode.Text = tmp;
            treeView.Focus();
            treeView.SelectedNode = nextNode;
        }
        private void changeAdmin_Click(object sender, EventArgs e)
        {
            frmChangeAdmin admin = new frmChangeAdmin();
            if (admin.ShowDialog() == DialogResult.OK)
            {
                string[] strAdmin = _global.tmp.Split('|');
                string curPassword = strAdmin[0];
                if (!_dbManager.isCorrectAdmin(curPassword))
                {
                    MessageBox.Show("您输入的信息错误。");
                    return;
                }
                string newPassword = strAdmin[1];
                string confirmPassword = strAdmin[2];
                if (newPassword.Length == 0 || confirmPassword.Length == 0 || !confirmPassword.Equals(newPassword))
                {
                    MessageBox.Show("您输入的信息错误。");
                    return;
                }
                if (!_dbManager.updateAdmin(newPassword))
                {
                    MessageBox.Show("修改管理员密码时发生了错误。");
                    return;
                }
            }
        }
        private void tsHistory_Click(object sender, EventArgs e)
        {
            togglePanel(panelHistory);
            makeToolBtnNormal();
            initGridLog();
        }
        private void dgHistory_CellClick(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgHistory.SelectedRows;
            if (rows.Count == 0) return;
            foreach (DataGridViewRow row in rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["mark"].Value);
                row.Cells["mark"].Value = !isChecked;
            }
        }
        private void btnDelHistory_Click(object sender, EventArgs e)
        {
            DataGridViewRowCollection rows = dgHistory.Rows;
            if (rows.Count == 0) return;
            int nDeletedRow = 0;
            foreach (DataGridViewRow row in rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["mark"].Value);
                if (isChecked) 
                {
                    string strUsername = Convert.ToString(row.Cells["name"].Value).Trim();
                    string strMoment = Convert.ToString(row.Cells["moment"].Value).Trim();
                    if (!_dbManager.deleteUserLog(strUsername, strMoment))
                    {
                        MessageBox.Show("数据库删除时发生了错误");
                        return;
                    }
                    nDeletedRow++;
                }
            }
            if (nDeletedRow != 0) initGridLog();
        }
        private void tsImagination_Click(object sender, EventArgs e)
        {
            if (flashType != 1 || !panelFlash.Visible)
            {
                flashType = 1;
                togglePanel(panelFlash);
                makeToolBtnNormal();
                initGridFlash();
            }
        }
        private void tsRelaxmethod_Click(object sender, EventArgs e)
        {
            if (flashType != 2 || !panelFlash.Visible)
            {
                flashType = 2;
                togglePanel(panelFlash);
                makeToolBtnNormal();
                initGridFlash();
            }
        }
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            frmAddModifyFlash frmFlash = new frmAddModifyFlash();
            string strTitle = "";
            switch (flashType)
            {
                case 1: // 상상완화
                    strTitle = "想象放松(添加项目)";
                    break;
                case 2: // 완화방법
                    strTitle = "放松方法(添加项目)";
                    break;
                default:
                    break;
            }
            frmFlash.Text = strTitle;
            _global.current = flashType; // 1: 상상완화, 2: 완화방법
            _global.moment = (dgFlash.SelectedRows.Count == 0) ? "" : dgFlash.SelectedRows[0].Cells["flashno"].Value.ToString(); // 플래쉬번호(1-base)
            _global.tmp = "add"; // 추가냐 변화냐
            _global.count = dgFlash.Rows.Count;
            int indexSel = (_global.moment.Equals("")) ? _global.count : Int32.Parse(_global.moment);
            if (frmFlash.ShowDialog(this) == DialogResult.OK)
            {
                initGridFlash();
                dgFlash.Rows[indexSel].Selected = false;
            }
        }
        private void btnChangeFlash_Click(object sender, EventArgs e)
        {
            if (dgFlash.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择项目！");
                return;
            }
            frmAddModifyFlash frmFlash = new frmAddModifyFlash();
            string strTitle = "";
            switch (flashType)
            {
                case 1: // 상상완화
                    strTitle = "想象放松(修改项目)";
                    break;
                case 2: // 완화방법
                    strTitle = "放松方法(修改项目)";
                    break;
                default:
                    break;
            }
            frmFlash.Text = strTitle;
            _global.current = flashType; // 1: 상상완화, 2: 완화방법
            _global.moment = dgFlash.SelectedRows[0].Cells["flashno"].Value.ToString(); // 플래쉬번호(1-base)
            _global.tmp = "change"; // 추가냐 변화냐
            _global.count = dgFlash.Rows.Count;
            if (frmFlash.ShowDialog(this) == DialogResult.OK)
            {
                initGridFlash();
            }
        }
        private void btnDelFlash_Click(object sender, EventArgs e)
        {
            int nSelRows = dgFlash.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("清选择行！");
                return;
            }
            // 지우기
            int indexSel = dgFlash.SelectedRows[0].Index;
            dgFlash.Rows.RemoveAt(indexSel);
            // 지운 행 다음부터 행번호 고치기
            int nRows = dgFlash.Rows.Count;
            for (int i = indexSel; i < nRows; i++)
            {
                dgFlash[0, i].Value = (i + 1).ToString();
            }
            _dbManager.deleteFlash(nRows + 1, flashType, indexSel + 1); // 지워지기전에는 총 레코드수가 nRows + 1개
        }
        private void btnUpFlash_Click(object sender, EventArgs e)
        {
            int nSelRows = dgFlash.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("清选择行！");
                return;
            }
            int indexSel = dgFlash.SelectedRows[0].Index;
            if (indexSel == 0) return; // 첫번째 행을 선택하였으므로 탈퇴
            int nRows = dgFlash.Rows.Count;
            // 번호 바꾸기
            dgFlash[0, indexSel].Value = indexSel.ToString();
            dgFlash[0, indexSel - 1].Value = (indexSel + 1).ToString();
            DataGridViewRow row = dgFlash.Rows[indexSel - 1]; // 앞의 행을 보관
            dgFlash.Rows.RemoveAt(indexSel - 1);
            if (indexSel == nRows - 1) // 마지막행을 올렸으므로 추가
            {
                dgFlash.Rows.Add(row);
            }
            else // 삽입
            {
                dgFlash.Rows.Insert(indexSel, row);
            }
            _dbManager.replaceFlashNo(flashType, indexSel + 1, indexSel, nRows);
        }
        private void btnDownFlash_Click(object sender, EventArgs e)
        {
            int nSelRows = dgFlash.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("清选择行！");
                return;
            }
            int indexSel = dgFlash.SelectedRows[0].Index;
            int nRows = dgFlash.Rows.Count;
            if (indexSel == nRows - 1) return; // 마지막 행을 선택하였으므로 탈퇴
            // 번호 바꾸기
            dgFlash[0, indexSel].Value = (indexSel + 2).ToString();
            dgFlash[0, indexSel + 1].Value = (indexSel + 1).ToString();
            DataGridViewRow row = dgFlash.Rows[indexSel]; // 선택한 행 보관
            dgFlash.Rows.RemoveAt(indexSel);
            if (indexSel == nRows - 2) // 마지막에서 두번째 행을 내리웠으므로 추가
            {
                dgFlash.Rows.Add(row);
            }
            else
            {
                dgFlash.Rows.Insert(indexSel + 1, row);
            }
            dgFlash.Rows[indexSel + 1].Selected = true;
            _dbManager.replaceFlashNo(flashType, indexSel + 1, indexSel + 2, nRows);
        }
    }
}