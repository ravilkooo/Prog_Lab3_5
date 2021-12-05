using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prog_Lab3_5
{
    class Data
    {
        public string Text;
        public string FileName;
        public System.Text.RegularExpressions.Match Match { get; set; }

        internal void ReadFromFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                Text = sr.ReadToEnd().Replace("\r", "");  //стандартный символ конца строки	
            }

        }

        internal void Find(string re)
        {
            this.Match = Regex.Match(this.Text, re);
        }

        internal void Next()
        {
            this.Match = this.Match?.NextMatch();
        }
    }
}
