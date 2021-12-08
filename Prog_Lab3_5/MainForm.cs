using Prog_Lab3_5.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prog_Lab3_5
{
    public partial class MainForm : Form
    {
        Data data = new Data();
        public MainForm()
        {
            InitializeComponent();
            this.data = new Data();
            data.ReadFromFile(Settings.Default.DefaultFileName);
            richTextBox1.Text = data.Text;
            //
            listBox1.Items.Add(@"\s+\w+\s+");
            listBox1.Items.Add(@"\b(of|or)\b");
            listBox1.Items.Add(@"\b\d{4}\b");
            listBox1.Click += (s, e) =>
            {
                textBox1.Text = listBox1.Text;
                data.Find(textBox1.Text);
                ShowMatch();
            };

            
            

        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.data.ReadFromFile(dlg.FileName);
                richTextBox1.Text = data.Text;
            }
        }
        private void Find(object sender, EventArgs e)
        {
            data.Find(textBox1.Text);
            this.ShowMatch();
        }
        private void ShowMatch()
        {
            var m = data.Match;
            if (m != null && m.Success)
            {
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.SelectionStart = m.Index;

                richTextBox1.SelectionLength = m.Value.Length;
                
                richTextBox1.ScrollToCaret();
                richTextBox1.SelectionBackColor = Color.Yellow;
                richTextBox2.Text = $"Найдено[{m.Index}]: ##{m.Value}##\n";
            }
            for (int i = 0; i < m.Groups.Count; i++)
            {
                richTextBox2.Text += String.Format("Groups[{0}]={1}\n", i, m.Groups[i]);
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                data.Find(textBox1.Text);
                this.ShowMatch();
                e.SuppressKeyPress = true;
            }
        }
        private void NextMatch(object sender, EventArgs e)
        {
            data.Next();
            this.ShowMatch();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.DefaultFileName = data.FileName;
            Settings.Default.Save();
        }
        private void ofOrClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ofc, orc;
            data.GetOfOrStatistics(out ofc, out orc);
            richTextBox2.Text = $" of: {ofc}, or: {orc}";
        }
        private void firstQstWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISet<String> words = data.FindSentencesFirstWords();
            richTextBox2.Text = String.Join(", ", words);
        }
        private void statFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new StatisticsForm(data.FirstLetterCounts()).ShowDialog();
        }

        private void task13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            var sentences = data.FindFiveSentencesBiggestPunct();
            foreach (var p in sentences)
            {
                richTextBox2.Text += $"{p.Item1} : {p.Item2}\n";
            }
        }
    }
}