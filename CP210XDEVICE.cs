using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Microsoft.Win32;

namespace FireSky
{
    public class ComStatus
    {
        public static int baudRate = 0;
        public static Parity parity = Parity.None;
        public static int byteSize = 0;
        public static StopBits stopBit = StopBits.None;
        public static int readTimeout = 0;
        public static byte parityReplace = 0;
        public static Encoding encoding = Encoding.Default;
    }

    public class CP210XDEVICE
    {
        // 귀환값
        public const int CP210x_SUCCESS = 0;
        public const int CP210x_DEVICE_NOT_FOUND = 255;
        public const int CP210x_INVALID_HANDLE = 1;
        public const int CP210x_INVALID_PARAMETER = 2;
        public const int CP210x_DEVICE_IO_FAILED = 3;

        // 기호렬최대길이
        public const int CP210x_MAX_DEVICE_STRLEN = 256;
        public const int CP210x_MAX_PRODUCT_STRLEN = 126;
        public const int CP210x_MAX_SERIAL_STRLEN = 63;
        public const int CP210x_MAX_MAXPOWER = 250;

        // 장치정보
        public static IntPtr hDevice = IntPtr.Zero;
        public static ushort vendorID = 0;
        public static ushort productID = 0;
        public static UInt32 numDevice = 0;
        public static string deviceSN;
        public static string portName = "";

        // 귀환값
        public static int barData = 0;
        public static int SPO2 = 0; // 산소포화도
        public static int HR = 0; // 맥박

