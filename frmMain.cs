using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ComReader;
using SEMS_FlashLibrary;
using System.IO;

namespace simri
{
    public partial class frmMain : Form
    {
        private int reportType = 0;
        private frmComChart frmChart = null;
        private HeartRateSetting HrSetting = null;
        private _report report = null;
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            // 폼창 설정
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            // 콤리더창
            frmChart = new frmComChart();
            frmChart.Hide();

            // 첫 화면 띄우기
            axShockwaveView.Location = new Point(0, 0);
            axShockwaveView.Size = this.ClientSize;
            axShockwaveView.ScaleMode = 2;
            axShockwaveView.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(this.axShockwaveFlash_FSCommandMain);
            axShockwaveView.LoadMovie(0, _path.gui + "firstscene.swf");

            // 작은 창구
            axShockwaveFlash.ScaleMode = 2;
            axShockwaveFlash.Hide();
            axShockwaveFlash.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(this.axShockwaveFlash_FSCommandGame);

            // 타이머 설정
            tmGame.Interval = _game_data.tickGameInterval;
            tmGame.Tick += new EventHandler(tmGame_Tick);
            tmCollector.Interval = _game_data.tickCollectInterval;
            tmCollector.Tick += new EventHandler(tmCollector_Tick);

            // 사운드 페지
            axShockwaveSound.Location = new Point(0, 0);
            axShockwaveSound.Size = this.ClientSize;
            axShockwaveSound.ScaleMode = 2;
            axShockwaveSound.Hide();
            axShockwaveSound.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(this.axShockwaveFlash_FSCommandSound);
            axShockwaveSound.LoadMovie(0, _path.gui + "soundmenu.swf");
            axShockwaveSound.SetVariable("soundData", getSoundData());

