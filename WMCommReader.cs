using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Linq;

namespace ComReader
{
    public delegate void ComReaderEvent();
    public delegate void PassBackEvent(int curvedLineData, int barData, int pulseData, int oximeterData);
    public class WMComReader : IDisposable
    {
        private static bool bSuccess;
        public static bool BSuccess
        {
            get { return bSuccess; }
            set { bSuccess = value; }
        }
        private Bitmap image;
        private Graphics g;
        private Pen lineP;
        private Pen penBack;
        private float oldY;
        private int step;
        private int backstep;

        public static WMComReader MyComReader
        {
            get
            {
                if (_wmComReader == null) BeginFindComPort();
                return _wmComReader;
            }
        }
        private static WMComReader _wmComReader;
 
        public static void BeginFindComPort()
        {
            string comName = FindComName();
            if (!string.IsNullOrEmpty(comName))
                _wmComReader = new WMComReader(comName);
        }
        private static bool bRead;
        private static void JudgeData(object comPort)
        {
            System.Collections.Generic.List<int> myData = new System.Collections.Generic.List<int>();
            SerialPortExtend myCom = null;
            if (comPort is SerialPortExtend)
            {
                myCom = (SerialPortExtend)comPort;
            }
            bool isBegin = false;
            while (bRead)
            {
                int myB = 0;
                try
                {
                    myB = myCom.ReadByte();
                }
                catch
                {
                }
                isBegin = myB >= 128 ? true : false;
                if (isBegin)
                {
                    if (myData.Count == 5)
                    {
                        BSuccess = true;
                        break;
                    }
                    myData.Clear();
                }
                myData.Add(myB);
            }
        }
        public static string FindComName()
        {
            string comName = string.Empty;
            if (!BSuccess)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");
                if (key != null)
                {
                    string[] arrayKey = key.GetValueNames();

                    foreach (string s in arrayKey)
                    {
                        string queryCom = string.Empty;
                        if (s.Length >= 11)
                        {
                            if (s.Substring(0, 11) == "\\Device\\VCP")
                            {
                                queryCom = key.GetValue(s) as string;
                            }
                        }
                        if (s.Length >= 14)
                        {
                            if (s.Substring(0, 14) == "\\Device\\Serial")
                            {
                                queryCom = key.GetValue(s) as string;
                            }
                        }
                        if (queryCom != null && queryCom.StartsWith("COM") && BSuccess != true)
                        {
                            using (SerialPortExtend _methodPort = new SerialPortExtend())
                            {
                                try
                                {
                                    _methodPort.PortName = queryCom;
                                    _methodPort.ParityReplace = 0;
                                    _methodPort.Parity = System.IO.Ports.Parity.Odd;
                                    _methodPort.StopBits = System.IO.Ports.StopBits.One;
                                    _methodPort.DataBits = 8;
                                    _methodPort.BaudRate = 4800;
                                    _methodPort.Open();
                                    bRead = true;
                                    Thread threadStart = new Thread(new ParameterizedThreadStart(JudgeData));
                                    threadStart.Priority = ThreadPriority.AboveNormal;
                                    threadStart.Start(_methodPort);
                                    Thread.Sleep(500);
                                    bRead = false;
                                    if (bSuccess == true)
                                    {
                                        comName = queryCom;
                                        break;
                                    }
                                    else
                                    {
                                        if (_methodPort.IsOpen) _methodPort.Dispose();
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }

                        }
                    }
                }

            }
            return comName;
        }

        /// <summary>
        /// 端口打开成功
        /// </summary>
        public event ComReaderEvent ComOpenSuccess;

        /// <summary>
        /// 端口打开失败
        /// </summary>
        public event ComReaderEvent ComOpenError;

        /// <summary>
        /// Com口读取数据失败,这样将停止读取数据
        /// </summary>
        public event ComReaderEvent ComReadError;

        /// <summary>
        /// 线程开始接收数据
        /// </summary>
        public event ComReaderEvent RunIsBegin;
        /// <summary>
        /// 线程开始接收数据（第一次）
        /// </summary>
        private bool runIsBegin = true;

        /// <summary>
        /// 正确数据开始
        /// </summary>
        public event ComReaderEvent RightDataIsBegin; private bool rightDataIsBegin = true;
        /// <summary>
        /// 错误数据开始
        /// </summary>
        public event ComReaderEvent WrongDataIsBegin; private bool wrongDataIsBegin = true;

        /// <summary>
        /// 接到每一个数据后引发（理论为60次/秒）
        /// </summary>
        public event PassBackEvent PassBackDataEvent;
        /// <summary>
        /// 接到每一个数据后引发（理论为60次/秒）(包含图片信息)
        /// </summary>
        public event GetMessage2 PassBackDataImageEvent;


        /// <summary>
        /// 处理数据方法
        /// </summary>
        /// <param name="fiveByte">5个int的数据包</param>
        public delegate void GetMessage1(int[] fiveByte);

        /// <summary>
        /// 处理数据方法
        /// </summary>
        /// <param name="curvedLineImage">曲线图片数据</param>
        /// <param name="barData">捧图数据</param>
        /// <param name="pulseData">脉率数据</param>
        /// <param name="oximeterData">血氧数据</param>
        public delegate void GetMessage2(System.Drawing.Bitmap curvedLineImage, int barData, int pulseData, int oximeterData);

        /// <summary>
        /// 端口名称
        /// </summary>
        private static string _comName;

        public static string ComNameReal
        {
            get { return WMComReader._comName; }
            set { WMComReader._comName = value; }
        }

        /// <summary>
        /// 收集数据是否进行中
        /// </summary>
        private static bool isRunning = false;

        public static bool IsRunning
        {
            get { return WMComReader.isRunning; }
            set { WMComReader.isRunning = value; }
        }



        /// <summary>
        /// 调用GetMessage2委托时生成图片的宽度
        /// </summary>
        public int ImageWidth = 500;

        /// <summary>
        /// 调用GetMessage2委托时生成图片的高度
        /// </summary>
        public int ImageHeight = 110;

        /// <summary>
        /// 背影色
        /// </summary>
        public Color BackColor = System.Drawing.Color.FromArgb(222, 222, 222);

        /// <summary>
        /// 端口读取对象
        /// </summary>
        private SerialPortExtend _serialPort = new SerialPortExtend();

        /// <summary>
        /// 获取和设置端口名称
        /// </summary>
        public WMComReader(string comName)
        {
            totalArray = new byte[totalNum];
            _comName = comName;
            image = new Bitmap(340, 54);
            g = Graphics.FromImage(image);
            lineP = new Pen(Color.Green, 2);
            penBack = new Pen(Color.Black, 2);
        }

        Thread _thread;
        public void Start(object delegateObj)
        {
            if (isRunning || _serialPort.IsOpen)
            {
                isRunning = true;
                return;
            }
            //端口丢失时，重新扫描。2010.7.8  韩玉松
            if (_comName == "")
            {
                BSuccess = false;
                _comName = FindComName();
            }
            _serialPort = new SerialPortExtend();
            _serialPort.PortName = _comName;
            _serialPort.ParityReplace  = 0;
            _serialPort.Parity = System.IO.Ports.Parity.Odd;
            _serialPort.StopBits = System.IO.Ports.StopBits.One;
            _serialPort.DataBits = 8;
            _serialPort.BaudRate = 19200;
            _serialPort.Open();
            _thread = new Thread(new ParameterizedThreadStart(this.ReadData));
            isRunning = true;
            _thread.Start(delegateObj);
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="delegateObj"></param>
        private void ReadData(object delegateObj)
        {

            try
            {
                System.Collections.Generic.List<byte> threadReceiveData = new System.Collections.Generic.List<byte>();
                while (isRunning)
                {
                    byte bReceive = 0;
                    bReceive = (byte)_serialPort.ReadByte();
                    threadReceiveData.Add(bReceive);
                    if (threadReceiveData.Count == 10)
                    {
                        DataDealWith(threadReceiveData.ToArray<byte>(), delegateObj);
                        threadReceiveData.Clear();
                    }
                }
            }
            catch
            {
            }
        }
        //数据处理字段
        private byte[] totalArray;
        private const int totalNum = 500;
        private int totalCursor = 0;
        private const int realNum = 5;
        private int realCursor;
        System.Drawing.Bitmap tempImage = null;
        int timeStep = 0;//几次刷新一次 减少刷新的次数可减少CPU的占用
        double yStep = ((double)54) / 128.0;

        private void DataDealWith(byte[] bytearray, object delegateObj)
        {
            for (int i = 0; i < bytearray.Length; i++)
            {
                totalArray[totalCursor] = bytearray[i];
                totalCursor = (totalCursor + 1) % 500;
            }
            byte[] realData = new byte[realNum];
            while ((totalCursor - realCursor + totalNum) % totalNum > realNum)
            {
                if (totalArray[realCursor] > 127)
                {
                    int cursor;
                    for (cursor = 0; cursor < realNum; cursor++)
                    {
                        realData[cursor] = totalArray[realCursor];
                        if (realData[cursor] > 127 && cursor > 0)
                        {
                            break;
                        }
                        else
                        {
                            totalArray[realCursor] = 0;
                            realCursor = (realCursor + 1) % totalNum;
                        }
                    }
                    if (cursor == 5)
                    {
                        int barValue = 0;
                        int xinLv = 0;
                        int xueYang = 0;
                        //皮电
                        if (realData[2] == 0x25 || realData[2] == 0x2A)
                        {
                            return;
                        }
                        //脉搏波形
                        step = (step + 1) % 340;
                        backstep = (step + 5) % 340;
                        g.DrawLine(penBack, backstep, 0, backstep, 54);
                        g.DrawLine(lineP, step, oldY, step + 1, 54 - (float)yStep * realData[1]);
                        oldY = 54 - (float)(yStep * realData[1]);
                        tempImage = (Bitmap)image.Clone();
                        //脉搏跳动
                        if ((realData[2] & 0xF) < 16)
                        {
                            barValue = realData[2] & 0xF;
                        }
                        else
                        {
                            barValue = 0;
                        }
                        //心率，血氧
                        if (realData[4] < 127)
                        {
                            xinLv = (realData[1] & 0x40) * 2 + realData[3] & 0x7F;
                            if ((realData[4] & 0x7F) <= 100)
                            {
                                xueYang = realData[4] & 0x7F;
                            }
                        }
                        if (PassBackDataEvent != null)
                        {
                            try { PassBackDataEvent(realData[1], barValue, xinLv, xueYang); }
                            catch { }
                        }
                        #region 通过事件形式传出图片数据及数字数据
                        try
                        {

                            if (timeStep == 0)
                            {
                                timeStep = 15;
                                if (PassBackDataImageEvent != null)
                                {
                                    PassBackDataImageEvent(tempImage, barValue, xinLv, xueYang);
                                }
                                #region 通过委托传出图片及数字数据
                                if (delegateObj is GetMessage2)
                                {
                                    ((GetMessage2)delegateObj)(tempImage, barValue, xinLv, xueYang);
                                }
                                #endregion
                            }
                            timeStep--;
                        }
                        catch (Exception) { }
                        #endregion
                    }
                }
                else
                {
                    realCursor = (realCursor + 1) % totalNum;
                }
            }
        }
        /// <summary>
        /// 停止采集数据
        /// </summary>
        public void Stop()
        {
            isRunning = false;
        }
        #region IDisposable 成员

        /// <summary>
        /// 释放内存
        /// </summary>
        public void Dispose()
        {
            //try
            //{
                PassBackDataEvent = null;
                RightDataIsBegin = null;
                WrongDataIsBegin = null;
                ComReadError = null;
                Stop();
                if (_serialPort != null)
                {
                    //try
                    //{
                        if (_thread != null) _thread.Join();
                        if (_serialPort != null)
                        {
                            if (_serialPort.IsOpen) _serialPort.Close();
                            _serialPort.Dispose();

                        }
                    //}
                    //catch { }
                }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        #endregion

        /// <summary>
        /// 制作心率图
        /// </summary>
        /// <param name="HeartRates">所发生的心率值组</param>
        /// <param name="TimeInterval">每次心率取值的时间间隔(秒单位)</param>
        /// <param name="TimeLong">总共取值所用时间(秒单位)</param>
        /// <returns>制作完成的图片</returns>
        public Image MarkHeartRateImage(int[] HeartRates, int TimeInterval, int TimeLong)
        {
            Bitmap methodImg = new Bitmap(560, 200);
            Graphics methodG = Graphics.FromImage(methodImg);
            methodG.Clear(Color.White);

            Pen fuzhuP = new Pen(Color.FromArgb(220, 220, 220), 1f);
            fuzhuP.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            Pen blackP = new Pen(Color.Black, 1f);

            Font methodFont = new Font("宋体", 9f, FontStyle.Regular);

            float yUnit = (175f - 5f) / 180f;
            float xUnit = (550f - 30f) / ((float)TimeLong);

            methodG.DrawLine(blackP, 30f, 5f, 30f, 175f);
            methodG.DrawLine(blackP, 30f, 175f, 550f, 175f);

            for (int i = 1; i < 9; i++)
            {
                float myForY = i * 20 * yUnit + 5f;
                methodG.DrawLine(fuzhuP, 30f, myForY, 550f, myForY);
                methodG.DrawLine(blackP, 28f, myForY, 30f, myForY);
                methodG.DrawString(Convert.ToString(180 - i * 20), methodFont, Brushes.Black, 5f, myForY - 5f);
            }
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            for (int i = 1; i < 10; i++)
            {
                float myForX = i * (TimeLong / 10f) * xUnit + 30f;
                methodG.DrawLine(fuzhuP, myForX, 5f, myForX, 175f);
                methodG.DrawString((i * (TimeLong / 10f)).ToString(), methodFont, Brushes.Black, myForX, 176f, sf);
            }

            for (int i = 1; i < HeartRates.Length; i++)
            {
                float y1 = (180 * yUnit - HeartRates[i - 1] * yUnit) + 5f;
                float y2 = (180 * yUnit - HeartRates[i] * yUnit) + 5f;
                float x1 = TimeInterval * i * xUnit + 30f;
                float x2 = TimeInterval * (i + 1) * xUnit + 30f;
                methodG.DrawLine(blackP, x1, y1, x2, y2);
            }

            Bitmap returnImg = new Bitmap(580, 220);


            Graphics reG = Graphics.FromImage(returnImg);
            reG.Clear(Color.White);
            reG.DrawImage(methodImg, 20, 0, 560, 200);
            reG.DrawString("时间(秒)", methodFont, Brushes.Black, 300, 202, sf);
            sf.FormatFlags = StringFormatFlags.DirectionVertical;
            reG.DrawString("心率(次/分钟)", methodFont, Brushes.Black, 4, 80, sf);
            return returnImg;
        }
    }
}
