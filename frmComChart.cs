using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FireSky;
using simri;

namespace ComReader
{
    delegate void  SetPictureStatus();
    public partial class frmComChart : Form
    {
        SetPictureStatus setPictureStatus;
        string strChartStatus="";
        /// <summary>
        /// ComReader对象
        /// </summary>
        public WMComReader myComReader {
            set {
                if (value == null) return;
                _myComReader = value;
                _myComReader.ImageHeight = this.picChart.Height;
                _myComReader.ImageWidth = this.picChart.Width;
                _myComReader.PassBackDataImageEvent += new WMComReader.GetMessage2(_myComReader_PassBackDataImageEvent);
            }
            get {
                if (_myComReader == null){
                    try { _myComReader = WMComReader.MyComReader; }
                    catch (Exception) {  }
                }
                if (_myComReader == null) return null;
                _myComReader.ImageHeight = this.picChart.Height;
                _myComReader.ImageWidth = this.picChart.Width;
                _myComReader.PassBackDataImageEvent += new WMComReader.GetMessage2(_myComReader_PassBackDataImageEvent);
                _myComReader.WrongDataIsBegin += new ComReaderEvent(_myComReader_WrongDataIsBegin);
                _myComReader.ComReadError += new ComReaderEvent(_myComReader_ComReadError);
                _myComReader.RunIsBegin += new ComReaderEvent(_myComReader_RunIsBegin);
                return _myComReader;
            }
        }

        void _myComReader_RunIsBegin()
        {
            if (this.picIcon.InvokeRequired)
            {
                this.picIcon.Invoke(setPictureStatus = delegate()
                {
                    if (strChartStatus != "yellow")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.yellowStatus;
                        strChartStatus = "yellow";
                        this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                    }
                    this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                });
            }
            else
            {
                if (strChartStatus != "yellow")
                {
                    this.picIcon.Image = global::simri.Properties.Resources.yellowStatus;
                    strChartStatus = "yellow";
                    this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                }
                this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
            }
        }

        void _myComReader_ComReadError()
        {
            _myComReader = null;
            if (this.picIcon.InvokeRequired)
            {
                this.picIcon.Invoke(setPictureStatus = delegate()
                {
                    if (strChartStatus != "red")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.redStatus;
                        strChartStatus = "red";
                        this.toolTip.SetToolTip(this.picIcon, "设备异常请检查设备！");
                    }
                    this.toolTip.SetToolTip(this.picIcon, "设备异常请检查设备！");
                });
            }
            else
            {
                if (strChartStatus != "red")
                {
                    this.picIcon.Image = global::simri.Properties.Resources.redStatus;
                    strChartStatus = "red";
                    this.toolTip.SetToolTip(this.picIcon, "设备异常请检查设备！");
                }
                this.toolTip.SetToolTip(this.picIcon, "设备异常请检查设备！");
            }
        }

        void _myComReader_WrongDataIsBegin()
        {
            if (this.picIcon.InvokeRequired)
            {
                this.picIcon.Invoke(setPictureStatus = delegate()
                {
                    if (strChartStatus != "yellow")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.yellowStatus;
                        strChartStatus = "yellow";
                        this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                    }
                    this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                });
            }
            else
            {
                if (strChartStatus != "yellow")
                {
                    this.picIcon.Image = global::simri.Properties.Resources.yellowStatus;
                    strChartStatus = "yellow";
                    this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                }
                this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
            }
        }
        private WMComReader _myComReader = null;

        private WMComReader.GetMessage2 getData;
        void _myComReader_PassBackDataImageEvent(Bitmap curvedLineImage, int barData, int pulseData, int oximeterData)
        {
       
            if (picChart.InvokeRequired){
                this.Invoke(getData, curvedLineImage, barData, pulseData, oximeterData);
            }
            else{
                this.picChart.Image = curvedLineImage;
                this.lblSPO2.Text = oximeterData.ToString() + "%";
                _device_data.SPO2 = oximeterData; // 산소포화도 보관
                if (oximeterData > 100 || oximeterData < 50)
                {
                    if (strChartStatus != "yellow")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.yellowStatus;
                        strChartStatus = "yellow";
                        //this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                    }
                    this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                }
                else
                {
                    if (strChartStatus != "green")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.greenStatus;
                        strChartStatus = "green";
                    }
                    this.toolTip.SetToolTip(this.picIcon, "数据正常！");
                }
                this.lblHR.Text = pulseData.ToString();
                _device_data.HR = pulseData; // 맥박 보관
                if (pulseData > 150 || pulseData < 50)
                {
                    if (strChartStatus != "yellow")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.yellowStatus;
                        strChartStatus = "yellow";
                    }
                    this.toolTip.SetToolTip(this.picIcon, "请检查指夹是否夹好，耐心等待。");
                }
                else
                {
                    if (strChartStatus != "green")
                    {
                        this.picIcon.Image = global::simri.Properties.Resources.greenStatus;
                        strChartStatus = "green";
                    }
                    this.toolTip.SetToolTip(this.picIcon, "数据正常！");
                }
                this.progressBar.Value = barData;
                _device_data.Bar = barData;
            }
        }
        
        public frmComChart()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height - 96);
            getData = new WMComReader.GetMessage2(_myComReader_PassBackDataImageEvent);
            this.toolTip.SetToolTip(this.picIcon, "设备异常请检查设备！");
            trackBar1_Scroll(this.trackBar, null);
            this.trackBar.Focus();
            initDeviceReader();
        }

        public void initDeviceReader()
        {
            if (_myComReader == null)
            {
                if (CP210XDEVICE.getDeviceInfo())
                {
                    _myComReader = new WMComReader(CP210XDEVICE.portName);
                    _myComReader.Start(getData);
                }
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            this.Opacity = (double)tb.Value / 100f ;
        }

        public void stop_comreader()
        {
            if (_myComReader != null)
            {
                try
                {
                    _myComReader.Stop();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            else
            {
                return;
            }
        }

        #region 浮动窗体代码
        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 0x0001;
        const int HTCAPTION = 0x0002;
        const int WM_NCLBUTTONDBLCLK = 0xA3;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if (m.Result == (IntPtr)HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    break;
                case WM_NCLBUTTONDBLCLK:
                    return;
                default:
                    base.WndProc(ref m);
                    break;
            }
          
        }
        #endregion

    }
}