            // 보고서 패널
            Color bkColor = Color.FromArgb(226, 235, 247);
            lnkSave.BackColor = bkColor;
            lnkPrint.BackColor = bkColor;
            lnkClose.BackColor = bkColor;
            panelReport.Controls.Add(lnkSave);
            panelReport.Controls.Add(lnkPrint);
            panelReport.Controls.Add(lnkClose);
            panelReport.Controls.Add(webBrowser);
            panelReport.Hide();
        }
        private void axShockwaveFlash_FSCommandMain(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            string strCmd = e.command;
            string strArg = e.args;
            if (strCmd.Equals("exit")) this.Close();
            else if (strCmd.Equals("report"))
            {
                #region 
                switch (strArg)
                {
                    case "save":
                        saveFileDialog.Filter = "word文件(*.doc)|*.doc";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            if (report.type == 1)
                            {
                                report.makePingguWordDocument(saveFileDialog.FileName);
                            }
                            else
                            {
                                report.makeWordDocument(saveFileDialog.FileName);
                            }
                        }
                        break;
                    case "print":
                        webBrowser.Print();
                        break;
                    case "close":
                        report = null;
                        panelReport.Hide();
                        break;
                    default:
                        break;
                }
                #endregion
            }
            else if (strCmd.Equals("login")) // 로그인단추를 눌렀을 때
            {
                #region

                string[] strUserInfo = strArg.Split('|');
                if (strUserInfo.Length == 2)
                {
                    _user.name = strUserInfo[0];
                    _user.password = strUserInfo[1];
                    if (_dbManager.isCoreect())
                    {
                        // 여기서 프로그레스바
                        _dbManager.recordLogin();
                        axShockwaveView.LoadMovie(0, _path.gui + "menuscene.swf");
                    }
                    else
                    {
                        axShockwaveView.SetVariable("isCorrectUser", "invalidate");
                    }
                }
                #endregion
            }
            else if (strCmd.Equals("regist")) // 등록단추를 눌렀을 때
            {
                #region
                string[] strRegistInfo = strArg.Split('|');
                if (strRegistInfo.Length == 7)
                {
                    _record.username = strRegistInfo[0];
                    _record.password = strRegistInfo[1];
                    _record.gender = strRegistInfo[2];
                    _record.birthYear = Convert.ToInt32(strRegistInfo[3]);
                    _record.birthMonth = Convert.ToInt32(strRegistInfo[4]);
                    _record.birthDay = Convert.ToInt32(strRegistInfo[5]);
                    _record.job = strRegistInfo[6];
                    if (_dbManager.isExist(_record.username))
                    {
                        MessageBox.Show("您要注册的用户已存在！");
                        return;
                    }
                    _dbManager.registUser();
                }
                #endregion
            }
            else if (strCmd.Equals("goto")) // 메뉴창문에서 해당 항목을 눌렀을 때
            {
                #region
                double widthScene, heightScene, leftLayer, topLayer, widthLayer, heightLayer, xScale, yScale, scaleFactor, xAnchorStart, xAnchorEnd;
                string strFlashString;
                switch (strArg)
                {
                    case "pinggu": // 평가
                        #region
                        _game_data.name = "measurescene.swf";
                        axShockwaveView.LoadMovie(0, _path.gui + "measurescene.swf");
                        // 평가창문 열면서 53개 질문창 열기
                        widthScene = 1024; heightScene = 768;
                        topLayer = 146.0f; heightLayer = 557.0f;
                        widthLayer = widthScene * heightLayer / heightScene;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        scaleFactor = (xScale > yScale) ? yScale : xScale;
                        axShockwaveFlash.Location = new Point(Convert.ToInt32((this.ClientSize.Width - widthLayer * scaleFactor) / 2), Convert.ToInt32(topLayer * yScale));
                        axShockwaveFlash.Size = new Size(Convert.ToInt32(widthLayer * scaleFactor), Convert.ToInt32(heightLayer * scaleFactor));
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "总体53道题组件男.swf");
                        axShockwaveFlash.Show();
                        _global.current = 0;

                        widthScene = 1024; heightScene = 768;
                        leftLayer = 34; widthLayer = 955;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(axShockwaveFlash.Top));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(axShockwaveFlash.Height));
                        reportType = 0;
                        //regionType = 1;
                        frmChart.initDeviceReader();
                        break;
                        #endregion
                    // 사운드
                    #region
                    case "soundemotion": // 정서음악
                        axShockwaveSound.Visible = true;
                        axShockwaveSound.SetVariable("curMusicMode", "emotion");
                        widthScene = 1024; heightScene = 768;
                        leftLayer = 361.9; topLayer = 181; widthLayer = 555; heightLayer = 400;
                        xScale = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width) / widthScene;
                        yScale = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(topLayer * yScale));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(heightLayer * yScale));
                        reportType = 1;
                        //regionType = 1;
                        frmChart.initDeviceReader();
                        break;
                    case "soundself": // 개성음악
                        axShockwaveSound.Visible = true;
                        axShockwaveSound.SetVariable("curMusicMode", "self");
                        widthScene = 1024; heightScene = 768;
                        leftLayer = 361.9; topLayer = 181; widthLayer = 555; heightLayer = 400;
                        xScale = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width) / widthScene;
                        yScale = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(topLayer * yScale));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(heightLayer * yScale));
                        reportType = 1;
                        //regionType = 1;
                        frmChart.initDeviceReader();
                        break;
                    case "soundfree": // 자체음악
                        axShockwaveSound.Visible = true;
                        axShockwaveSound.SetVariable("curMusicMode", "free");
                        widthScene = 1024; heightScene = 768;
                        leftLayer = 361.9; topLayer = 181; widthLayer = 555; heightLayer = 400;
                        xScale = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width) / widthScene;
                        yScale = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(topLayer * yScale));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(heightLayer * yScale));
                        reportType = 1;
                        //regionType = 1;
                        frmChart.initDeviceReader();
                        break;
                    #endregion
                    case "filemanage": // 화일관리
                        #region
                        // 리용자정보
                        axShockwaveView.LoadMovie(0, _path.gui + "filemanage.swf");
                        _dbManager.getUserInfo();
                        axShockwaveView.SetVariable("strUsername", _user_info.name);
                        axShockwaveView.SetVariable("strGender", _user_info.gender);
                        axShockwaveView.SetVariable("strBirthday", _user_info.birthday);
                        axShockwaveView.SetVariable("strJob", _user_info.job);
                        // 자료그리드
                        List<_user_log> log = new List<_user_log>();
                        int count = _dbManager.getUserLog(ref log);
                        string strLogData = "";
                        for (int i = 0; i < count; i++ )
                        {
                            int type = log.ElementAt(i).type;
                            string itemGrid = log.ElementAt(i).moment + "|";
                            if (type == 1)
                            {
                                itemGrid += "压力评估";
                            }
                            else
                            {
                                string[] arryReport = log.ElementAt(i).report.Split('|');
                                itemGrid += arryReport[0];
                            }
                            itemGrid += "|" + "查看";
                            strLogData += (i == count - 1) ? itemGrid : itemGrid + " &";
                        }
                        axShockwaveView.SetVariable("strGridData", strLogData);
                        // 보고서 위치 결정
                        widthScene = 1024; heightScene = 768;
                        leftLayer = 34; topLayer = 231.1; widthLayer = 955; heightLayer = 420;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(topLayer * yScale));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(heightLayer * yScale));
                        reportType = 2;
                        //regionType = 0;
                        break;
                        #endregion
                    case "imagination": // 상상
                        #region
                        axShockwaveFlash.Location = new Point(0, 0);
                        axShockwaveFlash.Size = this.ClientSize;
                        axShockwaveFlash.LoadMovie(0, _path.gui + "imagination.swf");
                        strFlashString = _dbManager.getFlashString(1);
                        if (!strFlashString.Equals(""))
                        {
                            strFlashString = strFlashString.Replace('\\', '/');
                            strFlashString = strFlashString.Replace("////", "//");
                            strFlashString = strFlashString.Replace("//", "/");
                            axShockwaveFlash.SetVariable("strFashData", strFlashString);
                        }
                        axShockwaveFlash.Show();
                        _game_data.name = "imagination.swf";
                        widthScene = 1024; heightScene = 768;
                        leftLayer = 273.6; topLayer = 176.7; widthLayer = 695; heightLayer = 480.2;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(topLayer * yScale));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(heightLayer * yScale));
                        reportType = 1;
                        frmChart.initDeviceReader();
                        break;
                        #endregion
                    case "relaxcontrol": // 완화조종
                        #region
                        axShockwaveView.LoadMovie(0, _path.gui + "gamerelaxcontrol.swf");
                        widthScene = 1024; heightScene = 768;
                        topLayer = 150.1; widthLayer = 723.1; heightLayer = 550.1; xAnchorStart = 267; xAnchorEnd = 1010;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        scaleFactor = (xScale > yScale) ? yScale : xScale;
                        axShockwaveFlash.Location = new Point(Convert.ToInt32(xAnchorStart * xScale + ((xAnchorEnd - xAnchorStart) * xScale - widthLayer * scaleFactor) / 2), Convert.ToInt32(topLayer * yScale));
                        axShockwaveFlash.Size = new Size(Convert.ToInt32(widthLayer * scaleFactor), Convert.ToInt32(heightLayer * scaleFactor));
                        axShockwaveFlash.Visible = true;
                        panelReport.Location = axShockwaveFlash.Location;
                        panelReport.Size = axShockwaveFlash.Size;
                        reportType = 1;
                        //regionType = 0;
                        frmChart.initDeviceReader();
                        break;
                        #endregion
                    case "relaxmethod": // 완화방법
                        #region
                        axShockwaveFlash.Location = new Point(0, 0);
                        axShockwaveFlash.Size = this.ClientSize;
                        axShockwaveFlash.LoadMovie(0, _path.gui + "gamerelaxmethod.swf");
                        strFlashString = _dbManager.getFlashString(2);
                        if (!strFlashString.Equals(""))
                        {
                            strFlashString = strFlashString.Replace('\\', '/');
                            strFlashString = strFlashString.Replace("////", "//");
                            strFlashString = strFlashString.Replace("//", "/");
                            axShockwaveFlash.SetVariable("strFashData", strFlashString);
                        }
                        axShockwaveFlash.Show();
                        _game_data.name = "gamerelaxmethod.swf";
                        widthScene = 1024; heightScene = 768;
                        leftLayer = 273.6; topLayer = 176.7; widthLayer = 695; heightLayer = 480.2;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        panelReport.Location = new Point(Convert.ToInt32(leftLayer * xScale), Convert.ToInt32(topLayer * yScale));
                        panelReport.Size = new Size(Convert.ToInt32(widthLayer * xScale), Convert.ToInt32(heightLayer * yScale));
                        reportType = 1;
                        frmChart.initDeviceReader();
                        break;
                        #endregion
                    case "sight": // 전경
                        #region
                        axShockwaveView.LoadMovie(0, _path.gui + "gamesight.swf");
                        widthScene = 1024; heightScene = 768;
                        topLayer = 150.1; widthLayer = 723.1; heightLayer = 550.1; xAnchorStart = 267; xAnchorEnd = 1010;
                        xScale = Convert.ToDouble(this.ClientSize.Width) / widthScene;
                        yScale = Convert.ToDouble(this.ClientSize.Height) / heightScene;
                        scaleFactor = (xScale > yScale) ? yScale : xScale;
                        axShockwaveFlash.Location = new Point(Convert.ToInt32(xAnchorStart * xScale + ((xAnchorEnd - xAnchorStart) * xScale - widthLayer * scaleFactor) / 2), Convert.ToInt32(topLayer * yScale));
                        axShockwaveFlash.Size = new Size(Convert.ToInt32(widthLayer * scaleFactor), Convert.ToInt32(heightLayer * scaleFactor));
                        axShockwaveFlash.Visible = true;
                        panelReport.Location = axShockwaveFlash.Location;
                        panelReport.Size = axShockwaveFlash.Size;
                        reportType = 1;
                        frmChart.initDeviceReader();
                        break;
                        #endregion
                    default:
                        break;
                }
                #endregion
            }
            else if (strCmd.Equals("goback")) goToMenu();
            else if (strCmd.Equals("showin")) // 모든 게임은 여기서 적재되므로 여기서 해당 이름을... 
            {
                #region
                if (tmGame.Enabled) tmGame.Stop();
                if (tmCollector.Enabled) tmCollector.Stop();
                _game_data.name = strArg;
                axShockwaveFlash.LoadMovie(0, _path.swf + strArg);
                #endregion
            }
            else if (strCmd.Equals("showreport"))
            {
                #region
                _global.moment = strArg;
                string pinggu = _dbManager.getUserReport(_global.moment).Trim();
                if (!pinggu.Equals("")) showReport(pinggu);
                #endregion
            }
            else if (strCmd.Equals("setting"))
            {
                #region
                frmConfirm confirm = new frmConfirm();
                if (confirm.ShowDialog() == DialogResult.OK && _dbManager.isCorrectAdmin(_global.tmp))
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                }
                #endregion
            }
            else
            {
                // 검출되지 않은 통보문
                #region
                // 상상완화일 때 "Start" 명령이 온다
                if (!strCmd.Equals("Start")) System.Windows.Forms.MessageBox.Show("command=" + strCmd + ", args=" + strArg);
                Console.WriteLine("command=" + strCmd + ", args=" + strArg);
                return;
                #endregion
            }
        }
        private void axShockwaveFlash_FSCommandSound(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            string command = e.command;
            string args = e.args;
            switch (command)
            {
                case "goback":
                    goToMenu();
                    break;
                case "Start":
                    _global.tmp = args;
                    if (tmCollector.Enabled) tmCollector.Stop();
                    _game_data.tickCollectInterval = 3000;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                    hideChart();
                    tmCollector.Stop();
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    string[] strMusicPos = _global.tmp.Split('|');
                    switch (strMusicPos[0])
                    {
                        case "emotion":
                            _game_data.result = "情绪音乐" + "-" + strMusicPos[1] + "-" + strMusicPos[2];
                            break;
                        case "free":
                            _game_data.result = "自选音乐" + "-" + strMusicPos[1] + "-" + strMusicPos[2];
                            break;
                        case "self":
                            _game_data.result = "个性音乐" + "-" + strMusicPos[1] + "-" + strMusicPos[2];
                            break;
                        default:
                            _game_data.result = "";
                            break;
                    }
                    _game_data.result += "|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++ )
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(3, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("3#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveSound.SetVariable("report", "false");
                    break;
                default:
                    break;
            }
        }
        private void axShockwaveFlash_FSCommandGame(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            string strCmd = e.command;
            string strArg = e.args;
            switch (_game_data.name)
            {
                case "measurescene.swf": // 평가
                    game_pinggu(strCmd, strArg);
                    break;
                case "imagination.swf": // 상상완화
                    game_imagination(strCmd, strArg);
                    break;
                case "secretgarden.swf": // 완화조종에서 비밀정원(소녀)
                    game_secretgarden(strCmd, strArg);
                    break;
                case "shooter.swf": // 전경에서 총쏘기
                    game_shooter(strCmd, strArg);
                    break;
                case "flight.swf": // 전경에서장대쥐고 바줄타기
                    game_flight(strCmd, strArg);
                    break;
                case "borisu.swf": // 전경에서 완화조종에서보리수(비줄기)
                    game_borisu(strCmd, strArg);
                    break;
                case "doubaobao.swf": // 전경에서 도우바오바오(애기울음)
                    game_doubaobao(strCmd, strArg);
                    break;
                case "searchpearl.swf": // 전경에서 진주찾기
                    game_searchpearl(strCmd, strArg);
                    break;
                case "gamerelaxmethod.swf": // 완화방법
                    game_relaxmethod(strCmd, strArg);
                    break;
                default:
                    break;
            }
        }
        private void showReport(string data)
        {
            _global.tmp = data;
            webBrowser.Location = new Point(0, 0);
            webBrowser.Size = panelReport.Size;
            int interval = 50, bottomMargin = 30, rightMargin = 30;
            lnkClose.Location = new Point(panelReport.Width - rightMargin - lnkClose.Width, panelReport.Height - bottomMargin - lnkClose.Height);
            lnkPrint.Location = new Point(lnkClose.Left - interval - lnkPrint.Width, lnkClose.Top);
            lnkSave.Location = new Point(lnkPrint.Left - interval - lnkSave.Width, lnkClose.Top);
            report = new _report();
            webBrowser.DocumentText = report.getHtml(webBrowser.Version.Major);

            Rectangle rcPanel = panelReport.ClientRectangle;
            System.Drawing.Drawing2D.GraphicsPath panelPath = new System.Drawing.Drawing2D.GraphicsPath();
            panelReport.Show();
            if (reportType == 2)
            {
                lnkSave.Hide();
                lnkPrint.Hide();
                lnkClose.Hide();
            }
            else
            {
                lnkSave.Show();
                lnkPrint.Show();
                lnkClose.Show();
            }
        }
        private void timer_secretgarden() // 타이머:비밀정원(소녀)
        {
            int heartRate = _device_data.HR;
            if (heartRate == 0 || heartRate == 191)
            {
                return;
            }
            _game_data.avgHR += heartRate;
            _game_data.tickCount++;
            if (_game_data.tickCount == _game_data.maxTickCount)
            {
                // 평균값계산
                _game_data.avgHR /= _game_data.maxTickCount;
                // 신호보내기
                int progress = HrSetting.HrSet(_game_data.avgHR);
                if (progress == 0)
                {
                    // 초기화
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    return;
                }
                axShockwaveFlash.SetVariable("CNum", progress.ToString());
                if (progress == 1)
                {
                    _game_data.progressValue++;
                }
                else if (progress == -1)
                {
                    if (_game_data.progressValue > 0)
                    {
                        _game_data.progressValue--;
                    } 
                    else
                    {
                        _game_data.progressValue = 0;
                    }
                }
                if (_game_data.progressValue <= _game_data.maxProgressValue)
                {//前两格
                    _game_data.maxTickCount = 2;
                    switch (_game_data.level)
                    {
                        case 0: // 초급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1: // 중급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2: // 고급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                else
                {
                    _game_data.maxTickCount = 3;
                    switch (_game_data.level)
                    {
                        case 0:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                // 초기화
                _game_data.avgHR = 0;
                _game_data.tickCount = 0;
            }
        }
        private void timer_borisu() // 타이머:보리수(비줄기)
        {
            int heartRate = _device_data.HR;
            if (heartRate == 0 || heartRate == 191)
            {
                return;
            }
            _game_data.avgHR += heartRate;
            _game_data.tickCount++;
            if (_game_data.tickCount == _game_data.maxTickCount)
            {
                // 평균값계산
                _game_data.avgHR /= _game_data.maxTickCount;
                // 신호보내기
                int progress = HrSetting.HrSet(_game_data.avgHR);
                if (progress == 0)
                {
                    // 초기화
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    return;
                }
                axShockwaveFlash.SetVariable("CNum", progress.ToString());
                if (progress == 1)
                {
                    _game_data.progressValue++;
                }
                else if (progress == -1)
                {
                    if (_game_data.progressValue > 0)
                    {
                        _game_data.progressValue--;
                    }
                    else
                    {
                        _game_data.progressValue = 0;
                    }
                }
                if (_game_data.progressValue <= _game_data.maxProgressValue)
                {//前两格
                    _game_data.maxTickCount = 2;
                    switch (_game_data.level)
                    {
                        case 0: // 초급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1: // 중급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2: // 고급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                else
                {
                    _game_data.maxTickCount = 3;
                    switch (_game_data.level)
                    {
                        case 0:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                // 초기화
                _game_data.avgHR = 0;
                _game_data.tickCount = 0;
            }
        }
        private void timer_doubaobao() // 타이머:도우바오바오(애기울음)
        {
            int heartRate = _device_data.HR;
            if (heartRate == 0 || heartRate == 191)
            {
                return;
            }
            _game_data.avgHR += heartRate;
            _game_data.tickCount++;
            if (_game_data.tickCount == _game_data.maxTickCount)
            {
                // 평균값계산
                _game_data.avgHR /= _game_data.maxTickCount;
                // 신호보내기
                int progress = HrSetting.HrSet(_game_data.avgHR);
                if (progress == 0)
                {
                    // 초기화
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    return;
                }
                axShockwaveFlash.SetVariable("CNum", progress.ToString());
                if (progress == 1)
                {
                    _game_data.progressValue++;
                }
                else if (progress == -1)
                {
                    if (_game_data.progressValue > 0)
                    {
                        _game_data.progressValue--;
                    }
                    else
                    {
                        _game_data.progressValue = 0;
                    }
                }
                if (_game_data.progressValue <= _game_data.maxProgressValue)
                {//前两格
                    _game_data.maxTickCount = 2;
                    switch (_game_data.level)
                    {
                        case 0: // 초급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1: // 중급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2: // 고급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                else
                {
                    _game_data.maxTickCount = 3;
                    switch (_game_data.level)
                    {
                        case 0:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                // 초기화
                _game_data.avgHR = 0;
                _game_data.tickCount = 0;
            }
        }
        private void timer_searchpearl() // 타이머:진주찾기
        {
            int heartRate = _device_data.HR;
            if (heartRate == 0 || heartRate == 191)
            {
                return;
            }
            _game_data.avgHR += heartRate;
            _game_data.tickCount++;
            if (_game_data.tickCount == _game_data.maxTickCount)
            {
                // 평균값계산
                _game_data.avgHR /= _game_data.maxTickCount;
                // 신호보내기
                int progress = HrSetting.ZhenzhuHrSet(_game_data.avgHR);
                if (progress != -100) axShockwaveFlash.SetVariable("CNum", progress.ToString());
                // 초기화
                _game_data.avgHR = 0;
                _game_data.tickCount = 0;
            }
        }
        private void timer_shooter() // 타이머:총쏘기
        {
            int heartRate = _device_data.HR;
            if (heartRate == 0 || heartRate == 191)
            {
                return;
            }
            _game_data.avgHR += heartRate;
            _game_data.tickCount++;
            if (_game_data.tickCount == _game_data.maxTickCount)
            {
                // 평균값계산
                _game_data.avgHR /= _game_data.maxTickCount;
                // 신호보내기
                int progress = HrSetting.HrSet(_game_data.avgHR);
                if (progress == 0)
                {
                    // 초기화
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    return;
                }
                axShockwaveFlash.SetVariable("CNum", progress.ToString());
                if (progress == 1)
                {
                    _game_data.progressValue++;
                }
                else if (progress == -1)
                {
                    _game_data.progressValue--;
                }
                if (_game_data.progressValue <= _game_data.maxProgressValue)
                {//前两格
                    _game_data.maxTickCount = 2;
                    switch (_game_data.level)
                    {
                        case 0: // 초급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1: // 중급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2: // 고급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                else
                {
                    _game_data.maxTickCount = 3;
                    switch (_game_data.level)
                    {
                        case 0:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                // 초기화
                _game_data.avgHR = 0;
                _game_data.tickCount = 0;
            }
        }
        private void timer_flight() // 타이머:장대쥐고 바줄타기
        {
            int heartRate = _device_data.HR;
            if (heartRate == 0 || heartRate > 150)
            {
                axShockwaveFlash.SetVariable("CNum", "0");
                return;
            }
            if (!_game_data.isStart) return;
            _game_data.avgHR += heartRate;
            _game_data.tickCount++;
            if (_game_data.tickCount == _game_data.maxTickCount)
            {
                // 평균값계산
                _game_data.avgHR /= _game_data.maxTickCount;
                // 신호보내기
                int progress = HrSetting.HrSet(_game_data.avgHR);
                if (progress == 0)
                {
                    axShockwaveFlash.SetVariable("CNum", "0");
                    // 초기화
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    return;
                }
                axShockwaveFlash.SetVariable("CNum", progress.ToString());
                if (progress == 1)
                {
                    _game_data.progressValue++;
                }
                else if (progress == -1)
                {
                    if (_game_data.progressValue > 0)
                    {
                        _game_data.progressValue--;
                    }
                    else
                    {
                        _game_data.progressValue = 0;
                    }
                }
                if (_game_data.progressValue <= _game_data.maxProgressValue)
                {//前两格
                    _game_data.maxTickCount = 2;
                    switch (_game_data.level)
                    {
                        case 0: // 초급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1: // 중급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2: // 고급
                            HrSetting.NatureCountConst = 3;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                else
                {
                    _game_data.maxTickCount = 3;
                    switch (_game_data.level)
                    {
                        case 0:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 2;
                            break;
                        case 1:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                        case 2:
                            HrSetting.NatureCountConst = 5;
                            HrSetting.BeyondRangeCountConst = 1;
                            break;
                    }
                }
                // 초기화
                _game_data.avgHR = 0;
                _game_data.tickCount = 0;
            }
        }
        private void tmCollector_Tick(object source, EventArgs e)
        {
            int spO2 = _device_data.SPO2 , hr = _device_data.HR, bar = _device_data.Bar;
            if (spO2 !=0 ) _game_data.dataSPO2.Add(_device_data.SPO2);
            if (hr != 0) _game_data.dataHR.Add(_device_data.HR);
            if (bar != 0) _game_data.dataBar.Add(_device_data.Bar);
        }
        private void tmGame_Tick(object source, EventArgs e)
        {
            switch (_game_data.name)
            {
                case "secretgarden.swf": // 비밀정원(소녀)
                    timer_secretgarden();
                    break;
                case "borisu.swf": // 보리수(비줄기)
                    timer_borisu();
                    break;
                case "doubaobao.swf": // 도우바오바오(애기울음)
                    timer_doubaobao();
                    break;
                case "searchpearl.swf": // 진주찾기
                    timer_searchpearl();
                    break;
                case "shooter.swf": // 총쏘기
                    timer_shooter();
                    break;
                case "flight.swf": // 장대쥐고 바줄타기
                    timer_flight();
                    break;
                default:
                    break;
            }
            // 게임처리부
#region 
            //_game_data.sumHR += _device_data.HR; // 맥박값 루적
            //_game_data.tickCount++;
            //if (_game_data.tickCount == _game_data.maxTickCount) // maxTickCount번(3번) 읽었다
            //{
            //    _game_data.signals[_game_data.count] = _game_data.sumHR;
            //    _game_data.tickCount = 0;
            //    _game_data.sumHR = 0;
            //    _game_data.count++;
            //}
            //if (_game_data.count == _game_data.maxCount)
            //{
            //    // 령검사
            //    int sumOffset = 0;
            //    for (int i = 0; i < _game_data.maxCount; i++ )
            //    {
            //        sumOffset += _game_data.signals[i];
            //    }
            //    if (sumOffset == 0)
            //    {
            //        _game_data.count = 0;
            //        _game_data.tickCount = 0;
            //        _game_data.sumHR = 0;
            //        return;
            //    }
            //    // 오차값을 루적한다
            //    sumOffset = 0;
            //    for (int i = 0; i < _game_data.maxCount - 2; i++ )
            //    {
            //        sumOffset += _game_data.signals[i + 1] - _game_data.signals[i];
            //    }
            //    int curCNum;
            //    if (_game_data.level == 0) // 초급일 때
            //    {
            //        curCNum = (sumOffset <= 12) ? 1 : -1;
            //        curCNum = curCNum + _game_data.prevCNum;
            //        if (curCNum > 0)
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "1"); // 전진
            //            _game_data.prevCNum = 1;
            //        }
            //        else if (curCNum == 0)
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "0"); // 정지
            //            _game_data.prevCNum = 0;
            //        }
            //        else
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "-1"); // 후진
            //            _game_data.prevCNum = -1;
            //        }
            //    } else if (_game_data.level == 1)  // 중급일 때
            //    {
            //        curCNum = (sumOffset < 9) ? 1 : -1;
            //        curCNum = curCNum + _game_data.prevCNum;
            //        if (curCNum > 0)
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "1"); // 전진
            //            _game_data.prevCNum = 1;
            //        }
            //        else if (curCNum == 0)
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "0"); // 정지
            //            _game_data.prevCNum = 0;
            //        }
            //        else
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "-1"); // 후진
            //            _game_data.prevCNum = -1;
            //        }
            //    }
            //    else if (_game_data.level == 2)  // 고급일 때
            //    {
            //        curCNum = (sumOffset <= 0) ? 1 : -1;
            //        curCNum = curCNum + _game_data.prevCNum;
            //        if (curCNum > 0)
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "1"); // 전진
            //            _game_data.prevCNum = 1;
            //        }
            //        else if (curCNum == 0)
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "0"); // 정지
            //            _game_data.prevCNum = 0;
            //        }
            //        else
            //        {
            //            axShockwaveFlash.SetVariable("CNum", "-1"); // 후진
            //            _game_data.prevCNum = -1;
            //        }
            //    }
            //    _game_data.count = 0;
            //    _game_data.tickCount = 0;
            //    _game_data.sumHR = 0;
            //}
#endregion
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmChart != null)
            {
                frmChart.stop_comreader();
            }
        }
        private string getSoundData()
        {
            int countFolder;
            List<_folder> folderList = null;
            // 정서음악
            folderList = new List<_folder>();
            countFolder = _dbManager.getFolderList(1, ref folderList);
            string emotionText = "";
            string emotionFolder = "";
            string emotionFile = "";
            for (int i = 0; i < countFolder; i++)
            {
                _folder folder = folderList[i];
                emotionText += (i == countFolder - 1) ? folder.comment : folder.comment + "|";
                emotionFolder += (i == countFolder - 1) ? folder.name : folder.name + "|";
                List<_file> fileList = new List<_file>();
                int countFile = _dbManager.getFolderLeafs(1, folder.no, ref fileList);
                for (int j = 0; j < countFile; j++)
                {
                    _file file = fileList[j];
                    string pair = file.title + "|" + _path.music + file.name;
                    emotionFile += (j == countFile - 1) ? pair : pair + "&";
                }
                emotionFile += (i == countFolder - 1) ? "" : "@";
            }

            // 개성음악
            folderList = new List<_folder>();
            countFolder = _dbManager.getFolderList(2, ref folderList);
            string freeText = "";
            string freeFolder = "";
            string freeFile = "";
            for (int i = 0; i < countFolder; i++)
            {
                _folder folder = folderList[i];
                freeText += (i == countFolder - 1) ? folder.comment : folder.comment + "|";
                freeFolder += (i == countFolder - 1) ? folder.name : folder.name + "|";
                List<_file> fileList = new List<_file>();
                int countFile = _dbManager.getFolderLeafs(2, folder.no, ref fileList);
                for (int j = 0; j < countFile; j++)
                {
                    _file file = fileList[j];
                    string pair = file.title + "|" + _path.music + file.name;
                    freeFile += (j == countFile - 1) ? pair : pair + "&";
                }
                freeFile += (i == countFolder - 1) ? "" : "@";
            }

            // 개성음악
            folderList = new List<_folder>();
            countFolder = _dbManager.getFolderList(3, ref folderList);
            string selfText = "";
            string selfFolder = "";
            string selfFile = "";
            for (int i = 0; i < countFolder; i++)
            {
                _folder folder = folderList[i];
                selfText += (i == countFolder - 1) ? folder.comment : folder.comment + "|";
                selfFolder += (i == countFolder - 1) ? folder.name : folder.name + "|";
                List<_file> fileList = new List<_file>();
                int countFile = _dbManager.getFolderLeafs(3, folder.no, ref fileList);
                for (int j = 0; j < countFile; j++)
                {
                    _file file = fileList[j];
                    string pair = file.title + "|" + _path.music + file.name;
                    selfFile += (j == countFile - 1) ? pair : pair + "&";
                }
                selfFile += (i == countFolder - 1) ? "" : "@";
            }
            string soundData = emotionText + "$" + emotionFolder + "$" + emotionFile + "#" + selfText + "$" + selfFolder + "$" + selfFile + "#" + freeText + "$" + freeFolder + "$" + freeFile;
            return soundData;
        }
        private void game_pinggu(string command, string args)  // 사건처리부:비밀정원(소녀)
        {
            switch (command)
            {
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "saveT":
                    if (_global.current == 0)
                    {
                        _game_data.result = args.Trim();
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "总体8道.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "saveT8":
                    if (_global.current == 1)
                    {
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "3.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "enter":
                    if (_global.current == 2)
                    {
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "数字记忆广度.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "szjygd":
                    if (_global.current == 3)
                    {
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "视觉辨别.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "jdfys":
                    if (_global.current == 4)
                    {
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "注意广度.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "zygd":
                    if (_global.current == 5)
                    {
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "图形推理.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "txtl":
                    if (_global.current == 6)
                    {
                        axShockwaveFlash.LoadMovie(0, _path.pinggu + "爱好.swf");
                        _global.current++;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                case "saveT3":
                    if (_global.current == 7)
                    {
                        hideChart();
                        DateTime now = DateTime.Now;
                        _global.moment = now.ToString();
                        if (!_dbManager.updateUserReport(1, _game_data.result, now))
                        {
                            MessageBox.Show("更新数据库时发生了错误。");
                            return;
                        }
                        showReport("1#" + _game_data.result);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    }
                    break;
                default:
                    break;
            }
        }
        private void game_relaxmethod(string command, string args)
        {
            switch (command)
            {
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Fsend":
                    hideChart();
                    goToMenu();
                    break;
                case "Start":
                    frmChart.initDeviceReader();
                    _global.tmp = args;
                    if (tmCollector.Enabled) tmCollector.Stop();
                    _game_data.tickCollectInterval = 3000;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    tmCollector.Start();
                    break;
                case "EXIT":
                    hideChart();
                    tmCollector.Stop();
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result = "放松方法-" + _global.tmp;
                    _game_data.result += "|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(3, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("3#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveFlash.SetVariable("report", "false");
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void game_imagination(string command, string args)
        {
            switch (command)
            {
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Fsend":
                    hideChart();
                    goToMenu();
                    break;
                case "Start":
                    frmChart.initDeviceReader();
                    _global.tmp = args;
                    if (tmCollector.Enabled) tmCollector.Stop();
                    _game_data.tickCollectInterval = 3000;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    tmCollector.Start();
                    break;
                case "EXIT":
                    hideChart();
                    tmCollector.Stop();
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result = "想象放松-" + _global.tmp;
                    _game_data.result += "|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(3, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("3#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveFlash.SetVariable("report", "false");
                    break;
            }
        }
        private void game_secretgarden(string command, string args)  // 사건처리부:비밀정원(소녀)
        {
            switch (command)
            {
                case "Start":
                    frmChart.initDeviceReader();
                    _game_data.tickGameInterval = 1500;
                    _game_data.tickCollectInterval = 3000;
                    tmGame.Interval = _game_data.tickGameInterval;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    _game_data.maxTickCount = 2;
                    _game_data.progressValue = 0;
                    _game_data.maxProgressValue = 2;
                    break;
                case "over": // 시작화면에서 탈퇴단추
                    goToMenu();
                    break;
                case "Back1":
                    break;
                case "Easy":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 0;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 2;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 2;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Hard":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 1;
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "More":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 2;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 0.5f;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                case "End":
                    hideChart();
                    tmGame.Stop();
                    tmCollector.Stop();
                    break;
                case "R":
                    string[] result = args.Split('|');
                    int criterion = Int32.Parse(result[1]);
                    _game_data.result = "放松调适-神秘花园-" + _game_data.levelname[_game_data.level] + "|";
                    int nTick = Int32.Parse(result[2]); 
                    int nMin = nTick / 60; // 분
                    int nSecond = nTick % 60; // 초
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result += nMin.ToString() + "分钟" + nSecond.ToString() + "秒|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    _game_data.result += (criterion == 100) ? "恭喜饱览花园美景！|" : "只看到了" + criterion.ToString() + "%的美景，建议进行放松训练！|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++ )
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(2, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("2#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveView.SetVariable("report", "false");
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void game_borisu(string command, string args) // 사건처리부:보리수(비줄기)
        {
            switch (command)
            {
                case "Start":
                    frmChart.initDeviceReader();
                    _game_data.tickGameInterval = 1500;
                    _game_data.tickCollectInterval = 3000;
                    tmGame.Interval = _game_data.tickGameInterval;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    _game_data.maxTickCount = 3;
                    _game_data.progressValue = 0;
                    _game_data.maxProgressValue = 0;
                    break;
                case "over": // 시작화면에서 탈퇴단추
                    goToMenu();
                    break;
                case "Back1":
                    break;
                case "Easy":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 0;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 5;
                    HrSetting.BeyondRangeCountConst = 2;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 2;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Hard":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 1;
                    HrSetting.NatureCountConst = 5;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "More":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 2;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 5;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 0.5f;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                case "End":
                    hideChart();
                    tmGame.Stop();
                    tmCollector.Stop();
                    break;
                case "R":
                    string[] result = args.Split('|');
                    int criterion = Int32.Parse(result[1]);
                    _game_data.result = "放松调适-菩提树-" + _game_data.levelname[_game_data.level] + "|";
                    int nTick = Int32.Parse(result[2]);
                    int nMin = nTick / 60; // 분
                    int nSecond = nTick % 60; // 초
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result += nMin.ToString() + "分钟" + nSecond.ToString() + "秒|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    _game_data.result += (criterion == 100) ? "恭喜，菩提树枯木逢春了！|" : "菩提树只恢复了" + criterion.ToString() + "成青春，建议进行放松训练！|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(2, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("2#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveView.SetVariable("report", "false");
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void game_doubaobao(string command, string args) // 사건처리부:도우바오바오(애기울음)
        {
            switch (command)
            {
                case "Start":
                    frmChart.initDeviceReader();
                    _game_data.tickGameInterval = 1500;
                    _game_data.tickCollectInterval = 3000;
                    tmGame.Interval = _game_data.tickGameInterval;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    _game_data.maxTickCount = 2;
                    _game_data.progressValue = 0;
                    _game_data.maxProgressValue = 0;
                    break;
                case "over": // 시작화면에서 탈퇴단추
                    goToMenu();
                    break;
                case "Back":
                case "Back1":
                    break;
                case "Easy":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 0;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 2;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 2;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Hard":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 1;
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "More":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 2;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 0.5f;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                case "End":
                    hideChart();
                    tmGame.Stop();
                    tmCollector.Stop();
                    break;
                case "R":
                    string[] result = args.Split('|');
                    int criterion = Int32.Parse(result[1]);
                    _game_data.result = "情景调适-豆宝宝-" + _game_data.levelname[_game_data.level] + "|";
                    int nTick = Int32.Parse(result[2]);
                    int nMin = nTick / 60; // 분
                    int nSecond = nTick % 60; // 초
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result += nMin.ToString() + "分钟" + nSecond.ToString() + "秒|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    _game_data.result += (criterion == 100) ? "恭喜，宝宝已经安然入睡了！|" : "“哄”宝宝的功力只有" + criterion.ToString() + "分，建议进行放松训练！|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(2, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("2#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveView.SetVariable("report", "false");
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void game_searchpearl(string command, string args) // 사건처리부:진주찾기
        {
            switch (command)
            {
                case "Start":
                    frmChart.initDeviceReader();
                    _game_data.tickGameInterval = 1500;
                    _game_data.tickCollectInterval = 3000;
                    tmGame.Interval = _game_data.tickGameInterval;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    _game_data.maxTickCount = 3;
                    break;
                case "over": // 시작화면에서 탈퇴단추
                    goToMenu();
                    break;
                case "Back1":
                    break;
                case "Easy":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 0;
                    HrSetting = new HeartRateSetting();
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 2;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Hard":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "More":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 2;
                    HrSetting = new HeartRateSetting();
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                case "End":
                    hideChart();
                    tmGame.Stop();
                    tmCollector.Stop();
                    break;
                case "R":
                    string[] result = args.Split('|');
                    int criterion = Int32.Parse(result[1]);
                    _game_data.result = "情景调适-采珍珠-" + _game_data.levelname[_game_data.level] + "|";
                    int nTick = Int32.Parse(result[2]);
                    int nMin = nTick / 60; // 분
                    int nSecond = nTick % 60; // 초
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result += nMin.ToString() + "分钟" + nSecond.ToString() + "秒|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    switch (criterion)
                    {
                        case 50:
                            _game_data.result += "宝瓶装满了，恭喜哦！|";
                            break;
                        case 80:
                            _game_data.result += "宝瓶装满了，恭喜哦！|";
                            break;
                        case 100:
                            _game_data.result += "宝瓶装满了，恭喜哦！|";
                            break;
                        default:
                            _game_data.result += "采到" + criterion.ToString() + "颗珍珠，建议进行放松训练！|";
                            break;
                    }
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(2, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("2#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveView.SetVariable("report", "false");
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void game_shooter(string command, string args) // 사건처리부:총쏘기
        {
            switch (command)
            {
                case "Start":
                    frmChart.initDeviceReader();
                    _game_data.tickGameInterval = 1500;
                    _game_data.tickCollectInterval = 3000;
                    tmGame.Interval = _game_data.tickGameInterval;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    _game_data.maxTickCount = 2;
                    _game_data.progressValue = 0;
                    _game_data.maxProgressValue = 2;
                    break;
                case "over": // 시작화면에서 탈퇴단추
                    goToMenu();
                    break;
                case "Back1":
                    break;
                case "Easy":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 0;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 2;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 2;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Hard":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 1;
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "More":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 2;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 0.5f;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                case "End":
                    hideChart();
                    tmGame.Stop();
                    tmCollector.Stop();
                    break;
                case "R":
                    string[] result = args.Split('|');
                    int criterion = Int32.Parse(result[1]);
                    _game_data.result = "情景调适-神射手-" + _game_data.levelname[_game_data.level] + "|";
                    int nTick = Int32.Parse(result[2]);
                    int nMin = nTick / 60; // 분
                    int nSecond = nTick % 60; // 초
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result += nMin.ToString() + "分钟" + nSecond.ToString() + "秒|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    switch (result[0])
                    {
                        case "枪":
                            _game_data.result += (criterion == 25) ? "恭喜，全部射中靶心！|" : criterion.ToString() + "%枪射中靶心，建议进行放松训练！|";
                            break;
                        case "箭":
                            _game_data.result += (criterion == 25) ? "恭喜，全部射中靶心！|" : criterion.ToString() + "箭射中靶心，建议进行放松训练！|";
                            break;
                        case "弹弓":
                            _game_data.result += (criterion == 25) ? "恭喜，全部射中靶心！|" : "打中" + criterion.ToString() + "个，建议进行放松训练！|";
                            break;
                        default:
                            _game_data.result += "|";
                            break;
                    }
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(2, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("2#" + _game_data.result);
                    _game_data.result = "";
                    axShockwaveView.SetVariable("report", "false");
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void game_flight(string command, string args) // 사건처리부:장대쥐고 바줄타기
        {
            switch (command)
            {
                case "chuan":
                    _game_data.isStart = true;
                    break;
                case "Start":
                    frmChart.initDeviceReader();
                    _game_data.tickGameInterval = 1500;
                    _game_data.tickCollectInterval = 3000;
                    tmGame.Interval = _game_data.tickGameInterval;
                    tmCollector.Interval = _game_data.tickCollectInterval;
                    if (_game_data.dataSPO2 == null)
                    {
                        _game_data.dataSPO2 = new List<int>();
                        _game_data.dataHR = new List<int>();
                        _game_data.dataBar = new List<int>();
                    }
                    else
                    {
                        _game_data.dataSPO2.Clear();
                        _game_data.dataHR.Clear();
                        _game_data.dataBar.Clear();
                    }
                    _game_data.avgHR = 0;
                    _game_data.tickCount = 0;
                    _game_data.maxTickCount = 2;
                    _game_data.progressValue = 0;
                    _game_data.maxProgressValue = 1;
                    _game_data.isStart = false;
                    break;
                case "over": // 시작화면에서 탈퇴단추
                    goToMenu();
                    break;
                case "Back1":
                    break;
                case "Easy":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 0;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 2;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 2;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Hard":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 1;
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 1;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "More":
                    if (_device_data.HR == 0) break;
                    _game_data.level = 2;
                    HrSetting = new HeartRateSetting();
                    HrSetting.NatureCountConst = 3;
                    HrSetting.BeyondRangeCountConst = 1;
                    HrSetting.MinRange = 0;
                    HrSetting.MaxRange = 0.5f;
                    axShockwaveFlash.SetVariable("CNum", "1");
                    HrSetting.ClearHrQueue();
                    tmGame.Start();
                    tmCollector.Start();
                    break;
                case "Chart":
                    frmChart.Visible = !frmChart.Visible;
                    break;
                case "Exit":
                case "End":
                    _game_data.isStart = false;
                    hideChart();
                    tmGame.Stop();
                    tmCollector.Stop();
                    break;
                case "R":
                    string[] result = args.Split('|');
                    int criterion = Int32.Parse(result[1]);
                    _game_data.result = "情景调适-高空挑战-" + _game_data.levelname[_game_data.level] + "|";
                    int nTick = Int32.Parse(result[2]);
                    int nMin = nTick / 60; // 분
                    int nSecond = nTick % 60; // 초
                    int totalCount, count;
                    // 평균 맥박, 평균산소포화도, 평균바
                    int avgHr = 0, avgSPO2 = 0, avgBar = 0;
                    totalCount = _game_data.dataHR.Count;
                    for (count = 0; count < totalCount; count++) avgHr += _game_data.dataHR[count];
                    avgHr = (totalCount == 0) ? 0 : avgHr / totalCount;
                    totalCount = _game_data.dataSPO2.Count;
                    for (count = 0; count < totalCount; count++) avgSPO2 += _game_data.dataSPO2[count];
                    avgSPO2 = (totalCount == 0) ? 0 : avgSPO2 / totalCount;
                    totalCount = _game_data.dataBar.Count;
                    for (count = 0; count < totalCount; count++) avgBar += _game_data.dataBar[count];
                    avgBar = (totalCount == 0) ? 0 : avgBar / totalCount;
                    _game_data.result += nMin.ToString() + "分钟" + nSecond.ToString() + "秒|";
                    _game_data.result += avgHr.ToString() + "次/min|";
                    _game_data.result += avgBar.ToString() + "|";
                    _game_data.result += avgSPO2.ToString() + "%|";
                    _game_data.result += (criterion == 100) ? "恭喜完成比赛！|" : "走完全程的" + criterion.ToString() + "%，建议进行放松训练！|";
                    totalCount = _game_data.dataHR.Count;
                    string data = "";
                    for (count = 0; count < totalCount; count++)
                    {
                        data += (count == totalCount - 1) ? _game_data.dataHR[count].ToString() : _game_data.dataHR[count].ToString() + "&";
                    }
                    _game_data.result += data;
                    DateTime now = DateTime.Now;
                    _global.moment = now.ToString();
                    _dbManager.updateUserReport(2, _game_data.result, now);
                    _game_data.dataSPO2.Clear();
                    _game_data.dataHR.Clear();
                    _game_data.dataBar.Clear();
                    _global.moment = _user.moment;
                    showReport("2#" + _game_data.result);
                    _game_data.result = "";
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("command=" + command + ", args=" + args);
                    break;
            }
        }
        private void hideChart()
        {
            if (frmChart != null) frmChart.Hide();
        }
        private void goToMenu()
        {
            if (axShockwaveFlash.Visible)
            {
                axShockwaveFlash.Hide();
                axShockwaveFlash.LoadMovie(0, _path.gui + "empty.swf");
            }
            if (frmChart != null && frmChart.Visible)
            {
                frmChart.Hide();
            }
            if (axShockwaveSound.Visible)
            {
                axShockwaveSound.Hide();
            }
            if (tmGame.Enabled)
            {
                tmGame.Stop();
            }
            if (tmCollector.Enabled)
            {
                tmCollector.Stop();
            }
            if (panelReport.Visible)
            {
                panelReport.Hide();
            }
            axShockwaveView.LoadMovie(0, _path.gui + "menuscene.swf");
        }
        private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (reportType)
            {
                case 0: // 평가
                    goToMenu();
                    break;
                case 1: // 기타
                    panelReport.Hide();
                    if (_game_data.name.Equals("gamerelaxmethod.swf") || _game_data.name.Equals("imagination.swf"))
                    {
                        axShockwaveFlash.SetVariable("report", "true");
                    }
                    else
                    {
                        axShockwaveView.SetVariable("report", "true");
                    }
                    if (axShockwaveSound.Visible)
                    {
                        axShockwaveSound.SetVariable("report", "true");
                    }
                    break;
            }
            report = null;
        }
        private void lnkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            webBrowser.Print();
        }
        private void lnkSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            saveFileDialog.Filter = "word文件(*.doc)|*.doc";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (report.type == 1)
                {
                    report.makePingguWordDocument(saveFileDialog.FileName);
                }
                else
                {
                    report.makeWordDocument(saveFileDialog.FileName);
                }
            }
        }
    }
}