        // 임포트된 함수 선언
        private const string CP210xManu = "CP210xManufacturing.dll";
        [DllImport(CP210xManu, EntryPoint = "CP210x_GetNumDevices")]// 장치개수 얻기
        public static extern int getNumDevices([In, Out] ref UInt32 deviceNumb);
        [DllImport(CP210xManu, EntryPoint = "CP210x_Open")]// 장치 열기
        public static extern int open([In] ushort deviceNumb, [In, Out] ref IntPtr deviceHandle);
        [DllImport(CP210xManu, EntryPoint = "CP210x_Close")]// 장치 닫기
        public static extern int close([In] IntPtr deviceHandle);
        [DllImport(CP210xManu, EntryPoint = "CP210x_GetDeviceVid")]// vid얻기
        public static extern int getDeviceVid([In] IntPtr deviceHandle, [In, Out] ref ushort Vid);
        [DllImport(CP210xManu, EntryPoint = "CP210x_GetDevicePid")]// pid얻기
        public static extern int getDevicePid([In] IntPtr deviceHandle, [In, Out] ref ushort Pid);
        [DllImport(CP210xManu, EntryPoint = "CP210x_GetDeviceSerialNumber")]// 계렬번호얻기
        public static extern int getDeviceSerialNumber([In] IntPtr deviceHandle, [In, Out] ref byte Product, [In, Out] ref ushort Length, [In, Out] bool ConvertToASCII);
        public static bool getDeviceInfo()
        {
            bool bResult = false;
            if (getNumDevices(ref numDevice) == CP210x_SUCCESS)
            {
                if (open(0, ref hDevice) == CP210x_SUCCESS)
                {
                    if (getDeviceVid(hDevice, ref vendorID) == CP210x_SUCCESS)
                    {
                        if (getDevicePid(hDevice, ref productID) == CP210x_SUCCESS)
                        {
                            byte[] serialArray = new byte[CP210x_MAX_SERIAL_STRLEN];
                            ushort len = 0;
                            if (getDeviceSerialNumber(hDevice, ref serialArray[0], ref len, true) == CP210x_SUCCESS)
                            {
                                deviceSN = "";
                                for (int i = 0; i < len; i++)
                                {
                                    deviceSN += Convert.ToChar(serialArray[i]);
                                }
                                if (close(hDevice) == CP210x_SUCCESS)
                                {
                                    try
                                    {
                                        RegistryKey localMachine = Registry.LocalMachine;
                                        RegistryKey currentControlSet = localMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\");
                                        RegistryKey enumKey = currentControlSet.OpenSubKey(@"Enum\USB\");
                                        string hexVid = "0000" + Convert.ToString(vendorID, 16).ToLower();
                                        hexVid = hexVid.Substring(hexVid.Length - 4);
                                        string hexPid = "0000" + Convert.ToString(productID, 16).ToLower();
                                        hexPid = hexPid.Substring(hexPid.Length - 4);
                                        char[] strTemp = deviceSN.ToCharArray();
                                        string strSerial = "";
                                        for (int i = 0; i < strTemp.Length; i++)
                                        {
                                            if (strTemp[i] == 32)
                                            {
                                                strSerial += (char)95;
                                            }
                                            else
                                            {
                                                strSerial += strTemp[i];
                                            }
                                        }
                                        string vidpidstringlowv440 = "Vid_" + hexVid + @"&Pid_" + hexPid + @"&Mi_00\" + strSerial + "_00";
                                        string vidpidstringHighV50 = "Vid_" + hexVid + "&Pid_" + hexPid + @"\" + strSerial;
                                        try
                                        {
                                            RegistryKey vidPidKey = enumKey.OpenSubKey(vidpidstringlowv440);
                                            RegistryKey portKey = vidPidKey.OpenSubKey("Device Parameters");
                                            portName = portKey.GetValue("PortName").ToString();
                                            portKey.Close();
                                            vidPidKey.Close();
                                        }
                                        catch (System.Exception ex)
                                        {
                                            Console.WriteLine(ex.Message.ToString());
                                            RegistryKey vidPidKey = enumKey.OpenSubKey(vidpidstringHighV50);
                                            RegistryKey portKey = vidPidKey.OpenSubKey("Device Parameters");
                                            portName = portKey.GetValue("PortName").ToString();
                                            portKey.Close();
                                            vidPidKey.Close();
                                        }
                                        finally
                                        {
                                            enumKey.Close();
                                        }
                                        if (!portName.Equals(""))
                                        {
                                            bResult = true;
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Console.WriteLine(ex.Message.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return bResult;
        }
        public static bool getData()
        {
            // 열기
            SerialPort comport = new SerialPort(portName);
            comport.Open();

            // 청소하기
            comport.DiscardInBuffer();
            comport.DiscardOutBuffer();

            // 포구상태 보관
            ComStatus.baudRate = comport.BaudRate;
            ComStatus.parity = comport.Parity;
            ComStatus.parityReplace = comport.ParityReplace;
            ComStatus.baudRate = comport.DataBits;
            ComStatus.stopBit = comport.StopBits;
            ComStatus.readTimeout = comport.ReadTimeout;
            ComStatus.encoding = comport.Encoding;

            // 포구상태 설정
            comport.BaudRate = 19200;
            comport.Parity = Parity.Odd;
            comport.ParityReplace = 0;
            comport.DataBits = 8;
            comport.StopBits = StopBits.One;
            comport.ReadTimeout = 6000;
            comport.Encoding = System.Text.Encoding.ASCII;

            // 탐색시작
            bool isContinue = true;

            int lenPacket = 5;
            byte[] packet = new byte[lenPacket];
            do
            {
                // 1. 입구점으로 들어오는 파케트 얻기
                // 첫 시작점 탐색
                packet[0] = (byte)comport.ReadByte();
                if (packet[0] <= 127) continue;
                // 나머지 바이트 읽는다
                // 실패하면 읽기를 계속한다
                int i;
                for (i = 1; i < lenPacket; i++)
                {
                    packet[i] = (byte)comport.ReadByte();
                    if (packet[i] > 127) break;
                }
                if (i != lenPacket || packet[2] == 0x25 || packet[2] == 0x2A) continue;
                // 2. 자료처리
                int valSPO2 = 0;
                int valHR = 0;
                int barValue = 0;
                if ((packet[2] & 0xF) < 16)
                {
                    barValue = packet[2] & 0xF;
                }
                else
                {
                    barValue = 0;
                }
                barData = barValue;
                if (packet[4] < 127)
                {
                    valHR = (packet[1] & 0x40) * 2 + packet[3] & 0x7F;
                    if ((packet[4] & 0x7F) <= 100)
                    {
                        valSPO2 = packet[4] & 0x7F;
                    }
                    SPO2 = valSPO2;
                    HR = valHR;
                    isContinue = false;
                    break;
                }
            } while (isContinue);

            // 포구상태 복귀
            comport.BaudRate = ComStatus.baudRate;
            comport.Parity = ComStatus.parity;
            comport.ParityReplace = ComStatus.parityReplace;
            comport.DataBits = ComStatus.baudRate;
            comport.StopBits = ComStatus.stopBit;
            comport.ReadTimeout = ComStatus.readTimeout;
            comport.Encoding = ComStatus.encoding;

            // 포구 닫기

            // 청소하기
            comport.DiscardInBuffer();
            comport.DiscardOutBuffer();
            comport.Close();

            return !isContinue;
        }
    }
}
