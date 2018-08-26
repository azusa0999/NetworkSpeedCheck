using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.Threading;

namespace NetworkSpeedCheck
{
    class Program
    {
        static TXTFile _file;
        static string[] ipList;
        static StringBuilder _text = new StringBuilder();

        static void Main(string[] args)
        {
            //ip목록이 있는 txt 파일을 처음 로드 시 초기화
            string _readTXT_Path = AppDomain.CurrentDomain.BaseDirectory + "IPList.txt";
            //반응시간을 축적할 파일 대상 생성
            string _saveCSV_path = AppDomain.CurrentDomain.BaseDirectory + "RoundTimeInfo_" + System.DateTime.Now.ToString("yyyyMMdd") + ".csv";

            //txt 파일 불러오기
            ipList = TXTFile.ReadLIne(_readTXT_Path);

            //반응시간을 작성한다.
            _file = new TXTFile(_saveCSV_path);

            #region 1. IPList.txt의 내용을 한 번 확인하고 끝맺는 방식
            //foreach (string ip in ipList)
            //{
            //    NetworkCheck chk = new NetworkCheck();
            //    file.WriteLine(ip, chk.getRoundTime(ip).ToString());
            //}
            #endregion

            #region 2. IPList.txt의 내용을 비동기로 처리
            //foreach (string ip in ipList)
            //{
            //    Thread trd = new Thread(() => Run(ip));
            //    trd.Start();
            //}
            #endregion

            #region 3. 종료될 때까지 10초마다 확인
            System.Timers.Timer t = new System.Timers.Timer();

            t.Interval = 1000 * 10;//10초
            t.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            t.Start();

            #endregion


            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        static void timer_Elapsed(object sender, EventArgs e)
        {
            foreach (string ip in ipList)
            {
                Thread trd = new Thread(() => Run(ip));
                trd.Start();
            }
        }

        static void Run(string ip)
        {
            NetworkCheck chk = new NetworkCheck();
            string sVal = chk.getRoundTime(ip).ToString();
            AppendLine(_text, ip, sVal);

            //마지막 IP 차례라면
            if (ip == ipList[ipList.Length - 1])
            {
                _file.Write(_text.ToString());
            }
        }

        static void AppendLine(StringBuilder text, string ip, string val)
        {
            string str = string.Format("{0},{1},{2}", System.DateTime.Now.ToString("yyyyMMdd hhmmss"), ip, val);
            text.AppendLine(str);
            Console.WriteLine(str);
        }

    }
}
