using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace NetworkSpeedCheck
{
    public class TXTFile
    {
        public static string ReadText(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public static string[] ReadLIne(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }


        string _save_path = string.Empty;
        public string _Save_Path
        {
            get { return _save_path; }
        }

        public TXTFile(string save_path)
        {
            _save_path = save_path;

            using (StreamWriter file = new StreamWriter(_Save_Path, false, Encoding.UTF8))
            {
                file.WriteLine("Check DateTime,IP, Return Time");
            }
        }

        public void Write(string text)
        {
            using (StreamWriter file = new StreamWriter(_Save_Path, true, Encoding.UTF8))
            {
                file.Write(text);
            }
        }
    }
}
