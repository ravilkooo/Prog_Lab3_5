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
        Data data;
        public MainForm()
        {
            InitializeComponent();
            this.data = new Data();
            richTextBox1.Text = data.Text;
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; // расширения
            dlg.FilterIndex = 1;
            DialogResult res = dlg.ShowDialog(); // показываем диалог и ждём ok или отмены
            if (res == DialogResult.OK) // если не нажали отмену
            {
                this.data.ReadFromFile(dlg.FileName);
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
                richTextBox1.SelectionBackColor = Color.White; // сброс подсветки
                richTextBox1.SelectionStart = m.Index;
                // начало - место, на котором
                // в строке найдено регулярное выражение
                richTextBox1.SelectionLength = m.Value.Length;
                // длина найденного фрагмента
                richTextBox1.ScrollToCaret(); // прокрутка на выделенное место
                richTextBox1.SelectionBackColor = Color.Yellow; // подсветка

                richTextBox2.Text = $"Найдено[{m.Index}]: ##{m.Value}##\n";
            }
        }
    }
}
