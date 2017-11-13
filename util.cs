using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Microsoft.Office.Interop.Word;

namespace simri
{
    public class _global
    {
        public static string tmp = "";
        public static string moment = "";
        public static int current = 0;
        public static int count = 0;
    }
    public class _device_data
    {
        public static int SPO2;
        public static int HR;
        public static int Bar;
    }
    public class _game_data
    {
        public static string[] levelname = {"初级", "中级", "高级"};
        public static string name = "";
        public static int level = 0; // 현재 게임수준(0:초급, 1:중급, 2:고급)
        public static int tickGameInterval = 1500; // 미리초단위
        public static int tickCollectInterval = 3000;  // 미리초단위
        public static float avgHR = 0; // maxTickCount번 장치값을 루적한 평균값, 처리후 령으로 초기화
        public static int tickCount = 0; // 장치를 읽기 위하여 타이머가 똑딱인 수, 처리후 령으로 초기화
        public static int maxTickCount = 3; // 장치를 읽기 위하여 타이머가 똑딱여야할 최대값, 처리후 령으로 초기화
        public static string result = ""; // 게임결과
        public static int progressValue = 0;
        public static int maxProgressValue = 0;
        public static List<int> dataSPO2 = null; // 게임진행시 루적되는 산소포화도
        public static List<int> dataHR = null; // 게임진행시 루적되는 맥박
        public static List<int> dataBar = null; // 게임진행시 루적되는 바값
        public static bool isStart = false;
    }
    public class _record
    {
        public static string username = "";
        public static string password = "";
        public static string gender = "";
        public static int birthYear = 0;
        public static int birthMonth = 0;
        public static int birthDay = 0;
        public static string job = "";
        public static void empty()
        {
            username = "";
            password = "";
            gender = "male";
            birthYear = DateTime.Now.Year;
            birthMonth = DateTime.Now.Month;
            birthDay = DateTime.Now.Day;
            job = "";
        }
        public static void clone(ref _row row)
        {
            row.username = username;
            row.password = password;
            row.gender = gender;
            row.birthYear = birthYear;
            row.birthMonth = birthMonth;
            row.birthDay = birthDay;
            row.job = job;
        }
        public static bool isEqual(ref _row row, ref int[] fields)
        {
            fields[0] = (username.Equals(row.username))?0:1;
            fields[1] = (password.Equals(row.password))?0:1;
            fields[2] = (gender.Equals(row.gender))?0:1;
            fields[3] = (birthYear == row.birthYear)?0:1;
            fields[4] = (birthMonth == row.birthMonth)?0:1;
            fields[5] = (birthDay == row.birthDay)?0:1;
            fields[6] = (job.Equals(row.job))?0:1;
            bool bResult = true;
            for (int i = 0; i < 7; i++ )
            {
                if (fields[i] == 1)
                {
                    bResult = false;
                    break;
                }
            }
            return bResult;
        }
    }
    public class _row
    {
        public string username = "";
        public string password = "";
        public string gender = "";
        public int birthYear = 0;
        public int birthMonth = 0;
        public int birthDay = 0;
        public string job = "";
        public _row()
        {
            username = "";
            password = "";
            gender = "male";
            birthYear = DateTime.Now.Year;
            birthMonth = DateTime.Now.Month;
            birthDay = DateTime.Now.Day;
            job = "";
        }
    }
    public class _folder
    {
        public int no = 0;
        public string name = "";
        public string comment = "";
        public _folder()
        {
            no = 0;
            name = "";
            comment = "";
        }
    }
    public class _recordfolder
    {
        public static string name = "";
        public static string comment = "";
        public static void empty()
        {
            name = "";
            comment = "";
        }
        public static void clone(ref _folder folder)
        {
            folder.name = name;
            folder.comment = comment;
        }
        public static bool isEqual(ref _folder folder)
        {
            if (name.Equals(folder.name) && comment.Equals(folder.comment))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class _file
    {
        public string name = "";
        public string title = "";
        public _file()
        {
            name = "";
            title = "";
        }
    }
    public class _recordFlash
    {
        public static string title = "";
        public static string music = "";
        public static void empty()
        {
            title = "";
            music = "";
        }
        public static void clone(ref _flash flash)
        {
            flash.title = title;
            flash.music = music;
        }
        public static bool isEqual(ref _flash flash)
        {
            if (title.Equals(flash.title) && music.Equals(flash.music))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
    public class _flash
    {
        public string title = "";
        public string music = "";
        public _flash()
        {
            title = "";
            music = "";
        }
    }
    public class _recordImage
    {
        public static string filename = "";
        public static float life = 0;
        public static void empty()
        {
            filename = "";
            life = 0;
        }
        public static void clone(ref _image image)
        {
            image.filename = filename;
            image.life = life;
        }
        public static bool isEqual(ref _image image)
        {
            if (filename.Equals(image.filename) && life == image.life)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class _image
    {
        public int no = 0;
        public string filename = "";
        public float life = 0;
        public _image()
        {
            no = 0;
            filename = "";
            life = 0;
        }
    }
    public class _recordFile
    {
        public static string name = "";
        public static string title = "";
        public static void empty()
        {
            name = "";
            title = "";
        }
        public static void clone(ref _file file)
        {
            file.name = name;
            file.title = title;
        }
        public static bool isEqual(ref _file file)
        {
            if (name.Equals(file.name) && title.Equals(file.title))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class _path
    {
        public static string root = Path.GetFullPath(System.Windows.Forms.Application.StartupPath);
        public static string report = root + @"\report\";
        public static string temp = root + @"\temp\";
        public static string music = root + @"\music\";
        public static string js = root + @"\js\";
        public static string resource = root + @"\resources\";
        public static string gui = resource + @"\gui\";
        public static string db = resource + @"\db\";
        public static string pinggu = resource + @"\pinggu\";
        public static string swf = resource + @"\swf\";
        public static string imagination = resource + @"\flash\imagination\";
        public static string relexmethod = resource + @"\flash\relexmethod\";
        public static string image = resource + @"\images\";
        public static string icons = resource + @"\icons\";
    }
    public class _user_info
    {
        public static string name = "";
        public static string gender = "";
        public static string birthday = "";
        public static string job = "";
    }
    public class _user
    {
        public static string name = "";
        public static string password = "";
        public static string moment = "";
    }
    public class _user_log
    {
        public string username;
        public string moment;
        public string report;
        public int type;
        public _user_log()
        {
            type = 1;
            username = "";
            moment = "";
            report = "";
        }
    }
    public class _dbManager
    {
        private static OleDbConnection con;
        private static string conString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" + _path.db + "simri.mdb;" + "Persist Security Info=False;Jet OLEDB:Database Password=ckdwhdmltls";
        private static string sql;
        private static OleDbCommand dbcommand;
        private static OleDbDataReader datareader;
        public static bool registUser() // _record정보를 가지고 리용자등록
        {
            if (!open()) return false;
            bool bResult = false;
            sql = "INSERT INTO [user] VALUES('" + _record.username + "', '" + _record.password + "', '" + _record.gender + "', " + _record.birthYear + ", " + Convert.ToByte(_record.birthMonth) + ", " + Convert.ToByte(_record.birthDay) + ", '" + _record.job + "');";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
                bResult = true;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                bResult = false;
            }
            finally
            {
                con.Close();
            }
            return bResult;
        }
        public static bool updateUserReport(int type, string data, DateTime now)
        {
            // 자료기지 열기大
            if (!open()) return false;
            sql = "INSERT INTO [log] (username, moment, report, type) VALUES('" + _user.name + "', '" + now.ToString() + "', '" + data + "', " + type.ToString() + ");";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            return true;
        }
        public static bool updateRecord(ref int[] iFields, string strValues, string strValuesOrigin, string username)
        {
            if (!open()) return false;
            string[] arryFields = { "name", "password", "gender", "birthyear", "birthmonth", "birthday", "job" };
            char splitter = ',';
            string[] arryValues = strValues.Split(splitter);
            string[] arryValuesOrigin = strValuesOrigin.Split(splitter);
            int nField = arryFields.Length;
            bool bResult = false;
            int index = 0;
            for (int i = 0; i < nField; i++ )
            {
                if (iFields[i] == 1)
                {
                    sql = "UPDATE [user] SET user." + arryFields[i] + "=" + arryValues[index] + " WHERE (user.name)='" + username + "';";
                    index++;
                    try
                    {
                        dbcommand = new OleDbCommand(sql, con);
                        dbcommand.ExecuteNonQuery();
                        bResult = true;
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.Message);
                        bResult = false;
                        break;
                    }
                }
                
            }
            con.Close();
            return bResult;
        }
        public static bool isCorrectAdmin(string password)
        {
            // 자료기지 열기
            if (!open()) return false;
            // 리용자이름 검색
            sql = "SELECT user.name FROM [user] WHERE (((user.name)='admin') AND ((user.password)='" + password + "'));";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool updateAdmin(string password)
        {
            if (!open()) return false;
            sql = "UPDATE [user] SET user.password='" + password + "' WHERE (user.name)='admin';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            return true;
        }
        public static bool isCoreect() // _user정보(이름, 암호)를 가지고 정확한 리용자인가를 확인
        {
            // 자료기지 열기
            if (!open()) return false;

            // 리용자이름 검색
            sql = "SELECT user.name FROM [user] WHERE (((user.name)='" + _user.name + "') AND ((user.password)='" + _user.password +"'));";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool deleteUser(string username)
        {
            // 자료기지 열기
            if (!open()) return false;
            // 리용자이름 검색
            sql = "DELETE * FROM [user] WHERE (user.name)='" + username + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            con.Close();
            return false;
        }
        public static bool isExist(string username) // 리용자이름이 존재하는가를 본다
        {
            // 자료기지 열기
            if (!open()) return false;
            
            // 리용자이름 검색
            sql = "SELECT user.name FROM [user] WHERE (user.name)='" + username + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool open()
        {
            con = new OleDbConnection(conString);
            try
            {
                con.Open();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return true;
        }
        public static void getUserInfo()
        {
            // 자료기지 열기
            if (!open()) return;

            // 리용자이름 검색
            sql = "SELECT * FROM [user] WHERE (user.name)='" + _user.name + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            datareader.Read();
            _user_info.name = datareader["name"].ToString();
            _user_info.gender = datareader["gender"].ToString();
            _user_info.birthday = datareader["birthyear"].ToString() + "年 " + datareader["birthmonth"].ToString() + "月 " + datareader["birthday"].ToString() + "日";
            _user_info.job = datareader["job"].ToString();
            con.Close();
        }
        public static string getUserReport(string moment)
        {
            // 자료기지 열기
            if (!open()) return "";

            // 리용자이름 검색
            sql = "SELECT log.type, log.report FROM [log] WHERE (log.username)='" + _user.name + "' AND (log.moment)='" + moment.ToString() + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            datareader.Read();
            string pinggu = datareader["type"].ToString() + "#" + datareader["report"].ToString();
            con.Close();
            return pinggu;
        }
        public static void getUserInfo(string username) // 관리자페지에서 리용자이름을 가지고 레코드를 얻을 때
        {
            // 자료기지 열기
            if (!open()) return;

            // 리용자이름 검색
            sql = "SELECT * FROM [user] WHERE (user.name)='" + username + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            datareader.Read();
            _record.username = datareader["name"].ToString();
            _record.password = datareader["password"].ToString();
            _record.gender = datareader["gender"].ToString();
            _record.birthYear = Int32.Parse(datareader["birthyear"].ToString());
            _record.birthMonth = Int32.Parse(datareader["birthmonth"].ToString());
            _record.birthDay = Int32.Parse(datareader["birthday"].ToString());
            _record.job = datareader["job"].ToString();
            con.Close();
        }
        public static void recordLogin()
        {
            if (!open()) return;
            DateTime now = DateTime.Now;
            _user.moment = now.ToString();
            sql = "INSERT INTO [log] (username, moment) VALUES('" + _user.name + "', '" + now.ToString() + "');";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("无法执行的SQL语句。");
                Console.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public static int searchUserList(string strString, string strNumber, ref List<_row> userList)
        {
            // 자료기지 열기
            if (!open()) return 0;
            // 리용자이름 검색
            char splitter = ',';
            string[] arryString = strString.Split(splitter);
            int nString = arryString.Length;
            if (nString == 1 && arryString[0].Length == 0)
            {
                nString = 0;
            }
            string[] arryNumber = strNumber.Split(splitter);
            int nNumber = arryNumber.Length;
            if (nNumber == 1 && arryNumber[0].Length == 0)
            {
                nNumber = 0;
            }
            // SQL부문품 만들기
            #region 
            // 이름
            string strName = (nString == 0)?"":"(";
            if (nString != 0)
            {
                for (int i = 0; i < nString; i++)
                {
                    if (i == nString - 1)
                    {
                        strName += "(user.name) LIKE '%" + arryString[i] + "%'";
                    }
                    else
                    {
                        strName += "(user.name) LIKE '%" + arryString[i] + "%' Or ";
                    }
                }
                strName += ")";
            }
            // 성별
            string strGender = (nString == 0)?"":"(";
            if (nString != 0)
            {
                for (int i = 0; i < nString; i++)
                {
                    if (i == nString - 1)
                    {
                        strGender += "(user.gender) LIKE '%" + arryString[i] + "%'";
                    }
                    else
                    {
                        strGender += "(user.gender) LIKE '%" + arryString[i] + "%' Or ";
                    }
                }
                strGender += ")";
            }
            // 직업
            string strJob = (nString == 0)?"":"(";
            if (nString != 0)
            {
                for (int i = 0; i < nString; i++)
                {
                    if (i == nString - 1)
                    {
                        strJob += "(user.job) LIKE '%" + arryString[i] + "%'";
                    }
                    else
                    {
                        strJob += "(user.job) LIKE '%" + arryString[i] + "%' Or ";
                    }
                }
                strJob += ")";
            }
            //출년
            string strYear = (nNumber == 0)?"":"(";
            if (nNumber != 0)
            {
                for (int i = 0; i < nNumber; i++)
                {
                    if (i == nNumber - 1)
                    {
                        strYear += "(user.birthyear) LIKE '%" + arryNumber[i] + "%'";
                    }
                    else
                    {
                        strYear += "(user.birthyear) LIKE '%" + arryNumber[i] + "%' Or ";
                    }
                }
                strYear += ")";
            }
            //출월
            string strMonth = (nNumber == 0)?"":"(";
            if (nNumber != 0)
            {
                for (int i = 0; i < nNumber; i++)
                {
                    if (i == nNumber - 1)
                    {
                        strMonth += "(user.birthmonth) LIKE '%" + arryNumber[i] + "%'";
                    }
                    else
                    {
                        strMonth += "(user.birthmonth) LIKE '%" + arryNumber[i] + "%' Or ";
                    }
                }
                strMonth += ")";
            }
            // 출일
            string strDay = (nNumber == 0)?"":"(";
            if (nNumber != 0)
            {
                for (int i = 0; i < nNumber; i++)
                {
                    if (i == nNumber - 1)
                    {
                        strDay += "(user.birthday) LIKE '%" + arryNumber[i] + "%'";
                    }
                    else
                    {
                        strDay += "(user.birthday) LIKE '%" + arryNumber[i] + "%' Or ";
                    }
                }
                strDay += ")";
            }
            #endregion
            // SQL만들기
            #region 
            sql = "SELECT * FROM [user] WHERE (";
            if (nString != 0 && nNumber != 0)
            {
                sql += strName + " Or " + strGender + " Or " + strJob + " Or " + strYear + " Or " + strMonth + " Or " + strDay;
            } else if (nNumber != 0)
            {
                sql += strYear + " Or " + strMonth + " Or " + strDay;
            }
            else if (nString != 0)
            {
                sql += strName + " Or " + strGender + " Or " + strJob;
            }
            sql += ");";
            #endregion
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _row item = new _row();
                item.username = datareader["name"].ToString();
                item.password = datareader["password"].ToString();
                item.gender = datareader["gender"].ToString();
                item.birthYear = Int32.Parse(datareader["birthyear"].ToString());
                item.birthMonth = Int32.Parse(datareader["birthmonth"].ToString());
                item.birthDay = Int32.Parse(datareader["birthday"].ToString());
                item.job = datareader["job"].ToString();
                userList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static bool isExistFile(int type, int folderno, string filename)
        {
            // 자료기지 열기
            if (!open()) return false;

            // 리용자이름 검색
            sql = "SELECT file.name FROM [file] WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + filename + "' AND (file.folderno)=" + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool isExistFile(string filename)
        {
            // 자료기지 열기
            if (!open()) return false;

            // 리용자이름 검색
            sql = "SELECT file.name FROM [file] WHERE (file.name)='" + filename + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool isExistTitle(int type, int folderno, string title)
        {
            // 자료기지 열기
            if (!open()) return false;

            // 리용자이름 검색
            sql = "SELECT file.title FROM [file] WHERE (file.type)=" + type.ToString() + " AND (file.title)='" + title + "' AND (file.folderno)=" + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool isExistFolder(int type, string foldername)
        {
            // 자료기지 열기
            if (!open()) return false;

            // 리용자이름 검색
            sql = "SELECT folder.name FROM [folder] WHERE (folder.type)=" + type.ToString() + " AND (folder.name)='" + foldername + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool isExistComment(int type, string comment)
        {
            // 자료기지 열기
            if (!open()) return false;

            // 리용자이름 검색
            sql = "SELECT folder.comment FROM [folder] WHERE (folder.type)=" + type.ToString() + " AND (folder.comment)='" + comment + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            bool bResult = datareader.HasRows;
            con.Close();
            return bResult;
        }
        public static bool deleteFile(int type, int folderno, string filename, int totalCount, int curIndex)
        {
            // 자료기지 열기
            if (!open()) return false;
            // 리용자이름 검색
            sql = "DELETE * FROM [file] WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + filename + "' AND (file.folderno)=" + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            if (curIndex < totalCount)
            {
                for (int i = curIndex + 1; i <= totalCount; i++)
                {
                    sql = "UPDATE [file] SET file.no=" + (i - 1).ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.no)=" + i.ToString() + ";";
                    try
                    {
                        dbcommand = new OleDbCommand(sql, con);
                        dbcommand.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.Message);
                        con.Close();
                        return false;
                    }
                }
            }
            con.Close();
            return true;
        }
        public static bool deleteFolder(int type, string foldername, int totalCount, int curIndex)
        {
            // 자료기지 열기
            if (!open()) return false;
            // 폴더 지우기
            sql = "DELETE * FROM [folder] WHERE (folder.type)=" + type.ToString() + " AND (folder.name)='" + foldername + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            // 새끼 화일 지우기
            sql = "DELETE * FROM [file] WHERE (file.type)=" + type.ToString() + " AND (file.folderno)=" + curIndex.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            if (curIndex < totalCount)
            {
                for (int i = curIndex + 1; i <= totalCount; i++ )
                {
                    sql = "UPDATE [folder] SET folder.no=" + (i - 1).ToString() + " WHERE (folder.type)=" + type.ToString() + " AND (folder.no)=" + i.ToString() + ";";
                    try
                    {
                        dbcommand = new OleDbCommand(sql, con);
                        dbcommand.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.Message);
                        con.Close();
                        return false;
                    }
                }
            }
            con.Close();
            return true;
        }
        public static bool insertFile(int totalcount, int type, int folderno, int no, string name, string title)
        {
            if (!open()) return false;
            if (totalcount >= no)
            {
                for (int i = totalcount; i >= no; i--)
                {
                    sql = "UPDATE [file] SET file.no=" + (i + 1).ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.no)=" + i.ToString() + " AND (file.folderno)=" + folderno.ToString() + ";";
                    try
                    {
                        dbcommand = new OleDbCommand(sql, con);
                        dbcommand.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.Message);
                        con.Close();
                        return false;
                    }
                }
            }
            sql = "INSERT INTO [file] VALUES("+ type.ToString() + ", " + folderno.ToString() + ", " + no.ToString() + ", '" + name + "', '" + title + "');";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }
        public static bool insertFolder(int type, int totalcount, int no, string name, string comment)
        {
            if (!open()) return false;
            if (totalcount >= no)
            {
                for (int i = totalcount; i >= no; i-- )
                {
                    sql = "UPDATE [folder] SET folder.no=" + (i + 1).ToString() + " WHERE (folder.type)=" + type.ToString() + " AND (folder.no)=" + i.ToString() + ";";
                    try
                    {
                        dbcommand = new OleDbCommand(sql, con);
                        dbcommand.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.Message);
                        con.Close();
                        return false;
                    }
                }
            }
            sql = "INSERT INTO [folder] VALUES(" + type.ToString() + ", " + no.ToString() + ", '" + name + "', '" + comment + "');";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }
        public static bool replaceFileNo(int type, int folderno, int index, string name, int withIndex, string withName, int totalCount)
        {

            if (!open()) return false;
            sql = "UPDATE [file] SET file.no=" + (totalCount + 1).ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + withName + "' AND (file.folderno) = " + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            sql = "UPDATE [file] SET file.no=" + withIndex.ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + name + "' AND (file.folderno) = " + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            sql = "UPDATE [file] SET file.no=" + index.ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + withName + "' AND (file.folderno) = " + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }
        public static bool replaceFolderNo(int type, int index, string name, int withIndex, string withName, int totalCount)
        {

            if (!open()) return false;
            // 림시로 withIndex의 새끼들 모두 함께(한계수자 10000)
            sql = "UPDATE [file] SET file.folderno=10000 WHERE (file.type)=" + type.ToString() + " AND (file.folderno)=" + withIndex.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            // index
            sql = "UPDATE [folder] SET folder.no=" + withIndex.ToString() + " WHERE (folder.type)=" + type.ToString() + " AND (folder.name)='" + name + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            // index의 새끼화일들도 변경
            sql = "UPDATE [file] SET file.folderno=" + withIndex.ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.folderno)=" + index.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            // withIndex
            sql = "UPDATE [folder] SET folder.no=" + index.ToString() + " WHERE (folder.type)=" + type.ToString() + " AND (folder.name)='" + withName + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            // withIndex의 새끼화일들도 변경
            sql = "UPDATE [file] SET file.folderno=" + index.ToString() + " WHERE (file.type)=" + type.ToString() + " AND (file.folderno)=10000;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }
        public static bool updateFileRecord(int type, int folderno, ref _file fileOrig)
        {
            if (!open()) return false;
            if (!fileOrig.name.Equals(_recordFile.name)) // 이름업데이트
            {
                sql = "UPDATE [file] SET file.name='" + _recordFile.name + "' WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + fileOrig.name + "' AND (file.folderno)=" + folderno.ToString() + ";";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return false;
                }
            }
            if (!fileOrig.title.Equals(_recordFile.title)) // 제목 업데이트
            {
                sql = "UPDATE [file] SET file.title='" + _recordFile.title + "' WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + _recordFile.name + "' AND (file.folderno)=" + folderno.ToString() + ";";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return false;
                }
            }
            con.Close();
            return true;
        }
        public static bool updateFolderRecord(int type, ref _folder folderOrig)
        {
            if (!open()) return false;
            if (!folderOrig.name.Equals(_recordfolder.name)) // 이름업데이트
            {
                sql = "UPDATE [folder] SET folder.name='" + _recordfolder.name + "' WHERE (folder.type)=" + type.ToString() + " AND (folder.name)='" + folderOrig.name + "';";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return false;
                }
            }
            if (!folderOrig.comment.Equals(_recordfolder.comment)) // 콤멘트 업데이트
            {
                sql = "UPDATE [folder] SET folder.comment='" + _recordfolder.comment + "' WHERE (folder.name)='" + _recordfolder.name + "';";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return false;
                }
            }
            con.Close();
            return true;
        }
        public static void getFileInfo(int type, int folderno, string filename)
        {
            // 자료기지 열기
            if (!open()) return;
            // 리용자이름 검색
            sql = "SELECT * FROM [file] WHERE (file.type)=" + type.ToString() + " AND (file.name)='" + filename + "' AND (file.folderno)=" + folderno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            datareader.Read();
            _recordFile.name = datareader["name"].ToString();
            _recordFile.title = datareader["title"].ToString();
            con.Close();
        }
        public static void getFolderInfo(int type, string foldername)
        {
            // 자료기지 열기
            if (!open()) return;

            // 리용자이름 검색
            sql = "SELECT * FROM [folder] WHERE (folder.type)=" + type.ToString() + " AND (folder.name)='" + foldername + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            datareader.Read();
            _recordfolder.name = datareader["name"].ToString();
            _recordfolder.comment = datareader["comment"].ToString();
            con.Close();
        }
        public static int getFolderLeafs(int type, int folderno, ref List<_file> fileList)
        {
            // 자료기지 열기
            if (!open()) return 0;
            // 리용자이름 검색
            sql = "SELECT * FROM [file] WHERE (file.type)=" + type.ToString() + " AND (file.folderno)= " + folderno.ToString() + " ORDER BY file.no;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _file item = new _file();
                item.title = datareader["title"].ToString();
                item.name = datareader["name"].ToString();
                fileList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static int getFolderList(int type, ref List<_folder> folderList)
        {
            // 자료기지 열기
            if (!open()) return 0;
            // 리용자이름 검색
            sql = "SELECT * FROM [folder] WHERE (folder.type)=" + type.ToString() + " ORDER BY folder.no;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _folder item = new _folder();
                item.no = Int32.Parse(datareader["no"].ToString());
                item.name = datareader["name"].ToString();
                item.comment = datareader["comment"].ToString();
                folderList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static int getUserList(ref List<_row> userList)
        {
            // 자료기지 열기
            if (!open()) return 0;

            // 리용자이름 검색
            sql = "SELECT * FROM [user] WHERE Not (user.name)='admin';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _row item = new _row();
                item.username = datareader["name"].ToString();
                item.password = datareader["password"].ToString();
                item.gender = datareader["gender"].ToString();
                item.birthYear = Int32.Parse(datareader["birthyear"].ToString());
                item.birthMonth = Int32.Parse(datareader["birthmonth"].ToString());
                item.birthDay = Int32.Parse(datareader["birthday"].ToString());
                item.job = datareader["job"].ToString();
                userList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static int getUserLogList(ref List<_user_log> logList)
        {
            // 자료기지 열기
            if (!open()) return 0;

            // 리용자이름 검색
            sql = "SELECT * FROM [log] ORDER BY log.username, log.moment;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _user_log item = new _user_log();
                string strType = datareader["type"].ToString();
                item.type = (strType.Equals("")) ? 0 : Int32.Parse(strType);
                item.username = datareader["username"].ToString();
                item.moment = datareader["moment"].ToString();
                item.report = datareader["report"].ToString();
                logList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static int getUserLog(ref List<_user_log> log)
        {
            // 자료기지 열기
            if (!open()) return 0;

            // 리용자이름 검색
            sql = "SELECT log.moment, log.report, log.type FROM [log] WHERE (log.username)='" + _user.name + "' AND (log.report) Is Not Null  ORDER BY log.moment DESC;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _user_log item = new _user_log();
                item.moment = datareader["moment"].ToString();
                item.report = datareader["report"].ToString();
                item.type = Int32.Parse(datareader["type"].ToString());
                log.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static bool deleteUserLog(string username, string moment)
        {
            // 자료기지 열기
            if (!open()) return false;
            sql = "DELETE * FROM [log] WHERE (log.username)='" + username + "' AND (log.moment)='" + moment + "';";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }
        public static int getResult(int type, float avg, ref string comment, ref string recommend) // 스트레스평가에서 쓰인다.
        {
            int iResult = 0;
            // 자료기지 열기
            if (!open()) return iResult;

            // 리용자이름 검색
            sql = "SELECT stress.grade, stress.comment, stress.recommend FROM stress WHERE (((stress.type)=" + type.ToString() + ") AND ((stress.high)>=" + avg.ToString() + ") AND ((stress.low)<" + avg.ToString() + "));";
            // 리용자이름 검색
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            datareader.Read();
            comment = datareader["comment"].ToString();
            recommend = datareader["recommend"].ToString();
            iResult = Int32.Parse(datareader["grade"].ToString());
            con.Close();
            return iResult;
        }
        public static void getFlashInfo(int flashType, int flashNo)
        {
            // 자료기지 열기
            if (!open()) return;
            // 리용자이름 검색
            sql = "SELECT flash.title, flash.music FROM [flash] WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + flashNo.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            datareader.Read();
            _recordFlash.title = datareader["title"].ToString();
            _recordFlash.music = datareader["music"].ToString();
            con.Close();
        }
        public static int getImageList(int flashType, int flashNo, ref List<_image> imageList)
        {
            // 자료기지 열기
            if (!open()) return 0;
            sql = "SELECT * FROM [image] WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + flashNo.ToString() + " ORDER BY image.no;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _image item = new _image();
                item.life = float.Parse(datareader["life"].ToString());
                item.filename = datareader["filename"].ToString();
                imageList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static int getFlashList(int flashType, ref List<_flash> flashList)
        {
            // 자료기지 열기
            if (!open()) return 0;
            // 리용자이름 검색
            sql = "SELECT * FROM [flash] WHERE (flash.type)=" + flashType.ToString() + " ORDER BY flash.no;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            int count = 0;
            while (datareader.Read())
            {
                _flash item = new _flash();
                item.title = datareader["title"].ToString();
                item.music = datareader["music"].ToString();
                flashList.Add(item);
                count++;
            }
            con.Close();
            return count;
        }
        public static void insertFlash(int nCountFlash, int flashType, int flashNo, string strTitle, string strSoundfile, ref List<_image> imageList)
        {
            // 자료기지 열기
            if (!open()) return;
            // 삽입할 자리 만들기
            for (int no = nCountFlash; no > flashNo; no--)
            {
                // image테블안에서 flasytype== flashType 이고 flashno == i 인 모든 레코드에 관하여 flashno = i + 1
                sql = "UPDATE [image] SET image.flashno=" + (no + 1).ToString() + " WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + no.ToString() + ";";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return;
                }
                // flash테블안에서 type == flashType 이고 no == i인 모든 레코드에 관하여 no = i + 1
                sql = "UPDATE [flash] SET flash.no=" + (no + 1).ToString() + " WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + no.ToString() + ";";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return;
                }
            }
            // 삽입시작
            // flash테블안에 (flashType, flashNo + 1, strTitle, strSoundFile) 삽입
            string strFilename = Path.GetFileName(strSoundfile).Replace('\'', '`');// 파일이름속에 "'"이 들어있으면 "`"으로 교체
            sql = "INSERT INTO [flash] VALUES(" + flashType.ToString() + ", " + (flashNo + 1).ToString() + ", '" + strTitle + "', '" + strFilename + "');";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // 음성파일 복사
            switch (flashType)
            {
                case 1: // 상상완화
                    File.Copy(strSoundfile, _path.imagination + strFilename, true);
                    break;
                case 2: // 완화방법
                    File.Copy(strSoundfile, _path.relexmethod + strFilename, true);
                    break;
                default:
                    break;
            }
            // image테블안에 (flashType, flashNo + 1, imageList[i].no, imageList[i].filename, imageList[i].life)삽입, 순환시에 파일도 같이 복사한다.
            int count = imageList.Count;
            for (int i = 0; i < count; i++ )
            {
                strFilename = Path.GetFileName(imageList[i].filename).Replace('\'', '`');// 파일이름속에 "'"이 들어있으면 "`"으로 교체
                sql = "INSERT INTO [image] VALUES(" + flashType.ToString() + ", " + (flashNo + 1).ToString() + ", " + imageList[i].no.ToString() + ", '" + strFilename + "', " + imageList[i].life.ToString() + ");";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return;
                }
                switch (flashType)
                {
                    case 1: // 상상완화
                        File.Copy(imageList[i].filename, _path.imagination + strFilename, true);
                        break;
                    case 2: // 완화방법
                        File.Copy(imageList[i].filename, _path.relexmethod + strFilename, true);
                        break;
                    default:
                        break;
                }
            }
            con.Close();
        }
        public static void changeFlash(int flashType, int flashNo, string strTitle, string strSoundfile, ref List<_image> imageList)
        {
            // 자료기지 열기
            if (!open()) return;
            // flash표안에서 type == flashType 이고 no == flashNo인 레코드(유일)의 title과 music마당을 strTitle, strSoundfile으로 업데이트, 음성파일도 같이 복사
            sql = "UPDATE [flash] SET flash.title='" + strTitle + "' WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + flashNo.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            string strFilename = Path.GetFileName(strSoundfile).Replace('\'', '`');// 파일이름속에 "'"이 들어있으면 "`"으로 교체
            sql = "UPDATE [flash] SET flash.music='" + strFilename + "' WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + flashNo.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // 음성파일 복사
            bool bIsDiffer = (strFilename.Equals(strSoundfile)) ? false : true;
            switch (flashType)
            {
                case 1: // 상상완화
                    if (bIsDiffer) File.Copy(strSoundfile, _path.imagination + strFilename, true);
                    break;
                case 2: // 완화방법
                    if (bIsDiffer) File.Copy(strSoundfile, _path.relexmethod + strFilename, true);
                    break;
                default:
                    break;
            }
            // image테블안에서 flasytype== flashType 이고 flashno == flashNo인 모든 레코드를 삭제한다.
            sql = "DELETE * FROM [image] WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + flashNo + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // image테블안에 (flashType, flashNo, imageList[i].no, imageList[i].filename, imageList[i].life)삽입, 순환시에 파일도 같이 복사한다.
            int count = imageList.Count;
            for (int i = 0; i < count; i++)
            {
                strFilename = Path.GetFileName(imageList[i].filename).Replace('\'', '`');// 파일이름속에 "'"이 들어있으면 "`"으로 교체
                sql = "INSERT INTO [image] VALUES(" + flashType.ToString() + ", " + flashNo.ToString() + ", " + imageList[i].no.ToString() + ", '" + strFilename + "', " + imageList[i].life.ToString() + ");";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return;
                }
                bIsDiffer = (strFilename.Equals(imageList[i].filename)) ? false : true;
                switch (flashType)
                {
                    case 1: // 상상완화
                        if (bIsDiffer) File.Copy(imageList[i].filename, _path.imagination + strFilename, true);
                        break;
                    case 2: // 완화방법
                        if (bIsDiffer) File.Copy(imageList[i].filename, _path.relexmethod + strFilename, true);
                        break;
                    default:
                        break;
                }
            }
            con.Close();
        }
        public static void deleteFlash(int nCountFlash, int flashType, int flashNo)
        {
            // 자료기지 열기
            if (!open()) return;
            // image테블안에서 flasytype== flashType 이고 flashno == flashNo인 모든 레코드를 삭제한다.
            sql = "DELETE * FROM [image] WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + flashNo + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // flashe테블안에서 type== flashType 이고 no == flashNo인 레코드(유일)를 삭제한다.
            sql = "DELETE * FROM [flash] WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + flashNo + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // flash, image테블안에서 번호 재배치
            for (int i = flashNo + 1; i <= nCountFlash; i++)
            {
                sql = "UPDATE [flash] SET flash.no=" + (i - 1).ToString() + " WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + i.ToString() + ";";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return;
                }
                sql = "UPDATE [image] SET image.flashno=" + (i - 1).ToString() + " WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + i.ToString() + ";";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    dbcommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    con.Close();
                    return;
                }
            }
            con.Close();
        }
        public static void replaceFlashNo(int flashType, int flashno, int withflashno, int totalCount)
        {
            if (!open()) return;
            // 일단 flashno를 (totalCount + 1)값으로 대치
            sql = "UPDATE [flash] SET flash.no=" + (totalCount + 1).ToString() + " WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + flashno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            sql = "UPDATE [image] SET image.flashno=" + (totalCount + 1).ToString() + " WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + flashno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // withflashno를 flashno로 교체
            sql = "UPDATE [flash] SET flash.no=" + flashno.ToString() + " WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + withflashno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            sql = "UPDATE [image] SET image.flashno=" + flashno.ToString() + " WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + withflashno.ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            // (totalCount + 1)를 withflashno로 교체
            sql = "UPDATE [flash] SET flash.no=" + withflashno.ToString() + " WHERE (flash.type)=" + flashType.ToString() + " AND (flash.no)=" + (totalCount + 1).ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            sql = "UPDATE [image] SET image.flashno=" + withflashno.ToString() + " WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + (totalCount + 1).ToString() + ";";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                dbcommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                con.Close();
                return;
            }
            con.Close();
        }
        public static string getFlashString(int flashType)
        {
            // 자료기지 열기
            if (!open()) return "";
            // 리용자이름 검색
            sql = "SELECT * FROM [flash] WHERE (flash.type)=" + flashType.ToString() + " ORDER BY flash.no;";
            try
            {
                dbcommand = new OleDbCommand(sql, con);
                datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                return "";
            }
            int nFlash = 0;
            List<_flash> flashList = new List<_flash>();
            while (datareader.Read())
            {
                _flash item = new _flash();
                item.title = datareader["title"].ToString();
                item.music = datareader["music"].ToString();
                flashList.Add(item);
                nFlash++;
            }
            string result = "";
            string strPath = (flashType == 1) ? _path.imagination : _path.relexmethod;
            for (int i = 0; i < nFlash; i++)
            {
                result += flashList[i].title + '@' + strPath + flashList[i].music + '@';
                sql = "SELECT * FROM [image] WHERE (image.flashtype)=" + flashType.ToString() + " AND (image.flashno)=" + (i + 1).ToString() + " ORDER BY image.no;";
                try
                {
                    dbcommand = new OleDbCommand(sql, con);
                    datareader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message);
                    return "";
                }
                int nImage = 0;
                List<_image> imageList = new List<_image>();
                while (datareader.Read())
                {
                    _image item = new _image();
                    item.life = float.Parse(datareader["life"].ToString());
                    item.filename = datareader["filename"].ToString();
                    imageList.Add(item);
                    nImage++;
                }
                for (int j = 0; j < nImage; j++ )
                {
                    result += strPath + imageList[j].filename + '|' + imageList[j].life.ToString();
                    if (j != nImage - 1) result += '&';
                }
                if (i != nFlash - 1) result += '#';
            }
            con.Close();
            return result;
        }
    }
    public class _report
    {
        // 변수 지정
        #region
        public int type = 0;
        private string strReportData = "";
        private string strHTML = "";
        private string strHtmlBody = "";
        private int ieVersion = 0;
        // 총평변수
        private float totalAvg;
        // 생리반응 변수
        private string[] arryPhysiology = { "25, 34, 37", "10", "44", "41", "6, 28", "18, 40", "47", "1, 17, 31, 49", "9, 33", "21, 29", "19" };
        private float[] arryPhyTest = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // 시험값
        private float totalPhy = 0; // 총평
        // 정서반응 변수
        private string[] arryEmotion = { "5, 11, 22, 26, 30, 32, 38, 39, 46, 48", "7, 13, 35, 50", "8", "53", "52", "43" };
        private float[] arryEmoTest = { 0, 0, 0, 0, 0, 0 }; // 시험값
        private float totalEmo = 0; // 총평
        // 거동반응 변수
        private string[] arryBehavior = { "16", "2", "36, 45, 51", "23, 27", "14", "3", "12, 24" };
        private float[] arryBehTest = { 0, 0, 0, 0, 0, 0, 0 }; // 시험값
        private float totalBeh = 0; // 총평
        // 지식반응 변수
        private string[] arryKnow = { "4, 15, 20, 42" };
        private float[] arryKnoTest = { 1 }; // 시험값
        private float totalKno = 0; // 총평
        // 총평을 위한 변수
        private string strCommentTotal = "";
        private string strRecommendTotal = "";
        private int nGradeTotal = 0;
        // 생리반응총평변수
        private string strCommentPhysiology = "";
        private string strRecommendPhysiology = "";
        private int nGradePhysiology = 0;
        // 정서반응총평변수
        private string strCommentEmotion = "";
        private string strRecommendEmotion = "";
        private int nGradeEmotion = 0;
        // 거동반응총평변수
        private string strCommentBehavior = "";
        private string strRecommendBehavior = "";
        private int nGradeBehavior = 0;
        // 지삭반응총평변수
        private string strCommentKnow = "";
        private string strRecommendKnow = "";
        private int nGradeKnow = 0;
        // 생리반응그라프 라벨
        private string[] labelPhyList = { "感觉系统", "呼吸系统", "骨骼肌肉系统", "神经系统", "经历", "免疫系统", "睡眠", "消化系统", "心血管系统", "性", "综合" };
        // 정서반응그라프 라벨
        private string[] labelEmoList = { "抑郁情绪", "焦虑情绪", "敌意情绪", "无奈情绪", "孤独情绪", "躁动情绪" };
        // 행위반응그라프 라벨
        private string[] labelBehList = { "抱怨行为", "烦躁行为", "攻击行为", "退缩行为", "依赖行为", "指责行为", "转化性行为" };
        // 지식반응그라프 라벨
        string[] labelKnoList = { "认知操作受损" };
        #endregion
        public string getHtml(int ieVersionMajor)
        {
            ieVersion = ieVersionMajor;
            string strScript = (ieVersion > 6) ? "" : "<script src='" + _path.js + "png.js'" + "></script><style type='text/css'>.png24 {tmp:expression(setPng24(this));}</style>";
            string[] strData = _global.tmp.Split('#');
            type = Int32.Parse(strData[0]);
            strHTML = "";
            switch (type)
            {
                case 1: // 평가
                    strReportData = strData[1];
                    processPingguData();
                    processPingguGraph();
                    makePingguHtmlbody();
                    strHTML = "<html>" +
                        "<head>" +
                            "<meta http-equiv='content-type' content='text/html; charset=gb2312'/><style>body, table, td, th {line-height: 150%;}</style>" + strScript +
                            "<body  leftmargin = '50' topmargin = '50' background='" + _path.image + "bkhtml.jpg" + "'>" + strHtmlBody + "</body>" +
                        "</html>";
                    break;
                case 2: // 게임
                case 3: // 상상완화, 음악
                    strReportData = strData[1];
                    processGraph();
                    makeHtmlBody();
                    strHTML = "<html>" +
                        "<head>" +
                            "<meta http-equiv='content-type' content='text/html; charset=gb2312'/><style>body, table, td, th {line-height: 150%;}</style>" + strScript +
                            "<body  leftmargin = '50' topmargin = '50' background='" + _path.image + "bkhtml.jpg" + "'>" + strHtmlBody + "</body>" +
                        "</html>";
                    break;
                default:
                    break;
            }
            return strHTML;
        }
        private void processGraph()
        {
            string[] arryData = strReportData.Split('|');
            string strData = (type == 2) ? arryData[6] : arryData[4];
            arryData = strData.Split('&');
            bool isEmpty = (strData.Equals("")) ? true : false;
            int nData = (isEmpty) ? 300 : arryData.Length;
            int[] data = new int[nData];
            for (int i = 0; i < nData; i++) data[i] = (isEmpty)?i:Int32.Parse(arryData[i]);
            // 자원 초기화
            int imgWidth = 700, imgHeight = 300, margin = 10, gapsLabel = 10, gapsNumber = 5, gapsText = 2;
            System.Drawing.Font fontLabel = new System.Drawing.Font("新宋体", 10);
            System.Drawing.Font fontNumber = new System.Drawing.Font("新宋体", 8);
            Image img = new Bitmap(imgWidth, imgHeight);
            Graphics g = Graphics.FromImage(img);
            Pen pen = new Pen(Color.Black);
            Pen penGrid = new Pen(Color.Gray);
            Brush brush = new SolidBrush(Color.Black);
            int sizeLabel = (int)Math.Ceiling(fontLabel.GetHeight(g));
            int sizeNumber = (int)Math.Ceiling(fontNumber.GetHeight(g));
            // 그래프 시작
            // 1. 그리드 및 그리드라벨
            float xStart = margin + sizeLabel + gapsLabel + sizeNumber * 3 + gapsNumber;
            float xEnd = imgWidth - margin;
            float xInterval = (xEnd - xStart) / 10;
            float yStart = margin;
            float yEnd = imgHeight - margin - sizeLabel - gapsLabel - sizeNumber - gapsLabel;
            float yInterval = (yEnd - yStart) / 9;
            float xCenter, yCenter;
            StringFormat formatter = new StringFormat();
            formatter.LineAlignment = StringAlignment.Center;
            formatter.Alignment = StringAlignment.Far;
            for (int i = 0; i < 10; i++) // 가로선
            {
                yCenter = yStart + yInterval * i;
                if (i == 9)
                {
                    g.DrawLine(pen, xStart, yCenter, xEnd, yCenter);
                }
                else
                {
                    g.DrawLine(penGrid, xStart, yCenter, xEnd, yCenter);
                }
                if (i > 0 && i < 9) // x축 라벨
                {
                    RectangleF rectangle = new RectangleF(0, yCenter - yInterval, xStart - gapsNumber, yInterval * 2);
                    string strNumber = ((9 - i) * 20).ToString();
                    g.DrawString(strNumber, fontNumber, brush, rectangle, formatter);
                }
            }
            int xMax = 10 * (int)Math.Ceiling((double)nData / 10);
            formatter.LineAlignment = StringAlignment.Near;
            formatter.Alignment = StringAlignment.Center;
            for (int i = 0; i < 11; i++) // 세로선
            {
                xCenter = xStart + xInterval * i;
                if (i == 10)
                {
                    g.DrawLine(pen, xCenter, yStart, xCenter, yEnd);
                }
                else
                {
                    g.DrawLine(penGrid, xCenter, yStart, xCenter, yEnd);
                }
                if (i < 10) // y축 라벨
                {
                    RectangleF rectangle = new RectangleF(xCenter - xInterval, yEnd + gapsNumber, xInterval * 2, sizeNumber);
                    string strNumber = (xMax / 10 * i * _game_data.tickCollectInterval / 1000).ToString();
                    g.DrawString(strNumber, fontNumber, brush, rectangle, formatter);
                }
            }
            // 2. 그라프축 라벨표시
            formatter.LineAlignment = StringAlignment.Near;
            formatter.Alignment = StringAlignment.Center;
            RectangleF rc = new RectangleF();
            string strXLabel = "时间（秒）";
            rc.X = xStart; rc.Y = imgHeight - margin - sizeLabel; rc.Width = xEnd - xStart; rc.Height = sizeLabel;
            g.DrawString(strXLabel, fontLabel, brush, rc, formatter);
            string strYLabel = "心率\uFE35次/分钟\uFE36";
            int nStrYLabel = strYLabel.Length;
            float height = sizeLabel * nStrYLabel + gapsText * (nStrYLabel - 1);
            float yStartYLabel = yStart + (yEnd - yStart - height) / 2;
            for (int i = 0; i < nStrYLabel; i++)
            {
                g.DrawString(strYLabel.Substring(i, 1), fontLabel, brush, margin, yStartYLabel + i * (sizeLabel + gapsText));
            }
            // 3. 곡선그리기
            if (!isEmpty)
            {
                xInterval = (xEnd - xStart) / xMax;
                yInterval = (yEnd - yStart) / 200;
                for (int i = 0; i < nData - 1; i++)
                {
                    g.DrawLine(pen, xStart + i * xInterval, yEnd - data[i] * yInterval, xStart + (i + 1) * xInterval, yEnd - data[i + 1] * yInterval);
                }
            }
            img.Save(_path.temp + "tmp6.png", System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
        }
        private void makeHtmlBody()
        {
            string[] arryData = strReportData.Split('|');
            strHtmlBody = "<font size='5' color='#0066ff'><b>选用：" + arryData[0] + "</b></font></center><br>";
            strHtmlBody += (type == 2) ? "<font size='3' face='新宋体' color=''>用时：" + arryData[1] + "</font>" : "";
            int offset = (type == 2) ? 0 : 1;
            string strHR = "<font size='3' face='新宋体' color=''>平均心率：" + arryData[2 - offset] + "</font>";
            strHtmlBody += (type == 2) ? "&nbsp;&nbsp;&nbsp;&nbsp;" + strHR + "<br>" : strHR;
            string strBar = "<font size='3' face='新宋体' color=''>平均脉搏强度：" + arryData[3 - offset] + "</font>&nbsp;&nbsp;&nbsp;&nbsp;";
            strHtmlBody += (type == 2) ? strBar : "&nbsp;&nbsp;&nbsp;&nbsp;" + strBar;
            strHtmlBody += "<font size='3' face='新宋体' color=''>平均血氧饱和度：" + arryData[4 - offset] + "</font><br>";
            if (type == 2)
            {
                strHtmlBody += "<font size='3' face='新宋体' color=''>反馈：</font><br>";
                strHtmlBody += "&nbsp;&nbsp;&nbsp;&nbsp;<font size='3' face='新宋体' color=''>" + arryData[5] + "</font><br>";
            }
            strHtmlBody += "<font size='3' face='新宋体' color=''>心率变化图：</font><br>";
            strHtmlBody += (ieVersion > 6) ? "<img src='" + _path.temp + "tmp6.png" + "' width='700' height='300' border='0' alt=''>" : "<img src='" + _path.temp + "tmp6.png" + "' class='png24' width='700' height='300' border='0' alt=''>";
        }
        private void processPingguData()
        {
            // 총평계산
            int len = strReportData.Length;
            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                int mark = Int32.Parse(strReportData.Substring(i, 1));
                sum += mark;
            }
            totalAvg = (float)sum / (float)len;
            calcPingguResultItem(ref strReportData, ref arryPhysiology, ref arryPhyTest, ref totalPhy);
            calcPingguResultItem(ref strReportData, ref arryEmotion, ref arryEmoTest, ref totalEmo);
            calcPingguResultItem(ref strReportData, ref arryBehavior, ref arryBehTest, ref totalBeh);
            calcPingguResultItem(ref strReportData, ref arryKnow, ref arryKnoTest, ref totalKno);

            _dbManager.getUserInfo();
            nGradePhysiology = _dbManager.getResult(1, totalPhy, ref strCommentPhysiology, ref strRecommendPhysiology);
            nGradeEmotion = _dbManager.getResult(2, totalEmo, ref strCommentEmotion, ref strRecommendEmotion);
            nGradeBehavior = _dbManager.getResult(3, totalBeh, ref strCommentBehavior, ref strRecommendBehavior);
            nGradeKnow = _dbManager.getResult(4, totalKno, ref strCommentKnow, ref strRecommendKnow);
            nGradeTotal = _dbManager.getResult(5, totalAvg, ref strCommentTotal, ref strRecommendTotal);
        }
        private void processPingguGraph()
        {
            int imgWidth, imgHeight, margin, titleHeight, barWidth, gapsFontLine;
            float xStart, yStart, xGraphCenter, yGraphCenter, xCenter, yCenter, xInterval, yInterval, sizeLabel, sizeTitle;
            string lblTitle, label;
            System.Drawing.Font fontCaption, fontLabel;
            Image img; Graphics g; Pen pen; Brush brush;
            // ***********************************  총평그라프 ****************************************//
            #region
            imgWidth = 400; imgHeight = 150; margin = 5; titleHeight = 30; barWidth = 20; gapsFontLine = 5;
            img = new Bitmap(imgWidth, imgHeight);
            g = Graphics.FromImage(img);
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
            fontCaption = new System.Drawing.Font("新宋体", 14);
            fontLabel = new System.Drawing.Font("新宋体", 12);
            sizeTitle = fontCaption.GetHeight(g); sizeLabel = fontLabel.GetHeight(g);
            int bottomAxis = imgHeight - titleHeight - (int)sizeLabel - gapsFontLine;
            g.DrawLine(pen, margin, bottomAxis, imgWidth - margin, bottomAxis);
            xInterval = (float)(imgWidth - margin * 2) / (10 + 1);
            yInterval = (float)(bottomAxis - margin) / 10;
            for (int i = 1; i <= 10; i++)
            {
                xCenter = i * xInterval;
                if (i > nGradeTotal)
                {
                    g.DrawRectangle(pen, xCenter - barWidth / 2, bottomAxis - i * yInterval, barWidth, i * yInterval);
                }
                else
                {
                    g.FillRectangle(brush, xCenter - barWidth / 2, bottomAxis - i * yInterval, barWidth, i * yInterval);
                }
                label = i.ToString() + "级";
                g.DrawString(label, fontLabel, brush, xCenter - label.Length * sizeLabel / 2, bottomAxis + gapsFontLine);

            }
            lblTitle = "压力反应得分情况图";
            g.DrawString(lblTitle, fontCaption, brush, imgWidth / 2 - sizeTitle * lblTitle.Length / 2, imgHeight - sizeTitle);
            img.Save(_path.temp + "tmp0.png", System.Drawing.Imaging.ImageFormat.Png);
            #endregion
            // **************************************** 세부총평그라프 ******************************************//
            #region
            System.Drawing.Font fontMedia = new System.Drawing.Font("新宋体", 10);
            float sizeMedia = fontMedia.GetHeight(g);
            fontCaption = new System.Drawing.Font("新宋体", 14);
            fontLabel = new System.Drawing.Font("新宋体", 8);
            sizeTitle = fontCaption.GetHeight(g); sizeLabel = fontLabel.GetHeight(g);
            margin = 5; titleHeight = 30;
            int sizeGraph = 250;
            imgWidth = sizeGraph + margin * 2 + (int)sizeMedia * 4 + gapsFontLine * 2;
            imgHeight = sizeGraph + gapsFontLine * 2 + (int)sizeMedia * 2 + margin * 2 + titleHeight;
            img = new Bitmap(imgWidth, imgHeight);
            g = Graphics.FromImage(img);
            xInterval = (float)sizeGraph / 20;
            yInterval = (float)sizeGraph / 20;
            xStart = margin + 2 * sizeMedia + gapsFontLine;
            yStart = margin + sizeMedia + gapsFontLine;
            // 그파프제목
            lblTitle = "压力反应各维度得分情况图";
            g.DrawString(lblTitle, fontCaption, brush, imgWidth / 2 - lblTitle.Length * sizeTitle / 2, imgHeight - sizeTitle);
            // 시험결과그라프
            xGraphCenter = imgWidth / 2; yGraphCenter = yStart + sizeGraph / 2;
            PointF[] points = new PointF[4];
            points[0] = new PointF(xStart + (10 + nGradePhysiology) * xInterval, yGraphCenter); // 생리
            points[1] = new PointF(xGraphCenter, yStart + (10 - nGradeEmotion) * yInterval); // 정서
            points[2] = new PointF(xStart + (10 - nGradeKnow) * xInterval, yGraphCenter);// 지식
            points[3] = new PointF(xGraphCenter, yStart + (10 + nGradeBehavior) * yInterval);// 행위
            g.FillPolygon(new SolidBrush(Color.Gray), points);// 시험결과그라프4각형
            // 라벨표시
            label = nGradePhysiology.ToString();
            g.DrawString(label, fontLabel, brush, points[0].X - label.Length * sizeLabel / 2, points[0].Y + 5); // 생리
            label = nGradeEmotion.ToString();
            g.DrawString(label, fontLabel, brush, points[1].X + 5, points[1].Y - sizeLabel / 2); // 정서
            label = nGradeKnow.ToString();
            g.DrawString(label, fontLabel, brush, points[2].X - label.Length * sizeLabel / 2, points[2].Y + 5); // 지식
            label = nGradeBehavior.ToString();
            g.DrawString(label, fontLabel, brush, points[3].X + 5, points[3].Y - sizeLabel / 2); // 행위
            // 테두리4각형
            float[] dashValues = { 5, 2, 15, 4 };
            pen.Color = Color.Red;
            pen.DashPattern = dashValues;
            points[0] = new PointF(xStart + xInterval * 20, yGraphCenter); // 생리
            points[1] = new PointF(xGraphCenter, yStart); // 정서
            points[2] = new PointF(xStart, yStart + sizeGraph / 2); // 지식
            points[3] = new PointF(xGraphCenter, yStart + yInterval * 20); // 행위
            g.DrawPolygon(pen, points);
            points[0] = new PointF(xStart + xInterval * 18, yGraphCenter); // 생리
            points[1] = new PointF(xGraphCenter, yStart + yInterval * 2); // 정서
            points[2] = new PointF(xStart + xInterval * 2, yGraphCenter); // 지식
            points[3] = new PointF(xGraphCenter, yStart + yInterval * 18); // 행위
            g.DrawPolygon(pen, points);
            pen.Color = Color.Green;
            points[0] = new PointF(xStart + xInterval * 13, yGraphCenter); // 생리
            points[1] = new PointF(xGraphCenter, yStart + yInterval * 7); // 정서
            points[2] = new PointF(xStart + xInterval * 7, yGraphCenter); // 지식
            points[3] = new PointF(xGraphCenter, yStart + yInterval * 13); // 행위
            g.DrawPolygon(pen, points);
            // 좌표축 그리기
            pen = new Pen(Color.Black);
            g.DrawLine(pen, xStart, yGraphCenter, xStart + xInterval * 20, yGraphCenter);
            g.DrawLine(pen, xGraphCenter, yStart, xGraphCenter, yStart + yInterval * 20);
            float tickSize = 4;
            for (int i = 0; i < 21; i++)
            {
                xCenter = xStart + xInterval * i;
                g.DrawLine(pen, xCenter, yGraphCenter - tickSize / 2, xCenter, yGraphCenter + tickSize / 2);
                yCenter = yStart + i * yInterval;
                g.DrawLine(pen, xGraphCenter - tickSize / 2, yCenter, xGraphCenter + tickSize / 2, yCenter);
            }
            // 라벨표시
            label = "10";
            g.DrawString(label, fontLabel, brush, xStart - sizeLabel * label.Length / 2, yGraphCenter + tickSize);
            g.DrawString(label, fontLabel, brush, xStart + sizeGraph - sizeLabel * label.Length / 2, yGraphCenter + tickSize);
            g.DrawString(label, fontLabel, brush, xGraphCenter + tickSize, yStart - sizeLabel / 2);
            g.DrawString(label, fontLabel, brush, xGraphCenter + tickSize, yStart + sizeGraph - sizeLabel / 2);
            label = "生理"; g.DrawString(label, fontMedia, brush, xStart + sizeGraph + gapsFontLine, yGraphCenter - sizeMedia / 2);
            label = "认知"; g.DrawString(label, fontMedia, brush, xStart - sizeMedia * label.Length - gapsFontLine, yGraphCenter - sizeMedia / 2);
            label = "情绪"; g.DrawString(label, fontMedia, brush, xGraphCenter - sizeMedia * label.Length / 2, yStart - gapsFontLine - sizeMedia);
            label = "行为"; g.DrawString(label, fontMedia, brush, xGraphCenter - sizeMedia * label.Length / 2, yStart + sizeGraph + gapsFontLine);
            // 원점표시
            g.DrawString("0", fontLabel, brush, xGraphCenter + 5, yGraphCenter + 5);
            img.Save(_path.temp + "tmp1.png", System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
            #endregion
            // **************************************** 세부항목그라프 **************************************//
            generatePingguDetailGraph(2, ref arryPhyTest, "各种生理反应得分情况", ref labelPhyList); // 생리반응
            generatePingguDetailGraph(3, ref arryEmoTest, "各类情绪得分情况", ref labelEmoList); // 정서반응
            generatePingguDetailGraph(4, ref arryBehTest, "各类行为反应得分情况", ref labelBehList);// 거동반응
            generatePingguDetailGraph(5, ref arryKnoTest, "认知反应得分情况", ref labelKnoList);// 지식반응
        }
        private void makePingguHtmlbody()
        {
            string strIndentBig = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            string strIndentSmall = "&nbsp;&nbsp;&nbsp;&nbsp;";
            string strClass = (ieVersion > 6) ? "" : " class='png24' ";
            strHtmlBody = "";
            string strTerminal = "";
            // 시작
            #region
            strHtmlBody += "<table width='100%'><tr><td colspan='2' align='center'><font face='新宋体' size='5' color='#0066ff'><b>压力评估报告</b></font></td></tr>";
            strHtmlBody += "<tr><td colspan='2' align='left'><font size='4' face='造字工房悦黑体验版常规体' color='#000066'><b>一． 评估结果概述</b></font></td></tr>";
            strHtmlBody += "<tr>";
            strHtmlBody += "<td width='70%'>" + strIndentBig + "<font size='3' face='新宋体' color=''>" + strCommentTotal + "</font></td>";
            strHtmlBody += "<td align='center'><img src='" + _path.temp + "tmp0.png" + "' " + strClass + "' border='0' alt=''></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            strHtmlBody += "<td colspan='2'><font size='4' face='造字工房悦黑体验版常规体' color='#000066'><b>二．压力对您的影响及建议</b></font></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            string[] degree = { "并不明显", "并不明显", "并不明显", "一般", "一般", "一般", "比较明显", "比较明显", "非常明显", "非常明显" };
            string strDetail3 = "<font size='3' face='新宋体' color=''>测评结果显示：压力对您造成的消极影响在" + "生理方面表现" + degree[nGradePhysiology - 1] + "," + "在情绪方面表现" + degree[nGradeEmotion - 1] + "," + "在行为方面表现" + degree[nGradeBehavior - 1] + "," + "在认知方面表现" + degree[nGradeKnow - 1] + "。";
            strHtmlBody += "<td width='70%' valign='top'>" + strIndentBig + "<font size='3' face='新宋体' color=''>过高或持久的压力会对个体产生负面影响，这些影响会表现在生理、情绪、行为和认知四个方面。右图列出了您在这四个方面的得分，分数范围为1～10。得分越高，表明压力对您的影响越大。</font>" + strDetail3 + "</td>";
            strHtmlBody += "<td align='center'><img src='" + _path.temp + "tmp1.png" + "' " + strClass + "' border='0' alt=''></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            strHtmlBody += "<td colspan='2' align='left'><font size='4' face='造字工房悦黑体验版常规体' color='#000066'><b>四个维度的具体评估结果如下：</b></font></td>";
            strHtmlBody += "</tr>";
            #endregion
            // 생리반응
            #region
            strHtmlBody += "<tr>";
            strHtmlBody += "<td colspan='2' align='left'><font size='3.5' face='新宋体' color='#006600'><b>生理反应</b></font></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            string strDetailPhy = "<font size='3' face='新宋体' color=''><b>评估结果： </b></font><br>";
            strDetailPhy += strIndentSmall + "<font size='3' face='新宋体' color=''>你的生理维度的等级为" + nGradePhysiology.ToString() + "级，" + strCommentPhysiology + " </font><br>";
            strDetailPhy += "<font size='3' face='新宋体' color=''><b>具体表现： </b></font><br>";
            strDetailPhy += strIndentSmall + "<font size='3' face='新宋体' color=''>压力下的不良生理反应有不同的表现形式。右图列出了你在几类典型生理反应上的得分。</font><br>";
            strTerminal = "";
            if (nGradePhysiology >= 4)
            {
                strTerminal += (arryPhyTest[0] >= 2) ? "眼耳等感觉器官有些不舒服、" : "";
                strTerminal += (arryPhyTest[1] >= 2) ? "胸闷憋气、" : "";
                strTerminal += (arryPhyTest[2] >= 2) ? "肌肉酸痛、" : "";
                strTerminal += (arryPhyTest[3] >= 2) ? "手脚发冷、" : "";
                strTerminal += (arryPhyTest[4] >= 2) ? "容易疲劳，且难以恢复、" : "";
                strTerminal += (arryPhyTest[5] >= 2) ? "身体抵抗力下降，容易感冒或生口疮、" : "";
                strTerminal += (arryPhyTest[6] >= 2) ? "睡眠不大好、" : "";
                strTerminal += (arryPhyTest[7] >= 2) ? "肠胃不舒服、" : "";
                strTerminal += (arryPhyTest[8] >= 2) ? "心慌胸痛、" : "";
                strTerminal += (arryPhyTest[9] >= 2) ? "性方面的困扰、" : "";
                strTerminal += (arryPhyTest[10] >= 2) ? "头痛" : "";
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                strDetailPhy += strIndentSmall + "<font size='3' face='新宋体' color=''>具体表现为：时常感到" + strTerminal + "。</font><br>";
            }
            strDetailPhy += strIndentSmall + "<font size='3' face='新宋体' color=''>" + strRecommendPhysiology + "</font><br>";
            strHtmlBody += "<td width='70%'><font size='3' face='新宋体' color=''>" + strDetailPhy + "</font><br></td>";
            strHtmlBody += "<td align='center'><img src='" + _path.temp + "tmp2.png" + "' " + strClass + "' border='0' alt=''></td>";
            strHtmlBody += "</tr>";
            #endregion
            // 정서반응
            #region
            strHtmlBody += "<tr>";
            strHtmlBody += "<td colspan='2' align='left'><font size='3.5' face='新宋体' color='#006600'><b>情绪反应 </b></font></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            string strDetailEmotion = "<font size='3' face='新宋体' color=''><b>评估结果： </b></font><br>";
            strDetailEmotion += strIndentSmall + "<font size='3' face='新宋体' color=''>你的情绪维度的等级为" + nGradeEmotion.ToString() + "级，" + strCommentEmotion + " </font><br>";
            strDetailEmotion += "<font size='3' face='新宋体' color=''><b>具体表现： </b></font><br>";
            strDetailEmotion += strIndentSmall + "<font size='3' face='新宋体' color=''>压力下的消极情绪反应有不同的表现形式。右图列出了你在几类典型情绪反应上的得分。</font><br>";
            strTerminal = "";
            if (nGradeEmotion >= 4)
            {
                strTerminal += (arryEmoTest[0] >= 2) ? "情绪低落、容易自责、" : "";
                strTerminal += (arryEmoTest[1] >= 2) ? "紧张焦虑、忐忑不安、" : "";
                strTerminal += (arryEmoTest[2] >= 2) ? "容易发怒、" : "";
                strTerminal += (arryEmoTest[3] >= 2) ? "不知如何应对困境、" : "";
                strTerminal += (arryEmoTest[4] >= 2) ? "孤单、" : "";
                strTerminal += (arryEmoTest[5] >= 2) ? "崩溃感" : "";
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                strDetailEmotion += strIndentSmall + "<font size='3' face='新宋体' color=''>具体表现为：时常感到" + strTerminal + "。</font><br>";
            }
            strDetailEmotion += strIndentSmall + "<font size='3' face='新宋体' color=''>" + strRecommendEmotion + "</font><br>";
            strHtmlBody += "<td width='70%'><font size='3' face='新宋体' color=''>" + strDetailEmotion + "</font><br></td>";
            strHtmlBody += "<td align='center'><img src='" + _path.temp + "tmp3.png" + "' " + strClass + "' border='0' alt=''></td>";
            strHtmlBody += "</tr>";
            #endregion
            // 거동반응
            #region
            strHtmlBody += "<tr>";
            strHtmlBody += "<td colspan='2' align='left'><font size='3.5' face='新宋体' color='#006600'><b>行为反应  </b></font></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            string strDetailBehavior = "<font size='3' face='新宋体' color=''><b>评估结果： </b></font><br>";
            strDetailBehavior += strIndentSmall + "<font size='3' face='新宋体' color=''>你的行为维度的等级为" + nGradeBehavior.ToString() + "级，" + strCommentBehavior + " </font><br>";
            strDetailBehavior += "<font size='3' face='新宋体' color=''><b>具体表现： </b></font><br>";
            strDetailBehavior += strIndentSmall + "<font size='3' face='新宋体' color=''>压力下的消极行为反应有不同的表现形式。右图列出了你在几类典型行为反应上的得分。</font><br>";
            strTerminal = "";
            if (nGradeBehavior >= 4)
            {
                strTerminal += (arryBehTest[0] >= 2) ? "容易抱怨、" : "";
                strTerminal += (arryBehTest[1] >= 2) ? "坐立不安、" : "";
                strTerminal += (arryBehTest[2] >= 2) ? "倾向于用攻击的方式发泄情绪、" : "";
                strTerminal += (arryBehTest[3] >= 2) ? "沉默寡言，很少与人交流、" : "";
                strTerminal += (arryBehTest[4] >= 2) ? "变得依赖他人、" : "";
                strTerminal += (arryBehTest[5] >= 2) ? "容易自责或指责别人、" : "";
                int th12 = Int32.Parse(strReportData.Substring(11, 1));
                int th24 = Int32.Parse(strReportData.Substring(23, 1));
                if (th12 >= 2 && th24 >= 2)
                {
                    strTerminal += "饭量增加/吸烟量增加";
                }
                else if (th12 >= 2)
                {
                    strTerminal += "饭量增加";
                }
                else if (th24 >= 2)
                {
                    strTerminal += "吸烟量增加";
                }
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                strDetailBehavior += strIndentSmall + "具体表现为：时常感到" + strTerminal + "。";
            }
            strDetailBehavior += strIndentSmall + "<font size='3' face='新宋体' color=''>" + strRecommendBehavior + "</font><br>";
            strHtmlBody += "<td width='70%'><font size='3' face='新宋体' color=''>" + strDetailBehavior + "</font><br></td>";
            strHtmlBody += "<td align='center'><img src='" + _path.temp + "tmp4.png" + "' " + strClass + "' border='0' alt=''></td>";
            strHtmlBody += "</tr>";
            #endregion
            // 지식반응
            #region
            strHtmlBody += "<tr>";
            strHtmlBody += "<td colspan='2' align='left'><font size='3.5' face='新宋体' color='#006600'><b>认知反应 </b></font></td>";
            strHtmlBody += "</tr>";
            strHtmlBody += "<tr>";
            string strDetailKnow = "<font size='3' face='新宋体' color=''><b>评估结果： </b></font><br>";
            strDetailKnow += strIndentSmall + "<font size='3' face='新宋体' color=''>你的认知维度的等级为" + nGradeKnow.ToString() + "级，" + strCommentKnow + " </font><br>";
            strDetailKnow += "<font size='3' face='新宋体' color=''><b>具体表现： </b></font><br>";
            strDetailKnow += strIndentSmall + "<font size='3' face='新宋体' color=''>压力下的认知反应有不同的表现形式。右图列出了你在几类典型认知反应上的得分。</font><br>";
            strTerminal = "";
            if (nGradeKnow >= 4)
            {
                strTerminal += (arryKnoTest[0] >= 2) ? "认知力下降" : "";
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                strDetailKnow += strIndentSmall + "<font size='3' face='新宋体' color=''>具体表现为：时常感到" + strTerminal + "。</font><br>";
            }
            strDetailKnow += strIndentSmall + "<font size='3' face='新宋体' color=''>" + strRecommendKnow + "</font><br>";
            strHtmlBody += "<td width='70%'><font size='3' face='新宋体' color=''>" + strDetailKnow + "</font><br></td>";
            strHtmlBody += "<td align='center'><img src='" + _path.temp + "tmp5.png" + "' " + strClass + "' border='0' alt=''></td>";
            strHtmlBody += "</tr>";
            #endregion
            strHtmlBody += "</table>";
        }
        public void makeWordDocument(string filename)
        {
            object oMissing = System.Reflection.Missing.Value;
            // Word문서만들기
            _Application oWord = new Microsoft.Office.Interop.Word.Application();
            _Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            string[] arryData = strReportData.Split('|');
            string strTab = "        ";

            // 리용자정보
            string strUserInfo = "用户名:" + _user_info.name + strTab + "性别:" + _user_info.gender + strTab + "出生日期:" + _user_info.birthday + strTab + "职业:" + _user_info.job + strTab;
            Paragraph oParaUserInfo;
            oParaUserInfo = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaUserInfo.Range.Text = strUserInfo;
            oParaUserInfo.Range.InsertParagraphAfter();

            // 타이틀
            string strTitle = _global.moment + strTab + arryData[0];
            Paragraph oParaTitle;
            oParaTitle = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaTitle.Range.Text = strTitle;
            oParaTitle.Range.InsertParagraphAfter();

            string strSecondRow = "";
            if (type == 2)
            {
                strSecondRow += "用时：" + arryData[1] + strTab;
            }
            int offset = (type == 2) ? 0 : 1;
            strSecondRow += "平均心率：" + arryData[2 - offset] + strTab + "平均脉搏强度：" + arryData[3 - offset];

            Paragraph oParaSecondRow;
            oParaSecondRow = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaSecondRow.Range.Text = strSecondRow;
            oParaSecondRow.Range.InsertParagraphAfter();

            Paragraph oParaSecondThird;
            oParaSecondThird = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaSecondThird.Range.Text = "平均血氧饱和度：" + arryData[4 - offset];
            oParaSecondThird.Range.InsertParagraphAfter();

            if (type == 2)
            {
                Paragraph oParaInsert1;
                oParaInsert1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oParaInsert1.Range.Text = "反馈：";
                oParaInsert1.Range.InsertParagraphAfter();

                Paragraph oParaInsert2;
                oParaInsert2 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oParaInsert2.Range.Text = arryData[5];
                oParaInsert2.Range.InsertParagraphAfter();
            }

            Paragraph oParaSecondFourth;
            oParaSecondFourth = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaSecondFourth.Range.Text = "心率变化图：";
            oParaSecondFourth.Range.InsertParagraphAfter();

            Paragraph oParaPicture; // 그라프
            oParaPicture = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPicture.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp6.png");
            oParaPicture.Range.InsertParagraphAfter();

            oDoc.SaveAs(filename);
            oDoc.Close();
            oWord.Quit();
        }
        public void makePingguWordDocument(string filename)
        {
            object oMissing = System.Reflection.Missing.Value;
            // Word문서만들기
            _Application oWord = new Microsoft.Office.Interop.Word.Application();
            _Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            // 리용자정보
            #region
            string strTab = "        ";
            string strUserInfo = "用户名:" + _user_info.name + strTab + "性别:" + _user_info.gender + strTab + "出生日期:" + _user_info.birthday + strTab + "职业:" + _user_info.job + strTab;
            Paragraph oParaUserInfo;
            oParaUserInfo = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaUserInfo.Range.Text = strUserInfo;
            oParaUserInfo.Range.InsertParagraphAfter();
            #endregion

            // 총평 출력
            #region
            Paragraph oParaTitleTotal;
            oParaTitleTotal = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaTitleTotal.Range.Text = "一． 评估结果概述";
            oParaTitleTotal.Range.InsertParagraphAfter();

            Paragraph oParaCommentTotal;
            oParaCommentTotal = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaCommentTotal.Range.Text = strCommentTotal;
            oParaCommentTotal.Range.InsertParagraphAfter();

            Paragraph oParaPictureTotal; // 그라프
            oParaPictureTotal = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPictureTotal.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp0.png");
            oParaPictureTotal.Range.InsertParagraphAfter();
            #endregion

            // 세부항목 출력
            #region
            Paragraph oParaDetail1;
            oParaDetail1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaDetail1.Range.Text = "二．压力对您的影响及建议";
            oParaDetail1.Range.InsertParagraphAfter();

            Paragraph oParaDetail2;
            oParaDetail2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaDetail2.Range.Text = "过高或持久的压力会对个体产生负面影响，这些影响会表现在生理、情绪、行为和认知四个方面。右图列出了您在这四个方面的得分，分数范围为1～10。得分越高，表明压力对您的影响越大。";
            oParaDetail2.Range.InsertParagraphAfter();

            string[] degree = { "并不明显", "并不明显", "并不明显", "一般", "一般", "一般", "比较明显", "比较明显", "非常明显", "非常明显" };
            string strDetail3 = "测评结果显示：压力对您造成的消极影响在" + "生理方面表现" + degree[nGradePhysiology - 1] + "," + "在情绪方面表现" + degree[nGradeEmotion - 1] + "," + "在行为方面表现" + degree[nGradeBehavior - 1] + "," + "在认知方面表现" + degree[nGradeKnow - 1] + "。";
            Paragraph oParaDetail3;
            oParaDetail3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaDetail3.Range.Text = strDetail3;
            oParaDetail3.Range.InsertParagraphAfter();

            Paragraph oParaPictureDetail; // 그라프
            oParaPictureDetail = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPictureDetail.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp1.png");
            oParaPictureDetail.Range.InsertParagraphAfter();

            #endregion
            // 세부화 들어가기 전
            #region
            Paragraph oParaInDetail1;
            oParaInDetail1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail1.Range.Text = "注解：";
            oParaInDetail1.Range.InsertParagraphAfter();

            Paragraph oParaInDetail2;
            oParaInDetail2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail2.Range.Text = "压力对人的消极影响会表现在生理层面和情绪、行为、认知三个心理层面。";
            oParaInDetail2.Range.InsertParagraphAfter();

            Paragraph oParaInDetail3;
            oParaInDetail3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail3.Range.Text = "生理方面，个体会出现多种躯体不适症状；";
            oParaInDetail3.Range.InsertParagraphAfter();

            Paragraph oParaInDetail4;
            oParaInDetail4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail4.Range.Text = "情绪方面，个体会体验到很多消极情绪，如抑郁、焦虑等；";
            oParaInDetail4.Range.InsertParagraphAfter();

            Paragraph oParaInDetail5;
            oParaInDetail5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail5.Range.Text = "行为方面，个体会更容易通过消极的行为来释放压力，如回避、攻击等；";
            oParaInDetail5.Range.InsertParagraphAfter();

            Paragraph oParaInDetail6;
            oParaInDetail6 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail6.Range.Text = "认知方面，个体的注意力、记忆力、逻辑思维等能力会受到不良影响。";
            oParaInDetail6.Range.InsertParagraphAfter();

            Paragraph oParaInDetail7;
            oParaInDetail7 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaInDetail7.Range.Text = "四个维度的具体评估结果如下：";
            oParaInDetail7.Range.InsertParagraphAfter();
            #endregion
            // 생리반응 시작
            #region
            Paragraph oParaPhysiology1;
            oParaPhysiology1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology1.Range.Text = "生理反应 ";
            oParaPhysiology1.Range.InsertParagraphAfter();

            Paragraph oParaPhysiology2;
            oParaPhysiology2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology2.Range.Text = "评估结果： ";
            oParaPhysiology2.Range.InsertParagraphAfter();

            Paragraph oParaPhysiology3;
            oParaPhysiology3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology3.Range.Text = "你的生理维度的等级为" + nGradePhysiology.ToString() + "级，" + strCommentPhysiology;
            oParaPhysiology3.Range.InsertParagraphAfter();

            Paragraph oParaPhysiology4;
            oParaPhysiology4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology4.Range.Text = "具体表现：";
            oParaPhysiology4.Range.InsertParagraphAfter();

            Paragraph oParaPhysiology5;
            oParaPhysiology5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology5.Range.Text = "压力下的不良生理反应有不同的表现形式。右图列出了你在几类典型生理反应上的得分。";
            oParaPhysiology5.Range.InsertParagraphAfter();

            string strTerminal = "";
            if (nGradePhysiology >= 4)
            {
                strTerminal += (arryPhyTest[0] >= 2) ? "眼耳等感觉器官有些不舒服、" : "";
                strTerminal += (arryPhyTest[1] >= 2) ? "胸闷憋气、" : "";
                strTerminal += (arryPhyTest[2] >= 2) ? "肌肉酸痛、" : "";
                strTerminal += (arryPhyTest[3] >= 2) ? "手脚发冷、" : "";
                strTerminal += (arryPhyTest[4] >= 2) ? "容易疲劳，且难以恢复、" : "";
                strTerminal += (arryPhyTest[5] >= 2) ? "身体抵抗力下降，容易感冒或生口疮、" : "";
                strTerminal += (arryPhyTest[6] >= 2) ? "睡眠不大好、" : "";
                strTerminal += (arryPhyTest[7] >= 2) ? "肠胃不舒服、" : "";
                strTerminal += (arryPhyTest[8] >= 2) ? "心慌胸痛、" : "";
                strTerminal += (arryPhyTest[9] >= 2) ? "性方面的困扰、" : "";
                strTerminal += (arryPhyTest[10] >= 2) ? "头痛" : "";
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                Paragraph oParaPhysiologyTerminal;
                oParaPhysiologyTerminal = oDoc.Content.Paragraphs.Add(ref oMissing);
                oParaPhysiologyTerminal.Range.Text = "具体表现为：时常感到" + strTerminal + "。";
                oParaPhysiologyTerminal.Range.InsertParagraphAfter();
            }
            Paragraph oPara1PicturePhysiology;
            oPara1PicturePhysiology = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1PicturePhysiology.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp2.png");
            oPara1PicturePhysiology.Range.InsertParagraphAfter();

            Paragraph oParaPhysiology6;
            oParaPhysiology6 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology6.Range.Text = "图注：得分范围为0－4分，得分越高，说明表现越明显。";
            oParaPhysiology6.Range.InsertParagraphAfter();

            Paragraph oParaPhysiology7;
            oParaPhysiology7 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaPhysiology7.Range.Text = strRecommendPhysiology;
            oParaPhysiology7.Range.InsertParagraphAfter();
            #endregion
            // 정서반응 시작
            #region
            Paragraph oParaEmotion1;
            oParaEmotion1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion1.Range.Text = "情绪反应 ";
            oParaEmotion1.Range.InsertParagraphAfter();

            Paragraph oParaEmotion2;
            oParaEmotion2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion2.Range.Text = "评估结果： ";
            oParaEmotion2.Range.InsertParagraphAfter();

            Paragraph oParaEmotion3;
            oParaEmotion3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion3.Range.Text = "你的情绪维度的等级为" + nGradeEmotion.ToString() + "级，" + strCommentEmotion;
            oParaEmotion3.Range.InsertParagraphAfter();

            Paragraph oParaEmotion4;
            oParaEmotion4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion4.Range.Text = "具体表现：";
            oParaEmotion4.Range.InsertParagraphAfter();

            Paragraph oParaEmotion5;
            oParaEmotion5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion5.Range.Text = "压力下的消极情绪反应有不同的表现形式。右图列出了你在几类典型情绪反应上的得分。";
            oParaEmotion5.Range.InsertParagraphAfter();

            strTerminal = "";
            if (nGradeEmotion >= 4)
            {
                strTerminal += (arryEmoTest[0] >= 2) ? "情绪低落、容易自责、" : "";
                strTerminal += (arryEmoTest[1] >= 2) ? "紧张焦虑、忐忑不安、" : "";
                strTerminal += (arryEmoTest[2] >= 2) ? "容易发怒、" : "";
                strTerminal += (arryEmoTest[3] >= 2) ? "不知如何应对困境、" : "";
                strTerminal += (arryEmoTest[4] >= 2) ? "孤单、" : "";
                strTerminal += (arryEmoTest[5] >= 2) ? "崩溃感" : "";
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                Paragraph oParaEmotionTerminal;
                oParaEmotionTerminal = oDoc.Content.Paragraphs.Add(ref oMissing);
                oParaEmotionTerminal.Range.Text = "具体表现为：时常感到" + strTerminal + "。";
                oParaEmotionTerminal.Range.InsertParagraphAfter();
            }
            Paragraph oPara1PictureEmotion;
            oPara1PictureEmotion = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1PictureEmotion.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp3.png");
            oPara1PictureEmotion.Range.InsertParagraphAfter();

            Paragraph oParaEmotion6;
            oParaEmotion6 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion6.Range.Text = "图注：得分范围为0－4分，得分越高，说明表现越明显。";
            oParaEmotion6.Range.InsertParagraphAfter();

            Paragraph oParaEmotion7;
            oParaEmotion7 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaEmotion7.Range.Text = strRecommendEmotion;
            oParaEmotion7.Range.InsertParagraphAfter();
            #endregion
            // 거동반응 시작
            #region
            Paragraph oParaBehavior1;
            oParaBehavior1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior1.Range.Text = "行为反应  ";
            oParaBehavior1.Range.InsertParagraphAfter();

            Paragraph oParaBehavior2;
            oParaBehavior2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior2.Range.Text = "评估结果： ";
            oParaBehavior2.Range.InsertParagraphAfter();

            Paragraph oParaBehavior3;
            oParaBehavior3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior3.Range.Text = "你的行为维度的等级为" + nGradeBehavior.ToString() + "级，" + strCommentBehavior;
            oParaBehavior3.Range.InsertParagraphAfter();

            Paragraph oParaBehavior4;
            oParaBehavior4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior4.Range.Text = "具体表现：";
            oParaBehavior4.Range.InsertParagraphAfter();

            Paragraph oParaBehavior5;
            oParaBehavior5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior5.Range.Text = "压力下的消极行为反应有不同的表现形式。右图列出了你在几类典型行为反应上的得分。";
            oParaBehavior5.Range.InsertParagraphAfter();

            strTerminal = "";
            if (nGradeBehavior >= 4)
            {
                strTerminal += (arryBehTest[0] >= 2) ? "容易抱怨、" : "";
                strTerminal += (arryBehTest[1] >= 2) ? "坐立不安、" : "";
                strTerminal += (arryBehTest[2] >= 2) ? "倾向于用攻击的方式发泄情绪、" : "";
                strTerminal += (arryBehTest[3] >= 2) ? "沉默寡言，很少与人交流、" : "";
                strTerminal += (arryBehTest[4] >= 2) ? "变得依赖他人、" : "";
                strTerminal += (arryBehTest[5] >= 2) ? "容易自责或指责别人、" : "";
                int th12 = Int32.Parse(strReportData.Substring(11, 1));
                int th24 = Int32.Parse(strReportData.Substring(23, 1));
                Paragraph oParaBehaviorTerminal0;
                oParaBehaviorTerminal0 = oDoc.Content.Paragraphs.Add(ref oMissing);
                if (th12 >= 2 && th24 >= 2)
                {
                    oParaBehaviorTerminal0.Range.Text = "饭量增加/吸烟量增加";
                    oParaBehaviorTerminal0.Range.InsertParagraphAfter();
                }
                else if (th12 >= 2)
                {
                    oParaBehaviorTerminal0.Range.Text = "饭量增加";
                    oParaBehaviorTerminal0.Range.InsertParagraphAfter();
                }
                else if (th24 >= 2)
                {
                    oParaBehaviorTerminal0.Range.Text = "吸烟量增加";
                    oParaBehaviorTerminal0.Range.InsertParagraphAfter();
                }

            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                Paragraph oParaBehaviorTerminal1;
                oParaBehaviorTerminal1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oParaBehaviorTerminal1.Range.Text = "具体表现为：时常感到" + strTerminal + "。";
                oParaBehaviorTerminal1.Range.InsertParagraphAfter();
            }
            Paragraph oPara1PictureBehavior;
            oPara1PictureBehavior = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1PictureBehavior.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp4.png");
            oPara1PictureBehavior.Range.InsertParagraphAfter();

            Paragraph oParaBehavior6;
            oParaBehavior6 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior6.Range.Text = "图注：得分范围为0－4分，得分越高，说明表现越明显。";
            oParaBehavior6.Range.InsertParagraphAfter();

            Paragraph oParaBehavior7;
            oParaBehavior7 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaBehavior7.Range.Text = strRecommendBehavior;
            oParaBehavior7.Range.InsertParagraphAfter();
            #endregion
            // 지식반응 시작
            #region
            Paragraph oParaKnow1;
            oParaKnow1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow1.Range.Text = "认知反应  ";
            oParaKnow1.Range.InsertParagraphAfter();

            Paragraph oParaKnow2;
            oParaKnow2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow2.Range.Text = "评估结果： ";
            oParaKnow2.Range.InsertParagraphAfter();

            Paragraph oParaKnow3;
            oParaKnow3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow3.Range.Text = "你的认知维度的等级为" + nGradeKnow.ToString() + "级，" + strCommentKnow;
            oParaKnow3.Range.InsertParagraphAfter();

            Paragraph oParaKnow4;
            oParaKnow4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow4.Range.Text = "具体表现：";
            oParaKnow4.Range.InsertParagraphAfter();

            Paragraph oParaKnow5;
            oParaKnow5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow5.Range.Text = "压力下的认知反应有不同的表现形式。右图列出了你在几类典型认知反应上的得分。";
            oParaKnow5.Range.InsertParagraphAfter();

            strTerminal = "";
            if (nGradeKnow >= 4)
            {
                strTerminal += (arryKnoTest[0] >= 2) ? "认知力下降" : "";
            }
            if (strTerminal.Length != 0)
            {
                if (strTerminal.EndsWith("、"))
                {
                    strTerminal = strTerminal.Substring(0, strTerminal.Length - 1);
                }
                Paragraph oParaKnowTerminal;
                oParaKnowTerminal = oDoc.Content.Paragraphs.Add(ref oMissing);
                oParaKnowTerminal.Range.Text = "具体表现为：时常感到" + strTerminal + "。";
                oParaKnowTerminal.Range.InsertParagraphAfter();
            }
            Paragraph oPara1PictureKnow;
            oPara1PictureKnow = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1PictureKnow.Range.InlineShapes.AddPicture(FileName: _path.temp + "tmp5.png");
            oPara1PictureKnow.Range.InsertParagraphAfter();

            Paragraph oParaKnow6;
            oParaKnow6 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow6.Range.Text = "图注：得分范围为0－4分，得分越高，说明表现越明显。";
            oParaKnow6.Range.InsertParagraphAfter();

            Paragraph oParaKnow7;
            oParaKnow7 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oParaKnow7.Range.Text = strRecommendKnow;
            oParaKnow7.Range.InsertParagraphAfter();
            #endregion

            oDoc.SaveAs(filename);
            oDoc.Close();
            oWord.Quit();
        }
        private void generatePingguDetailGraph(int index, ref float[] arryTest, string strTitle, ref string[] labelList)
        {
            // 자원 초기화
            int imgWidth = 500, imgHeight = 300, margin = 10, gaps = 5, barSize = 10;
            System.Drawing.Font fontCaption = new System.Drawing.Font("新宋体", 14);
            System.Drawing.Font fontLabel = new System.Drawing.Font("新宋体", 12);
            System.Drawing.Font fontNumber = new System.Drawing.Font("新宋体", 8);
            Image img = new Bitmap(imgWidth, imgHeight);
            Graphics g = Graphics.FromImage(img);
            Pen pen = new Pen(Color.Black);
            Pen penGrid = new Pen(Color.Black);
            float[] dashValues = { 5, 2, 15, 4 };
            penGrid.DashPattern = dashValues;
            Brush brush = new SolidBrush(Color.Black);
            Color[] lstColor = { Color.Blue, Color.BlueViolet, Color.Brown, Color.BurlyWood, Color.CadetBlue, Color.Chartreuse, Color.Chocolate, Color.Coral, Color.CornflowerBlue, Color.Cyan, Color.DarkGreen };
            // 그래프 시작
            // 1. 제일 긴 라벨의 길이 계산(fontSize)
            int nData = labelList.Length;
            int fontSize = 0;
            for (int i = 0; i < nData; i++)
            {
                float size = fontLabel.GetHeight(g) * labelList[i].Length;
                if (fontSize < size)
                {
                    fontSize = (int)Math.Ceiling(size);
                }
            }
            int labelSize = (int)Math.Ceiling(fontLabel.GetHeight(g));
            int titleSize = (int)Math.Ceiling(fontCaption.GetHeight(g));
            int numberSize = (int)Math.Ceiling(fontNumber.GetHeight(g));
            // 2. 라벨 표시
            int yStart = margin;
            int yInterval = (imgHeight - 2 * margin - titleSize - numberSize - 2 * gaps) / (nData + 1);
            StringFormat formatter = new StringFormat();
            formatter.LineAlignment = StringAlignment.Center;
            formatter.Alignment = StringAlignment.Far;
            for (int i = 0; i < nData; i++)
            {
                RectangleF rectangle = new RectangleF(0, yStart + yInterval * i, margin + fontSize, yInterval * 2);
                g.DrawString(labelList[i], fontLabel, brush, rectangle, formatter);
            }
            // 3. 그리드 표시
            int xStart = margin + fontSize + gaps;
            int xInterval = (imgWidth - 2 * margin - fontSize - gaps) / 4;
            int yEnd = imgHeight - margin - titleSize - 2 * gaps - numberSize;
            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(penGrid, xStart + i * xInterval, yStart, xStart + i * xInterval, yEnd);
            }
            // 4. 막대 그라프 표시
            for (int i = 0; i < nData; i++)
            {
                int yCenter = yStart + yInterval * (i + 1);
                g.FillRectangle(new SolidBrush(lstColor[i]), xStart, yCenter - barSize / 2, (float)xInterval * arryTest[i], barSize);
                g.DrawString(arryTest[i].ToString(), fontNumber, brush, xStart + (float)xInterval * arryTest[i] + (float)gaps, yCenter - numberSize / 2);
            }
            // 5. 좌표측 표시
            g.DrawLine(pen, xStart, yStart, xStart, yEnd);
            g.DrawLine(pen, xStart, yEnd, imgWidth - margin, yEnd);
            // 6. 수자 표시
            for (int i = 0; i < 5; i++)
            {
                g.DrawString(i.ToString(), fontLabel, brush, xStart + i * xInterval - labelSize / 2, yEnd + gaps);
            }
            // 7. 타이틀 표시
            formatter.Alignment = StringAlignment.Center;
            RectangleF rc = new RectangleF(xStart, imgHeight - margin - titleSize - gaps, imgWidth - xStart, margin + titleSize + gaps);
            g.DrawString(strTitle, fontCaption, brush, rc, formatter);
            // 8. 자료보관 및 귀환
            img.Save(_path.temp + "tmp" + index.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
        }
        private void calcPingguResultItem(ref string strTest, ref string[] arryIndex, ref float[] arryTest, ref float total)
        {
            int sum, num;
            int lenIndexPhy = arryIndex.Length;
            sum = 0; num = 0;
            for (int i = 0; i < lenIndexPhy; i++)
            {
                string[] strTmp = arryIndex[i].Split(',');
                int len = strTmp.Length;
                int tmp = 0;
                for (int j = 0; j < len; j++)
                {
                    int mark = Int32.Parse(strTest.Substring(Int32.Parse(strTmp[j]) - 1, 1));
                    sum += mark;
                    tmp += mark;
                    num += 1;
                }
                arryTest[i] = (float)tmp / (float)len;
            }
            total = (float)sum / (float)num;
        }
    }
}
