using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simri
{
    public partial class frmAddModifyFlash : Form
    {
        private int nCountFlash = 0;
        private int flashType = 0;
        private int flashNo = 0;
        private string strMethod = "";
        public frmAddModifyFlash()
        {
            InitializeComponent();
        }
        private void frmAddModifyFlash_Load(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            flashType = _global.current;
            flashNo = (_global.moment.Equals("")) ? 0 : Int32.Parse(_global.moment);
            strMethod = _global.tmp;
            nCountFlash = _global.count;
            dgImage.Rows.Clear();
            if (_global.tmp.Equals("change")) // 변경이면 폼의 빈내용을 채운다
            {
                _dbManager.getFlashInfo(flashType, flashNo);
                txtTitle.Text = _recordFlash.title;
                txtSoundfile.Text = _recordFlash.music;
                // 그리드를 채운다
                List<_image> imageList = new List<_image>();
                int countImage = _dbManager.getImageList(flashType, flashNo, ref imageList);
                for (int i = 0; i < countImage; i++)
                {
                    dgImage.Rows.Add();
                    dgImage[0, i].Value = (i + 1).ToString(); // 번호
                    dgImage[1, i].Value = imageList[i].filename; // 파일명
                    dgImage[2, i].Value = imageList[i].life.ToString(); // 존속시간
                }
            }
        }
        private void AddImageToGrid(string filename, string life)
        {
            int nRows = dgImage.Rows.Count;
            dgImage.Rows.Add();
            dgImage[0, nRows].Value = (nRows + 1).ToString(); // 번호
            dgImage[1, nRows].Value = filename; // 파일명
            dgImage[2, nRows].Value = life; // 존속시간
            dgImage.Rows[nRows].Selected = true;
        }
        private void btnOpensound_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "音乐(*.mp3)|*.mp3";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSoundfile.Text = openFileDialog.FileName;
            }
        }
        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            int nSelRows = dgImage.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("请选择行！");
                return;
            }
            int indexSel = dgImage.SelectedRows[0].Index;
            string strFilename = dgImage[1, indexSel].Value.ToString();
            string strLife = dgImage[2, indexSel].Value.ToString();
            _global.tmp = strFilename + "|" + strLife;
            // 행정보 보관
            _recordImage.filename = strFilename;
            _recordImage.life = float.Parse(strLife);
            frmAddModifyImage frmImage = new frmAddModifyImage();
            if (frmImage.ShowDialog(this) == DialogResult.OK)
            {
                string[] arryImageInfo = _global.tmp.Split('|');
                strFilename = arryImageInfo[0];
                strLife = arryImageInfo[1];
                if (_recordImage.filename.Equals(strFilename) && _recordImage.life == float.Parse(strLife))
                {
                    MessageBox.Show("没有可修改的内容！");
                    return;
                }
                dgImage[1, indexSel].Value = strFilename;
                dgImage[2, indexSel].Value = strLife;
            }
        }
        private void btnDelImage_Click(object sender, EventArgs e)
        {
            int nSelRows = dgImage.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("请选择行！");
                return;    
            }
            // 지우기
            int indexSel = dgImage.SelectedRows[0].Index;
            dgImage.Rows.RemoveAt(indexSel);
            // 지운 행 다음부터 행번호 고치기
            int nRows = dgImage.Rows.Count;
            for (int i = indexSel; i < nRows; i++ )
            {
                dgImage[0, i].Value = (i + 1).ToString();
            }
        }
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            frmAddModifyImage frmImage = new frmAddModifyImage();
            string strTitle = "";
            switch (flashType)
            {
                case 1: // 상상완화
                    strTitle = "想象放松(添加图片)";
                    break;
                case 2: // 완화방법
                    strTitle = "放松方法(添加图片)";
                    break;
                default:
                    break;
            }
            frmImage.Text = strTitle;
            _global.tmp = ""; // 변경이 아니므로 빈기호렬
            if (frmImage.ShowDialog(this) == DialogResult.OK)
            {
                // 대화창에서 넘어온 이미지파일의 절대이름과 그의 존속시간 얻기
                string[] arryImgInfo = _global.tmp.Split('|');
                string strFilename = arryImgInfo[0];
                string strLife = arryImgInfo[1];
                // 현재 그리드에서 선택된 행의 인덱스 얻기
                int nRows = dgImage.Rows.Count;
                if (nRows == 0) // 그리드안에 행이 없는 경우
                {
                    // 추가한다
                    AddImageToGrid(strFilename, strLife);
                }
                else
                {
                    int nSelRows = dgImage.SelectedRows.Count;
                    int indexLastRow = dgImage.Rows.GetLastRow(DataGridViewElementStates.Displayed);
                    if (nSelRows == 0) // 선택된 행이 없으면 추가한다.
                    {
                        AddImageToGrid(strFilename, strLife);
                    }
                    else
                    {
                        int indexSel = dgImage.SelectedRows[0].Index;
                        if (indexSel == indexLastRow) // 맨 마지막행을 선택하였으므로 추가한다.
                        {
                            AddImageToGrid(strFilename, strLife);
                        }
                        else // 이제야 삽입!!!
                        {
                            dgImage.Rows.Insert(indexSel + 1, 1); // 선택된 행의 다음행에 삽입
                            dgImage[0, indexSel + 1].Value = (indexSel + 2).ToString(); // 번호
                            dgImage[1, indexSel + 1].Value = strFilename; // 파일명
                            dgImage[2, indexSel + 1].Value = strLife; // 존속시간
                            // 삽입된 다음행부터 번호 고치기
                            nRows = dgImage.Rows.Count;
                            for (int i = indexSel + 2; i < nRows; i++)
                            {
                                dgImage[0, i].Value = (i + 1).ToString(); // 번호
                            }
                            dgImage.Rows[indexSel + 1].Selected = true;
                        }
                    }
                }
            }
        }
        private void btnUpImage_Click(object sender, EventArgs e)
        {
            int nSelRows = dgImage.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("请选择行！");
                return;
            }
            int indexSel = dgImage.SelectedRows[0].Index;
            if (indexSel == 0) return; // 첫번째 행을 선택하였으므로 탈퇴
            int nRows = dgImage.Rows.Count;
            // 번호 바꾸기
            dgImage[0, indexSel].Value = indexSel.ToString();
            dgImage[0, indexSel - 1].Value = (indexSel + 1).ToString();
            DataGridViewRow row = dgImage.Rows[indexSel - 1]; // 앞의 행을 보관
            dgImage.Rows.RemoveAt(indexSel - 1);
            if (indexSel == nRows - 1) // 마지막행을 올렸으므로 추가
            {
                dgImage.Rows.Add(row);
            }
            else // 삽입
            {
                dgImage.Rows.Insert(indexSel, row);
            }
        }
        private void btnDownImage_Click(object sender, EventArgs e)
        {
            int nSelRows = dgImage.SelectedRows.Count;
            if (nSelRows == 0) // 선택된 행이 없으면 탈퇴
            {
                MessageBox.Show("请选择行！");
                return;
            }
            int indexSel = dgImage.SelectedRows[0].Index;
            int nRows = dgImage.Rows.Count;
            if (indexSel == nRows - 1) return; // 마지막 행을 선택하였으므로 탈퇴
            // 번호 바꾸기
            dgImage[0, indexSel].Value = (indexSel + 2).ToString();
            dgImage[0, indexSel + 1].Value = (indexSel + 1).ToString();
            DataGridViewRow row = dgImage.Rows[indexSel]; // 선택한 행 보관
            dgImage.Rows.RemoveAt(indexSel);
            if (indexSel == nRows - 2) // 마지막에서 두번째 행을 내리웠으므로 추가
            {
                dgImage.Rows.Add(row);
            }
            else
            {
                dgImage.Rows.Insert(indexSel + 1, row);
            }
            dgImage.Rows[indexSel + 1].Selected = true;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string strTitle = txtTitle.Text.Trim();
            string strSoundfile = txtSoundfile.Text.Trim();
            if (strTitle.Equals(""))
            {
                MessageBox.Show("清输入主题！");
                this.DialogResult = DialogResult.None;
                return;
            }
            if (strSoundfile.Equals(""))
            {
                MessageBox.Show("请选择音乐！");
                this.DialogResult = DialogResult.None;
                return;
            }
            if (MessageBox.Show("您输入的信息被保存在数据库中。", "确认窗口", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                processFlashData(strTitle, strSoundfile);
            }
            else
            {
                this.DialogResult = DialogResult.None;
                return;
            }
        }
        private void processFlashData(string strTitle, string strSoundfile)
        {
            List<_image> imageList = new List<_image>();
            int nCount = dgImage.Rows.Count;
            for (int i = 0; i < nCount; i++ )
            {
                _image img = new _image();
                img.no = i + 1; // 1-base
                img.filename = dgImage[1, i].Value.ToString();
                img.life = float.Parse(dgImage[2, i].Value.ToString());
                imageList.Add(img);
            }
            // 추가냐 변경이냐를 판단
            if (strMethod.Equals("add"))
            {
                _dbManager.insertFlash(nCountFlash + 1, flashType, flashNo, strTitle, strSoundfile, ref imageList); // 추가인 경우 레코드수는 nCountFlash + 1개
            }
            else if (strMethod.Equals("change"))
            {
                _dbManager.changeFlash(flashType, flashNo, strTitle, strSoundfile, ref imageList);
            }
            else
            {
                return;
            }
        }
    }
}
