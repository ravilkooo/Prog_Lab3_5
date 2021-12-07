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
        public Match Match { get; set; }

        internal void ReadFromFile(string fileName)
        {
            if (fileName != "")
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    FileName = fileName;
                    Text = sr.ReadToEnd().Replace("\r", "");  //стандартный символ конца строки	
                }
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

        public void GetOfOrStatistics(out int ofc, out int orc)
        {
            ofc = 0; orc = 0;
            foreach (Match m in Regex.Matches(this.Text, @"\b(of|or)\b"))
            {
                if (m.Value == "of") ofc++; else orc++;
            }
        }
        public ISet<string> FindSentencesFirstWords()
        {
            ISet<string> words = new HashSet<string>();
            foreach (Match m in Regex.Matches(this.Text, @"[?.!]\s*([A-Z][a-z]*)[^.!]*\?"))
            {
                words.Add(m.Groups[1].Value);
            }
            return words;
        }
        public IDictionary<string, int> FirstLetterCounts()
        {
            SortedDictionary<string, int> counts = new SortedDictionary<string, int>();
            Regex r = new Regex(@"\b([a-z]|[A-Z])");
            foreach (Match m in r.Matches(this.Text))
            {
                string b = m.Groups[1].Value.ToUpper();
                if (counts.ContainsKey(b))
                {
                    counts[b]++;
                }
                else
                {
                    counts[b] = 1; // при чтении было бы исключение «ключ не найден»
                }
            }
            return counts;
        }

    }
}
