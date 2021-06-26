using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace Pather
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createFolder();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                createFolder();
            }
        }

        private void createFolder()
        {
            string filePath = Form1.pathFile;
            string fileName = textBox1.Text;
            string fullPath = filePath + @"\" + fileName;

            if (Directory.Exists(fullPath))
            {
                MessageBox.Show(
                "Такая папка уже есть!",
                "Ошибка!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
                return;
            }

            Directory.CreateDirectory(fullPath);
            this.Close();
        }
    }
}
